using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BlazorServer.Areas.Identity.IdentityHostingStartup))]
namespace BlazorServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}