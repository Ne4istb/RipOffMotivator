using System;
using System.IO;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace SmartContractsProxy
{
    public class SmartContractsProxy : ISmartContractsProxy
    {
        readonly Web3 web3;
        readonly Contract contract;

        public SmartContractsProxy()
        {
            web3 = new Web3(Configuration.Server);
            var abi = ReadFileContent(Configuration.AbiFileName);

            contract = web3.Eth.GetContract(abi, Configuration.ContractAdress);
        }

        public async Task<string> AddGoalAsync(long amountInMilliethers, DateTime executionTime)
        {
            var betting = contract.GetFunction("betting");

            var fromAddress = await GetFromAddress();
            var gas = new HexBigInteger(1000000);
            var weis = ConvertMillietherToWei(amountInMilliethers);
			var uuid = Guid.NewGuid().ToString();


			var rejectionId = await betting.SendTransactionAsync(fromAddress, gas, weis, null, uuid, ToUnixTime(executionTime));
            return uuid;
        }

        HexBigInteger ConvertMillietherToWei(long amount){
            return new HexBigInteger((long)(amount * Math.Pow(10, 15)));
        }

        public async Task RejectAsync(string rejectionId)
        {
            var reject = contract.GetFunction("reject");

            var fromAddress = await GetFromAddress();
            var gas = new HexBigInteger(1000000);

            await reject.SendTransactionAsync(fromAddress, gas, gas, null, rejectionId);
        }

        Task<string> GetFromAddress()
        {
            return web3.Eth.CoinBase.SendRequestAsync();
        }

        static string ReadFileContent(string fileName)
        {
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var resourceName = $"SmartContractsProxy.assets.{fileName}";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Cannot read {fileName}", e);
            }
        }

        static long ToUnixTime(DateTime date)
        {
            return (date.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
