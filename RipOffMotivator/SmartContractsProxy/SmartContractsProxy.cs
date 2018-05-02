using System;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace SmartContractsProxy
{
    public class SmartContractsProxy : ISmartContractsProxy
    {
        const string Abi = "";
        const string Adress = "";
        const string senderAddress = "";
        readonly Web3 web3;
        readonly Contract contract;

        public SmartContractsProxy(){
            web3 = new Web3();
            contract = web3.Eth.GetContract(Abi, Adress);
        }

        public void Test (){
                
        }

        public void AddGoal(decimal amount, DateTime executionTime, Guid rejectTriggerId)
        {
            var addGoal = contract.GetFunction("addGoal");
        }

        public async void RejectAsync(Guid triggerId)
        {
            var rejectFunc = contract.GetFunction("reject");

            var receipt = await rejectFunc
                .SendTransactionAndWaitForReceiptAsync(senderAddress, null, triggerId.ToString());
        }
    }
}
