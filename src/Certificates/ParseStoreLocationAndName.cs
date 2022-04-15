using System;
using System.Security.Cryptography.X509Certificates;

namespace CodeSnips.Certificates
{
    public class ParseStoreLocationAndName
    {
        public static void Run()
        {
            StoreLocation storeLocation = StoreLocation.LocalMachine;
            StoreName storeName = StoreName.My;

            Parse(@"LocalMachine/My", ref storeLocation, ref storeName);
            Parse(@"CurrentUser/My", ref storeLocation, ref storeName);

        }

        private static void Parse(string storeDescription, ref StoreLocation certificateStoreLocation, ref StoreName certificateStoreName)
        {
            string[] path = storeDescription.Split('/');

            bool storeLocationParsed = Enum.TryParse<StoreLocation>(path[0], true, out certificateStoreLocation);
            bool storeNameParsed = Enum.TryParse<StoreName>(path[1], true, out certificateStoreName);

           Console.WriteLine($"storeLocationParsed: {storeLocationParsed}, storeNameParsed: {storeNameParsed}");
           Console.WriteLine($"StoreDescription: {storeDescription}, StoreLocation: {certificateStoreLocation}, StoreName: {certificateStoreName}");
        }
    }
}
