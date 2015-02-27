using System.Collections.Generic;
using System.Text;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public class PurchaseCommand : EnhancedEcommerceCommandBase
	{
		private readonly PurchaseActionFieldObject _purchaseAction;
		private readonly IEnumerable<ProductFieldObject> _lineItems;

		public PurchaseCommand(PurchaseActionFieldObject purchaseAction, IEnumerable<ProductFieldObject> lineItems)
		{
			_purchaseAction = purchaseAction;
			_lineItems = lineItems;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.BeforePageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();

			foreach (var lineItem in _lineItems)
			{
				var lcfg = new ConfigurationObject(lineItem.Info);
				sb.AppendFormat("ga('ec:addProduct', {0});", lcfg.Render());
				sb.AppendLine();
			}

			var pcfg = new ConfigurationObject(_purchaseAction.Info);
			sb.AppendFormat("ga('ec:setAction', 'purchase', {0});", pcfg.Render());
			sb.AppendLine();
			return sb.ToString();
		}
	}
}