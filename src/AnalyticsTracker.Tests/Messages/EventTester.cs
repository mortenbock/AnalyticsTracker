using NUnit.Framework;
using Vertica.AnalyticsTracker.Messages;

namespace AnalyticsTracker.Tests.Messages
{
	[TestFixture]
	public class EventTester
	{
		[Test]
		public void RenderMessage_ValueIsString_QuotedValueRendered()
		{
			var subj = new Event("myEventName");
			var renderedMessage = subj.RenderMessage();
			Assert.That(renderedMessage, Is.StringContaining("{'event': 'myEventName'}"));
		}
	}
}