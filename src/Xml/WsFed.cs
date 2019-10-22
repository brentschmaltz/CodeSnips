using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.IdentityModel.Protocols.WsFederation;

namespace CodeSnips.Xml
{
    public class WsFed
    {
        static string metadata = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<EntityDescriptor xmlns=""urn:oasis:names:tc:SAML:2.0:metadata"" ID=""_378e4391-c3a4-4fe1-9cc2-aca2776c057a"" entityID=""https://website.com"">
   <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
      <SignedInfo>
         <CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
         <SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
         <Reference URI=""#_378e4391-c3a4-4fe1-9cc2-aca2776c057a"">
            <Transforms>
               <Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
               <Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
            </Transforms>
            <DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
            <DigestValue>YFS2kBcZO3yBqpxO54JMglAIFFBheliF+/e6Dv3CREA=</DigestValue>
         </Reference>
      </SignedInfo>
      <SignatureValue>oSRNy/F7zmozK5MtUwfGPfQL/s0s09QjWfno6hDSkVz9bVv2/u9s60j+u+cHsZqvbR36LSaVq/eKq0TyphOkYrcp40VnSQ72+HkEuOU5CKhJB4ApWqzrRKAjbdEKsGSUd6kAvpLq1QwiA80luvG8D/pLdak11kiU0esozbpw4MVrk7k6f7gH388vEGL/3njDuJdQpRaYWAmSh5dV9O30UuOODv3DRwJOX/fEg5IsV47gZf8N1jn5uAU9OCeuVCM27cqK9nGVOjNrU9yCprtG+W/b+UReBa1K29t4XhTlOgcHuilcpaYDb8lqfCGpadUSZyki+6+EVsGRroSX+Z2Yg4/25UHXgJwaAVO5+X8nG/2VtSMZ8ak3KejZ6OfgP0TuwktLrPaitW0CMDQBii52+wVJ8L0RpLiwnm8MHQfLBmHUGtZhB1hVR+0Zxyrib7b6qm/WhDZgELzXwUKrdFPCO8jCSU8LQNjNy/rz4AjVQ70gUAlVjlCbhtaG/CbdY0OhRqOZ60nPwhKH8Dz8dMNE5xsgVWuRK/2szSVKwZcSC3NIz+X/5sYxDb6y78ad86G440er0Gpl99FpPDf7Bp4wcTpFCzEbTRH1UI/AJcnUlQNboIBEbxJ1nW8IKQr1CDclrPNMnjSpmzVl4=</SignatureValue>
      <KeyInfo>
         <X509Data>
            <X509Certificate>MIIEyDCCArCgAwIBAgIIHm9sSbojhkYwDQYJKoZIhvcNAQELBQAwEzERMA8GA1UEAwwIYmVxb20gQ0EwHhcNMTcwNzExMDAwMDAwWhcNMTkwNzExMDAwMDAwWjAgMR4wHAYDVQQDDBVwYXNzcG9ydF9zaWduaW5nX2NlcnQwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQC3pXxC03pOjTGzf/FuHqxvOsyc/AuKPzBRF0BL7wRK19bC2+ZExwzm3haHWnyZxLSfNQh6ddv70n4cXuP+0m/d+TeeyQx6dbmKKSz1vEzGYRJpIElUWJGsa4WrRLrI01lPXRzypmNaMwXfoLQf3ftPbPxFoZL5T9iPh7aUGMZr15GT1tHc6HWuudU5UmmQXZm1oPIOL9kV3P+A3pJdkAe0ZnuRNwUUQ+PLFvLh5sjXlcsmfB5LsqR9DeNNadnz5VEKQtRLO56rdYUP2LinJG+L9NRULVhy6IYbywtDLkyjESawhsyXNj3CLfXfA6GPa+dZK+3uhXte/UJPIO13jq9zdPEaM9uww24JSjrqhCsU4126+vteonxju4LNky4K/wdO33Z42tn0FLKbY4w==</X509Certificate>
         </X509Data>
      </KeyInfo>
   </Signature>
   <RoleDescriptor xmlns:fed=""http://docs.oasis-open.org/wsfed/federation/200706"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""fed:SecurityTokenServiceType"" protocolSupportEnumeration=""http://docs.oasis-open.org/wsfed/federation/200706"" ServiceDescription=""Staging1"">
      <KeyDescriptor use=""signing"">
         <KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
            <X509Data>
               <X509Certificate>MIIEyDCCArCgAwIBAgIIHm9sSbojhkYwDQYJKoZIhvcNAQELBQAwEzERMA8GA1UEAwwIYmVxb20gQ0EwHhcNMTcwNzExMDAwMDAwWhcNMTkwNzExMDAwMDAwWjAgMR4wHAYDVQQDDBVwYXNzcG9ydF9zaWduaW5nX2NlcnQwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQC3pXxC03pOjTGzf/FuHqxvOsyc/AuKPzBRF0BL7wRK19bC2+ZExwzm3haHW4Gf1HmalvAkrcusmVChtE/lk5j873kD64eH8YI7vKdFFbrk3hj4WklG1Uzedqe2TsRH4kakj6Z1y1Kt/mz3yj02YnyTdbsgsj8UoqjfAelbjGqw0I8AH97W5mFNx9eIxBvE7Vey3HRv3rCtSesRii6APJghReGJcZHNX9rx+wkM76UrdwVGOEKcdo2Fx0uEGUxXQ06fNJf9QwUqKKATq9Rbp9Ur1IfpYq5o1IMvxg4kslo5l5nmgMTKYInAi1LzY2IR6z6qqkNXuPYUCT6n1y4xgYBr6v30FZAQ8ZtJgwfuY7At3MVyjzVIqlofUPMHcTWxb3UmZIstZlSIRxw69YH0PQCcsuEiELbIbxOn5j4+NMR80wxHY+xLPmaIXVCJMgjlojx1kAaRwmJL5HbCQpFJXNQ9o2pqWJwIDAQABoxMwETAPBgNVHREECDAGhwR/AAABMA0GCSqGSIb3DQEBCwUAA4ICAQBlcZ7g2+qYDiW8i2Yb9n7ly4xDSygUi87kj6hBQqVy2RXW+H4jR9+1lXTwP/hBt4AyLOQq14lU3t0FFzCzOkll5s6DtJAze++6UQjK4+8MPDv6sSNprnM/eAzPQ42xnAF/fDuz1qyINB2RtO6n1UwcT4ZZ1UamGaoSZXRZ9l+TOabtCVkEUgOUfPl4WLJo+DuigSrey3hmb931cMB1Axyud0I+cDjhUz7ZM0aVvKKc3jAs9Agz56AJUejjOlcZ+SlraSVln8uewLC4ujnLoBncUfgCzDcc6Za7sB7wB/LTlIhsNY6NHhHTD3shhuo0usZF2liQ0QvUty+c9iKnicJ9nyZxLSfNQh6ddv70n4cXuP+0m/d+TeeyQx6dbmKKSz1vEzGYRJpIElUWJGsa4WrRLrI01lPXRzypmNaMwXfoLQf3ftPbPxFoZL5T9iPh7aUGMZr15GT1tHc6HWuudU5UmmQXZm1oPIOL9kV3P+A3pJdkAe0ZnuRNwUUQ+PLFvLh5sjXlcsmfB5LsqR9DeNNadnz5VEKQtRLO56rdYUP2LinJG+L9NRULVhy6IYbywtDLkyjESawhsyXNj3CLfXfA6GPa+dZK+3uhXte/UJPIO13jq9zdPEaM9uww24JSjrqhCsU4126+vteonxju4LNky4K/wdO33Z42tn0FLKbY4w==</X509Certificate>
            </X509Data>
         </KeyInfo>
      </KeyDescriptor>
      <fed:TokenTypesOffered>
         <fed:TokenType Uri=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1"" />
         <fed:TokenType Uri=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" />
      </fed:TokenTypesOffered>
      <fed:ClaimTypesOffered>
         <auth:ClaimType xmlns:auth=""http://docs.oasis-open.org/wsfed/authorization/200706"" Uri=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"" />
         <auth:ClaimType xmlns:auth=""http://docs.oasis-open.org/wsfed/authorization/200706"" Uri=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"" />
         <auth:ClaimType xmlns:auth=""http://docs.oasis-open.org/wsfed/authorization/200706"" Uri=""http://schemas.microsoft.com/ws/2008/06/identity/claims/role"" />
      </fed:ClaimTypesOffered>
      <fed:SecurityTokenServiceEndpoint>
         <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:Address>http://website.com:8080/issue/wstrust/message/username</wsa:Address>
         </wsa:EndpointReference>
      </fed:SecurityTokenServiceEndpoint>
      <fed:SecurityTokenServiceEndpoint>
         <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:Address>http://website.com:8080/issue/wstrust/message/certificate</wsa:Address>
         </wsa:EndpointReference>
      </fed:SecurityTokenServiceEndpoint>
      <fed:SecurityTokenServiceEndpoint>
         <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:Address>https://website.com/issue/wstrust/mixed/username</wsa:Address>
         </wsa:EndpointReference>
      </fed:SecurityTokenServiceEndpoint>
      <fed:SecurityTokenServiceEndpoint>
         <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:Address>https://website.com/issue/wstrust/mixed/certificate</wsa:Address>
         </wsa:EndpointReference>
      </fed:SecurityTokenServiceEndpoint>
      <fed:PassiveRequestorEndpoint>
         <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:Address>https://website.com/issue/hrd</wsa:Address>
         </wsa:EndpointReference>
      </fed:PassiveRequestorEndpoint>
   </RoleDescriptor>
   <RoleDescriptor xmlns:fed=""http://docs.oasis-open.org/wsfed/federation/200706"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""fed:ApplicationServiceType"" protocolSupportEnumeration=""http://docs.oasis-open.org/wsfed/federation/200706"" ServiceDescription=""Staging1"">
      <KeyDescriptor use=""signing"">
         <KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
            <X509Data>
               <X509Certificate>MIIEyDCCArCgAwIBAgIIHm9sSbojhkYwDQYJKoZIhvcNAQELBQAwEzERMA8GA1UEAwwIYmVxb20gQ0EwHhcNMTcwNzExMDAwMDAwWhcNMTkwNzExMDAwMDAwWjAgMR4wHAYDVQQDDBVwYXNzcG9ydF9zaWduaW5nX2NlcnQwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQC3pXxC03pOjTGzf/FuHqxvOsyc/AuKPzBRF0BL7wRK19bC2+ZExwzm3haHW4Gf1HmalvAkrcusmVChtE/lk5j873kD64eH8YI7vKdFFbrk3hj4WklG1Uzedqe2TsRH4kakj6Z1y1Kt/mz3yj02YnyTdbsgsj8UoqjfAelbjGqw0I8AH97W5mFNx9eIxBvE7Vey3HRv3rCtSesRii6APJghReGJcZHNX9rx+wkM76UrdwVGOEKcdo2Fx0uEGUxXQ06fNJf9QwUqKKATq9Rb8FmMLZWo5NOU/8hD5yGz/eAxbUq0qav4Pta9VeVuZK8MzqNotooC3M9gSm+R4RH5+QX5DTCs/oXyEJUHLeuhoENHXiPsrycnWMmnTzYyQ1LTBE25+JyT+x0OWSYa6wANTwm3MYNNo4V+hIz5YeSFJTBAb1mFyOK2lGqiqlpQExFf8T9yiiNtKhyorOdeNsY89BP+aEU9XxLSfNQh6ddv70n4cXuP+0m/d+TeeyQx6dbmKKSz1vEzGYRJpIElUWJGsa4WrRLrI01lPXRzypmNaMwXfoLQf3ftPbPxFoZL5T9iPh7aUGMZr15GT1tHc6HWuudU5UmmQXZm1oPIOL9kV3P+A3pJdkAe0ZnuRNwUUQ+PLFvLh5sjXlcsmfB5LsqR9DeNNadnz5VEKQtRLO56rdYUP2LinJG+L9NRULVhy6IYbywtDLkyjESawhsyXNj3CLfXfA6GPa+dZK+3uhXte/UJPIO13jq9zdPEaM9uww24JSjrqhCsU4126+vteonxju4LNky4K/wdO33Z42tn0FLKbY4w==</X509Certificate>
            </X509Data>
         </KeyInfo>
      </KeyDescriptor>
      <fed:TokenTypesOffered>
         <fed:TokenType Uri=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1"" />
         <fed:TokenType Uri=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" />
      </fed:TokenTypesOffered>
      <fed:ClaimTypesOffered>
         <auth:ClaimType xmlns:auth=""http://docs.oasis-open.org/wsfed/authorization/200706"" Uri=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"" />
         <auth:ClaimType xmlns:auth=""http://docs.oasis-open.org/wsfed/authorization/200706"" Uri=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"" />
         <auth:ClaimType xmlns:auth=""http://docs.oasis-open.org/wsfed/authorization/200706"" Uri=""http://schemas.microsoft.com/ws/2008/06/identity/claims/role"" />
      </fed:ClaimTypesOffered>
      <fed:PassiveRequestorEndpoint>
         <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:Address>https://website.com/issue/hrd</wsa:Address>
         </wsa:EndpointReference>
      </fed:PassiveRequestorEndpoint>
   </RoleDescriptor>
   <SPSSODescriptor protocolSupportEnumeration=""urn:oasis:names:tc:SAML:2.0:protocol"">
      <KeyDescriptor use=""signing"">
         <KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
            <X509Data>
               <X509Certificate>MIIEyDCCArCgAwIBAgIIHm9sSbojhkYwDQYJKoZIhvcNAQELBQAwEzERMA8GA1UEAwwIYmVxbBNzRtp9Ur1IfpYq5o1IMvxg4kslo5l5nmgMTKYInAi1LzY2IR6z6qqkNXuPYUCT6n1y4xgYBr6v30FZAQ8ZtJgwfuY7At3MVyjzVIqlofUPMHcTWxb3UmZIstZlSIRxw69YH0PQCcsuEiELbIbxOn5j4+NMR80wxHY+xLPmaIXVCJMgjlojx1kAaRwmJL5HbCQpFJXNQ9o2pqWJwIDAQABoxMwETAPBgNVHREECDAGhwR/AAABMA0GCSqGSIb3DQEBCwUAA4ICAQBlcZ7g2+qYDiW8i2Yb9n7ly4xDSygUi87kj6hBQqVy2RXW+H4jR9+1lXTwP/hBt4AyLOQq14lU3t0FFzCzOkll5s6DtJAze++6UQjK4+8MPDv6sSNprnM/eAzPQ42xnAF/fDuz1qyINB2RtO6n1UwcT4ZZ1UamGaoSZXRZ9l+TOabtCVkEUgOUfPl4WLJo+DuigSrey3hmb931cMB1Axyud0I+cDjhUz7ZM0aVvKKc3jAs9Agz56AJUejjOlcZ+SlraSVln8uewLC4ujnLoBncUfgCzDcc6Za7sB7wB/LTlIhsNY6NHhHTD3shhuo0usZF2liQ0QvUty+c9iKnicJ9nyZxLSfNQh6ddv70n4cXuP+0m/d+TeeyQx6dbmKKSz1vEzGYRJpIElUWJGsa4WrRLrI01lPXRzypmNaMwXfoLQf3ftPbPxFoZL5T9iPh7aUGMZr15GT1tHc6HWuudU5UmmQXZm1oPIOL9kV3P+A3pJdkAe0ZnuRNwUUQ+PLFvLh5sjXlcsmfB5LsqR9DeNNadnz5VEKQtRLO56rdYUP2LinJG+L9NRULVhy6IYbywtDLkyjESawhsyXNj3CLfXfA6GPa+dZK+3uhXte/UJPIO13jq9zdPEaM9uww24JSjrqhCsU4126+vteonxju4LNky4K/wdO33Z42tn0FLKbY4w==</X509Certificate>
            </X509Data>
         </KeyInfo>
      </KeyDescriptor>
      <NameIDFormat>urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress</NameIDFormat>
      <AssertionConsumerService Binding=""urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST"" Location=""https://website.com/issue/hrd/saml2callback"" index=""0"" />
   </SPSSODescriptor>
</EntityDescriptor>";

        public static void Run()
        {
            var stringReader = new StringReader(metadata);
            var reader = XmlTextReader.Create(stringReader);
            var serializer = new WsFederationMetadataSerializer();
            var config = serializer.ReadMetadata(reader);
        }
    }
}
