using System;
using System.Collections.Generic;
using System.Text;

namespace RipOffMotivator.Models
{
    public class Tag
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public bool Used { get; set; }
		public bool NotUsed => !Used;
	}
}
