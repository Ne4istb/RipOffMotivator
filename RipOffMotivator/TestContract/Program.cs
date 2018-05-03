using System;
using System.Threading.Tasks;

namespace TestContract
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var sc = new SmartContractsProxy.SmartContractsProxy();
            var set = await sc.AddGoalAsync(100.0m, DateTime.Now, Guid.NewGuid());
            Console.WriteLine("Hello World!");

            var get = await sc.RejectAsync(Guid.NewGuid());
            Console.WriteLine("Weeee!");

        }
    }
}
