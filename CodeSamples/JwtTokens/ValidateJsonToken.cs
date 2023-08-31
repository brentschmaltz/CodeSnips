﻿using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CodeSnips.JwtTokens
{
    public class ValidateJsonToken
    {
        public static void Run()
        {
            var handler = new JsonWebTokenHandler();
            var token = handler.ReadJsonWebToken("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhIjp7InByb3AxIjpbInZh" +
                "bHVlMSIsInZhbHVlMiJdfSwiZXhwIjoxNjkyNzA2ODAzLCJpYXQiOjE2OTI3MDMyMDMsIm5iZiI6MTY5MjcwMzIwM30.dZ" +
                "lSC8OLOKUvSCXu9uZFb91NQwWBJ7dwSxEn-WA2RVEBW1l3Wu2wdobOuezivtzX9OWpgYyvzog4nHfD-Yco4Ou5qYqN0JbB" +
                "fU7gESA95iDtDhbDzVB8T3W3T7k3a1LHwVNZbBGTnZQMgF447R-euIlMcGY6p5WQFLyFPZjBOKW2nlwh0MYQQ-5mZpEgzE" +
                "pdcHVOjdO0pV1EJTKFjHuiUuFsI6d6i3dLOQYX2JpaclT--pTFqs4Nx1O8gn9bKbfKTpGlpy2CpJJLmmzKqsLWtrXL0eSD" +
                "QQBK0ynvd6ssRARH_L3f9Hf05oBRqiqO3ukWw8qAoBE-ZF6906gYnwMcYA");

            if (token.TryGetPayloadValue("a", out Dictionary<string, string[]> dictionary))
            {
                Console.WriteLine("The dictionary has been correctly deserialized.");
            }

            string p = @"{""a"": {""prop1"": [ ""value1"", ""value2"" ] }, ""exp"": 1692706803,  ""iat"": 1692703203,  ""nbf"": 1692703203}";
            JsonWebToken jwt1 = new JsonWebToken("{}", p);
            jwt1.TryGetPayloadValue("a", out Dictionary<string, string> dictionary2);


            string jwt3 = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNkZDV6Z0ZDY2VpZ0gya28wLUh1Y1EzNXptVnBST2kwQVpfQXV2Ql9wSTQiLCJ0eXAiOiJKV1QifQ.eyJhdCI6ImV5SjBlWEFpT2lKS1YxUWlMQ0poYkdjaU9pSlNVekkxTmlJc0luZzFkQ0k2SW1sa1l6UnlZVzVaTlhKR1ZTMHdka0ZtV0hSa1FsVlBhMmRCVFNJc0ltdHBaQ0k2SW1sa1l6UnlZVzVaTlhKR1ZTMHdka0ZtV0hSa1FsVlBhMmRCVFNKOS5leUpoZFdRaU9pSm9kSFJ3Y3pvdkwyOW1abWxqWldGd2NITXViR2wyWlM1amIyMGlMQ0pwYzNNaU9pSm9kSFJ3Y3pvdkwzTjBjeTUzYVc1a2IzZHpMWEJ3WlM1dVpYUXZabU00T0daa016a3RZV1UzWmkwME5EazJMV0kyTUdVdE5HWXlNbU00T1dJM05qa3pMeUlzSW1saGRDSTZNVFkzTlRNM05UQTFNaXdpYm1KbUlqb3hOamMxTXpjMU1EVXlMQ0psZUhBaU9qRTJOelUwTmpFM05USXNJbUZwYnlJNklrVXlUbWRaU21obVNXSjZMekZ5U1ROd05YUk1SWHBwZFZkcFdUTkJVVUU5SWl3aVlYQndhV1FpT2lJNVpUUmhOVFEwTWkxaE5XTTVMVFJtTm1ZdFlqQXpaaTAxWWpsbVkyRmhaakkwWWpFaUxDSmhjSEJwWkdGamNpSTZJaklpTENKamJtWWlPbnNpYW5kcklqcDdJbXQwZVNJNklsSlRRU0lzSW00aU9pSnlaV1IwWTNsWFRVUlVWbms1YkZveVoycHZNMUJQWWt4dmFXNU1SR3RCYnkxMWQyVlVTMUZMYVRab09HNTZWR1JNYlVJMmIwbDRNRnBIZUZCamRsRkdNVkY2YzNGNFRqbExRVXB4TTNsTlVtRjJYMUpNWTJwUFVHWXRUMmRrUnpGSGFGTXpVMWhLVUdsdGNFUXdRemh0YmpGMlpHRnFjeTFzYUdGUE1VOWthSHAyYkhOVFp6bHBja2REVkZwelUzSk5aWFpmUm5wYU4wdExPRWRJVm1KT1YwdGZVVlpLYlY5RmVrZzNlRnBFWlRkc1QwNUtSVVV4WWxCV1lpMHlkRkUwTFVSTldISmZhRmROTUdFNE9GTm1kSGRKUkRoSExVNVlSVXRHTms5dWFYQlhXVEpxVEcxelpHWlNYekpZVjBZeWEzZHZXa3RaVUhJMk1HMTJkRlI1VkZCMVFVMXBNSE5zVG1jMFVuVXdRMEpXYVdabkxVZElUVk4zY0dGb2VuaDVNVFEwZDBKdFJUQlZNMjQ0ZEZGNFREVjJOMDl2UWpkUFYyVlplV2RYUVdneFRrcGFiV05GTTB0NlpqSk9hVFpLZURacU1WRWlMQ0psSWpvaVFWRkJRaUlzSW1Gc1p5STZJbEpUTWpVMklpd2lhMmxrSWpvaVUyUmtOWHBuUmtOalpXbG5TREpyYnpBdFNIVmpVVE0xZW0xV2NGSlBhVEJCV2w5QmRYWkNYM0JKTkNKOWZTd2lhV1J3SWpvaWFIUjBjSE02THk5emRITXVkMmx1Wkc5M2N5MXdjR1V1Ym1WMEwyWmpPRGhtWkRNNUxXRmxOMll0TkRRNU5pMWlOakJsTFRSbU1qSmpPRGxpTnpZNU15OGlMQ0p2YVdRaU9pSmpPVFZrTldNNU1pMDBaVGd6TFRRM1ltWXRZVEZpWmkxak5qUmxOR0k0WW1ReVpqZ2lMQ0p5YUNJNklqQXVRVUZCUVU5bU1rbGZTQzExYkd0VE1rUnJPR2w1U25ReWF6bFRUbUZST0ZJNFEwNU9iM28yZWxwQ1ltTnpaVmxDUVVGQkxpSXNJbk4xWWlJNkltTTVOV1ExWXpreUxUUmxPRE10TkRkaVppMWhNV0ptTFdNMk5HVTBZamhpWkRKbU9DSXNJblJwWkNJNkltWmpPRGhtWkRNNUxXRmxOMll0TkRRNU5pMWlOakJsTFRSbU1qSmpPRGxpTnpZNU15SXNJblYwYVNJNklsVndPRFZQVVVaaGJrVlRiMVpzZDJWbE1GbERRVUVpTENKMlpYSWlPaUl4TGpBaWZRLld6MHFrMURpa0FPQTNRYnhVcXZsNkVLYzM1bGI3dXBhamZFaTYyLWFKLWFkZkdNOWNlRVJtQThvYUFkZERtMzhwVU11ZGxSYjRZS293bkNlSXl5enFvdXZVZmQ4aC1wVnJWbXlzeGRVSGxzaEkxZEIzRVBEUU5FQ3NEN1J2VFp6LVZFYWM0ek1NT3FrSkxjdmNGd1NuZXpKRWVlZHlLaE9QYmxwLUdGYVN3TmdmUVR1d0FtU0Y0MThXNGltMHNxRGxzT284NHROZVJLZDIzWlJaNWVVNTFWUUxVTVNtdFdoQUhHTjR3UFNSeWFMWHRBd0taY1huLWlNTmVQdFA2TGNBZEJEN09EVUdqRC1mYmJlZWpjeGdHWXNpZzg2VWZnczBWWE5JejhDZjdFT3RuS2gtTkJtVTlXOUE1M0JPdkZLZlk2TkVGX0hLNlhnaXJBcVF0czZTQSIsInRzIjoxNjc1Mzc1MzY5LCJtIjoiUE9TVCIsInAjUzI1NiI6IjQtNmNIaDdVcUVicnJLTy1VNV9DeGo3MjJxb3JXb2RlaTdyLVRxSDlUaFkiLCJxI1MyNTYiOiI0N0RFUXBqOEhCU2EtX1RJbVctNUpDZXVRZVJrbTVOTXBKV1pHM2hTdUZVIiwiYWF0IjoiZXlKaGJHY2lPaUp1YjI1bElpd2lkSGx3SWpvaVNsZFVJbjAuZXlKcFlYUWlPakUyTnpVek56VXdOVElzSW1Gd2NHbGtJam9pT1dVMFlUVTBOREl0WVRWak9TMDBaalptTFdJd00yWXROV0k1Wm1OaFlXWXlOR0l4SWl3aWFYTmpiMjV6ZFcxbGNpSTZJa1poYkhObElpd2lkbVZ5SWpvaVlYQndYMkZ6YzJWeWRHVmtYM1Z6WlhKZmJYTnpaWEoyYVdObFgzWXhJaXdpYzJOd0lqb2lMbVJsWm1GMWJIUWlMQ0p3ZFdsa0lqb2lNVEF3TXpkR1JrVTRRME16TVRNMU5pSXNJblZ3YmlJNklrZDFaWE4wUUZBeVVHRnBaRk5CVXpJdVkyTnpZM1J3TG01bGRDSXNJblJwWkNJNkltWmpPRGhtWkRNNUxXRmxOMll0TkRRNU5pMWlOakJsTFRSbU1qSmpPRGxpTnpZNU15SXNJbTVpWmlJNk1UWTNOVE0zTlRBMU1pd2laWGh3SWpveE5qYzFORFl4TnpVeUxDSnBjM01pT2lJNVpUUmhOVFEwTWkxaE5XTTVMVFJtTm1ZdFlqQXpaaTAxWWpsbVkyRmhaakkwWWpGQVpqWTRObVEwTWpZdE9HUXhOaTAwTW1SaUxUZ3hZamN0WVdJMU56aGxNVEV3WTJOa0lpd2lZWFZrSWpvaWFIUjBjSE02THk5dlptWnBZMlZoY0hCekxteHBkbVV1WTI5dEluMC4ifQ.ma4jocqVM8oPx3mMjUhYcxXXYCtWEyZ0dhdnIrVOFywyqBMddFbNR_K7y0YUhZdr2tfSIpg1eWICJRZ32CUA--H19mipQbqFgCZ5aZ-XuBoko-RFCMkL-0jwaiE8u7n7zfFjzmWHuj5JuzKaWZzaG2dVZlaynzOehUSYIESHCPivwinStaR85f-86tavI-MNDPTNk4pICi5Mh7gaLzkRYH_gNW8gqQQzTgFeAtzKwQIpC-gA8kX9bfX-Vnonv51i4-MF1IEvollRWxv-dSvn_ktLyZp0_ejKYuxUkX2UrRtGpoDojI1T20ipGxRsibMXob9Bh5YhHl1Db85DilAh7w";
            string jwt = "eyJ0eXAiOiJKV1QiLCJub25jZSI6ImdJSFE1T09SbldNVWh3VjFPOGdZcDBxWUxXM3FvMnVjRU94YmlFckdQLUUiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDcvIiwiaWF0IjoxNjA0NTczNTk5LCJuYmYiOjE2MDQ1NzM1OTksImV4cCI6MTYwNDU3NzQ5OSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFVUUF1LzhSQUFBQXcweDZ6THdDUER0YVRpNGpZZGJDb2RCRDdIMzVvcWkwUkt3Q3BwOWdFeUVRWUk0aExoeUpwdkFNdUpGZ2pJYkJEaWNyTUVOeURaZEc3ME4vZjBBTnRnPT0iLCJhbXIiOlsid2lhIiwibWZhIl0sImFwcF9kaXNwbGF5bmFtZSI6IkNDRiBEZW1vIEFwcCIsImFwcGlkIjoiMTc3MzIxNGYtNzJiOC00OGY5LWFlMTgtODFlMzBmYWIwNGRiIiwiYXBwaWRhY3IiOiIwIiwiZGV2aWNlaWQiOiIwMGE0YmVlOS1kMTQ5LTRlOWYtYmVjMi02MmMxY2EwMWUwNDciLCJmYW1pbHlfbmFtZSI6IlJpZWNoZXJ0IiwiZ2l2ZW5fbmFtZSI6Ik1haWsiLCJpZHR5cCI6InVzZXIiLCJpbl9jb3JwIjoidHJ1ZSIsImlwYWRkciI6Ijc4LjE0Ny4yMzAuMTg5IiwibmFtZSI6Ik1haWsgUmllY2hlcnQiLCJvaWQiOiJiY2VkOTJmZS03YzIwLTQ1NmUtOWFmZC01YjE4YzM4M2RlODEiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMTcyMTI1NDc2My00NjI2OTU4MDYtMTUzODg4MjI4MS0zOTI4MjQxIiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDNCRkZEOUMxMTBFMjEiLCJyaCI6IjAuQVJvQXY0ajVjdkdHcjBHUnF5MTgwQkhiUjA4aGN4ZTRjdmxJcmhpQjR3LXJCTnNhQUtzLiIsInNjcCI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIiwic2lnbmluX3N0YXRlIjpbImR2Y19tbmdkIiwiZHZjX2NtcCIsImttc2kiXSwic3ViIjoiTlJkOVpTNWpBYzlVZ2pTTnNNd0FaY2J4N2tUcUJMa1RkRl9vVUxZYkpMVSIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJXVyIsInRpZCI6IjcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0NyIsInVuaXF1ZV9uYW1lIjoibWFyaWVjaGVAbWljcm9zb2Z0LmNvbSIsInVwbiI6Im1hcmllY2hlQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiI3eWdQcWNHOWMwbTktQXp4WURndEFRIiwidmVyIjoiMS4wIiwid2lkcyI6WyJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6IlVHek9BVklxSkI3QjNfbHY2V0k3akp0eUpFTFVlbGJ2OXNxT0dtQTNjTGcifSwieG1zX3RjZHQiOjEyODkyNDE1NDd9.W2HWWU81GLob3ivVj7yDyf2KotfRwzRA3AM3bpZqLwiODCVL7_opd4zuItSAqOzvOIe9qDa8kIb4soVw08xY4UUxOdKuUE2eZenGjvDsaxKLvAfu7r3TJ8iPXCLOM3qobCnMvJrMgcemcX8esPR4o2pwE5sJERhe3xE0Amm2B6bSDRrM-tH5emWPi8xnb9z8-oRJWKr2aqft-1Yb9sVPYavYoNwzH0K_Cc47IhiGYq_OMOojguHP21g1WJ7MBCv-RgH3tsLFJuTsd6Yf8TZF6-Wb_mDMNSJVAKc_1RzdDlL1U1qq9u40ouelRPsbjLhxPVsab8eeCEIY5Ojd2o5MTQ";
            string jwt2 = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkExMjhDQkMtSFMyNTYiLCJ4NXQiOiJ5UkRDN0FROGwyTnQzT0FTM3pfdGZocF9adkkiLCJ6aXAiOiJERUYifQ.wH8KCHy-z7oXicJaqj5eLCmq0YCBW6k0fiknfXZc1zjhXL1IwRBRXy9-G24ECUKS6e2s3xj5223gmaIDhukkGyL6ywDgXLqSiz-nHjiX9Vs7cC8q0rLJUldAAMjbEq5AI72evWRZMpDV5YsDbEsI_tsLf4NfX7-rerLcTrHpTbOS5q1A8crkiyTcOSyy9ZEYRULm0AS4LHest-PL8DTUx0z1hUZI-ul3DlKAYFsFPlmmwGVbbi3RxRGSquYH2utzmhznISpfuHZ2dBefASET7H8kMeygyxsHoQDVcK-P-DkHMPTUhwMVY75J-O38w_7hDLwW66T4K3nqTGYFumj2BQ.liGRpGF-x21lzf18kNEatw.fOeW-IeM0A19wa00yT1UPn-BEdbojJhiApcPd2CEltFptPztErUTXzsZlgp4L6StvwPTqB_FbyLnmi_kjwDAGOazIPeKvUlYECA7-2OEsvZOqtbpJHqMbJWMLBP4xavwno2Qinf2eXfv-wxBhLMLpoFLZRkGxG7icF17XbBj7Jn-iPPZtDXyRYp8FT6sPdNo3m62Nhy3bdH7j22cdBGp4IrMbDw90viPJzNbcdCsplqtpOGJoS5sLJjy78wJngRd_xA4_x3LFr--O6Q6D5swgoJ3fLCj8r9P5Zr3yUCeuwoJFC5E4gwzKRk8c8uWZeg0VwEKOD6yujr6JAtMOxV6TF7WLwEjzrq42fWUNKqWIqy9W28YIP19N0cajretn1KfofCTib4jJSl-uNnDS4ymBm0kHTgEBCyEoYTwYsk2aJgS5Ay33kGHBVv1APon_ASUWTKluSZveP4RLVjF80vXcwtnhAnNVCpUmoxuGgjENHgRiNM8e1eu4U4K55hEa15ZuoYosy2CsYQgsH52UsxZ96Wj1g8uc6Ekh2FF0elawQmL9EG3F9bEcXyk7WLvVBYIhb3Q4EgtQSGvvvyqYvjHn_PPBdxGNPQ_mZ6DXL1tGuK1zQ-vQaJYAMYrxaRtemgV7a7w4eGwSzmTPiu4MOIU3bVDAlO4DaSRq9Zk_Wz8bhuJOiPgIc5FfCLSLKfEkhYMP7VCuWGxqi0PwukCL16XAMJ5B3RpLcx5bFGf5MeK2RWhxfFDYsv_W91rYlY21o0t1VkixF6472c1HhHZYs6mpWGUUoq1MC82JS0o5qafj_cHTzMa8VkcNgsE7bhb3UWbdhvoIsqH1Pa2EaWwDKDOO3x8JJko2LgFYMU_bkw_Xa2Jp2Wh9ZCUfdhNvqxDnAFcnerBqyJrxutecsBZpZB91GiFqR2Yda8G600zPB_3Le0uNyTeIx_sglrUoRzjF4jgJNZaWgU6rZSFQfL93mcFGO3TkBD0Rdr00IRYmuPXQauIhKK8IiCWeLcqhifFVT6F42J-bWCztMVzCjQk0v-b5hxgouzm7bPTRfeGAeFkSJnSletH-gnIyp5XYWHlJqNBI3LdHHmpu-sIFVLdIQ-fXqpuvyCUCRoE8-EUQWqp3-AKCcQHx_IhOYC0W7pg4X1PfeHJQ5C_RZEb4KiK_hiVYaw5-VRxWGRekIoYtmDlbU_hlkGmZ-ztPFZfINxp5yvZ3YZkckjzzPmr4t_oFVVmNbLIN6OLiuV8rBB-0MsMNwTyOVE5TTCyviCEb-lReR5HAbf69nOJoEOxkJA-kzStRT-g7O-NnSJjyglQmSj3ug25NCKxzkcB4v8R5I8B8s5MAVG1KDAFkJHx7U7NtEYjA0oRFo2aRpuGd3Vknh3it1GpPuzWJbrymCGbhenwpNyETwTH6Gb7maAzfFV5Krp-6OE4GOKFLKB2VqPO3Gijla_kE0wacmnSe4XMRJ_Lgg463Qe051kJVcHf_q_T0XE4nf6wlYaQzuBq8qAJ5s6dC39rMcJaK78gpoMXvtTzHNYP78MExt3g4YMZt3mM-a2RBEaCWY717Rfdo1sPVb4XU-06uAF0VCvtHGuCTpwn9bfdS8rifr_gyDabt0GS8FSCtAnxaxtz9Pk0RGJ1lCZSUJNHrJDh9wPEs84PE3a7wP5eH7zauGx-1Lut0knCFjZhOAZFEGAnSB-daZCX9sJ5jgtta2QAdx-sgPh7-I23mFOq.tJoAAOkS8hiVNTSFNkiKmw";
            try
            {
                JsonWebToken jsonToken3 = new JsonWebToken(jwt3);
                jsonToken3.TryGetPayloadValue("ts", out long timestamp);

                JsonWebToken jsonToken = new JsonWebToken(jwt2);
                JsonWebToken jsonWebToken = new JsonWebToken(jwt);
                JsonWebKeySet jsonWebKeySet = new JsonWebKeySet(@"{ ""keys"":[{ ""kty"":""RSA"",""use"":""sig"",""kid"":""kg2LYs2T0CTjIfj4rt6JIynen38"",""x5t"":""kg2LYs2T0CTjIfj4rt6JIynen38"",""n"":""yTKa6m5GFOllz7oIHFCkvRJoBv7wLMuKIPLHbFGh5yOiO8o3akoqMhf1x6MxINGhZo6dkIrhVlVfWJhEJZPVaQdvyvVmlIZruhcbz3PGMqPAbjq2JqbB1mMnsyGHx-ovP0Cm5xj8sgI8wm67p3nosqzqFvg6mPKVO-w1QBr5seDU2AwU2DR88LF2v03Zjgn4mGvPdUOXihTQoNlf-nJFduXMDyRgZabnR2HlYHhagHwy1beWW1WtEaPz8iBN_0bGkGw705aDBUHJkdTty1mzsCZRur_n0imqXu9IzoSyiq5d0yKrRA5xkA-K3DMeRMquZ5QvPT9Eee4EZfFL97zBfQ"",""e"":""AQAB"",""x5c"":[""MIIDBTCCAe2gAwIBAgIQQiR8gZNKuYpH6cP+KIE5ijANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTIwMDgyODAwMDAwMFoXDTI1MDgyODAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMkymupuRhTpZc+6CBxQpL0SaAb+8CzLiiDyx2xRoecjojvKN2pKKjIX9cejMSDRoWaOnZCK4VZVX1iYRCWT1WkHb8r1ZpSGa7oXG89zxjKjwG46tiamwdZjJ7Mhh8fqLz9ApucY/LICPMJuu6d56LKs6hb4OpjylTvsNUAa+bHg1NgMFNg0fPCxdr9N2Y4J+Jhrz3VDl4oU0KDZX/pyRXblzA8kYGWm50dh5WB4WoB8MtW3lltVrRGj8/IgTf9GxpBsO9OWgwVByZHU7ctZs7AmUbq/59Ipql7vSM6EsoquXdMiq0QOcZAPitwzHkTKrmeULz0/RHnuBGXxS/e8wX0CAwEAAaMhMB8wHQYDVR0OBBYEFGcWXwaqmO25Blh2kHHAFrM/AS2CMA0GCSqGSIb3DQEBCwUAA4IBAQDFnKQ98CBnvVd4OhZP0KpaKbyDv93PGukE1ifWilFlWhvDde2mMv/ysBCWAR8AGSb1pAW/ZaJlMvqSN/+dXihcHzLEfKbCPw4/Mf2ikq4gqigt5t6hcTOSxL8wpe8OKkbNCMcU0cGpX5NJoqhJBt9SjoD3VPq7qRmDHX4h4nniKUMI7awI94iGtX/vlHnAMU4+8y6sfRQDGiCIWPSyypIWfEA6/O+SsEQ7vZ/b4mXlghUmxL+o2emsCI1e9PORvm5yc9Y/htN3Ju0x6ElHnih7MJT6/YUMISuyob9/mbw8Vf49M7H2t3AE5QIYcjqTwWJcwMlq5i9XfW2QLGH7K5i8""],""issuer"":""https://login.microsoftonline.com/{tenantid}/v2.0""},{ ""kty"":""RSA"",""use"":""sig"",""kid"":""18pnMg3UmrWvBK_tkDAbjgM5CmA"",""x5t"":""18pnMg3UmrWvBK_tkDAbjgM5CmA"",""n"":""v3tn90CVkqJ57gTZu8bbC37NX0RloPlEnelHmqobAEiDLRuqw7Hv2M5o9iRFhF4sSw64fr6P33stLWKpzVmm4y6HUi89QeQmYCNYzxQy2V-tBiLxWX3vtVYgUFwfZDz4TIEu_Ia7rgTg8aHJ8t_b6mz_xPaWlLJWSFBlNY22z2KX87ULrE5AVNMr125aaPWLhxCGWYrnk5KdMrDGb1cuOExzX4S-_fQrRAWTpQWhqi0bEn9Y0vIWKD9-2CkLmZlJGgOueICSuKwwWXm87RKergHVS9sEGkSaBwWOtCPWLsv01Nc0sZymNs3BkPZsQKioYkdox6beXSQwYsmXtBZHjQ"",""e"":""AQAB"",""x5c"":[""MIIC8TCCAdmgAwIBAgIQSoIG9pq9C4lOtPUfxLmCSDANBgkqhkiG9w0BAQsFADAjMSEwHwYDVQQDExhsb2dpbi5taWNyb3NvZnRvbmxpbmUudXMwHhcNMjAwOTEzMDAwMDAwWhcNMjUwOTEzMDAwMDAwWjAjMSEwHwYDVQQDExhsb2dpbi5taWNyb3NvZnRvbmxpbmUudXMwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC/e2f3QJWSonnuBNm7xtsLfs1fRGWg+USd6UeaqhsASIMtG6rDse/Yzmj2JEWEXixLDrh+vo/fey0tYqnNWabjLodSLz1B5CZgI1jPFDLZX60GIvFZfe+1ViBQXB9kPPhMgS78hruuBODxocny39vqbP/E9paUslZIUGU1jbbPYpfztQusTkBU0yvXblpo9YuHEIZZiueTkp0ysMZvVy44THNfhL799CtEBZOlBaGqLRsSf1jS8hYoP37YKQuZmUkaA654gJK4rDBZebztEp6uAdVL2wQaRJoHBY60I9Yuy/TU1zSxnKY2zcGQ9mxAqKhiR2jHpt5dJDBiyZe0FkeNAgMBAAGjITAfMB0GA1UdDgQWBBTuNouNBOGNFuUdRuSpaTYO2ZRL6DANBgkqhkiG9w0BAQsFAAOCAQEAKZD3Hn+79D/S9Xby/8zSHhYlsXM4FrxDUggt29o15EdBdxyLHCwc3bXZI2PMn0u5vBoz9T0U/MnzoUIxdkmSI9qhfpaxrz7LPEFsMLN6uqRfN9XbypaGcHXP4Qb4xLjLqlmqc8TCUAi0MAokCVSLl83lIbF8/Kw4hSYWMs7HdiuJLHwIgWSd9mguaPks+w64268bw7yZnKutE8xj1x3f6f6AqD3yZr6B1IfnVgrWX4kQIF8z0XB4J5jPaCcORRRB1GV/aSKhNFXWbJ7Z2UnX5f27dX1mgrnFb48jAZLuoeRn7M7+gtj/wPzRiYN845c9wyx91HDo4xqvq/dFtRQ3dQ==""],""issuer"":""https://login.microsoftonline.com/{tenantid}/v2.0""},{ ""kty"":""RSA"",""use"":""sig"",""kid"":""1LTMzakihiRla_8z2BEJVXeWMqo"",""x5t"":""1LTMzakihiRla_8z2BEJVXeWMqo"",""n"":""3sKcJSD4cHwTY5jYm5lNEzqk3wON1CaARO5EoWIQt5u-X-ZnW61CiRZpWpfhKwRYU153td5R8p-AJDWT-NcEJ0MHU3KiuIEPmbgJpS7qkyURuHRucDM2lO4L4XfIlvizQrlyJnJcd09uLErZEO9PcvKiDHoois2B4fGj7CsAe5UZgExJvACDlsQSku2JUyDmZUZP2_u_gCuqNJM5o0hW7FKRI3MFoYCsqSEmHnnumuJ2jF0RHDRWQpodhlAR6uKLoiWHqHO3aG7scxYMj5cMzkpe1Kq_Dm5yyHkMCSJ_JaRhwymFfV_SWkqd3n-WVZT0ADLEq0RNi9tqZ43noUnO_w"",""e"":""AQAB"",""x5c"":[""MIIDYDCCAkigAwIBAgIJAIB4jVVJ3BeuMA0GCSqGSIb3DQEBCwUAMCkxJzAlBgNVBAMTHkxpdmUgSUQgU1RTIFNpZ25pbmcgUHVibGljIEtleTAeFw0xNjA0MDUxNDQzMzVaFw0yMTA0MDQxNDQzMzVaMCkxJzAlBgNVBAMTHkxpdmUgSUQgU1RTIFNpZ25pbmcgUHVibGljIEtleTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAN7CnCUg+HB8E2OY2JuZTRM6pN8DjdQmgETuRKFiELebvl/mZ1utQokWaVqX4SsEWFNed7XeUfKfgCQ1k/jXBCdDB1NyoriBD5m4CaUu6pMlEbh0bnAzNpTuC+F3yJb4s0K5ciZyXHdPbixK2RDvT3Lyogx6KIrNgeHxo+wrAHuVGYBMSbwAg5bEEpLtiVMg5mVGT9v7v4ArqjSTOaNIVuxSkSNzBaGArKkhJh557pridoxdERw0VkKaHYZQEerii6Ilh6hzt2hu7HMWDI+XDM5KXtSqvw5ucsh5DAkifyWkYcMphX1f0lpKnd5/llWU9AAyxKtETYvbameN56FJzv8CAwEAAaOBijCBhzAdBgNVHQ4EFgQU9IdLLpbC2S8Wn1MCXsdtFac9SRYwWQYDVR0jBFIwUIAU9IdLLpbC2S8Wn1MCXsdtFac9SRahLaQrMCkxJzAlBgNVBAMTHkxpdmUgSUQgU1RTIFNpZ25pbmcgUHVibGljIEtleYIJAIB4jVVJ3BeuMAsGA1UdDwQEAwIBxjANBgkqhkiG9w0BAQsFAAOCAQEAXk0sQAib0PGqvwELTlflQEKS++vqpWYPW/2gCVCn5shbyP1J7z1nT8kE/ZDVdl3LvGgTMfdDHaRF5ie5NjkTHmVOKbbHaWpTwUFbYAFBJGnx+s/9XSdmNmW9GlUjdpd6lCZxsI6888r0ptBgKINRRrkwMlq3jD1U0kv4JlsIhafUIOqGi4+hIDXBlY0F/HJPfUU75N885/r4CCxKhmfh3PBM35XOch/NGC67fLjqLN+TIWLoxnvil9m3jRjqOA9u50JUeDGZABIYIMcAdLpI2lcfru4wXcYXuQul22nAR7yOyGKNOKULoOTE4t4AeGRqCogXSxZgaTgKSBhvhE+MGg==""],""issuer"":""https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad/v2.0""},{ ""kty"":""RSA"",""use"":""sig"",""kid"":""xP_zn6I1YkXcUUmlBoPuXTGsaxk"",""x5t"":""xP_zn6I1YkXcUUmlBoPuXTGsaxk"",""n"":""2pWatafeb3mB0A73-Z-URwrubwDldWvivRu19GNC61MBOb3fZ4I4lyhUhNuS7aJRPJIFB6zl-HFx1nHpGg74BHe0z9skODHYZEACd2iKBIet55DdduIe1CXsZ9keyEmNaGv3XS4OW_7IDM0j5wR9OHugUifkH3PQIcFvTYanHmXojTmgjIOWoz7y0okpyN9-FbZRzdfx-ej-njaj5gR8r69muwO5wlTbIG20V40R6zYh-QODMUpayy7jDGFGw5vjFH9Ca0tLZcNQq__JKE_mp-0fODOAQobOrBUoASFkyCd95BVW7KJrndvW7ofRWaCTuZZOy5SnU4asbjMrgxFZFw"",""e"":""AQAB"",""x5c"":[""MIIDYDCCAkigAwIBAgIJAJzCyTLC+DjJMA0GCSqGSIb3DQEBCwUAMCkxJzAlBgNVBAMTHkxpdmUgSUQgU1RTIFNpZ25pbmcgUHVibGljIEtleTAeFw0xNjA3MTMyMDMyMTFaFw0yMTA3MTIyMDMyMTFaMCkxJzAlBgNVBAMTHkxpdmUgSUQgU1RTIFNpZ25pbmcgUHVibGljIEtleTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBANqVmrWn3m95gdAO9/mflEcK7m8A5XVr4r0btfRjQutTATm932eCOJcoVITbku2iUTySBQes5fhxcdZx6RoO+AR3tM/bJDgx2GRAAndoigSHreeQ3XbiHtQl7GfZHshJjWhr910uDlv+yAzNI+cEfTh7oFIn5B9z0CHBb02Gpx5l6I05oIyDlqM+8tKJKcjffhW2Uc3X8fno/p42o+YEfK+vZrsDucJU2yBttFeNEes2IfkDgzFKWssu4wxhRsOb4xR/QmtLS2XDUKv/yShP5qftHzgzgEKGzqwVKAEhZMgnfeQVVuyia53b1u6H0Vmgk7mWTsuUp1OGrG4zK4MRWRcCAwEAAaOBijCBhzAdBgNVHQ4EFgQU11z579/IePwuc4WBdN4L0ljG4CUwWQYDVR0jBFIwUIAU11z579/IePwuc4WBdN4L0ljG4CWhLaQrMCkxJzAlBgNVBAMTHkxpdmUgSUQgU1RTIFNpZ25pbmcgUHVibGljIEtleYIJAJzCyTLC+DjJMAsGA1UdDwQEAwIBxjANBgkqhkiG9w0BAQsFAAOCAQEAiASLEpQseGNahE+9f9PQgmX3VgjJerNjXr1zXWXDJfFE31DxgsxddjcIgoBL9lwegOHHvwpzK1ecgH45xcJ0Z/40OgY8NITqXbQRfdgLrEGJCoyOQEbjb5PW5k2aOdn7LBxvDsH6Y8ax26v+EFMPh3G+xheh6bfoIRSK1b+44PfoDZoJ9NfJibOZ4Cq+wt/yOvpMYQDB/9CNo18wmA3RCLYjf2nAc7RO0PDYHSIq5QDWV+1awmXDKgIdRpYPpRtn9KFXQkpCeEc/lDTG+o6n7nC40wyjioyR6QmHGvNkMR4VfSoTKCTnFATyDpI1bqU2K7KNjUEsCYfwybFB8d6mjQ==""],""issuer"":""https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad/v2.0""}]}");
                string payload = Base64UrlEncoder.Decode(jsonWebToken.EncodedPayload);
                string header = Base64UrlEncoder.Decode(jsonWebToken.EncodedHeader);
                Console.WriteLine($"payload: '{payload}'.");
                Console.WriteLine($"header: '{header}'.");

                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKeys = jsonWebKeySet.GetSigningKeys(),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                JsonWebTokenHandler jsonWebTokenHandler = new JsonWebTokenHandler();
                TokenValidationResult tokenValidationResult = jsonWebTokenHandler.ValidateToken(jwt, tokenValidationParameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: '{ex}' ");
            }
        }
    }
}
