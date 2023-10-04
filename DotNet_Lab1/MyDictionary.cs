using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace DotNet_Lab1
{
    public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private class Node
        {
            public KeyValuePair<TKey, TValue> Value { get; set; }
            public Node? Next { get; set; }
            public Node? Prev { get; set; }

            public Node(KeyValuePair<TKey, TValue> value)
            {
                Value = value;
            }
        }

        private Node? head;
        private Node? tail;

        public TValue this[TKey key]
        {
            get
            {
                Node result = TryGetNode(key, false) ??
                    throw new KeyNotFoundException("The given key was not present in the dictionary");

                return result.Value.Value;
            }
            set
            {
                Node? result = TryGetNode(key, false);
                if (result is not null)
                {
                    result.Value = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            var keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
            Add(keyValuePair);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            var node = new Node(item);
            node.Prev = head;

            if (tail is not null)
            {
                tail.Next = node;
            }

            head ??= node;
            tail = node;

            Count++;
        }

        public void Clear()
        {
            head = null;
            tail = null;

            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var node = TryGetNode(item.Key, true, item.Value);

            return node is not null;
        }

        public bool ContainsKey(TKey key)
        {
            var node = TryGetNode(key, false);

            return node is not null;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Node? node = head;
            while (node is not null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            value = default;

            var node = TryGetNode(key, false);
            bool result = node is not null;

            if (result)
            {
                value = node!.Value.Value;
            }

            return result;
        }

        private Node? TryGetNode(TKey key, bool isValuePassed, TValue? value = default)
        {
            Node? node = head;
            while (node is not null)
            {
                if (node.Value.Key.Equals(key) &&
                    (!isValuePassed ||
                    (node.Value.Value?.Equals(value) ?? value is null) ) )
                {
                    return node;
                }
                node = node.Next;
            }

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}