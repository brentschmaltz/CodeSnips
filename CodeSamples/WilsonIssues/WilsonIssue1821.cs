using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace CodeSnips.WilsonIssues
{
    public class WilsonIssue1821
    {
        public static void Run()
        {
            JwtSecurityToken jwtSecurityTokenDateTimeOffset = new JwtSecurityToken(expires: DateTimeOffset.UtcNow.DateTime.AddMinutes(15));
            JwtSecurityToken jwtSecurityTokenDateTimeOffsetUniversalTime = new JwtSecurityToken(expires: DateTimeOffset.UtcNow.DateTime.ToUniversalTime().AddMinutes(15));
            JwtSecurityToken jwtSecurityTokenDateTime = new JwtSecurityToken(expires: DateTime.UtcNow.AddMinutes(15));

            DateTime dateTimeOffset = DateTimeOffset.UtcNow.DateTime.AddMinutes(15);
            DateTime dateTimeOffsetLocaltime = DateTimeOffset.Now.DateTime.AddMinutes(15);
            DateTime dateTimeOffsetUniversalTime = DateTimeOffset.UtcNow.DateTime.ToUniversalTime().AddMinutes(15);
            DateTime dateTimeUtc = DateTime.UtcNow.AddMinutes(15);
            DateTime dateTimeLocaltime = DateTime.Now.AddMinutes(15);

            long epochTimeOffset = EpochTime.GetIntDate(dateTimeOffset);
            long epochTimeOffsetLocaltime = EpochTime.GetIntDate(dateTimeOffsetLocaltime);
            long epochTimeOffsetUniversalTime = EpochTime.GetIntDate(dateTimeOffsetUniversalTime);
            long epochTimeUtc = EpochTime.GetIntDate(dateTimeUtc);
            long epochTimeLocalTime = EpochTime.GetIntDate(dateTimeLocaltime);
        }
    }
}
