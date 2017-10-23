using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class CheckoutStepMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject[] _productFields;
        private readonly CheckoutActionFieldObject _checkoutActionField;

        public CheckoutStepMeasurement(CheckoutActionFieldObject checkoutActionField, ProductFieldObject[] products) : base("checkout")
        {
            _productFields = products;
            _checkoutActionField = checkoutActionField;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            return new Dictionary<string, object>
            {
                {
                    "checkout", new Dictionary<string, object>
                    {
                        {"actionField",  _checkoutActionField.Info },
                        {"products",  _productFields.Select(p=>p.Info).ToArray() }
                    }
                }
            };
        }
    }
}