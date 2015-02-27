using NUnit.Framework;
using Vertica.AnalyticsTracker;
using Vertica.AnalyticsTracker.Commands.Ecommerce;

namespace AnalyticsTracker.Tests.Commands.Ecommerce
{
	[TestFixture]
	public class TransactionCommandTester
	{
		[Test]
		public void Render_FullTransacion_AllFieldsTracked()
		{
			var info = new TransactionInfo("ord123", "Achme store", 250.45M, 10.15M, 15M, AnalyticsCurrency.DKK);
			info.AddItem("Black shirt", "S1002", "Shirts", 100M, 2);
			info.AddItem("White pants", "S1004", "Pants", 50.45M, 1);

			var subject = new TransactionCommand(info);
			string rendered = subject.RenderCommand();
			Assert.That(rendered, Is.StringContaining("ga('ecommerce:addTransaction', {'id': 'ord123','affiliation': 'Achme store','revenue': 250.45,'shipping': 10.15,'tax': 15,'currency': 'DKK'});"));
			Assert.That(rendered, Is.StringContaining("ga('ecommerce:addItem', {'id': 'ord123','name': 'Black shirt','sku': 'S1002','category': 'Shirts','price': 100,'quantity': 2,'currency': 'DKK'});"));
			Assert.That(rendered, Is.StringContaining("ga('ecommerce:addItem', {'id': 'ord123','name': 'White pants','sku': 'S1004','category': 'Pants','price': 50.45,'quantity': 1,'currency': 'DKK'});"));
			Assert.That(rendered, Is.StringContaining("ga('ecommerce:send');"));
		}
	}
}