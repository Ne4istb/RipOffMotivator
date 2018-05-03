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
            var rejectionId = await sc.AddGoalAsync(1000, DateTime.Now);
            Console.WriteLine($"To reject: {rejectionId}");

            //var get = await sc.RejectAsync(Guid.NewGuid());
            //Console.WriteLine("Weeee!");

        }
    }
}
