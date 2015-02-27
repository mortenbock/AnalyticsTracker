namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public abstract class EnhancedEcommerceCommandBase : CommandBase
	{
		public override string[] RequiredPlugins
		{
			get { return new[] { "ec" }; }
		}
	}
}