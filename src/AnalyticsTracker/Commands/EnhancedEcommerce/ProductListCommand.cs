using System.Collections.Generic;
using System.Text;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Commands.Events;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public class ProductListCommand : EnhancedEcommerceCommandBase
	{
		private readonly IEnumerable<ImpressionFieldObject> _impressions;
		private readonly EventCommand _trackingEvent;

		public ProductListCommand(IEnumerable<ImpressionFieldObject> impressions) : this(impressions, null) { }

		public ProductListCommand(IEnumerable<ImpressionFieldObject> impressions, EventCommand trackingEvent)
		{
			_impressions = impressions;
			_trackingEvent = trackingEvent;
		}

		public override CommandOrder Order
		{
			get { return _trackingEvent != null ? CommandOrder.AfterPageView : CommandOrder.BeforePageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();
			foreach (var impression in _impressions)
			{
				var icfg = new ConfigurationObject(impression.Info);
				sb.AppendFormat("ga('ec:addImpression', {0});", icfg.Render());
				sb.AppendLine();
			}

			if (_trackingEvent != null)
				sb.Append(_trackingEvent.RenderCommand());

			return sb.ToString();
		}
	}
}