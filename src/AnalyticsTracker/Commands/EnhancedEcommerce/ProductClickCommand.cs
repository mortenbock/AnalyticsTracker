using System.Text;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Commands.Events;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public class ProductClickCommand : EnhancedEcommerceCommandBase
	{
		private readonly ProductClickActionFieldObject _clickAction;
		private readonly ProductFieldObject _product;
		private readonly EventCommand _trackingEvent;

		public ProductClickCommand(ProductClickActionFieldObject clickAction, ProductFieldObject product, EventCommand trackingEvent)
		{
			_clickAction = clickAction;
			_product = product;
			_trackingEvent = trackingEvent;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.AfterPageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();
			var pcfg = new ConfigurationObject(_product.Info);
			sb.AppendFormat("ga('ec:addProduct', {0});", pcfg.Render());
			sb.AppendLine();

			var ccfg = new ConfigurationObject(_clickAction.Info);

			sb.AppendFormat("ga('ec:setAction', 'click', {0});", ccfg.Render());
			sb.AppendLine();

			if (_trackingEvent != null)
				sb.Append(_trackingEvent.RenderCommand());

			return sb.ToString();
		}
	}
}