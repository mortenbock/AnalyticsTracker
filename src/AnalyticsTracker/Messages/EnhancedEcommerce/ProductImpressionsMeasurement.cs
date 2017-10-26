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
            var objects = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                objects.Add("currencyCode", _currencyCode);
            }
            objects.Add("impressions", _impressionFieldObjects.Select(i=>i.Info).ToArray());
            return objects;
        }
    }
}