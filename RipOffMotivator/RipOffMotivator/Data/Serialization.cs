using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace RipOffMotivator.Data
{
	public static class Serialization
	{
		public static string SerializeToJson(object obj)
		{
			var json = JsonConvert.SerializeObject(obj);
			return json;
		}

		public static T DeserializeFromJson<T>(string jsonObj)
		{
			var result = JsonConvert.DeserializeObject<T>(jsonObj);
			return result;
		}
	}
}
