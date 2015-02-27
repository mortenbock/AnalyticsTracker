using System.Text;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce
{
	public class ProductDetailCommand : EnhancedEcommerceCommandBase
	{
		private readonly ProductFieldObject _product;

		public ProductDetailCommand(ProductFieldObject product)
		{
			_product = product;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.BeforePageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();
			var pcfg = new ConfigurationObject(_product.Info);
			sb.AppendFormat("ga('ec:addProduct', {0});", pcfg.Render());
			sb.AppendLine();
			sb.AppendLine("ga('ec:setAction', 'detail');");
			return sb.ToString();
		}
	}
}