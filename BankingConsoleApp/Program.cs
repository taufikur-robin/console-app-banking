using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BankingConsoleApp;
public class Program
{
    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection, configuration);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var app = serviceProvider.GetService<App>();
        app?.Run();
    }
    
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("BankingApi", client =>
        {
            client.BaseAddress = new Uri(configuration["BankingApiBaseUrl"]);
        });
        
        services.AddTransient<App>();
    }
}