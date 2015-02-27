namespace Vertica.AnalyticsTracker
{
	public abstract class CommandBase
	{
		public abstract CommandOrder Order { get; }
		public virtual string[] RequiredPlugins { get { return new string[0]; } }
		public abstract string RenderCommand();
	}
}