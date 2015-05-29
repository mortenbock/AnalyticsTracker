using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands
{
	public class PageViewCommand : CommandBase
	{
		private readonly string _page;
		private readonly string _title;

		public PageViewCommand(string page = null, string title = null)
		{
			_page = page;
			_title = title;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.AfterPageView; }
		}

		public override string RenderCommand()
		{
			var pageInfo = new Dictionary<string, object>
			{
				{"hitType", "pageview"},
				{"page", _page},
				{"title", _title}
			};
			var config = new ConfigurationObject(pageInfo);
			return string.Format("ga('send', {0});", config.Render());
		}
	}
}