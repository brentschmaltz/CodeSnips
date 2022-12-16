using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens.Saml2;
using System;

namespace CodeSnips.WilsonIssues
{
    public class WilsonIssue1873
    {
        private static string samlToken = "<saml:Assertion xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"_e9c223c5-1792-4a85-9a44-938e0dcb017d\" IssueInstant=\"2022-06-10T09:59:33.679Z\" Version=\"2.0\"><saml:Issuer>FooBar</saml:Issuer><saml:Subject><saml:NameID>UserKey</saml:NameID><saml:SubjectConfirmation Method = \"urn:oasis:names:tc:SAML:2.0:cm:bearer\"></saml:SubjectConfirmation></saml:Subject>	<saml:Conditions NotBefore = \"2022-06-10T09:59:33.665Z\" NotOnOrAfter=\"2022-06-10T10:59:33.669Z\"><saml:Condition xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:type=\"saml:OneTimeUseType\" /></saml:Conditions></saml:Assertion>";
        public static void Run()
        {
            IdentityModelEventSource.ShowPII = true;

            Saml2SecurityTokenHandler saml2SecurityTokenHandler = new Saml2SecurityTokenHandler();
            try
            {
                saml2SecurityTokenHandler.ReadToken(samlToken);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Saml2SecurityTokenHandler.ReadToken threw: '{ex.Message}'.");
            }

            try
            {
                saml2SecurityTokenHandler.ReadSaml2Token(samlToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Saml2SecurityTokenHandler.ReadSaml2Token threw: '{ex.Message}'.");
            }
        }
    }
}
