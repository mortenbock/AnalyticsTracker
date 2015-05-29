using NUnit.Framework;
using Vertica.AnalyticsTracker.Commands;

namespace AnalyticsTracker.Tests.Commands
{
	[TestFixture]
	public class PageViewCommandTester
	{
		[Test]
		public void Render_HasPage_SetsPage()
		{
			var subject = new PageViewCommand("/some-url?with=query");
			string rendered = subject.RenderCommand();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview','page': '/some-url?with=query'});"));
		}

		[Test]
		public void Render_HasTitle_SetsTitle()
		{
			var subject = new PageViewCommand(title: "My title");
			string rendered = subject.RenderCommand();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview','title': 'My title'});"));
		}

		[Test]
		public void Render_HasPageAndTitle_SetsBoth()
		{
			var subject = new PageViewCommand("/some-url", "My title");
			string rendered = subject.RenderCommand();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview','page': '/some-url','title': 'My title'});"));
		}

		[Test]
		public void Render_HasNoInfo_PurePageView()
		{
			var subject = new PageViewCommand();
			string rendered = subject.RenderCommand();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview'});"));
		}

	}
}