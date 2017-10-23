using System;
using System.Text;
using System.Web;
using Vertica.AnalyticsTracker.Extensions;

namespace Vertica.AnalyticsTracker.Modules
{
	public class AnalyticsHttpModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.EndRequest += ContextOnEndRequest;
		}

		private void ContextOnEndRequest(object sender, EventArgs eventArgs)
		{
			if (HttpContext.Current.Request.IsAnalyticsTrackingEnabled())
			{
				var renderBody = AnalyticsTracker.Current.RenderForHeader() + TagManager.Current.RenderHeader();

				if(string.IsNullOrWhiteSpace(renderBody))
					return;
				
				var encodedString = Convert.ToBase64String(Encoding.Default.GetBytes(renderBody), Base64FormattingOptions.None);
				
				if(encodedString.Length > 50000)
					throw new NotSupportedException("Implementation for headers over 50000 needs implementation");

				var response = HttpContext.Current.Response;
				
				int i = 0;
				const int bytePerHeader = 5000;
				while (encodedString.Length > i * bytePerHeader)
				{
					response.AddHeader($"AnalyticsTracker-{i}", encodedString.SafeSubstring(bytePerHeader * i, bytePerHeader));
					i++;
				}
			}
		}

		public void Dispose()
		{
			
		}
	}
}