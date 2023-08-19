using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, "<some name>"),
    new Claim("aud", "some audience"),
};

var key = new byte[1024];
new Random().NextBytes(key);
var securityKey = new SymmetricSecurityKey(key);
var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
var tokenDescriptorSystem = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(claims),
    Issuer = "some issuer",
    Expires = DateTime.UtcNow.AddMinutes(10),
    SigningCredentials = signingCredentials
};

var tokenHandler = new JwtSecurityTokenHandler();
var token = tokenHandler.CreateToken(tokenDescriptorSystem);
var tokenString = tokenHandler.WriteToken(token);

JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(tokenString);
string json = Base64UrlEncoder.Decode(jwtSecurityToken.EncodedPayload);
Console.WriteLine($"Token: {tokenString}");

