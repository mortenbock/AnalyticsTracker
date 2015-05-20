using System.Collections.Generic;
using NUnit.Framework;
using Vertica.AnalyticsTracker;

namespace AnalyticsTracker.Tests
{
	[TestFixture]
	public class ConfigurationObjectTester
	{
		[Test]
		public void Render_IntValue_RendersInt()
		{
			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", 1234 } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("{'mykey': 1234}"));
		}

		[Test]
		public void Render_StringValue_RendersString()
		{
			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", "myval" } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("{'mykey': 'myval'}"));
		}

		[Test]
		public void Render_StringValue_RendersEscapedString()
		{
			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", "my'val" } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining(@"{'mykey': 'my\u0027val'}"));
		}

		[Test]
		public void Render_StringValueWithLineBreak_RendersEscapedString()
		{
			var subj = new ConfigurationObject(new Dictionary<string, object> {{"mykey", @"my
val"}});
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining(@"{'mykey': 'my\r\nval'}"));
		}

		[Test]
		public void Render_DictionaryValue_RendersObject()
		{
			var value = new Dictionary<string, object>
			{
				{"dicKey1", "myValue"},
				{"dicKey2", 1}
			};
			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", value } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("{'mykey': {'dicKey1': 'myValue','dicKey2': 1}}"));
		}

		[Test]
		public void Render_ArrayValue_RendersArray()
		{
			var value = new[] { "val1", "val2" };

			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", value } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("{'mykey': ['val1','val2']}"));
		}

		[Test]
		public void Render_DictionaryValueWithArray_RendersObject()
		{
			var value = new Dictionary<string, object>
			{
				{"dicKey1", "myValue"},
				{"dicKey2", new object[] {"val1", 1234}}
			};

			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", value } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("{'mykey': {'dicKey1': 'myValue','dicKey2': ['val1',1234]}}"));
		}

		[Test]
		public void Render_ArrayOfDictionaries_RendersArrayOfObjects()
		{
			var value = new[]
			{
				new Dictionary<string, object> {{"mykey1","val1"},{"mykey2",1234}}, 
				new Dictionary<string, object> {{"mykey1","val2"},{"mykey2",2345}}
			};

			var subj = new ConfigurationObject(new Dictionary<string, object> { { "mykey", value } });
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("{'mykey': [{'mykey1': 'val1','mykey2': 1234},{'mykey1': 'val2','mykey2': 2345}]}"));
		}
	}
}