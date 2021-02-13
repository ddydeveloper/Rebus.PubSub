using System;
using System.Threading.Tasks;
using Rebus.Handlers;

namespace PubSub.Subscriber2
{
    public class Handler : IHandleMessages<string>
    {
        public async Task Handle(string message)
        {
            await Task.Delay(1);
            Console.WriteLine("Got string: {0}", message);
        }
    }
}