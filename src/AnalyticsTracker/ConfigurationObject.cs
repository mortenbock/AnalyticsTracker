using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
					var value = v.Value;
					string renderedValue;
					if (value is Array)
					{
						var sb = new StringBuilder();
						sb.Append("[");
						var arrayValues = (value as Array).Cast<object>();
						var renderedValues = arrayValues.Select(RenderValue);
						sb.Append(string.Join(",", renderedValues));
						sb.Append("]");
						renderedValue = sb.ToString();
					}
					else
					{
						renderedValue = RenderValue(value);
					}
					return string.Format("'{0}': {1}", v.Key, renderedValue);
				}));
			return string.Format("{{{0}}}", values);
		}

		private static string RenderValue(object value)
		{
			string renderedValue;
			if (value is bool)
			{
				renderedValue = value.ToString().ToLowerInvariant();
			}
			else if (value is int || value is uint)
			{
				renderedValue = value.ToString();
			}
			else if (value is decimal)
			{
				renderedValue = ((decimal) value).ToString("0.######", CultureInfo.InvariantCulture);
			}
			else if (value is Dictionary<string, object>)
			{
				renderedValue = new ConfigurationObject((Dictionary<string, object>) value).Render();
			}
			else
			{
				renderedValue = string.Format("'{0}'", HttpUtility.JavaScriptStringEncode(value.ToString()));
			}
			return renderedValue;
		}

		public string Render(string outputIfEmpty)
		{
			return _values.Any() ? Render() : outputIfEmpty;
		}
	}
}