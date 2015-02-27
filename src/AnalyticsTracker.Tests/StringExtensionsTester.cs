using NUnit.Framework;
using Vertica.AnalyticsTracker.Extensions;

namespace AnalyticsTracker.Tests
{
	[TestFixture]
	public class StringExtensionsTester
	{
		[Test]
		public void Test()
		{
			var safeSubstring = "Mikael".SafeSubstring(3, 10);

			Assert.That(safeSubstring, Is.EqualTo("ael"));
		}
	}
}