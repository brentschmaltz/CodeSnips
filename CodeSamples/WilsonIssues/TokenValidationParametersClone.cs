using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace CodeSnips.WilsonIssues
{
    public class TokenValidationParametersClone
    {
        public static void Run()
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();
//            tokenValidationParameters.PropertyBag = new Dictionary<string, object>();
//            tokenValidationParameters.PropertyBag.Add("objectInOrginalTVP", "OrginalTVP");
            TokenValidationParameters tokenValidationParametersCloned = tokenValidationParameters.Clone();
            tokenValidationParametersCloned.PropertyBag = new Dictionary<string, object>();
            tokenValidationParametersCloned.PropertyBag.Add("objectInClonedTVP", "ClonedTVP");
            TokenValidationParameters tokenValidationParametersCloned2 = tokenValidationParameters.Clone();
            tokenValidationParametersCloned2.PropertyBag.Add("objectInClonedTVP2", "ClonedTVP2");
        }
    }
}
