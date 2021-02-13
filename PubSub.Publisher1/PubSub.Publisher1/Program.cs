using System;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;

namespace PubSub.Publisher1
{
    public static class Program
    {
        private static void Main()
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                    .Transport(t => t.UseRabbitMq("amqp://localhost", "PubSub.Publisher"))
                    .Start();

                var isRun = true;

                while (isRun)
                {
                    Console.WriteLine(@"a) Publish string
q) Quit");

                    var keyChar = char.ToLower(Console.ReadKey(true).KeyChar);
                    var bus = activator.Bus.Advanced.SyncBus;

                    switch (keyChar)
                    {
                        case 'a':
                            bus.Publish("Hello there, this is a string message from a publisher!");
                            break;

                        case 'q':
                            isRun = false;
                            break;

                        default:
                            Console.WriteLine($"There's no option '{keyChar}'");
                            break;
                    }
                }

                Console.WriteLine("Quitting!");
            }
        }
    }
}