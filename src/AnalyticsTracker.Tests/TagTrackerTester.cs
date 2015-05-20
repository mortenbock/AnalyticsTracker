using NUnit.Framework;
using Vertica.AnalyticsTracker;
using Vertica.AnalyticsTracker.Messages;

namespace AnalyticsTracker.Tests
{
	[TestFixture]
	public class TagTrackerTester
	{
		[Test]
		public void Render_Configured_SetsIdAndDataLayerName()
		{
			var subj = new TagTracker();
			subj.SetAccount("GTM-12345");
			subj.SetDataLayerName("myDataLayer");
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("(window,document,'script','myDataLayer','GTM-12345')"));
		}

		[Test]
		public void Render_WithVariable_CorrectDataLayerName()
		{
			var subj = new TagTracker();
			subj.SetAccount("GTM-12345");
			subj.SetDataLayerName("myDataLayer");
			subj.AddMessage(new Variable("myVariable", "myValue"));
			var rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("var myDataLayer = [];"));
			Assert.That(rendered, Is.StringContaining("myDataLayer.push({'myVariable': 'myValue'});"));
		}
	}
}