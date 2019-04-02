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
		    Assert.That(rendered, Is.StringContaining("var myDataLayer = myDataLayer || []; function tagManagerPush(obj){myDataLayer.push(obj);}"));
		    Assert.That(rendered, Is.StringContaining("window.tagManagerPush({'myVariable': 'myValue'});"));
		}

		[Test]
		public void RenderScript_Configured_SetsConfiguration()
		{
			var subj = new TagTracker();
			subj.SetAccount("GTM-12345");
			subj.SetDataLayerName("myDataLayer");
			subj.SetEnvironmentAuth("auth1234");
			subj.SetEnvironmentPreview("env-2");
			var rendered = subj.RenderScript();
			Assert.That(rendered, Is.StringContaining("(window,document,'script','myDataLayer','GTM-12345')"));
			Assert.That(rendered, Is.StringContaining("id='+i+dl+'&gtm_auth=auth1234&gtm_preview=env-2&gtm_cookies_win=x'"));
		}

		[Test]
		public void RenderNoScript_Configured_SetsConfiguration()
		{
			var subj = new TagTracker();
			subj.SetAccount("GTM-12345");
			subj.SetDataLayerName("myDataLayer");
			subj.SetEnvironmentAuth("auth1234");
			subj.SetEnvironmentPreview("env-2");
			var rendered = subj.RenderNoScript();
			Assert.That(rendered, Is.StringContaining("id=GTM-12345&gtm_auth=auth1234&gtm_preview=env-2&gtm_cookies_win=x"));
			Assert.That(rendered, Is.Not.StringContaining("myDataLayer"));
		}

		[Test]
		public void RenderDataLayer_WithVariable_CorrectDataLayerName()
		{
			var subj = new TagTracker();
			subj.SetAccount("GTM-12345");
			subj.SetDataLayerName("myDataLayer");
			subj.AddMessage(new Variable("myVariable", "myValue"));
			var rendered = subj.RenderDataLayer();
			Assert.That(rendered, Is.StringContaining("var myDataLayer = myDataLayer || []; function tagManagerPush(obj){myDataLayer.push(obj);}"));
			Assert.That(rendered, Is.StringContaining("window.tagManagerPush({'myVariable': 'myValue'});"));
		}
	}
}