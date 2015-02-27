using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects
{
	public class ActionFieldObject
	{
		private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

		public ActionFieldObject(string id, string affiliation, decimal? revenue, decimal? tax, decimal? shipping, string coupon, string list, uint? step, string option)
		{
			_info["id"] = id;
			_info["affiliation"] = affiliation;
			_info["revenue"] = revenue;
			_info["tax"] = tax;
			_info["shipping"] = shipping;
			_info["coupon"] = coupon;
			_info["list"] = list;
			_info["step"] = step;
			_info["option"] = option;
		}

		public Dictionary<string, object> Info { get { return _info; } }

	}
}