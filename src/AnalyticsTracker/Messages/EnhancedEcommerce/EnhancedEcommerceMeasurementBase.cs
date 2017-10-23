using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Messages.EnhancedEcommerce
{
    public abstract class EnhancedEcommerceMeasurementBase : MessageBase
    {
        private readonly string _eventName;

        protected EnhancedEcommerceMeasurementBase(string eventName = null)
        {
            _eventName = eventName;
        }
        public sealed override string RenderMessage(string dataLayerName)
        {
            var baseDictionary = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_eventName))
            {
                baseDictionary.Add("event", _eventName);
            }
            baseDictionary.Add("ecommerce", CreateMeasurement());
            return Push(dataLayerName, new ConfigurationObject(baseDictionary));
        }

        public abstract Dictionary<string, object> CreateMeasurement();
    }
}