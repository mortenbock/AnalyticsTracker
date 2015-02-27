using System.Text;

namespace Vertica.AnalyticsTracker.Commands.Ecommerce
{
	public class TransactionCommand : CommandBase
	{
		private readonly TransactionInfo _transactionInfo;

		public TransactionCommand(TransactionInfo transactionInfo)
		{
			_transactionInfo = transactionInfo;
		}

		public override string[] RequiredPlugins
		{
			get { return new[] { "ecommerce" }; }
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.AfterPageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();

			var transactionConfig = new ConfigurationObject(_transactionInfo.Info);
			sb.AppendFormat("ga('ecommerce:addTransaction', {0});", transactionConfig.Render());
			sb.AppendLine();
			foreach (var item in _transactionInfo.Items)
			{
				var itenConfig = new ConfigurationObject(item.Info);
				sb.AppendFormat("ga('ecommerce:addItem', {0});", itenConfig.Render());
				sb.AppendLine();
			}

			sb.AppendLine("ga('ecommerce:send');");
			return sb.ToString();
		}
	}
}