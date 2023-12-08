using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNet_Lab1.Core;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace DotNet_Lab1.Core.Tests
{
    [TestClass]
    public class MyDictionaryTests
    {
        #region Add
        [TestMethod]
        public void Add_KeyValue_Count()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;

            myDictionary.Add(key, value);

            int count = myDictionary.Count;
        }

        [TestMethod]
        public void Add_KeyValuePair()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            KeyValuePair<string, int> kvp = new(key, value);

            myDictionary.Add(kvp);

            int count = myDictionary.Count;
            Assert.AreEqual(1, count);
        }
        #endregion

        #region IndexerGetter
        [TestMethod]
        public void IndexerGetter_ValidValue_ReturnValidValue()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;

            myDictionary.Add(key, value);

            int resultValue = myDictionary[key];
            Assert.AreEqual(value, resultValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IndexerGetter_NullKey_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string? key = null;

            int resultValue = myDictionary[key];
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IndexerGetter_NonExistingKey_ShouldThrowKeyNotFoundException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "NonExistingKey";

            int resultValue = myDictionary[key];
        }
        #endregion

        #region IndexerSetter
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IndexerSetter_NullKey_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string? key = null;
            int value = 1;

            myDictionary[key] = value;
        }

        [TestMethod]
        public void IndexerSetter_NonExistingKey_AddNewElement()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;

            myDictionary[key] = value;

            int resultValue = myDictionary[key];
            int count = myDictionary.Count;
            Assert.AreEqual(value, resultValue);
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void IndexerSetter_ExistingKey_ChangeExistingElement()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            int newValue = 1;

            myDictionary.Add(key, value);
            myDictionary[key] = newValue;

            int resultValue = myDictionary[key];
            int count = myDictionary.Count;
            Assert.AreEqual(newValue, resultValue);
            Assert.AreEqual(1, count);
        }
        #endregion
    }
}