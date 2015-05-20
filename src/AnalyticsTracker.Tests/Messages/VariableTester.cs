using System.Collections.Generic;
using NUnit.Framework;
using Vertica.AnalyticsTracker.Messages;

namespace AnalyticsTracker.Tests.Messages
{
	[TestFixture]
	public class VariableTester
	{
		[Test]
		public void RenderMessage_ValueIsString_QuotedValueRendered()
		{
			var subj = new Variable("myvar", "myvalue");
			var renderedMessage = subj.RenderMessage("dataLayer");
			Assert.That(renderedMessage, Is.StringContaining("{'myvar': 'myvalue'}"));
		}

		[Test]
		public void RenderMessage_ValueIsDecimal_ValueRendered()
		{
			var subj = new Variable("myvar", 12.34M);
			var renderedMessage = subj.RenderMessage("dataLayer");
			Assert.That(renderedMessage, Is.StringContaining("{'myvar': 12.34}"));
		}

		[Test]
		public void RenderMessage_ValueIsArray_ArrayRendered()
		{
			var subj = new Variable("myvar", new[] { "val1", "val2" });
			var renderedMessage = subj.RenderMessage("dataLayer");
			Assert.That(renderedMessage, Is.StringContaining("{'myvar': ['val1','val2']}"));
		}

		[Test]
		public void RenderMessage_ValueIsDictionary_ObjectRendered()
		{
			var subj = new Variable("myvar", new Dictionary<string, object> {{"key1", "val1"}});
			var renderedMessage = subj.RenderMessage("dataLayer");
			Assert.That(renderedMessage, Is.StringContaining("{'myvar': {'key1': 'val1'}}"));
		}
	}
}