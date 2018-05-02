using System;
using Nethereum.Web3;
using Nethereum.RPC.Eth;
using System.Threading.Tasks;
using System.IO;

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
            var server = args[0];
            var binFileName = args[1];

            var addres = await DeployAsync(server, binFileName);

            Console.Write(addres);
        }

        async static Task<string> DeployAsync(string server, string binFileName)
        {
            var web3 = new Web3(server);

            //var createAccount = web3.Personal.NewAccount;
            //var newAccount = await createAccount.SendRequestAsync(DeployAccountPass);

            var coinbase = new EthCoinBase(web3.Client);
            await web3.Personal.UnlockAccount.SendRequestAsync(coinbase, DeployAccountPass);

            var bin = ReadFileContent(binFileName);

            var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(bin, coinbase.ToString());

            return receipt.ToString();
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
