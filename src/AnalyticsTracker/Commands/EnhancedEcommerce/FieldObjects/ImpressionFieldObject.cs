using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects
{
	public class ImpressionFieldObject
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

		public ImpressionFieldObject(string id, string name, string list, string brand, string category, string variant, uint position, decimal price)
		{
			_info["id"] = id;
			_info["name"] = name;
			_info["list"] = list;
			_info["brand"] = brand;
			_info["category"] = category;
			_info["variant"] = variant;
			_info["position"] = position;
			_info["price"] = price;
		}

		public Dictionary<string, object> Info { get { return _info; } }
	}
}