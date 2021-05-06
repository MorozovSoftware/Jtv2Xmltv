using System.Collections.Generic;

namespace UsefulTools
{
    public class DictionaryTools
    {
        public static T GetReplacedOrDefault<T>(T defaultObject, Dictionary<T, T> replaceMap)
        {
            return replaceMap.TryGetValue(defaultObject, out T replacedObject) ? replacedObject : defaultObject;
        }
        public static K AddOrGet<T,K>(T key, Dictionary<T, K> dictionary) where K : new()
        {
            if (!dictionary.TryGetValue(key, out K value))
            {
                value = new();
                dictionary.Add(key, value);
            }
            return value;
        }
    }
}
