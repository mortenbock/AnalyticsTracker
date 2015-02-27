using System.Collections.Generic;
using System.Web;

namespace Vertica.AnalyticsTracker
{
	public class AnalyticsTracker
	{
		public static IHtmlString Render(string account = "xxxxx", bool trackDefaultPageview = true, bool displayFeatures = false, Dictionary<string, object> trackerConfiguration = null)
		{
			var current = Current;
			current.SetAccount(account);
			current.TrackDefaultPageview = trackDefaultPageview;
			if (displayFeatures) current.Require("displayfeatures");
			current.SetTrackerConfiguration(trackerConfiguration);

			return new HtmlString(current.Render());
		}

		public static CommandTracker Current
		{
			get
			{
				var httpContext = HttpContext.Current;
				if (httpContext == null) return new CommandTracker();

				var tracker = httpContext.Items["AnalyticsTracker"] as CommandTracker;
				if (tracker != null) return tracker;

				tracker = new CommandTracker();
				httpContext.Items["AnalyticsTracker"] = tracker;
				return tracker;
			}
		}

		public static IHtmlString ClientCommand(CommandBase command)
		{
			Current.Require(command.RequiredPlugins);
			return new HtmlString(command.RenderCommand());
		}
	}
}
