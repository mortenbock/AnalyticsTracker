using System.Collections.Generic;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class ProductDetailMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject _productField;

        public ProductDetailMeasurement(ProductFieldObject productField)
        {
            _productField = productField;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            return new Dictionary<string, object>
            {
                {
                    "detail", new Dictionary<string, object>
                    {
                        {
                            "products", new []{ _productField.Info}
                        }
                    }
                }
            };
        }
    }
}