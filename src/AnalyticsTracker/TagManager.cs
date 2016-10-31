using System.Web;

namespace Vertica.AnalyticsTracker
{
	public class TagManager
	{

		public static IHtmlString Render(string account = "GTM-XXXX", string dataLayerName = "dataLayer", string environmentAuth = null, string environmentPreview = null)
		{
			var current = GetConfiguredCurrent(account, dataLayerName, environmentAuth, environmentPreview);
			return new HtmlString(current.Render());
		}

		public static IHtmlString RenderDataLayer(string dataLayerName = "dataLayer")
		{
			var current = GetConfiguredCurrent(dataLayerName: dataLayerName);
			return new HtmlString(current.RenderDataLayer());
		}

		public static IHtmlString RenderScript(string account = "GTM-XXXX", string dataLayerName = "dataLayer", string environmentAuth = null, string environmentPreview = null)
		{
			var current = GetConfiguredCurrent(account, dataLayerName, environmentAuth, environmentPreview);
			return new HtmlString(current.RenderScript());
		}

		public static IHtmlString RenderNoScript(string account = "GTM-XXXX", string environmentAuth = null, string environmentPreview = null)
		{
			var current = GetConfiguredCurrent(account, environmentAuth: environmentAuth, environmentPreview: environmentPreview);
			return new HtmlString(current.RenderNoScript());
		}

		private static TagTracker GetConfiguredCurrent(string account = "GTM-XXXX", string dataLayerName = "dataLayer", string environmentAuth = null, string environmentPreview = null)
		{
			var current = Current;
			current.SetAccount(account);
			current.SetDataLayerName(dataLayerName);
			current.SetEnvironmentAuth(environmentAuth);
			current.SetEnvironmentPreview(environmentPreview);
			return current;
		}

		public static TagTracker Current
		{
			get
			{
				var httpContext = HttpContext.Current;
				if (httpContext == null) return new TagTracker();

				var tracker = httpContext.Items["TagTracker"] as TagTracker;
				if (tracker != null) return tracker;

				tracker = new TagTracker();
				httpContext.Items["TagTracker"] = tracker;
				return tracker;
			}
		}
	}
}