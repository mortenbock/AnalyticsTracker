using System;
using Vertica.AnalyticsTracker.Commands;

namespace Vertica.AnalyticsTracker.Messages
{
    public class CookieGuardedMessage : MessageBase
    {
        private readonly MessageBase _guardedMessage;
        private readonly string _commandId;
        private readonly int _cookieExpirationDays;
        private readonly DateTime? _now;

        public CookieGuardedMessage(MessageBase guardedMessage, string commandId, int cookieExpirationDays = 365) : this(guardedMessage, commandId, cookieExpirationDays, null) { }

        /// <summary>
        /// For testing purposes
        /// </summary>
        /// <param name="guardedMessage"></param>
        /// <param name="commandId"></param>
        /// <param name="cookieExpirationDays"></param>
        /// <param name="now"></param>
        internal CookieGuardedMessage(MessageBase guardedMessage, string commandId, int cookieExpirationDays, DateTime? now)
        {
            _guardedMessage = guardedMessage;
            _commandId = commandId;
            _cookieExpirationDays = cookieExpirationDays;
            _now = now;
        }

        public override string RenderMessage()
        {
            var guardedScript = _guardedMessage.RenderMessage();
            return CookieGuardedScript.GuardScript(guardedScript, _commandId, _now, _cookieExpirationDays);
        }
    }
}