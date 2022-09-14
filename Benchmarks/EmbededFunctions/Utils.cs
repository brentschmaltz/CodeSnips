using System;
using System.Diagnostics.Tracing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    internal class AuthenticationFailureType
    {
        internal static AuthenticationFailureType UnsupportedAccessTokenType => new UnsupportedAccessTokenType();
    }

    internal class UnsupportedAccessTokenType : AuthenticationFailureType
    {

    }

    internal class AuthenticationException : Exception
    {
        internal AuthenticationException(string message, Exception ex)
            : base(message, ex)
        {
        }

        internal AuthenticationFailureType AuthenticationFailureType { get; set; } = new AuthenticationFailureType();
    }

    internal class AuthenticationResult
    {
    }

    internal class Context
    {
    }

    internal class Logger
    {
        internal static Exception LogException(EventLevel eventLevel, AuthenticationException authenticationException, Context context)
        {
            return authenticationException;
        }

        internal static string FormatInvariant(string message, string label, Exception ex)
        {
            return message;
        }
    }

    internal class LogMessages
    {
        internal static string S2S12086 => "S2S12086:";
    }

    internal class Policy
    {
        internal string Label => "Label";
        
        internal bool IsAccessTokenTypeValid(string tokenType)
        {
            return false;
        }
    }

    internal class Utils
    {
        internal static AuthenticationFailureType GetAuthenticationFailureType(Exception ex) => new AuthenticationFailureType();
    }
}
