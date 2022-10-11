using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class EmbededFunctions
    {
        readonly static int loops = 100000;
        readonly static int numberofHandlers = 4;

        [Benchmark]
        public void UsingLamda()
        {
            Policy policy = new Policy();
            Context context = new Context();
            IList<Exception> exceptions = new List<Exception>();
            AuthenticationResult authenticationResult = new AuthenticationResult();

            for (int i = 0; i < loops; i++)
            {
                for (int j = 0; j < numberofHandlers; j++)
                { 
                    // validate AccessTokenType
                    // we log informational as the next policy could succeed and this may not have been an error.
                    var validateAccessTokenTypeSuccess = EmbededFunctionsDelegator.ValidateAndCollectExceptions(
                        () => policy.IsAccessTokenTypeValid("AccessTokenType"),
                        isValid => isValid ? null : Logger.LogException(
                                    EventLevel.Informational,
                                    new AuthenticationException(Logger.FormatInvariant("LogMessages.S2S12404", "Label", new Exception()), new Exception())
                                    { AuthenticationFailureType = AuthenticationFailureType.UnsupportedAccessTokenType },
                                    context),
                            policy,
                            context,
                            exceptions,
                            authenticationResult);

                    if (!validateAccessTokenTypeSuccess)
                        continue;
                }
            }
        }

        [Benchmark]
        public void DirectCalls()
        {
            Policy policy = new Policy();
            Context context = new Context();
            IList<Exception> exceptions = new List<Exception>();
            AuthenticationResult authenticationResult = new AuthenticationResult();
            Exception exception = new Exception();

            for (int i = 0; i < loops; i++)
            {
                for (int j = 0; j < numberofHandlers; j++)
                {
                    // validate AccessTokenType
                    // we log informational as the next policy could succeed and this may not have been an error.
                    if (!policy.IsAccessTokenTypeValid("AccessTokenType"))
                        EmbededFunctionsDelegator.CollectException(
                            policy,
                            exceptions,
                            authenticationResult,
                            Logger.LogException(
                                    EventLevel.Informational,
                                    new AuthenticationException(Logger.FormatInvariant("LogMessages.S2S12404", "Label", new Exception()), new Exception())
                                    { AuthenticationFailureType = AuthenticationFailureType.UnsupportedAccessTokenType },
                                    context),
                            Utils.GetAuthenticationFailureType(exception));

                        continue;
                }
            }
        }
    }
}
