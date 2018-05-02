using System;

using RipOffMotivator.Droid;

using Xamarin.Forms;

[assembly: Dependency(typeof(ContractService))]
namespace RipOffMotivator.Droid
{
	class ContractService: IContractService
	{
		public bool AddGoal(DateTime time, decimal amount, Guid rejectTrigger)
		{
			return true;
		}
	}
}