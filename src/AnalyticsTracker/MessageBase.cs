namespace Vertica.AnalyticsTracker
{
	public abstract class MessageBase
	{
		public abstract string RenderMessage(string dataLayerName);

		protected string Push(string dataLayerName, ConfigurationObject obj)
		{
			return string.Format("{0}.push({1});",dataLayerName, obj.Render());
		}
	}
}