using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;

namespace PubSub.Publisher2
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<Worker>();
            services.AddSingleton(services);
            services.AddRebus();
        }
        
        private static void AddRebus(this IServiceCollection services)
        {
            services.AddRebus(configure => configure
                .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                .Transport(t => t.UseRabbitMq("amqp://localhost", "PubSub.Publisher2"))
                .Routing(r => r.TypeBased().Map<string>("PubSub.Publisher2"))
            );
        }
    }
}