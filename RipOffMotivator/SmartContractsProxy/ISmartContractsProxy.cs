﻿using System;

namespace SmartContractsProxy
{
    public interface ISmartContractsProxy
    {
        void AddGoal(decimal amount, DateTime executionTime, Guid rejectTriggerId);
        void RejectAsync(Guid triggerId);
    }
}
