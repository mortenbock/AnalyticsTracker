using System.Collections.Generic;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class ProductClickEvent : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject _productField;
        private readonly string _list;
        private readonly string _currencyCode;

        public ProductClickEvent(string currencyCode, string list, ProductFieldObject product) : base("productClick")
        {
            _productField = product;
            _currencyCode = currencyCode;
            _list = list;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var objects = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                objects.Add("currencyCode", _currencyCode);
            }

            objects.Add("click", new Dictionary<string, object>
            {
                {"actionField", new ProductClickActionFieldObject(_list).Info },
                {"products", new []{ _productField.Info} }
            });
            return objects;
        }
    }
}