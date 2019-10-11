using BlazorAuthorization.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlazorAuthorization.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public AuthService(AuthenticationStateProvider authenticationStateProvider,
                           HttpClient httpClient,
                           ILocalStorageService localStorage,
                           NavigationManager navigationManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;

            _httpClient.BaseAddress = new System.Uri(_navigationManager.BaseUri);
        }

        /// <summary>
        /// Makes an HTTP POST request to the given controller with the given object as the body.
        /// Returns the deserialized response content.
        /// </summary>
        public async Task<TResult> PostAsync<TRequest, TResult>(string controller, TRequest body)
        {
            try
            {
                string stringData = Newtonsoft.Json.JsonConvert.SerializeObject(body);

                var contentData = new StringContent(
                    stringData,
                    System.Text.Encoding.UTF8,
                    "application/json");
                var response = await _httpClient.PostAsync(controller, contentData);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            return await PostAsync<RegisterModel, RegisterResult>("api/accounts", registerModel);
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var result = await PostAsync<LoginModel, LoginResult>("api/Login", loginModel);

            if (result.Successful)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return result;
            }

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
