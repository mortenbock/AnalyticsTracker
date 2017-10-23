using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class RemoveFromCartEvent : EnhancedEcommerceMeasurementBase
    {
        private readonly string _currencyCode;
        private readonly ProductFieldObject[] _productFields;

        public RemoveFromCartEvent(string currencyCode, ProductFieldObject[] products) : base("removeFromCart")
        {
            _currencyCode = currencyCode;
            _productFields = products;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            return new Dictionary<string, object>
            {
                { "currencyCode",_currencyCode},
                {
                    "remove", new Dictionary<string, object>
                    {
                        {"products",  _productFields.Select(p=>p.Info).ToArray() }
                    }
                }
            };
        }
    }
}