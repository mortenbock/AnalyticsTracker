using System.Web;

namespace Vertica.AnalyticsTracker.Extensions
{
	public static class ExtensionsToHttpRequest
	{
		public static bool IsAnalyticsTrackingEnabled(this HttpRequest request)
		{
			var s = request.Headers["AnalyticsTracker-Enabled"];
			if (string.IsNullOrWhiteSpace(s))
				return false;

			bool result;
			bool.TryParse(s, out result);

			return result;
		}
	}
}