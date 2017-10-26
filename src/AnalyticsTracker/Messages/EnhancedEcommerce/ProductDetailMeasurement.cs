using System.Collections.Generic;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class ProductDetailMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject _productField;
        private readonly string _currencyCode;

        public ProductDetailMeasurement(string currencyCode, ProductFieldObject productField)
        {
            _productField = productField;
            _currencyCode = currencyCode;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var objects = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                objects.Add("currencyCode", _currencyCode);
            }

            objects.Add("detail", new Dictionary<string, object>
            {
                {
                    "products", new []{ _productField.Info}
                }
            });
            return objects;
        }
    }
}