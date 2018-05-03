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
            var rejectId = await sc.AddGoalAsync(100, DateTime.Now);
            Console.WriteLine("Hello World!");

            await sc.RejectAsync(rejectId);
            Console.WriteLine("Weeee!");

        }
    }
}
