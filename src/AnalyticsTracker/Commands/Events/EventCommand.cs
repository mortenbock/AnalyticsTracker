using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.Events
{
	public class EventCommand : CommandBase
	{
		private readonly string _category;
		private readonly string _action;
		private readonly string _label;
		private readonly uint? _value;
		private readonly bool _nonInteraction;
		private readonly bool _useBeacon;

		public EventCommand(string category, string action, string label = "", uint? value = null, bool nonInteraction = false, bool useBeacon = false)
		{
			_category = category;
			_action = action;
			_label = label;
			_value = value;
			_nonInteraction = nonInteraction;
			_useBeacon = useBeacon;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.AfterPageView; }
		}

		public override string RenderCommand()
		{
			var eventInfo = new Dictionary<string, object>
			{
				{"hitType", "event"},
				{"eventCategory", _category},
				{"eventAction", _action},
				{"eventLabel", _label}
			};

			if(_value.HasValue)
				eventInfo.Add("eventValue", _value.Value);

			if (_nonInteraction)
				eventInfo.Add("nonInteraction", true);

			if(_useBeacon)
				eventInfo.Add("useBeacon", true);

			var config = new ConfigurationObject(eventInfo);
			return string.Format("ga('send', {0});", config.Render());
		}
	}
}
