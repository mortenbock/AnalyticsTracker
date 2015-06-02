using System;
using System.Text;

namespace Vertica.AnalyticsTracker.Commands
{
	public class CookieGuardedCommand : CommandBase
	{
		private readonly CommandBase _guardedCommand;
		private readonly string _commandId;
		private readonly int _cookieExpirationDays;
		private readonly DateTime? _now;

		public CookieGuardedCommand(CommandBase guardedCommand, string commandId, int cookieExpirationDays = 365) : this(guardedCommand, commandId, cookieExpirationDays, null) { }

		/// <summary>
		/// For testing purposes
		/// </summary>
		/// <param name="guardedCommand"></param>
		/// <param name="commandId"></param>
		/// <param name="cookieExpirationDays"></param>
		/// <param name="now"></param>
		internal CookieGuardedCommand(CommandBase guardedCommand, string commandId, int cookieExpirationDays, DateTime? now)
		{
			_guardedCommand = guardedCommand;
			_commandId = commandId;
			_cookieExpirationDays = cookieExpirationDays;
			_now = now;
		}

		public override string[] RequiredPlugins
		{
			get
			{
				return _guardedCommand.RequiredPlugins;

			}
		}

		public override CommandOrder Order
		{
			get { return _guardedCommand.Order; }
		}

		public override string RenderCommand()
		{
			var cookiePrefix = "AnalyticsTrackerGuard";

			var sb = new StringBuilder();

			string encodedId = Uri.EscapeDataString(_commandId);
			var clause = string.Format("if (document.cookie.search(/{0}{1}=true/) === -1) {{", cookiePrefix, encodedId);
			sb.AppendLine(clause);

			sb.Append(_guardedCommand.RenderCommand());

			DateTime currentTime = _now ?? DateTime.Now;
			var inOneYear = currentTime.AddDays(_cookieExpirationDays);
			var cookieSetter = string.Format("document.cookie = '{0}{1}=true; Expires=' + new Date({2}, {3:00}, {4:00}).toUTCString();", cookiePrefix, encodedId, inOneYear.Year, inOneYear.Month - 1, inOneYear.Day);
			sb.AppendLine(cookieSetter);
			sb.AppendLine("}");
			return sb.ToString();
		}
	}
}