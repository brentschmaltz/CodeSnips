using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnips.BasicCLR
{
    public class ObjectEquals
    {
        public static void Run()
        {
            var jwt = new JwtSecurityToken();
            string str = "string";
            var other = (object)str as JwtSecurityToken;
            var equals = str.Equals(other);
            var refEquals = ReferenceEquals(str, other);
        }
    }
}
