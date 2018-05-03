using System;
using System.Collections.Generic;
using System.Text;

namespace RipOffMotivator.Models
{
    public class CreateGoalBC
    {
		public IList<Tag> Tags { get; set; }
		public Tag SelectedTag { get; set; }
	}
}
