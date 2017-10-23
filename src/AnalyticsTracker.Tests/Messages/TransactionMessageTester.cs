using NUnit.Framework;
using Vertica.AnalyticsTracker.Messages.Ecommerce;

namespace AnalyticsTracker.Tests.Messages
{
	[TestFixture]
	public class TransactionMessageTester
	{
		[Test]
		public void RenderMessage_Rendered()
		{
			var subj = new TransactionMessage("t12345", "Acme", 100.50M,10,10, new []
			{
				new TransactionItemInfo("Black shirt", "S1234","Shirts", 50,1),
				new TransactionItemInfo("White shirt", "S1235","Shirts", 50.50M,1)
			});

			var renderedMessage = subj.RenderMessage();
			Assert.That(renderedMessage, Is.StringContaining("'transactionId': 't12345'"));
			Assert.That(renderedMessage, Is.StringContaining("'transactionAffiliation': 'Acme'"));
			Assert.That(renderedMessage, Is.StringContaining("'transactionTotal': 100.5"));
			Assert.That(renderedMessage, Is.StringContaining("'transactionTax': 10"));
			Assert.That(renderedMessage, Is.StringContaining("'transactionShipping': 10"));
			Assert.That(renderedMessage, Is.StringContaining("{'name': 'Black shirt','sku': 'S1234','category': 'Shirts','price': 50,'quantity': 1}"));
			Assert.That(renderedMessage, Is.StringContaining("{'name': 'White shirt','sku': 'S1235','category': 'Shirts','price': 50.5,'quantity': 1}"));
		}
	}
}