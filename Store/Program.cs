using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Store.Extensions;
using System.Threading.Tasks;

namespace Store
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                .CreateDefault(args)
                .AddRootComponents()
                .AddClientServices();

            await builder.Build().RunAsync();
        }
    }
}

