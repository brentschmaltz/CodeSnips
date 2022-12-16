//------------------------------------------------------------------------------
//
// Copyright (c) Brent Schmaltz
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
using System.Runtime.InteropServices;

namespace CodeSnips
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct SEC_TOKEN_BINDING
    {
        public short keyCount;
        public byte* keyParameters;
        public byte majorVersion;
        public byte minorVersion;
    }

    class Marshaling
    {
        public unsafe static void Run()
        {
            var secTokenBinding = new SEC_TOKEN_BINDING();
            var keyParameters = new byte[3];
            fixed (byte* keyBytes = keyParameters)
            {
                *keyBytes = 1;
                *(keyBytes + 1) = 2;
                *(keyBytes + 2) = 3;

                secTokenBinding.keyParameters = keyBytes;

                secTokenBinding.keyCount = 3;
                secTokenBinding.majorVersion = 4;
                secTokenBinding.minorVersion = 5;

                int size = sizeof(byte) + sizeof(byte) + sizeof(short) + sizeof(byte) * secTokenBinding.keyCount;
                var data = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);
                IntPtr keyBytesPtr = Marshal.AllocHGlobal(secTokenBinding.keyCount);

                Marshal.WriteInt16(ptr, secTokenBinding.keyCount);
                Marshal.WriteByte(ptr, 2, secTokenBinding.keyParameters[0]);
                Marshal.WriteByte(ptr, 3, secTokenBinding.keyParameters[1]);
                Marshal.WriteByte(ptr, 4, secTokenBinding.keyParameters[2]);
                Marshal.WriteByte(ptr, 5, secTokenBinding.majorVersion);
                Marshal.WriteByte(ptr, 6, secTokenBinding.minorVersion);
                Marshal.Copy(ptr, data, 0, size);
                Marshal.Copy(keyParameters, 0, keyBytesPtr, secTokenBinding.keyCount);
            }
        }

    }
}
