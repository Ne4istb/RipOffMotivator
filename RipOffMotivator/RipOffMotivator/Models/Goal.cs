using System;
using System.Linq;

namespace RipOffMotivator.Models
{
	public class Goal
	{
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public long Amount { get; set; }

		public Guid TagId { get; set; }

		public string RejectTrigger { get; set; }

		internal bool IsExpired(DateTime? now = default(DateTime?))
		{
			return Date < (now?? DateTime.Now);
		}
	}
}