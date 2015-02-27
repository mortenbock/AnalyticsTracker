using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Vertica.AnalyticsTracker
{
	public class ConfigurationObject
	{
		private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

		public ConfigurationObject(Dictionary<string, object> values)
		{
			_values = values ?? _values;
		}

		public string Render()
		{
			string values = string.Join(",", _values.Where(v => v.Value != null)
				.Select(v =>
				{
					string value;
					if (v.Value is bool)
					{
						value = v.Value.ToString().ToLowerInvariant();
					}
					else if (v.Value is int || v.Value is uint)
					{
						value = v.Value.ToString();
					}
					else if(v.Value is decimal)
					{
						value = ((decimal) v.Value).ToString("0.######", CultureInfo.InvariantCulture);
					}
					else
					{
						value = string.Format("'{0}'", HttpUtility.JavaScriptStringEncode(v.Value.ToString()));
					}

					return string.Format("'{0}': {1}", v.Key, value);
				}));
			return string.Format("{{{0}}}", values);
		}

		public string Render(string outputIfEmpty)
		{
			return _values.Any() ? Render() : outputIfEmpty;
		}
	}
}