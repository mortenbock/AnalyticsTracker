using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class PurchaseMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject[] _productFields;
        private readonly PurchaseActionFieldObject _purchaseActionField;

        public PurchaseMeasurement(PurchaseActionFieldObject purchaseActionField, ProductFieldObject[] products)
        {
            _productFields = products;
            _purchaseActionField = purchaseActionField;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            return new Dictionary<string, object>
            {
                {
                    "purchase", new Dictionary<string, object>
                    {
                        {"actionField",  _purchaseActionField.Info },
                        {"products",  _productFields.Select(p=>p.Info).ToArray() }
                    }
                }
            };
        }
    }
}