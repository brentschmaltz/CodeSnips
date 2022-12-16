using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CodeSnips.Threading
{
    public class ClearMap
    {
        private static ConcurrentDictionary<string, ObjectToClear> CertificateToRsaMap = new ConcurrentDictionary<string, ObjectToClear>();
        private static int maximumCertificateToRsaMap = 100;
        private static object clearMapObject = new object();

        public static void Run()
        {

            for (int i = 0; i < 4 * maximumCertificateToRsaMap; i++)
                AddNewObject();
        }

        public static void AddNewObject()
        {
            if (CertificateToRsaMap.Count == maximumCertificateToRsaMap)
            {
                ClearCertificateToRsaMap clearCertificateToRsaMap = new ClearCertificateToRsaMap(CertificateToRsaMap, clearMapObject);
                (new Thread(new ThreadStart(clearCertificateToRsaMap.Clear))).Start();

                CertificateToRsaMap = new ConcurrentDictionary<string, ObjectToClear>();
            }

            CertificateToRsaMap[Guid.NewGuid().ToString()] = new ObjectToClear();
        }

        internal class ClearCertificateToRsaMap
        {
            object _clearMapObject;
            ConcurrentDictionary<string, ObjectToClear> _rsaToCertificateMap;

            internal ClearCertificateToRsaMap(ConcurrentDictionary<string, ObjectToClear> rsaToCertificateMap, object clearMapObject)
            {
                _rsaToCertificateMap = rsaToCertificateMap;
                _clearMapObject = clearMapObject;
            }

            internal void Clear()
            {
                Console.WriteLine("Clearing");
                lock (_clearMapObject)
                {
                    foreach (ObjectToClear objectToClear in _rsaToCertificateMap.Values)
                        if (objectToClear != null)
                            objectToClear.Clear();

                    _rsaToCertificateMap.Clear();
                }
            }
        }

        internal class ObjectToClear
        {
            public ObjectToClear() { }

            public void Clear() { }
        }
    }
}
