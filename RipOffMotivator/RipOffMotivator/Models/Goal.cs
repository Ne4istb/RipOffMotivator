using System;
using System.Linq;

namespace RipOffMotivator.Models
{
	public class Goal
	{
		public Guid TagId { get; set; }

		public string Title { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }

		internal bool IsExpired(DateTime? now = default(DateTime?))
		{
			return Date < (now?? DateTime.Now);
		}
	}
}