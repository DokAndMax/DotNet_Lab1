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
            Assert.AreEqual(1, count);
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
            Assert.AreEqual(value, resultValue, "Значення елементу відрізнається від очікуваного");
            Assert.AreEqual(1, count, "Кількість елементів в словнику відрізняється від очікуваної");
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
            Assert.AreEqual(newValue, resultValue, "Значення елементу відрізнається від очікуваного");
            Assert.AreEqual(1, count, "Кількість елементів в словнику відрізняється від очікуваної");
        }
        #endregion

        #region Insert
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void Insert_AtValidIndex_NewElementAtPosition(int index)
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int tempValue = 1;
            int value = 5;
            myDictionary.Add($"{key}{tempValue}", tempValue++);
            myDictionary.Add($"{key}{tempValue}", tempValue++);
            myDictionary.Add($"{key}{tempValue}", tempValue++);

            myDictionary.Insert(key, value, index);

            int resultValue = myDictionary[key];
            int resultValueAtIndex = GetValue(myDictionary);
            int count = myDictionary.Count;
            Assert.AreEqual(value, resultValue, "Значення елементу відрізнається від очікуваного");
            Assert.AreEqual(value, resultValueAtIndex, "Значення елементу в заданій позиції відрізнається від очікуваного");
            Assert.AreEqual(4, count, "Кількість елементів в словнику відрізняється від очікуваної");

            int GetValue(MyDictionary<string, int> dictionary)
            {
                int i = 0;
                foreach (var (_, v) in dictionary)
                {
                    if(i++ == index)
                    {
                        return v;
                    }
                }

                return -1;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullKey_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string? key = null;
            int value = 1;

            myDictionary.Insert(key, value, 0);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Insert_AtInvalidIndex_ShouldThrowArgumentOutOfRangeException(int invalidIndex)
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;

            myDictionary.Insert(key, value, invalidIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Insert_ExistingKey_ShouldThrowArgumentException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;

            myDictionary.Add(key, value);
            myDictionary.Insert(key, value, 0);
        }
        #endregion
    }
}