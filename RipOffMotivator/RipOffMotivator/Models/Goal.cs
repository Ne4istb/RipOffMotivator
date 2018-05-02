using System;
using System.Linq;

namespace RipOffMotivator.Models
{
	public class Goal
	{
		public Guid TagId;

		public string Title;
		public DateTime Time;
		public decimal Amount;
	}
}