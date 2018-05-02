using System;
using System.Linq;

namespace RipOffMotivator
{
    public interface IContractService
    {
        bool AddGoal(DateTime time, decimal amount, Guid rejectTrigger);
    }
}