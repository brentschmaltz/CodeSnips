using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Saml2;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Xml;

namespace CodeSnips.BasicCLR
{
    public class ReflectionSnips
    {
        private static readonly Assembly samlAssembly = typeof(Microsoft.IdentityModel.Tokens.Saml2.Saml2Action).Assembly;
        private const BindingFlags getPropertyFlags = BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private const BindingFlags invokeMethodFlags = BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic;

        public static void Run()
        {
            IdentityModelEventSource.ShowPII = true;
            Saml2SecurityTokenHandler saml2SecuritytokenHandler = new Saml2SecurityTokenHandler();
            var symmetricKeyString = "VbbbbmlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";

            // ten days
            var symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(symmetricKeyString));
            var subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "bob@contoso.com"),
                    new Claim(ClaimTypes.Name, "bob"),
                    new Claim(ClaimTypes.GroupSid, "123456789"),
                });

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = "https://relyingParty.com",
                Issuer = "https://identityProvider.com",
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                Subject = subject
            };

            Saml2SecurityToken saml2SecurityToken = saml2SecuritytokenHandler.CreateToken(tokenDescriptor) as Saml2SecurityToken;
            string samltoken = saml2SecuritytokenHandler.WriteToken(saml2SecurityToken);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = "https://relyingParty.com",
                ValidIssuer = "https://identityProvider.com",
                IssuerSigningKey = symmetricSecurityKey
            };

            ClaimsPrincipal claimsPrincipal = saml2SecuritytokenHandler.ValidateToken(samltoken, tokenValidationParameters, out SecurityToken securityToken);


            Saml2SecurityToken validatedSaml2SecurityToken = securityToken as Saml2SecurityToken;
            Saml2Assertion saml2Assertion = validatedSaml2SecurityToken.Assertion;
            object xmlTokenStream = GetProperty(typeof(Saml2Assertion), saml2Assertion, "XmlTokenStream");

            MemoryStream memoryStream = new MemoryStream();
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8);
            Call(xmlTokenStream, "WriteTo", new object[] { writer });

            writer.Flush();
            byte[] bytes = memoryStream.ToArray();
            string samlString = UTF8Encoding.UTF8.GetString(bytes);


            //if (securityToken is Saml2SecurityToken saml2Token && saml2Token.Assertion.XmlTokenStream != null)
            //{
            //    saml2Token.Assertion.XmlTokenStream.WriteTo(writer, null, null);
            //    return true;
            //}

            //return false;
        }

        private static object GetProperty(Type type, object instance, string propertyName)
        {
            return type.InvokeMember(propertyName, getPropertyFlags, null, instance, null);
        }

        public static object Call(object instance, string methodName, params object[] args)
        {
            return instance.GetType().InvokeMember(methodName, invokeMethodFlags, null, instance, args);
        }
    }
}
