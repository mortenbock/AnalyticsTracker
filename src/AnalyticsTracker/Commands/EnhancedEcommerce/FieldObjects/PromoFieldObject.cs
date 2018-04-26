using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects
{
    public class PromoFieldObject
    {
        private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

        public PromoFieldObject(string id, string name, string creative = null, string position = null)
        {
            _info["id"] = id;
            _info["name"] = name;
            _info["creative"] = creative;
            _info["position"] = position;
        }

        public Dictionary<string, object> Info => _info;
    }
}
