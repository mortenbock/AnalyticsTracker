namespace Vertica.AnalyticsTracker
{
	public abstract class MessageBase
	{
		public abstract string RenderMessage();

		protected string Push(ConfigurationObject obj)
		{
			return $"window.tagManagerPush({obj.Render()});";
		}
	}
}