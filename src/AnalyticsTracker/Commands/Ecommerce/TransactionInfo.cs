using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.Ecommerce
{
	public class TransactionInfo
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();
		private readonly List<TransactionItemInfo> _items = new List<TransactionItemInfo>();

		public TransactionInfo(string id, string affiliation, decimal revenue, decimal shipping, decimal tax, AnalyticsCurrency currency)
		{
			_info["id"] = id;
			_info["affiliation"] = affiliation;
			_info["revenue"] = revenue;
			_info["shipping"] = shipping;
			_info["tax"] = tax;
			_info["currency"] = currency;
		}

		public void AddItem(string name, string sku, string category, decimal price, uint quantity)
		{
			_items.Add(new TransactionItemInfo(_info["id"] as string, name, sku, category, price, quantity, (AnalyticsCurrency)_info["currency"]));
		}

		public IEnumerable<TransactionItemInfo> Items { get { return _items; } }
		public Dictionary<string, object> Info { get { return _info; } }
	}
}