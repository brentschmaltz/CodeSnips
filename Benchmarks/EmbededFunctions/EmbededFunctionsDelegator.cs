using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Benchmarks
{
    internal class EmbededFunctionsDelegator
    {
        internal static T ValidateAndCollectExceptions<T>(
            Func<T> validateFunc,
            Func<T, Exception> getException,
            Policy policy,
            Context context,
            IList<Exception> exceptions,
            AuthenticationResult authenticationResult)
        {
            try
            {
                T result = validateFunc();
                Exception ex = getException(result);

                // there could be token validation related exceptions like SecurityTokenInvalidSignatureException/SecurityTokenValidationException which are not AuthenticationException
                if (ex != null)
                {
                    var authFailureType = Utils.GetAuthenticationFailureType(ex);
                    if (!(ex is AuthenticationException))
                        ex = Logger.LogException(EventLevel.Informational, new AuthenticationException(Logger.FormatInvariant(LogMessages.S2S12086, policy.Label, ex), ex), context);

                    CollectException(policy, exceptions, authenticationResult, ex, authFailureType);
                }
                return result;
            }
            catch (AuthenticationException authException)
            {
                // we do not log here because we assume all AuthenticationExceptions are piped through S2SLogger.LogException(...)
                CollectException(policy, exceptions, authenticationResult, authException, authException.AuthenticationFailureType);
            }
            catch (Exception ex)
            {
                // we log here because this is not a AuthenticationException, could be double logging for ArgumentNull and some others
                // we log informational as the next policy could succeed and this may not have been an error.
                Exception exception = Logger.LogException(EventLevel.Informational, new AuthenticationException(Logger.FormatInvariant(LogMessages.S2S12086, policy.Label, ex), ex), context);
                CollectException(policy, exceptions, authenticationResult, exception, Utils.GetAuthenticationFailureType(ex));
            }

            return default;
        }

        internal static void CollectException(
            Policy policy, 
            IList<Exception> exceptions, 
            AuthenticationResult authenticationResult, 
            Exception exception, 
            AuthenticationFailureType authenticationFailureType)
        {
            exceptions.Add(exception);
            //authenticationResult.InboundPolicyEvaluationResults.Add(
            //    new InboundPolicyEvaluationResult(policy)
            //    {
            //        Exception = ex,
            //        ValidationFailureMessage = ex.Message,
            //        AuthenticationFailureType = failureType
            //    });

        }
    }
}
