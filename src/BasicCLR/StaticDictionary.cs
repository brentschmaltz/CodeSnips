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

using System.Collections.Generic;

namespace CodeSnips.BasicCLR
{

    public class StaticDictionary
    {
        public static IDictionary<string, string> DefaultInboundMap = ClaimTypeMapping.InboundMap;

        private IDictionary<string, string> _inboundMap;

        public StaticDictionary()
        {
            _inboundMap = new Dictionary<string, string>(ClaimTypeMapping.InboundMap);
            _inboundMap = DefaultInboundMap;
        }

        public IDictionary<string, string> InboundMap 
        {
            get { return _inboundMap; }

            set { _inboundMap = value; }
        }
    }

    /// <summary>
    /// Defines the inbound and outbound mapping for claim claim types from jwt to .net claim 
    /// </summary>
    static class ClaimTypeMapping
    {
        private static Dictionary<string, string> _inboundMap = null;

        /// <summary>
        /// Initializes static members of the <see cref="ClaimTypeMapping"/> class. 
        /// </summary>
        static ClaimTypeMapping()
        {
            _inboundMap = new Dictionary<string, string>
            {
                { "oid", "http://schemas.microsoft.com/identity/claims/objectidentifier" },
                { "scp", "http://schemas.microsoft.com/identity/claims/scope" },
                { "tid", "http://schemas.microsoft.com/identity/claims/tenantid" },           
                { "acr", "http://schemas.microsoft.com/claims/authnclassreference" },
                { "adfs1email", "http://schemas.xmlsoap.org/claims/EmailAddress" },
                { "adfs1upn", "http://schemas.xmlsoap.org/claims/UPN" },
            };

        }

        /// <summary>
        /// Gets the InboundClaimTypeMap used by JwtSecurityTokenHandler when producing claims from jwt. 
        /// </summary>
        public static IDictionary<string, string> InboundMap
        {
            get
            {
                return _inboundMap;
            }
        }
    }
}
