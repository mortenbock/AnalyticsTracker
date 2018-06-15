using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class PromoViewMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly PromoFieldObject[] _promoFieldObjects;

        public PromoViewMeasurement(PromoFieldObject[] promoFieldObjects) : base("promoView")
        {
            _promoFieldObjects = promoFieldObjects;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var promotions =
                new Dictionary<string, object> { { "promotions", _promoFieldObjects.Select(i => i.Info).ToArray() } };

            return new Dictionary<string, object>
            {
                { "promoView", promotions }
            };
        }
    }
}
