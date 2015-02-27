using System.Collections.Generic;
using NUnit.Framework;
using Vertica.AnalyticsTracker;

namespace AnalyticsTracker.Tests
{
	[TestFixture]
	public class CommandTrackerTester
	{
		[Test]
		public void Render_AccountSet_CreatesTracker()
		{
			var subj = new CommandTracker();
			subj.SetAccount("UA-00000000-1");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('create', 'UA-00000000-1', 'auto');"));
		}

		[Test]
		public void Render_NoConfig_CreatesTrackerWithAuto()
		{
			var subj = new CommandTracker();
			subj.SetAccount("UA-00000000-1");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('create', 'UA-00000000-1', 'auto');"));
		}

		[Test]
		public void Render_WithConfig_CreatesTrackerWithConfig()
		{
			var subj = new CommandTracker();
			subj.SetAccount("UA-00000000-1");
			subj.SetTrackerConfiguration(new Dictionary<string, object>
			{
				{"name", "myTracker"},
				{"siteSpeedSampleRate", 50},
				{"alwaysSendReferrer", true}
			});
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('create', 'UA-00000000-1', {'name': 'myTracker','siteSpeedSampleRate': 50,'alwaysSendReferrer': true});"));
		}

		[Test]
		public void Render_NoPageSet_DefaultEnabled_SetDefaultPage()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('send', 'pageview');"));
		}

		[Test]
		public void Render_NoPageSet_DefaultDisabled_NoPageSet()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = false;
			string rendered = subj.Render();
			Assert.That(rendered, Is.Not.StringContaining("ga('send', 'pageview'"));
		}

		[Test]
		public void Render_PageSet_PageTracked()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			subj.SetPage("/foo");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('set', {'page': '/foo'})"));
		}

		[Test]
		public void Render_CurrencySet_CurrencyTracked()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			subj.SetCurrency(AnalyticsCurrency.DKK);
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('set', {'&cu': 'DKK'})"));
		}

		[Test]
		public void Render_WithDisplayFeatures_DisplayFeaturesAdded()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			subj.Require("displayfeatures");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('require', 'displayfeatures');"));
		}
	}
}