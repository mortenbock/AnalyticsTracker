using System;
using System.Text;

namespace Vertica.AnalyticsTracker.Commands
{
    public class CookieGuardedScript
    {
        public static string GuardScript(string guardedScript, string commandId, DateTime? now, int cookieExpirationDays)
        {
            var cookiePrefix = "AnalyticsTrackerGuard";

            var sb = new StringBuilder();

            string encodedId = Uri.EscapeDataString(commandId);
            var clause = string.Format("if (document.cookie.search(/{0}{1}=true/) === -1) {{", cookiePrefix, encodedId);
            sb.AppendLine(clause);

            sb.Append(guardedScript);

            DateTime currentTime = now ?? DateTime.Now;
            var inOneYear = currentTime.AddDays(cookieExpirationDays);
            var cookieSetter =
                string.Format("document.cookie = '{0}{1}=true; Expires=' + new Date({2}, {3:00}, {4:00}).toUTCString();",
                    cookiePrefix, encodedId, inOneYear.Year, inOneYear.Month - 1, inOneYear.Day);
            sb.AppendLine(cookieSetter);
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}