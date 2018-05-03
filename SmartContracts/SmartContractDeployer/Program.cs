using System;
using Nethereum.Web3;
using Nethereum.RPC.Eth;
using System.Threading.Tasks;
using System.IO;
using Nethereum.Hex.HexTypes;

namespace SmartContractDeployer
{
    class Program
    {
        const string DeployAccountPass = "";

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        public static async Task MainAsync(string[] args)
        {
            //var server = args[0];
            //var binFileName = args[1];
            var server = "http://192.168.43.66:8000/";
            var binFileName = "/Users/Ne4istb/Sources/Xamarin/RipOffMotivator/SmartContracts/Contracts/Motivator_sol_Motivator.bin";

            try
            {
                var address = await DeployAsync(server, binFileName);
                Console.Write(address);
            }
            catch (Exception ex){
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        async static Task<string> DeployAsync(string server, string binFileName)
        {
            var web3 = new Web3(server);

            var coinbase = new EthCoinBase(web3.Client);
            await web3.Personal.UnlockAccount.SendRequestAsync("0xf5e9f9bf71945930ed6e5304ed8396602041a965", "", (int?)null);

            var bin = ReadFileContent(binFileName);

            var fromAddress = await web3.Eth.CoinBase.SendRequestAsync();
            var gas = new HexBigInteger(1000000);

            var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(bin, fromAddress, gas);

            return receipt.ContractAddress;
        }

        static string ReadFileContent(string fileName){
            try
            {   
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Cannot read {fileName}", e);
            }
        }
    }
}
