using NUnit.Framework;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Messages.EnhancedEcommerce;

namespace AnalyticsTracker.Tests.Messages.EnhancedEcommerce
{
    [TestFixture]
    public class AddToCartEventTester
    {
        [Test]
        public void RenderMessage_AddsCurrency()
        {
            var subj = new AddToCartEvent("DKK", new[] { new ProductFieldObject("pid", "pname", quantity: 2) }, "mylist");
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("'currencyCode': 'DKK'"));
        }

        [Test]
        public void RenderMessage_AddsAction()
        {
            var subj = new AddToCartEvent("DKK", new[] { new ProductFieldObject("pid", "pname", quantity: 2) }, "mylist");
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("'actionField': {'list': 'mylist'}"));
        }

        [Test]
        public void RenderMessage_AddsProducts()
        {
            var subj = new AddToCartEvent("DKK", new[] { new ProductFieldObject("pid", "pname", quantity: 2) }, "mylist");
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("'products': [{'id': 'pid','name': 'pname','quantity': 2}]"));
        }
    }
}