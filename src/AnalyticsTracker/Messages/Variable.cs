using System.Collections.Generic;

namespace Vertica.AnalyticsTracker.Messages
{
	public class Variable : MessageBase
	{
		private readonly string _name;
		private readonly object _value;

		private Variable(string name, object value)
		{
			_name = name;
			_value = value;
		}

		public Variable(string name, string value) : this(name, (object)value) { }
		public Variable(string name, string[] values) : this(name, (object)values) { }
		public Variable(string name, int value) : this(name, (object)value) { }
		public Variable(string name, int[] values) : this(name, (object)values) { }
		public Variable(string name, uint value) : this(name, (object)value) { }
		public Variable(string name, uint[] values) : this(name, (object)values) { }
		public Variable(string name, decimal value) : this(name, (object)value) { }
		public Variable(string name, decimal[] values) : this(name, (object)values) { }
		public Variable(string name, Dictionary<string, object> value) : this(name, (object)value) { }
		public Variable(string name, Dictionary<string, object>[] values) : this(name, (object)values) { }

		public override string RenderMessage(string dataLayerName)
		{
			var vals = new Dictionary<string, object> { { _name, _value } };
			var obj = new ConfigurationObject(vals);
			return Push(dataLayerName, obj);
		}
	}
}