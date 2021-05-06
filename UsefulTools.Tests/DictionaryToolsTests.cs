using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UsefulTools.Tests
{
    [TestClass]
    public class DictionaryToolsTests
    {
        [TestMethod]
        public void GetReplacedOrDefault_StringReplaced()
        {
            string subject = "ReplaceMe";
            string expected = "Ok";
            Dictionary<string, string> replaceMap = new()
            { 
            [subject] = expected,
            ["OtherKey"] = "OtherValue"
            };

            string actual = DictionaryTools.GetReplacedOrDefault(subject, replaceMap);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetReplacedOrDefault_StringDefault()
        {
            string expected = "Ok";
            Dictionary<string, string> replaceMap = new()
            {
                ["OtherKey"] = "OtherValue"
            };

            string actual = DictionaryTools.GetReplacedOrDefault(expected, replaceMap);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddOrGet_Get()
        {
            int key = 42;
            string expected = "Answer";
            Dictionary<int, object> dictionary = new()
            {
                [0] = "SomeValue",
                [key] = expected
            };

            object actual = DictionaryTools.AddOrGet(key, dictionary);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddOrGet_Add()
        {
            int key = 42;
            Dictionary<int, object> dictionary = new()
            {
                [0] = "SomeValue",
            };

            DictionaryTools.AddOrGet(key, dictionary);
            object actual = dictionary[key];

            Assert.IsNotNull(actual);
        }
    }
}