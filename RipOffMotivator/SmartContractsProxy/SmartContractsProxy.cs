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
        // TODO: move to config
        const string AbiFileName = "/Users/Ne4istb/Sources/Xamarin/RipOffMotivator/SmartContracts/Contracts/Motivator_sol_Motivator.abi";
        const string ContractAdress = "0x11c497098f270cc6cfeda25bbd1c406027bc1cf8";
        const string SenderAddress = "";

        readonly Web3 web3;
        readonly Contract contract;

        public SmartContractsProxy()
        {
            web3 = new Web3();
            var abi = ReadFileContent(AbiFileName);

            contract = web3.Eth.GetContract(abi, ContractAdress);
        }

        public async Task<string> AddGoalAsync(long amountInMilliethers, DateTime executionTime)
        {
            var betting = contract.GetFunction("betting");

            var fromAddress = await GetFromAddress();
            var gas = new HexBigInteger(1000000);
            var weis = ConvertMillietherToWei(amountInMilliethers);

            var rejectionId = await betting.CallAsync<long>(fromAddress, gas, weis, ToUnixTime(executionTime));
            return rejectionId.ToString();
        }

        HexBigInteger ConvertMillietherToWei(long amount){
            return new HexBigInteger((long)(amount * Math.Pow(10, 15)));
        }

        public async Task RejectAsync(string rejectionId)
        {
            var reject = contract.GetFunction("reject");

            var fromAddress = await GetFromAddress();
            var gas = new HexBigInteger(1000000);
            var rectionTrigger = Convert.ToInt64(rejectionId);

            await reject.SendTransactionAsync(fromAddress, gas, gas, null, rectionTrigger);
        }

        Task<string> GetFromAddress()
        {
            return web3.Eth.CoinBase.SendRequestAsync();
        }

        static string ReadFileContent(string fileName)
        {
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

        static long ToUnixTime(DateTime date)
        {
            return (date.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
