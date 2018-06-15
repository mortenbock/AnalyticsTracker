using NUnit.Framework;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Messages.EnhancedEcommerce;

namespace AnalyticsTracker.Tests.Messages.EnhancedEcommerce
{
    [TestFixture]
    public class PromoViewMeasurementTester
    {
        [Test]
        public void RenderMessage_AddsPromoView()
        {
            var subj = new PromoViewMeasurement(new[] { new PromoFieldObject("JUNE_PROMO13", "June Sale") });
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("{'event': 'promoView','ecommerce': {'promoView': {'promotions': [{'id': 'JUNE_PROMO13','name': 'June Sale'}]}}}"));
        }

        [Test]
        public void RenderMessage_AddsPromoViewWithOptional()
        {
            var subj = new PromoViewMeasurement(new[] { new PromoFieldObject("JUNE_PROMO13", "June Sale", creative: "banner1", position: "slot1") });
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("{'event': 'promoView','ecommerce': {'promoView': {'promotions': [{'id': 'JUNE_PROMO13','name': 'June Sale','creative': 'banner1','position': 'slot1'}]}}}"));
        }
    }
}
