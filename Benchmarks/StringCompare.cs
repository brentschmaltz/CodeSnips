using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers;
using System;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class StringComparer
    {
        static int loops = 10000;
        static long compares = 0;
        static string authorizationHeader = "MSAuth1.0 popToken=\"eyJhbGciOiJSUzI1NiIsImtpZCI6IlJzYVNlY3VyaXR5S2V5MjA0OFByb3ZDc3BfU2luZ2xldG9uIiwidHlwIjoiSldUIiwiY3R5IjoiSldUIn0.eyJhdCI6ImV5SmhiR2NpT2lKU1V6STFOaUlzSW10cFpDSTZJbEp6WVZObFkzVnlhWFI1UzJWNU1qQTBPRkJ5YjNaRGMzQmZVMmx1WjJ4bGRHOXVJaXdpZEhsd0lqb2lTbGRVSWl3aVkzUjVJam9pU2xkVUluMC5leUpoY0hCcFpDSTZJQ0pEUVRBME9VSkZOeTA1UVRsRkxUUXhPVGd0T0VVeE9TMUJRakkxTmtFMU1VRTVPVU1pTENKaGNIQnBaR0ZqY2lJNklDSXlJaXdpWVdOeUlqb2dJaklpTENKaGRXUWlPaUFpYUhSMGNITTZMeTl5Wld4NWFXNW5MbkJoY25SNUxtTnZiUzhpTENKaGJIUnpaV05wWkNJNklDSTFNVE16UlRsRE5TMDBRemd5TFRRNVFVRXRRakpGT0Mwek16VTVORGxET1RaQk5URWlMQ0poZW5CaFkzSWlPaUFpTWlJc0ltVjRjQ0k2SUNJeE5qVTBPRE13TmpjNUlpd2lhV0YwSWpvZ0lqRTJOVFEzTkRReU56a2lMQ0p1WW1ZaU9pQWlNVFkxTkRjME5ESTNPU0lzSW1semN5STZJQ0pvZEhSd2N6b3ZMM04wY3k1dGFXTnliM052Wm5RdWIyNXNhVzVsTG1OdmJTSXNJbTlwWkNJNklDSkdSVFF4T0RCQk15MDBSRGMzTFRRM1FrUXRPRFV5TVMwNE1EUkNSakZHT1VKR01EY2lMQ0pwWkhSNWNDSTZJQ0poY0hBaUxDSmhiWElpT2lBaWNIZGtJaXdpZEdsa0lqb2dJbVZoT0dFME16a3lMVFV4TldVdE5EZ3haaTA0TnpsbExUWTFOekZtWmpKaE9HRXpOaUlzSW5abGNpSTZJQ0l4TGpBaUxDSmpibVlpT25zaWFuZHJJanA3SW10MGVTSTZJbEpUUVNJc0ltNGlPaUl4U2toRk5FOW1jbGhGTkRBNWRUVmtZVXN0VjAwNUxXUTROVXBJWlZrMVR6ZFFjME4xYkRBMWNYUkVObU42WjJsRmVVSXRhMlZhZG5Vek5rRnFkbUpMYVhacFFVZ3dibTl1YTNwV2JEZE5ZbUZQVjFGNVRXOWlaMk0zYVZGalZGTjRabUpPU0RkSFJUbEdNbXRHZVU1V1l6SjZSVzFKUkZsQ2JsTnFVbGQxV0VReGNtcGZXVGx2Y0ZodmIzQXpaV3RyU2pCR1p6ZHdaRlZHVDNBdFNGWkxjakYzZW1obGVYZEdVMjEyUjIxcmVYQnljMHhvYVhwSU5WUkhPREZwY1VGUlVFUTFZVFp4TjJwbFZYSXpOMGhrWWpkek5VcFFiV3RRZVdGQ2VtMDFXREkyU1UxcVYyMXRVMkpYYmpaMVdFaEthVzlQYkdreWRIVkRabGMwY0ZKWlNXOWxaM0poUzFWaGJGWXpTbFpYTVRFM1oyaE1VMVZmYmt4UVRFOU5jbFJoZFRGbVJFaEVZVWhrUmpSMlpVVlpPVVpUZDNGMlZGOVFlSHBmUlhSbGVucG9aMHhGYzNGQ01HSk1kbFZzTkhsTWVVNDJiVkVpTENKbElqb2lRVkZCUWlJc0ltRnNaeUk2SWxKVE1qVTJJaXdpYTJsa0lqb2lVbk5oVTJWamRYSnBkSGxMWlhreU1EUTRVSEp2ZGtOemNGOVRhVzVuYkdWMGIyNGlmWDE5LlNKdHpqUVlHVHpqR0dla184bEJSSVhEcXVfQ2VJQVlKZUJlM3JJSFBFX1NWQUJNV2c1YlBZdGJnZWFKanF2S2ZvZHlQbGZ1b2Z0N20tZWJsZEZrNEpoTC0xVXhCZFM2azVpOWNhdkM1U0ZReWRYY3ZwdkZQd3BoSVljbDBKX3NoV05oZW5xR1lPNnZvcnRrZndQeFlnQm5oWVdUQ0FNWU5XOFdDQXZoX0VOWldlelY4czU4TXBSVXBYX3hTQUQ2YXpPYkg5OGFabEpkblIteDE4VVhvTWdCWTJTUjEweC1xN2hmSENadXBzdFA3TU5hZTFSeTdzZ0djRlZ2aTUyN1dGaEo3WE93aDZheVhtWmxaTHRmMmZPWEVLMDF0SEdxUW90cWU4VkVBaUxTb0NXRndMWTdfMlA4MnRFTWZIUU5CS3hScF9BeGFWbW14ekNLanJNTGJudyIsInRzIjoxNjU0NzQ0Mjg3LCJtIjoiR0VUIiwicCNTMjU2IjoiaWw3YXNvSmpKRU1obmdVZVN0NHRIVnU4Wnh4NEVGR19GRGVKZkwzLW9QRSIsInEjUzI1NiI6IjQ3REVRcGo4SEJTYS1fVEltVy01SkNldVFlUmttNU5NcEpXWkczaFN1RlUiLCJwZnQiOiJleUpoYkdjaU9pSlNVekkxTmlJc0ltdHBaQ0k2SWxKellWTmxZM1Z5YVhSNVMyVjVNakEwT0ZCeWIzWkRjM0JmVTJsdVoyeGxkRzl1SWl3aWRIbHdJam9pU2xkVUlpd2libTl1WTJVaU9pSmhTR1p4TlRKTWRFTjVjRXd4TUVGT1VIbHpabm81YTBWTGRIUTRVa014ZGs4M01UUkZOblZQZUhOSkluMC5leUpoY0hCcFpDSTZJQ0pEUVRBME9VSkZOeTA1UVRsRkxUUXhPVGd0T0VVeE9TMUJRakkxTmtFMU1VRTVPVU1pTENKaGNIQnBaR0ZqY2lJNklDSXlJaXdpWVdOeUlqb2dJaklpTENKaGRXUWlPaUFpYUhSMGNITTZMeTl5Wld4NWFXNW5MbkJoY25SNUxtTnZiUzhpTENKaGJIUnpaV05wWkNJNklDSTFNVE16UlRsRE5TMDBRemd5TFRRNVFVRXRRakpGT0Mwek16VTVORGxET1RaQk5URWlMQ0poZW5CaFkzSWlPaUFpTWlJc0ltVjRjQ0k2SUNJeE5qVTBPRE13TmpnM0lpd2lhV0YwSWpvZ0lqRTJOVFEzTkRReU9EY2lMQ0p1WW1ZaU9pQWlNVFkxTkRjME5ESTROeUlzSW1semN5STZJQ0pvZEhSd2N6b3ZMM04wY3k1dGFXTnliM052Wm5RdWIyNXNhVzVsTG1OdmJTSXNJbTlwWkNJNklDSkdSVFF4T0RCQk15MDBSRGMzTFRRM1FrUXRPRFV5TVMwNE1EUkNSakZHT1VKR01EY2lMQ0pwWkhSNWNDSTZJQ0poY0hBaUxDSmhiWElpT2lBaWNIZGtJaXdpZEdsa0lqb2dJbVZoT0dFME16a3lMVFV4TldVdE5EZ3haaTA0TnpsbExUWTFOekZtWmpKaE9HRXpOaUlzSW5abGNpSTZJQ0l4TGpBaWZRLnFZN3RXaC1rV0cwU09VYUpDcG9NU25oTWxZbjlfck1UMHJKQ18tQjJsd3lkVDNSLXNtQVVxaWh1bEttdXMzLXdHX2Q4MzY4RUxSZU5acGRPRWRtWlc4bk1zSlRhLXpuYW1iQjV6VUVWSURJckRkRkJpUzRJRXJ1SndyZXVIS0xLeE05UmU1ODh6c1hjVHExeDNWdFhUYndOOXZpVDNVc2E2RzNYa1ZJNVNIRnNfcGxLcDVNT1hGUlNLbTJMdW00RGZDMk1IRDhvcVhBVUVWcU44bFU3U2IxVmdqSmU1XzFodDdnc2hEejNBZHMzN01OeVNPOXZWZjZmbVB0S3FCNFAwaGdSRkI4VHlsUlJhWnh4NkwxYWhFODRSM2FpRzVjZG1aMDNtOXNPVDg5eDdfcGczTXBCZDc0REpQd2RKcmdyMFlsVWlVQ1AxaU56c2tCREhDNk5FdyJ9.eq6EWVBQ2u0hZ7HanaSrSAxpfh1A8mwltm_By0Iw87wvxYmwl-ECRUQqkmt_q_WOyJC4nWrQYOQlM40E4AaAncWgSTY3DSpp0MN7VXzQBxBq2c3tt_Xokt-rFzw1j6Iuk6W3ypQi-sl95Qgetd8DSPOUdvwd2z9Sr735jqvIbVMdt-aR38y4TlPahNUuOqnXnv8Hjn3w1_aCBeH9zK52NzIOdk3Z8kCWF7ONAEZWD6GoG1lNSUtyYDbgDvpuoDUi2i8Imf4j1Mg9ZPAMKUqm9dzEFubPzWtjHG1KIy8D-M3DmDNrlaoPcYbJYi-na16zAN2hs8_iXG2Wub7addwGfg\", type=\"AT_POP\"";
        static string expectedProtocol = "MSAuth1.0";
        
        [Benchmark]
        public void CompareIgnoreCase()
        {
            for (int i = 0; i < loops; i++)
            {
                StringCompareIgnoreCase(authorizationHeader, expectedProtocol);
                    compares++;
            }
        }
        private bool StringCompareIgnoreCase(string header, string protocol)
        {
            return string.Compare(header, 0, protocol, 0, protocol.Length, StringComparison.OrdinalIgnoreCase) == 0;
        }

        [Benchmark]
        public void CompareBool()
        {
            for (int i = 0; i < loops; i++)
            {
                StringCompareBool(authorizationHeader, expectedProtocol);
                compares++;
            }
        }
        private bool StringCompareBool(string header, string protocol)
        {
            return string.Compare(header, 0, protocol, 0, protocol.Length, true) == 0;
        }


        [Benchmark]
        public void Compare()
        {
            for (int i = 0; i < loops; i++)
            {
                if (StringNoCase(authorizationHeader, expectedProtocol))
                    compares++;
            }
        }

        private bool StringNoCase(string header, string protocol)
        {
            return string.Compare(header, 0, protocol, 0, protocol.Length) == 0;
        }

        [Benchmark]
        public void StartsWith()
        {
            for (int i = 0; i < loops; i++)
            {
                if (StartsWith(authorizationHeader, expectedProtocol))
                    compares++;
            }
        }

        private bool StartsWith(string header, string protocol)
        {
            return header.StartsWith(protocol);
        }
    }
}


//|        Method |     Mean |   Error |  StdDev | Allocated |
//|-------------- |---------:|--------:|--------:|----------:|
//|       Compare | 145.5 us | 2.89 us | 6.75 us |         - |
//| CompareNoCase | 537.4 us | 7.40 us | 6.92 us |         - |
//|    StartsWith | 435.1 us | 7.11 us | 6.65 us |         - |

//|        Method |     Mean |   Error |  StdDev | Allocated |
//|-------------- |---------:|--------:|--------:|----------:|
//|       Compare | 142.2 us | 2.74 us | 4.72 us |         - |
//| CompareNoCase | 542.7 us | 4.57 us | 4.05 us |         - |
//|    StartsWith | 440.2 us | 6.67 us | 6.24 us |         - |