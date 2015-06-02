using System;
using NUnit.Framework;
using Vertica.AnalyticsTracker.Commands;
using Vertica.AnalyticsTracker.Commands.Events;

namespace AnalyticsTracker.Tests.Commands
{
	[TestFixture]
	public class CookieGuardedCommandTester
	{
		[Test]
		public void Render_CleanId_CookieSetAndChecked()
		{
			DateTime? now = new DateTime(2014, 09, 05);
			var cmd = new CookieGuardedCommand(new EventCommand("cat", "act"), "myid", 365, now);
			var rendered = cmd.RenderCommand();
			Assert.That(rendered, Is.StringContaining("if (document.cookie.search(/AnalyticsTrackerGuardmyid=true/) === -1)"));
			Assert.That(rendered, Is.StringContaining("document.cookie = 'AnalyticsTrackerGuardmyid=true; Expires=' + new Date(2015, 08, 05).toUTCString();"));
		}

		[Test]
		public void Render_WeirdId_IdIsEncoded()
		{
			DateTime? now = new DateTime(2014, 09, 05);
			var cmd = new CookieGuardedCommand(new EventCommand("cat", "act"), "my id = weird;stuff", 365, now);
			var rendered = cmd.RenderCommand();
			Assert.That(rendered, Is.StringContaining("if (document.cookie.search(/AnalyticsTrackerGuardmy%20id%20%3D%20weird%3Bstuff=true/) === -1)"));
			Assert.That(rendered, Is.StringContaining("document.cookie = 'AnalyticsTrackerGuardmy%20id%20%3D%20weird%3Bstuff=true; Expires=' + new Date(2015, 08, 05).toUTCString();"));
		}

		[Test]
		public void Render_CustomTimeSpan_CorrectTimespanSet()
		{
			DateTime? now = new DateTime(2014, 09, 05);
			var cmd = new CookieGuardedCommand(new EventCommand("cat", "act"), "my id = weird;stuff", 7, now);
			var rendered = cmd.RenderCommand();
			Assert.That(rendered, Is.StringContaining("if (document.cookie.search(/AnalyticsTrackerGuardmy%20id%20%3D%20weird%3Bstuff=true/) === -1)"));
			Assert.That(rendered, Is.StringContaining("document.cookie = 'AnalyticsTrackerGuardmy%20id%20%3D%20weird%3Bstuff=true; Expires=' + new Date(2014, 08, 12).toUTCString();"));
		}
	}
}