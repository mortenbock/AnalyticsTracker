using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Messages
{
	public class Event : MessageBase
	{
		private readonly string _eventName;

		public Event(string eventName)
		{
			_eventName = eventName;
		}

		public override string RenderMessage(string dataLayerName)
		{
			var vals = new Dictionary<string, object> {{"event", _eventName}};
			var obj = new ConfigurationObject(vals);
			return Push(dataLayerName, obj);
		}
	}
}