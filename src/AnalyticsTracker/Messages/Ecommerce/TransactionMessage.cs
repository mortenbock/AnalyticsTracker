using System.Collections.Generic;
using System.Linq;

namespace Vertica.AnalyticsTracker.Messages.Ecommerce
{
	public class TransactionMessage : MessageBase
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

		public TransactionMessage(string transactionId, string affiliation, decimal total, decimal tax, decimal shipping, IEnumerable<TransactionItemInfo> items)
		{
			_info["transactionId"] = transactionId;
			_info["transactionAffiliation"] = affiliation;
			_info["transactionTotal"] = total;
			_info["transactionTax"] = tax;
			_info["transactionShipping"] = shipping;
			_info["transactionProducts"] = items.Select(i => i.Info).ToArray();
		}

		public override string RenderMessage(string dataLayerName)
		{
			return Push(dataLayerName, new ConfigurationObject(_info));
		}
	}
}