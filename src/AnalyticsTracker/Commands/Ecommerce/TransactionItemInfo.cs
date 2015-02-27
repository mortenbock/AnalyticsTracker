using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.Ecommerce
{
	public class TransactionItemInfo
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();
		public TransactionItemInfo(string id, string name, string sku, string category, decimal price, uint quantity, AnalyticsCurrency currency)
		{
			_info["id"] = id;
			_info["name"] = name;
			_info["sku"] = sku;
			_info["category"] = category;
			_info["price"] = price;
			_info["quantity"] = quantity;
			_info["currency"] = currency;
		}

		public Dictionary<string, object> Info { get { return _info; } }
	}
}