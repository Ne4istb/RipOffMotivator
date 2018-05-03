using System;
using System.Threading.Tasks;

namespace SmartContractsProxy
{
    public interface ISmartContractsProxy
    {
        Task<string> AddGoalAsync(long amountInMilliethers, DateTime executionTime);
        Task RejectAsync(string rejectionId);
    }
}
