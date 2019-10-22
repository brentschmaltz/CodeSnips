//------------------------------------------------------------------------------
//
// Copyright (c) AuthFactors.com.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using CodeSnips.Certificates;
using CodeSnips.Perf;
using CodeSnips.Xml;
using CodeSnips.BasicCLR;

namespace CodeSnips
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("=============== Environment ================");
            Console.WriteLine("");
            Console.WriteLine($"Environment.MachineName: {Environment.MachineName}");
            Console.WriteLine($"Environment.UserName: {Environment.UserName}");
            Console.WriteLine($"Environment.WindowsIdentity.User: {WindowsIdentity.GetCurrent().User}");
            Console.WriteLine($"Environment.CommonApplicationData: {Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}");
            Console.WriteLine($"WindowsIdentity.Type.ToString(): {WindowsIdentity.GetCurrent().GetType().ToString()}");
            Console.WriteLine("");
            Console.WriteLine("=============== Environment ================");
            Console.WriteLine("");
            //StringFormat.Run();
            //JsonSnip.Run();
            //GenerateBase64SymmetricKeys.Run();
            //CreateToken.Run();
            //WriteJwtSecurityToken.Run();
            //GetAccessTokenUsingAdal.Run();
            //XmlReaderSnips.Run();
            //GetCertificteSnip.GetCertificate(StoreName.My, StoreLocation.LocalMachine, "CN=SelfHostSts");
            //CompareTwoCerts.Compare(StoreName.My, StoreLocation.LocalMachine, "CN=SelfHostSts", StoreName.My, StoreLocation.CurrentUser, "CN=SelfHostSts");
            //ReadTokenPerf.Run();
            //ReadTokenPerf.Run();
            //ReadTokenPerf.Run();
            //ReadTokenPerf.Run();
            //ReadTokenPerf.Run();
            //InMemoryPrivateCert.Run();

            //string jws = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiJodHRwOi8vUzJTQmFja2VuZCIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2FkZDI5NDg5LTcyNjktNDFmNC04ODQxLWI2M2M5NTU2NDQyMC8iLCJpYXQiOjE0Njc0OTczMzAsIm5iZiI6MTQ2NzQ5NzMzMCwiZXhwIjoxNDY3NTAxMjMwLCJhcHBpZCI6IjJkMTQ5OTE3LTEyM2QtNGJhMy04Nzc0LTMyN2I4NzVmNTU0MCIsImFwcGlkYWNyIjoiMiIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2FkZDI5NDg5LTcyNjktNDFmNC04ODQxLWI2M2M5NTU2NDQyMC8iLCJvaWQiOiI5MTkxOTZmNi0zOGZkLTQ2ZjMtODY4Ni1hNDllMjk0NGIyNzciLCJzdWIiOiI5MTkxOTZmNi0zOGZkLTQ2ZjMtODY4Ni1hNDllMjk0NGIyNzciLCJ0aWQiOiJhZGQyOTQ4OS03MjY5LTQxZjQtODg0MS1iNjNjOTU1NjQ0MjAiLCJ2ZXIiOiIxLjAifQ.QLOroZZY53Gj97VuI2X66dxZ6vDIfJlDBwsDTAMJR8FcugucpWTyMtkCm9JcOHOb78lBwaMTJlOwUcb7qrwRrtjkxGCI3hUw-LBPREqM-AowlrUk1ORvB4CV7zDqH6m6s0LL91I3JpQEhMsQxo1OfcYyDR-vKJ5ybprYUgMIKmPeqGbUMLYDCwO9-0efl3LCdyI3FRlcbDg1960z2OlgmbFSlpQiT4bDDHszx1W0G0mJjO8Ypkfh3z_aBBoclkSR34lV_htJlCcW0CM7dopOzHACljCiJWgDh_q5pULLIWeGnYFKLtJZR7wSKp18a-k28xT_S1fgMqFooZ0r-5i3kA.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9";
            //string jwe = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiJodHRwOi8vUzJTQmFja2VuZCIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2FkZDI5NDg5LTcyNjktNDFmNC04ODQxLWI2M2M5NTU2NDQyMC8iLCJpYXQiOjE0Njc0OTczMzAsIm5iZiI6MTQ2NzQ5NzMzMCwiZXhwIjoxNDY3NTAxMjMwLCJhcHBpZCI6IjJkMTQ5OTE3LTEyM2QtNGJhMy04Nzc0LTMyN2I4NzVmNTU0MCIsImFwcGlkYWNyIjoiMiIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2FkZDI5NDg5LTcyNjktNDFmNC04ODQxLWI2M2M5NTU2NDQyMC8iLCJvaWQiOiI5MTkxOTZmNi0zOGZkLTQ2ZjMtODY4Ni1hNDllMjk0NGIyNzciLCJzdWIiOiI5MTkxOTZmNi0zOGZkLTQ2ZjMtODY4Ni1hNDllMjk0NGIyNzciLCJ0aWQiOiJhZGQyOTQ4OS03MjY5LTQxZjQtODg0MS1iNjNjOTU1NjQ0MjAiLCJ2ZXIiOiIxLjAifQ.QLOroZZY53Gj97VuI2X66dxZ6vDIfJlDBwsDTAMJR8FcugucpWTyMtkCm9JcOHOb78lBwaMTJlOwUcb7qrwRrtjkxGCI3hUw-LBPREqM-AowlrUk1ORvB4CV7zDqH6m6s0LL91I3JpQEhMsQxo1OfcYyDR-vKJ5ybprYUgMIKmPeqGbUMLYDCwO9-0efl3LCdyI3FRlcbDg1960z2OlgmbFSlpQiT4bDDHszx1W0G0mJjO8Ypkfh3z_aBBoclkSR34lV_htJlCcW0CM7dopOzHACljCiJWgDh_q5pULLIWeGnYFKLtJZR7wSKp18a-k28xT_S1fgMqFooZ0r-5i3kA";

            //CanReadJwt.Run(jws, "JWS");
            //CanReadJwt.Run(jwe, "JWE");
            //CanReadJwt.Run(jwe, "JWE");
            //CanReadJwt.Run(jws, "JWS");
            //CanReadJwt.Run(jwe, "JWE");
            //CanReadJwt.Run(jws, "JWS");

            BasicCLR.Enum.Run();

            //WsFed.Run();
            Console.WriteLine("");
            Console.WriteLine("===================================");
            Console.WriteLine("Press a key to close");
            Console.ReadKey();
        }
    }
}
