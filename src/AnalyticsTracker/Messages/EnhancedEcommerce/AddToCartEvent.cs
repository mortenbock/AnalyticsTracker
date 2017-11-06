using System.Collections.Generic;
using System.Linq;
using Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public class AddToCartEvent : EnhancedEcommerceMeasurementBase
    {
        private readonly string _currencyCode;
        private readonly ProductFieldObject[] _productFields;
        private readonly string _list;


        public AddToCartEvent(string currencyCode, ProductFieldObject[] products, string list = null) : base("addToCart")
        {
            _currencyCode = currencyCode;
            _productFields = products;
            _list = list;
        }

        public override Dictionary<string, object> CreateMeasurement()
        {
            var addToBasketMessage = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_list))
            {
                addToBasketMessage.Add("actionField", new AddToBasketActionFieldObject(_list));
            }
            addToBasketMessage.Add("products", _productFields.Select(p => p.Info).ToArray());

            var messages = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_currencyCode))
            {
                messages.Add("currencyCode", _currencyCode);
            }
            messages.Add("add", addToBasketMessage);
            return messages;
        }
    }
}