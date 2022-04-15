using Microsoft.IdentityModel.Validators;
using System;
using System.Net.Http;

namespace CodeSnips.WilsonIssues
{
    public class AadIssuerValidatorIssue1836
    {
        public static void Run()
        {
            HttpClient httpClient = new HttpClient();
            // httpclient will be null on both
            AadIssuerValidator aadIssuerValidatorNoHttpClient = AadIssuerValidator.GetAadIssuerValidator("address");
            AadIssuerValidator aadIssuerValidatorWithHttpClient = AadIssuerValidator.GetAadIssuerValidator("address", httpClient);

            // httpclient will be set on both
            aadIssuerValidatorWithHttpClient = AadIssuerValidator.GetAadIssuerValidator("address2", httpClient);
            aadIssuerValidatorNoHttpClient = AadIssuerValidator.GetAadIssuerValidator("address2");
        }
    }
}
