using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Config;
using Rebus.ServiceProvider;
using LogLevel = Rebus.Logging.LogLevel;

namespace PubSub.Subscriber1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(
            HostBuilderContext context,
            IServiceCollection services)
        {
            services.ConfigureRebus()
                .AddHostedService<Worker>()
                .AddSingleton(provider => provider);
        }

        private static IServiceCollection ConfigureRebus(this IServiceCollection services)
        {
            services.AutoRegisterHandlersFromAssemblyOf<Handler>();

            return services.AddRebus(configure => configure
                .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                .Transport(t => t.UseRabbitMq("amqp://localhost", "PubSub.Subscriber1"))
            );
        }
    }
}