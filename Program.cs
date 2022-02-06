// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.AppService.Fluent;

using IHost host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostingContext, configuration) =>
{
    configuration.Sources.Clear();

    IHostEnvironment env = hostingContext.HostingEnvironment;

    configuration
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

    IConfigurationRoot configurationRoot = configuration.Build();

    TransientFaultHandlingOptions options = new();
    configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
                     .Bind(options);

    Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
    Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");
})
.Build();

Console.WriteLine("Hello, World!");

// IWebApp app1 =

await host.RunAsync();

public class TransientFaultHandlingOptions
{
    public bool Enabled { get; set; }
    public TimeSpan AutoRetryDelay { get; set; }
}
