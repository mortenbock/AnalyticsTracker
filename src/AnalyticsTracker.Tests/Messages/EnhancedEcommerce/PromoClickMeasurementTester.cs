using NUnit.Framework;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;
using Vertica.AnalyticsTracker.Messages.EnhancedEcommerce;

namespace AnalyticsTracker.Tests.Messages.EnhancedEcommerce
{
    [TestFixture]
    public class PromoClickMeasurementTester
    {
        [Test]
        public void RenderMessage_AddsPromoView()
        {
            var subj = new PromoClickMeasurement(new[] { new PromoFieldObject("JUNE_PROMO13", "June Sale") });
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("{'event': 'promotionClick','ecommerce': {'promoClick': {'promotions': [{'id': 'JUNE_PROMO13','name': 'June Sale'}]}}}"));
        }

        [Test]
        public void RenderMessage_AddsPromoViewWithOptional()
        {
            var subj = new PromoClickMeasurement(new[] { new PromoFieldObject("JUNE_PROMO13", "June Sale", creative: "banner1", position: "slot1") });
            var renderedMessage = subj.RenderMessage();
            Assert.That(renderedMessage, Is.StringContaining("{'event': 'promotionClick','ecommerce': {'promoClick': {'promotions': [{'id': 'JUNE_PROMO13','name': 'June Sale','creative': 'banner1','position': 'slot1'}]}}}"));
        }
    }
}
