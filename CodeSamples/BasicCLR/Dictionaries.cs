using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeSnips.BasicCLR
{
    public class Dictionaries
    {
        private static Dictionary<string, object> _dictionary = new Dictionary<string, object> { { "1", "stringAAAAA" } };

        public static void Run()
        {
            IReadOnlyDictionary<string, object> dictionaryCreateNewReadonly = CreateNewReadonlyDictionary();
            IReadOnlyDictionary<string, object> dictionaryCreateNewDictionary = CreateNewDictionary();
            IReadOnlyDictionary<string, object> dictionaryReturnReadOnly = ReturnReadonlyDictionary();

            _dictionary.Add("2", "string2");
        }

        private static IReadOnlyDictionary<string, object> CreateNewReadonlyDictionary()
        {
            return new ReadOnlyDictionary<string, object>(_dictionary);
        }

        private static IReadOnlyDictionary<string, object> CreateNewDictionary()
        {
            return new Dictionary<string, object>(_dictionary);
        }

        private static IReadOnlyDictionary<string, object> ReturnReadonlyDictionary()
        {
            return _dictionary;
        }
    }
}
