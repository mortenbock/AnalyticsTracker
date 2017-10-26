using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class PurchaseMeasurement : EnhancedEcommerceMeasurementBase
    {
        private readonly ProductFieldObject[] _productFields;
        private readonly PurchaseActionFieldObject _purchaseActionField;
        private readonly string _currencyCode;

        public PurchaseMeasurement(string currencyCode, PurchaseActionFieldObject purchaseActionField, ProductFieldObject[] products)
        {
            _productFields = products;
            _currencyCode = currencyCode;
            _purchaseActionField = purchaseActionField;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var objects = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                objects.Add("currencyCode", _currencyCode);
            }

            objects.Add("purchase", new Dictionary<string, object>
            {
                {"actionField",  _purchaseActionField.Info },
                {"products",  _productFields.Select(p=>p.Info).ToArray() }
            });
            return objects;
        }
    }
}