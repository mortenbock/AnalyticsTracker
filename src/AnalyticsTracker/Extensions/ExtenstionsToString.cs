namespace Vertica.AnalyticsTracker.Extensions
{
	public static class ExtenstionsToString
	{
		public static string SafeSubstring(this string input, int startIndex, int length)
		{
			if (string.IsNullOrWhiteSpace(input))
				return string.Empty;

			if (startIndex + length > input.Length)
				return input.Substring(startIndex, input.Length - startIndex);

			return input.Substring(startIndex, length);

		}
	}
}