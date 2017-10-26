using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class CheckoutStepMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject[] _productFields;
        private readonly CheckoutActionFieldObject _checkoutActionField;
        private readonly string _currencyCode;

        public CheckoutStepMeasurement(string currencyCode, CheckoutActionFieldObject checkoutActionField, ProductFieldObject[] products) : base("checkout")
        {
            _productFields = products;
            _currencyCode = currencyCode;
            _checkoutActionField = checkoutActionField;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var objects = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                objects.Add("currencyCode", _currencyCode);
            }

            objects.Add("checkout", new Dictionary<string, object>
            {
                {"actionField",  _checkoutActionField.Info },
                {"products",  _productFields.Select(p=>p.Info).ToArray() }
            });
            return objects;
        }
    }
}