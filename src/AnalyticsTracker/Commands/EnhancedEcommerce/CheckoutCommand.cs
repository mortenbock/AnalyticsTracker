using System.Collections.Generic;
using System.Text;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public class CheckoutCommand : EnhancedEcommerceCommandBase
	{
		private readonly CheckoutActionFieldObject _checkoutAction;
		private readonly IEnumerable<ProductFieldObject> _lineItems;

		public CheckoutCommand(CheckoutActionFieldObject checkoutAction, IEnumerable<ProductFieldObject> lineItems)
		{
			_checkoutAction = checkoutAction;
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

			var ccfg = new ConfigurationObject(_checkoutAction.Info);
			sb.AppendFormat("ga('ec:setAction', 'checkout', {0});", ccfg.Render());
			sb.AppendLine();
			return sb.ToString();
		}
	}
}