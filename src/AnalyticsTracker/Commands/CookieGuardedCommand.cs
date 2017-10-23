using System;

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
		    var guardedScript = _guardedCommand.RenderCommand();
		    return CookieGuardedScript.GuardScript(guardedScript, _commandId, _now, _cookieExpirationDays);
		}
	}
}