using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyBlogWebAssembly.Client.Authentication;

namespace MyBlogWebAssembly.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //<Identity>
            builder.Services.AddHttpClient("Authenticated", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("Public", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<RoleAccountClaimsPrincipalFactory>();
            //</Identity>

            await builder.Build().RunAsync();
        }
    }
}
