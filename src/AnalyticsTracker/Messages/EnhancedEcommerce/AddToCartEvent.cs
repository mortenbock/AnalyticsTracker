using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class AddToCartEvent : EnhancedEcommerceMeasurementBase
    {
        private readonly string _currencyCode;
        private readonly ProductFieldObject[] _productFields;

        public AddToCartEvent(string currencyCode, ProductFieldObject[] products) : base("addToCart")
        {
            _currencyCode = currencyCode;
            _productFields = products;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var objects = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                objects.Add("currencyCode", _currencyCode);
            }
            objects.Add("add", new Dictionary<string, object>
            {
                {"products",  _productFields.Select(p=>p.Info).ToArray() }
            });
            return objects;
        }
    }
}