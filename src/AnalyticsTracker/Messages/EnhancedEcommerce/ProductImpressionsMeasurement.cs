using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class ProductImpressionsMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly string _currencyCode;
        private readonly ImpressionFieldObject[] _impressionFieldObjects;

        public ProductImpressionsMeasurement(string currencyCode, ImpressionFieldObject[] productImpressions) : base("productImpressions")
        {
            _impressionFieldObjects = productImpressions;
            _currencyCode = currencyCode;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            return new Dictionary<string, object>
            {
                {"currencyCode",  _currencyCode},
                {"impressions", _impressionFieldObjects.Select(i=>i.Info).ToArray() }
            };
        }
    }
}