using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;

namespace CodeSnips.WilsonIssues
{
    public class WilsonIssue1601
    {
        public static void Run()
        {
            ConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>("https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConnectConfiguration = configurationManager.GetConfigurationAsync().Result;
            string jsonFromSerialize = OpenIdConnectConfiguration.Write(openIdConnectConfiguration);
            string jsonFromConvert = JsonConvert.SerializeObject(openIdConnectConfiguration);
        }
    }
}
