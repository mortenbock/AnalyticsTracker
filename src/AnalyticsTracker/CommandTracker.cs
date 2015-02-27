using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Vertica.AnalyticsTracker
{
	public class CommandTracker
	{
		private string _account;
		private readonly List<CommandBase> _prePageView = new List<CommandBase>();
		private readonly List<CommandBase> _postPageView = new List<CommandBase>();
		private readonly Dictionary<string, bool> _requiredFeatures = new Dictionary<string, bool>();
		private ConfigurationObject _trackerConfiguration = new ConfigurationObject(new Dictionary<string, object>());
		public bool TrackDefaultPageview { get; set; }
		private readonly Dictionary<string, object> _fields = new Dictionary<string, object>();


		public void SetAccount(string account)
		{
			_account = account;
		}

		public void SetPage(string url, string title = null)
		{
			_fields["page"] = url;
			_fields["title"] = title;
		}

		public void SetCurrency(AnalyticsCurrency currency)
		{
			_fields["&cu"] = currency;
		}

		public void Require(params string[] features)
		{
			foreach (var feature in features)
			{
				_requiredFeatures[feature] = true;
			}
		}

		public void Track(CommandBase command)
		{
			Require(command.RequiredPlugins);
			switch (command.Order)
			{
				case CommandOrder.BeforePageView:
					_prePageView.Add(command);
					break;
				case CommandOrder.AfterPageView:
					_postPageView.Add(command);
					break;
			}
		}

		public string RenderForHeader()
		{
			var sb = new StringBuilder();

			RenderPrePageView(sb);
			RenderPostPageView(sb);

			return sb.ToString();
		}

		public string Render()
		{
			var sb = new StringBuilder();
			sb.AppendLine("<script>");
			sb.AppendLine(@"(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');");

			sb.AppendFormat("ga('create', '{0}', {1});", _account, _trackerConfiguration.Render("'auto'"));
			sb.AppendLine();

			RenderPrePageView(sb);

			if (TrackDefaultPageview)
				sb.AppendLine("ga('send', 'pageview');");

			RenderPostPageView(sb);

			sb.AppendLine("</script>");
			return sb.ToString();
		}

		private void RenderPostPageView(StringBuilder sb)
		{
			RenderCommands(sb, _postPageView);
		}

		private void RenderPrePageView(StringBuilder sb)
		{
			foreach (var requiredFeature in _requiredFeatures)
			{
				sb.AppendFormat("ga('require', '{0}');", HttpUtility.JavaScriptStringEncode(requiredFeature.Key));
				sb.AppendLine();
			}

			RenderFields(sb, _fields);

			RenderCommands(sb, _prePageView);
		}

		private void RenderFields(StringBuilder sb, Dictionary<string, object> fields)
		{
			if (!fields.Any()) return;

			var cfg = new ConfigurationObject(fields);
			sb.AppendFormat("ga('set', {0});", cfg.Render());
			sb.AppendLine();
		}

		public void SetTrackerConfiguration(Dictionary<string, object> trackerConfiguration)
		{
			_trackerConfiguration = new ConfigurationObject(trackerConfiguration);
		}

		private static void RenderCommands(StringBuilder sb, IEnumerable<CommandBase> commands)
		{
			foreach (var command in commands)
			{
				sb.AppendLine(command.RenderCommand());
			}
		}
	}
}