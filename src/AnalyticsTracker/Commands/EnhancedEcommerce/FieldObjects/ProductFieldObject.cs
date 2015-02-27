using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects
{
	public class ProductFieldObject
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

		public ProductFieldObject(string id, string name, string brand = null, string category = null, string variant = null, decimal? price = null, uint? quantity = null, string coupon = null, uint? position = null)
		{
			_info["id"] = id;
			_info["name"] = name;
			_info["brand"] = brand;
			_info["category"] = category;
			_info["variant"] = variant;
			_info["price"] = price;
			_info["quantity"] = quantity;
			_info["coupon"] = coupon;
			_info["position"] = position;
		}

		public Dictionary<string, object> Info { get { return _info; } }
	}
}