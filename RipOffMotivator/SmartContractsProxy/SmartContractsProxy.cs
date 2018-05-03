using System;
using System.IO;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace SmartContractsProxy
{
    public class SmartContractsProxy : ISmartContractsProxy
    {
        // TODO: move to config
        const string AbiFileName = "/Users/Ne4istb/Sources/Xamarin/RipOffMotivator/SmartContracts/storage_sol_Storage.abi";
        const string ContractAdress = "0x8ff4f1044b46e5b2c4e91a89cdff272a59573caa";
        const string SenderAddress = "";

        readonly Web3 web3;
        readonly Contract contract;

        public SmartContractsProxy(){
            web3 = new Web3();
            var abi = ReadFileContent(AbiFileName);

            contract = web3.Eth.GetContract(abi, ContractAdress);
        }

        public async Task<string> AddGoalAsync(decimal amount, DateTime executionTime, Guid rejectTriggerId)
        {
            var set = contract.GetFunction("set");

            var fromAddress = await GetFromAddress();
            var gas = new HexBigInteger(1000000);

            var receipt = await set.SendTransactionAsync(fromAddress, gas, gas, null, 42);
            return receipt;

            //var addGoal = contract.GetFunction("addGoal");
        }

        public async Task<string> RejectAsync(Guid triggerId)
        {
            var get = contract.GetFunction("get");

            var fromAddress = await GetFromAddress();
            var gas = new HexBigInteger(1000000);

            var input = new TransactionInput(null, gas, fromAddress);
            var output = await get.CallAsync<int>();

            return output.ToString();

            //var rejectFunc = contract.GetFunction("reject");

            //var receipt = await rejectFunc
                //.SendTransactionAndWaitForReceiptAsync(SenderAddress, null, triggerId.ToString());
        }

        Task<string> GetFromAddress(){
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
    }
}
