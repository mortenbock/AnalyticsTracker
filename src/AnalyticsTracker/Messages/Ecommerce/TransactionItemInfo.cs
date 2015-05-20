using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Messages.Ecommerce
{
	public class TransactionItemInfo
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();
		public TransactionItemInfo(string name, string sku, string category, decimal price, uint quantity)
		{
			_info["name"] = name;
			_info["sku"] = sku;
			_info["category"] = category;
			_info["price"] = price;
			_info["quantity"] = quantity;
		}

		public Dictionary<string, object> Info { get { return _info; } }
	}
}