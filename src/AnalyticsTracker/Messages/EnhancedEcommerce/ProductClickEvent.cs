using System.Collections.Generic;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class ProductClickEvent : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject _productField;
        private readonly string _list;

        public ProductClickEvent(string list, ProductFieldObject product) : base("productClick")
        {
            _productField = product;
            _list = list;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            return new Dictionary<string, object>
            {
                {
                    "click", new Dictionary<string, object>
                    {
                        {"actionField", new ProductClickActionFieldObject(_list) },
                        {"products", new []{ _productField.Info} }
                    }
                }
            };
        }
    }
}