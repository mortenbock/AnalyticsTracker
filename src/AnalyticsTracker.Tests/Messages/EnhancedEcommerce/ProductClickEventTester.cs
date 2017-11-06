using NUnit.Framework;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Messages.EnhancedEcommerce;

namespace AnalyticsTracker.Tests.Messages.EnhancedEcommerce
{
    [TestFixture]
    public class ProductClickEventTester
    {
        [Test]
        public void RenderMessage_AddsCurrency()
        {
            var subj = new ProductClickEvent("DKK", "mylist", new ProductFieldObject("pid", "pname"));
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("'currencyCode': 'DKK'"));
        }

        [Test]
        public void RenderMessage_AddsActionField()
        {
            var subj = new ProductClickEvent("DKK", "mylist", new ProductFieldObject("pid", "pname"));
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("'actionField': {'list': 'mylist'}"));
        }

        [Test]
        public void RenderMessage_AddsProducts()
        {
            var subj = new ProductClickEvent("DKK", "mylist", new ProductFieldObject("pid", "pname"));
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("'products': [{'id': 'pid','name': 'pname'}]"));
        }
    }
}