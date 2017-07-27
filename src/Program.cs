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

namespace CodeSnips
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============== Environment ================");
            Console.WriteLine($"Environment.MachineName: {Environment.MachineName}");
            Console.WriteLine($"Environment.UserName: {Environment.UserName}");
            Console.WriteLine($"Environment.WindowsIdentity.User: {WindowsIdentity.GetCurrent().User}");
            Console.WriteLine($"Environment.CommonApplicationData: {Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}");
            Console.WriteLine($"WindowsIdentity.Type.ToString(): {WindowsIdentity.GetCurrent().GetType().ToString()}");
            StringFormat.Run();
            //JsonSnip.Run();
            //GenerateBase64SymmetricKeys.Run();
            //CreateToken.Run();
            //WriteJwtSecurityToken.Run();
            //GetAccessTokenUsingAdal.Run();
            //XmlReaderSnips.Run();
            //GetCertificteSnip.GetCertificate(StoreName.My, StoreLocation.LocalMachine, "CN=SelfHostSts");
            //CompareTwoCerts.Compare(StoreName.My, StoreLocation.LocalMachine, "CN=SelfHostSts", StoreName.My, StoreLocation.CurrentUser, "CN=SelfHostSts");

            Console.WriteLine("\n =====\nPress a key to close");
            Console.ReadKey();
        }
    }
}
