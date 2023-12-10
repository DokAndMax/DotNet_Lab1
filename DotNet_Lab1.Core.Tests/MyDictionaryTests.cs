using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNet_Lab1.Core;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

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

        #region Clear
        [TestMethod]
        public void Clear_AllElementsRemoved()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            myDictionary.Add(key, value);

            myDictionary.Clear();

            int count = myDictionary.Count;
            Assert.AreEqual(0, count, "Кількість елементів в словнику відрізняється від очікуваної");
            Assert.ThrowsException<KeyNotFoundException>(() => myDictionary[key]);
        }
        #endregion

        #region Contains
        [TestMethod]
        public void Contains_ExistingElement_True()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            KeyValuePair<string, int> kvp = new(key, value);
            myDictionary.Add(kvp);

            var isContains = myDictionary.Contains(kvp);

            Assert.IsTrue(isContains);
        }

        [TestMethod]
        public void Contains_NonExistingElement_False()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            KeyValuePair<string, int> kvp = new(key, value);

            var isContains = myDictionary.Contains(kvp);

            Assert.IsFalse(isContains);
        }
        #endregion

        #region ContainsKey
        [TestMethod]
        public void ContainsKey_ExistingElement_True()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            myDictionary.Add(key, value);

            var isContains = myDictionary.ContainsKey(key);

            Assert.IsTrue(isContains);
        }

        [TestMethod]
        public void ContainsKey_NonExistingKey_False()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";

            var isContains = myDictionary.ContainsKey(key);

            Assert.IsFalse(isContains);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsKey_NullKey_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string? key = null;

            myDictionary.ContainsKey(key);
        }
        #endregion

        #region CopyTo
        [TestMethod]
        public void CopyTo_AtValidIndex_NewElementAtPosition()
        {
            var myDictionary = new MyDictionary<string, int>();

            string key = "key";
            int tempValue = 1;
            var array = new KeyValuePair<string, int>[2];

            myDictionary.Add($"{key}{tempValue}", tempValue++);
            myDictionary.Add($"{key}{tempValue}", tempValue++);

            myDictionary.CopyTo(array, 0);

            CollectionAssert.AreEqual(myDictionary.ToList(), array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            KeyValuePair<string, int>[]? array = null;

            myDictionary.CopyTo(array, 0);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_ArrayInvalidIndex_ShouldThrowArgumentOutOfRangeException(int invalidIndex)
        {
            var myDictionary = new MyDictionary<string, int>();
            var array = new KeyValuePair<string, int>[2];

            myDictionary.CopyTo(array, invalidIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_InvalidArrayLength_ShouldThrowArgumentException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int tempValue = 1;
            var array = new KeyValuePair<string, int>[1];

            myDictionary.Add($"{key}{tempValue}", tempValue++);
            myDictionary.Add($"{key}{tempValue}", tempValue++);

            myDictionary.CopyTo(array, 0);
        }
        #endregion

        #region Remove
        [TestMethod]
        public void Remove_ExistingKey_True()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            myDictionary.Add(key, value);

            var isRemoved = myDictionary.Remove(key);

            int count = myDictionary.Count;
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(0, count, "Кількість елементів в словнику відрізняється від очікуваної");
            Assert.ThrowsException<KeyNotFoundException>(() => myDictionary[key]);

        }

        [TestMethod]
        public void Remove_NonExistingKey_False()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";

            var isRemoved = myDictionary.Remove(key);

            int count = myDictionary.Count;
            Assert.IsFalse(isRemoved);
            Assert.AreEqual(0, count, "Кількість елементів в словнику відрізняється від очікуваної");
            Assert.ThrowsException<KeyNotFoundException>(() => myDictionary[key]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullKey_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string? key = null;

            myDictionary.Remove(key);
        }

        [TestMethod]
        public void Remove_ExistingKVPair_True()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            KeyValuePair<string, int> kvp = new(key, value);
            myDictionary.Add(kvp);

            var isRemoved = myDictionary.Remove(kvp);

            int count = myDictionary.Count;
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(0, count, "Кількість елементів в словнику відрізняється від очікуваної");
            Assert.ThrowsException<KeyNotFoundException>(() => myDictionary[key]);
        }

        [TestMethod]
        public void Remove_NonExistingKVPair_False()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            KeyValuePair<string, int> kvp = new(key, value);

            var isRemoved = myDictionary.Remove(kvp);

            int count = myDictionary.Count;
            Assert.IsFalse(isRemoved);
            Assert.AreEqual(0, count, "Кількість елементів в словнику відрізняється від очікуваної");
            Assert.ThrowsException<KeyNotFoundException>(() => myDictionary[key]);
        }
        #endregion

        #region TryGetValue
        [TestMethod]
        public void TryGetValue_ExistingKey_True()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";
            int value = 1;
            myDictionary.Add(key, value);

            var isContains = myDictionary.TryGetValue(key, out int resultValue);

            Assert.IsTrue(isContains);
            Assert.AreEqual(value, resultValue);

        }

        [TestMethod]
        public void TryGetValue_NonExistingKey_False()
        {
            var myDictionary = new MyDictionary<string, int>();
            string key = "key";

            var isContains = myDictionary.TryGetValue(key, out int resultValue);

            Assert.IsFalse(isContains);
            Assert.AreEqual(resultValue, default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValue_NullKey_ShouldThrowArgumentNullException()
        {
            var myDictionary = new MyDictionary<string, int>();
            string? key = null;

            myDictionary.TryGetValue(key, out int _);
        }
        #endregion

        #region GetEnumerator
        [TestMethod]
        public void GetEnumerator_ExistingKey_True()
        {
            KeyValuePair<string, int>[] expectedValues = [new("1", 1), new("2", 2), new("3", 3)];
            MyDictionary<string, int> myDictionary = [.. expectedValues];

            IEnumerator e1 = expectedValues.GetEnumerator();
            IEnumerator e2 = myDictionary.GetEnumerator();
            Assert.AreEqual(expectedValues.Length, myDictionary.Count);
            while (e2.MoveNext() && e1.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }
        #endregion

        #region Reverse
        [TestMethod]
        public void Reverse_ExistingKey_True()
        {
            KeyValuePair<string, int>[] expectedValues = [new("1", 1), new("2", 2), new("3", 3)];
            MyDictionary<string, int> myDictionary = [.. expectedValues];

            IEnumerator e1 = expectedValues.Reverse().GetEnumerator();
            IEnumerator e2 = myDictionary.Reverse().GetEnumerator();
            Assert.AreEqual(expectedValues.Length, myDictionary.Count);
            while (e2.MoveNext() && e1.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }
        #endregion
    }
}