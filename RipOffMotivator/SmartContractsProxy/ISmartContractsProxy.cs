using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;

namespace SmartContractsProxy
{
    public interface ISmartContractsProxy
    {
        Task<string> AddGoalAsync(decimal amount, DateTime executionTime, Guid rejectTriggerId);
        Task<string> RejectAsync(Guid triggerId);
    }
}
