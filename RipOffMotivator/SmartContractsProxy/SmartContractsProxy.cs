using System;

namespace SmartContractsProxy
{
    public class SmartContractsProxy : ISmartContractsProxy
    {
        public void AddGoal(decimal amount, DateTime executionTime, Guid rejectTriggerId)
        {
            throw new NotImplementedException();
        }

        public void Reject(Guid triggerId)
        {
            throw new NotImplementedException();
        }
    }
}
