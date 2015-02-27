using System.Text;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Commands.Events;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public class RemoveFromBasketCommand : EnhancedEcommerceCommandBase
	{
		private readonly ProductFieldObject _product;
		private readonly EventCommand _trackingEvent;

		public RemoveFromBasketCommand(ProductFieldObject product, EventCommand trackingEvent)
		{
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

			var lcfg = new ConfigurationObject(_product.Info);
			sb.AppendFormat("ga('ec:addProduct', {0});", lcfg.Render());
			sb.AppendLine();
			sb.AppendLine("ga('ec:setAction', 'remove');");

			if (_trackingEvent != null)
				sb.Append(_trackingEvent.RenderCommand());

			return sb.ToString();
		}
	}
}