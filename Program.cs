#region Assembly mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
    //
    // Summary:
    //     Represents a collection of keys and values.
    //
    // Type parameters:
    //   TKey:
    //     The type of the keys in the dictionary.
    //
    //   TValue:
    //     The type of the values in the dictionary.
    [Serializable]
    [DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
    {
        private struct Entry
        {
            public int hashCode;

            public int next;

            public TKey key;

            public TValue value;
        }

        //
        // Summary:
        //     Enumerates the elements of a System.Collections.Generic.Dictionary`2.
        //
        // Type parameters:
        //   TKey:
        //
        //   TValue:
        [Serializable]
        [__DynamicallyInvokable]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
        {
            private Dictionary<TKey, TValue> dictionary;

            private int version;

            private int index;

            private KeyValuePair<TKey, TValue> current;

            private int getEnumeratorRetType;

            internal const int DictEntry = 1;

            internal const int KeyValuePair = 2;

            //
            // Summary:
            //     Gets the element at the current position of the enumerator.
            //
            // Returns:
            //     The element in the System.Collections.Generic.Dictionary`2 at the current position
            //     of the enumerator.
            [__DynamicallyInvokable]
            public KeyValuePair<TKey, TValue> Current
            {
                [__DynamicallyInvokable]
                get
                {
                    return current;
                }
            }

            //
            // Summary:
            //     Gets the element at the current position of the enumerator.
            //
            // Returns:
            //     The element in the collection at the current position of the enumerator, as an
            //     System.Object.
            //
            // Exceptions:
            //   T:System.InvalidOperationException:
            //     The enumerator is positioned before the first element of the collection or after
            //     the last element.
            [__DynamicallyInvokable]
            object IEnumerator.Current
            {
                [__DynamicallyInvokable]
                get
                {
                    if (index == 0 || index == dictionary.count + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }

                    if (getEnumeratorRetType == 1)
                    {
                        return new DictionaryEntry(current.Key, current.Value);
                    }

                    return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                }
            }

            //
            // Summary:
            //     Gets the element at the current position of the enumerator.
            //
            // Returns:
            //     The element in the dictionary at the current position of the enumerator, as a
            //     System.Collections.DictionaryEntry.
            //
            // Exceptions:
            //   T:System.InvalidOperationException:
            //     The enumerator is positioned before the first element of the collection or after
            //     the last element.
            [__DynamicallyInvokable]
            DictionaryEntry IDictionaryEnumerator.Entry
            {
                [__DynamicallyInvokable]
                get
                {
                    if (index == 0 || index == dictionary.count + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }

                    return new DictionaryEntry(current.Key, current.Value);
                }
            }

            //
            // Summary:
            //     Gets the key of the element at the current position of the enumerator.
            //
            // Returns:
            //     The key of the element in the dictionary at the current position of the enumerator.
            //
            // Exceptions:
            //   T:System.InvalidOperationException:
            //     The enumerator is positioned before the first element of the collection or after
            //     the last element.
            [__DynamicallyInvokable]
            object IDictionaryEnumerator.Key
            {
                [__DynamicallyInvokable]
                get
                {
                    if (index == 0 || index == dictionary.count + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }

                    return current.Key;
                }
            }

            //
            // Summary:
            //     Gets the value of the element at the current position of the enumerator.
            //
            // Returns:
            //     The value of the element in the dictionary at the current position of the enumerator.
            //
            // Exceptions:
            //   T:System.InvalidOperationException:
            //     The enumerator is positioned before the first element of the collection or after
            //     the last element.
            [__DynamicallyInvokable]
            object IDictionaryEnumerator.Value
            {
                [__DynamicallyInvokable]
                get
                {
                    if (index == 0 || index == dictionary.count + 1)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }

                    return current.Value;
                }
            }

            internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
            {
                this.dictionary = dictionary;
                version = dictionary.version;
                index = 0;
                this.getEnumeratorRetType = getEnumeratorRetType;
                current = default(KeyValuePair<TKey, TValue>);
            }

            //
            // Summary:
            //     Advances the enumerator to the next element of the System.Collections.Generic.Dictionary`2.
            //
            // Returns:
            //     true if the enumerator was successfully advanced to the next element; false if
            //     the enumerator has passed the end of the collection.
            //
            // Exceptions:
            //   T:System.InvalidOperationException:
            //     The collection was modified after the enumerator was created.
            [__DynamicallyInvokable]
            public bool MoveNext()
            {
                if (version != dictionary.version)
                {
                    ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }

                while ((uint)index < (uint)dictionary.count)
                {
                    if (dictionary.entries[index].hashCode >= 0)
                    {
                        current = new KeyValuePair<TKey, TValue>(dictionary.entries[index].key, dictionary.entries[index].value);
                        index++;
                        return true;
                    }

                    index++;
                }

                index = dictionary.count + 1;
                current = default(KeyValuePair<TKey, TValue>);
                return false;
            }

            //
            // Summary:
            //     Releases all resources used by the System.Collections.Generic.Dictionary`2.Enumerator.
            [__DynamicallyInvokable]
            public void Dispose()
            {
            }

            //
            // Summary:
            //     Sets the enumerator to its initial position, which is before the first element
            //     in the collection.
            //
            // Exceptions:
            //   T:System.InvalidOperationException:
            //     The collection was modified after the enumerator was created.
            [__DynamicallyInvokable]
            void IEnumerator.Reset()
            {
                if (version != dictionary.version)
                {
                    ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }

                index = 0;
                current = default(KeyValuePair<TKey, TValue>);
            }
        }

        //
        // Summary:
        //     Represents the collection of keys in a System.Collections.Generic.Dictionary`2.
        //     This class cannot be inherited.
        //
        // Type parameters:
        //   TKey:
        //
        //   TValue:
        [Serializable]
        [DebuggerTypeProxy(typeof(Mscorlib_DictionaryKeyCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        [__DynamicallyInvokable]
        public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
        {
            //
            // Summary:
            //     Enumerates the elements of a System.Collections.Generic.Dictionary`2.KeyCollection.
            //
            // Type parameters:
            //   TKey:
            //
            //   TValue:
            [Serializable]
            [__DynamicallyInvokable]
            public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
            {
                private Dictionary<TKey, TValue> dictionary;

                private int index;

                private int version;

                private TKey currentKey;

                //
                // Summary:
                //     Gets the element at the current position of the enumerator.
                //
                // Returns:
                //     The element in the System.Collections.Generic.Dictionary`2.KeyCollection at the
                //     current position of the enumerator.
                [__DynamicallyInvokable]
                public TKey Current
                {
                    [__DynamicallyInvokable]
                    get
                    {
                        return currentKey;
                    }
                }

                //
                // Summary:
                //     Gets the element at the current position of the enumerator.
                //
                // Returns:
                //     The element in the collection at the current position of the enumerator.
                //
                // Exceptions:
                //   T:System.InvalidOperationException:
                //     The enumerator is positioned before the first element of the collection or after
                //     the last element.
                [__DynamicallyInvokable]
                object IEnumerator.Current
                {
                    [__DynamicallyInvokable]
                    get
                    {
                        if (index == 0 || index == dictionary.count + 1)
                        {
                            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                        }

                        return currentKey;
                    }
                }

                internal Enumerator(Dictionary<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
                    version = dictionary.version;
                    index = 0;
                    currentKey = default(TKey);
                }

                //
                // Summary:
                //     Releases all resources used by the System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator.
                [__DynamicallyInvokable]
                public void Dispose()
                {
                }

                //
                // Summary:
                //     Advances the enumerator to the next element of the System.Collections.Generic.Dictionary`2.KeyCollection.
                //
                // Returns:
                //     true if the enumerator was successfully advanced to the next element; false if
                //     the enumerator has passed the end of the collection.
                //
                // Exceptions:
                //   T:System.InvalidOperationException:
                //     The collection was modified after the enumerator was created.
                [__DynamicallyInvokable]
                public bool MoveNext()
                {
                    if (version != dictionary.version)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    }

                    while ((uint)index < (uint)dictionary.count)
                    {
                        if (dictionary.entries[index].hashCode >= 0)
                        {
                            currentKey = dictionary.entries[index].key;
                            index++;
                            return true;
                        }

                        index++;
                    }

                    index = dictionary.count + 1;
                    currentKey = default(TKey);
                    return false;
                }

                //
                // Summary:
                //     Sets the enumerator to its initial position, which is before the first element
                //     in the collection.
                //
                // Exceptions:
                //   T:System.InvalidOperationException:
                //     The collection was modified after the enumerator was created.
                [__DynamicallyInvokable]
                void IEnumerator.Reset()
                {
                    if (version != dictionary.version)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    }

                    index = 0;
                    currentKey = default(TKey);
                }
            }

            private Dictionary<TKey, TValue> dictionary;

            //
            // Summary:
            //     Gets the number of elements contained in the System.Collections.Generic.Dictionary`2.KeyCollection.
            //
            // Returns:
            //     The number of elements contained in the System.Collections.Generic.Dictionary`2.KeyCollection.
            //     Retrieving the value of this property is an O(1) operation.
            [__DynamicallyInvokable]
            public int Count
            {
                [__DynamicallyInvokable]
                get
                {
                    return dictionary.Count;
                }
            }

            //
            // Summary:
            //     Gets a value indicating whether the System.Collections.Generic.ICollection`1
            //     is read-only.
            //
            // Returns:
            //     true if the System.Collections.Generic.ICollection`1 is read-only; otherwise,
            //     false. In the default implementation of System.Collections.Generic.Dictionary`2.KeyCollection,
            //     this property always returns true.
            [__DynamicallyInvokable]
            bool ICollection<TKey>.IsReadOnly
            {
                [__DynamicallyInvokable]
                get
                {
                    return true;
                }
            }

            //
            // Summary:
            //     Gets a value indicating whether access to the System.Collections.ICollection
            //     is synchronized (thread safe).
            //
            // Returns:
            //     true if access to the System.Collections.ICollection is synchronized (thread
            //     safe); otherwise, false. In the default implementation of System.Collections.Generic.Dictionary`2.KeyCollection,
            //     this property always returns false.
            [__DynamicallyInvokable]
            bool ICollection.IsSynchronized
            {
                [__DynamicallyInvokable]
                get
                {
                    return false;
                }
            }

            //
            // Summary:
            //     Gets an object that can be used to synchronize access to the System.Collections.ICollection.
            //
            // Returns:
            //     An object that can be used to synchronize access to the System.Collections.ICollection.
            //     In the default implementation of System.Collections.Generic.Dictionary`2.KeyCollection,
            //     this property always returns the current instance.
            [__DynamicallyInvokable]
            object ICollection.SyncRoot
            {
                [__DynamicallyInvokable]
                get
                {
                    return ((ICollection)dictionary).SyncRoot;
                }
            }

            //
            // Summary:
            //     Initializes a new instance of the System.Collections.Generic.Dictionary`2.KeyCollection
            //     class that reflects the keys in the specified System.Collections.Generic.Dictionary`2.
            //
            // Parameters:
            //   dictionary:
            //     The System.Collections.Generic.Dictionary`2 whose keys are reflected in the new
            //     System.Collections.Generic.Dictionary`2.KeyCollection.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     dictionary is null.
            [__DynamicallyInvokable]
            public KeyCollection(Dictionary<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                }

                this.dictionary = dictionary;
            }

            //
            // Summary:
            //     Returns an enumerator that iterates through the System.Collections.Generic.Dictionary`2.KeyCollection.
            //
            // Returns:
            //     A System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator for the System.Collections.Generic.Dictionary`2.KeyCollection.
            [__DynamicallyInvokable]
            public Enumerator GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            //
            // Summary:
            //     Copies the System.Collections.Generic.Dictionary`2.KeyCollection elements to
            //     an existing one-dimensional System.Array, starting at the specified array index.
            //
            // Parameters:
            //   array:
            //     The one-dimensional System.Array that is the destination of the elements copied
            //     from System.Collections.Generic.Dictionary`2.KeyCollection. The System.Array
            //     must have zero-based indexing.
            //
            //   index:
            //     The zero-based index in array at which copying begins.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     array is null.
            //
            //   T:System.ArgumentOutOfRangeException:
            //     index is less than zero.
            //
            //   T:System.ArgumentException:
            //     The number of elements in the source System.Collections.Generic.Dictionary`2.KeyCollection
            //     is greater than the available space from index to the end of the destination
            //     array.
            [__DynamicallyInvokable]
            public void CopyTo(TKey[] array, int index)
            {
                if (array == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (index < 0 || index > array.Length)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                }

                if (array.Length - index < dictionary.Count)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                int count = dictionary.count;
                Entry[] entries = dictionary.entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0)
                    {
                        array[index++] = entries[i].key;
                    }
                }
            }

            //
            // Summary:
            //     Adds an item to the System.Collections.Generic.ICollection`1. This implementation
            //     always throws System.NotSupportedException.
            //
            // Parameters:
            //   item:
            //     The object to add to the System.Collections.Generic.ICollection`1.
            //
            // Exceptions:
            //   T:System.NotSupportedException:
            //     Always thrown.
            [__DynamicallyInvokable]
            void ICollection<TKey>.Add(TKey item)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
            }

            //
            // Summary:
            //     Removes all items from the System.Collections.Generic.ICollection`1. This implementation
            //     always throws System.NotSupportedException.
            //
            // Exceptions:
            //   T:System.NotSupportedException:
            //     Always thrown.
            [__DynamicallyInvokable]
            void ICollection<TKey>.Clear()
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
            }

            //
            // Summary:
            //     Determines whether the System.Collections.Generic.ICollection`1 contains a specific
            //     value.
            //
            // Parameters:
            //   item:
            //     The object to locate in the System.Collections.Generic.ICollection`1.
            //
            // Returns:
            //     true if item is found in the System.Collections.Generic.ICollection`1; otherwise,
            //     false.
            [__DynamicallyInvokable]
            bool ICollection<TKey>.Contains(TKey item)
            {
                return dictionary.ContainsKey(item);
            }

            //
            // Summary:
            //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection`1.
            //     This implementation always throws System.NotSupportedException.
            //
            // Parameters:
            //   item:
            //     The object to remove from the System.Collections.Generic.ICollection`1.
            //
            // Returns:
            //     true if item was successfully removed from the System.Collections.Generic.ICollection`1;
            //     otherwise, false. This method also returns false if item was not found in the
            //     original System.Collections.Generic.ICollection`1.
            //
            // Exceptions:
            //   T:System.NotSupportedException:
            //     Always thrown.
            [__DynamicallyInvokable]
            bool ICollection<TKey>.Remove(TKey item)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
                return false;
            }

            //
            // Summary:
            //     Returns an enumerator that iterates through a collection.
            //
            // Returns:
            //     An System.Collections.Generic.IEnumerator`1 that can be used to iterate through
            //     the collection.
            [__DynamicallyInvokable]
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            //
            // Summary:
            //     Returns an enumerator that iterates through a collection.
            //
            // Returns:
            //     An System.Collections.IEnumerator that can be used to iterate through the collection.
            [__DynamicallyInvokable]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            //
            // Summary:
            //     Copies the elements of the System.Collections.ICollection to an System.Array,
            //     starting at a particular System.Array index.
            //
            // Parameters:
            //   array:
            //     The one-dimensional System.Array that is the destination of the elements copied
            //     from System.Collections.ICollection. The System.Array must have zero-based indexing.
            //
            //   index:
            //     The zero-based index in array at which copying begins.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     array is null.
            //
            //   T:System.ArgumentOutOfRangeException:
            //     index is less than zero.
            //
            //   T:System.ArgumentException:
            //     array is multidimensional. -or- array does not have zero-based indexing. -or-
            //     The number of elements in the source System.Collections.ICollection is greater
            //     than the available space from index to the end of the destination array. -or-
            //     The type of the source System.Collections.ICollection cannot be cast automatically
            //     to the type of the destination array.
            [__DynamicallyInvokable]
            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (array.Rank != 1)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                }

                if (array.GetLowerBound(0) != 0)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                }

                if (index < 0 || index > array.Length)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                }

                if (array.Length - index < dictionary.Count)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                TKey[] array2 = array as TKey[];
                if (array2 != null)
                {
                    CopyTo(array2, index);
                    return;
                }

                object[] array3 = array as object[];
                if (array3 == null)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }

                int count = dictionary.count;
                Entry[] entries = dictionary.entries;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (entries[i].hashCode >= 0)
                        {
                            array3[index++] = entries[i].key;
                        }
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }
        }

        //
        // Summary:
        //     Represents the collection of values in a System.Collections.Generic.Dictionary`2.
        //     This class cannot be inherited.
        //
        // Type parameters:
        //   TKey:
        //
        //   TValue:
        [Serializable]
        [DebuggerTypeProxy(typeof(Mscorlib_DictionaryValueCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        [__DynamicallyInvokable]
        public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
        {
            //
            // Summary:
            //     Enumerates the elements of a System.Collections.Generic.Dictionary`2.ValueCollection.
            //
            // Type parameters:
            //   TKey:
            //
            //   TValue:
            [Serializable]
            [__DynamicallyInvokable]
            public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
            {
                private Dictionary<TKey, TValue> dictionary;

                private int index;

                private int version;

                private TValue currentValue;

                //
                // Summary:
                //     Gets the element at the current position of the enumerator.
                //
                // Returns:
                //     The element in the System.Collections.Generic.Dictionary`2.ValueCollection at
                //     the current position of the enumerator.
                [__DynamicallyInvokable]
                public TValue Current
                {
                    [__DynamicallyInvokable]
                    get
                    {
                        return currentValue;
                    }
                }

                //
                // Summary:
                //     Gets the element at the current position of the enumerator.
                //
                // Returns:
                //     The element in the collection at the current position of the enumerator.
                //
                // Exceptions:
                //   T:System.InvalidOperationException:
                //     The enumerator is positioned before the first element of the collection or after
                //     the last element.
                [__DynamicallyInvokable]
                object IEnumerator.Current
                {
                    [__DynamicallyInvokable]
                    get
                    {
                        if (index == 0 || index == dictionary.count + 1)
                        {
                            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                        }

                        return currentValue;
                    }
                }

                internal Enumerator(Dictionary<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
                    version = dictionary.version;
                    index = 0;
                    currentValue = default(TValue);
                }

                //
                // Summary:
                //     Releases all resources used by the System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator.
                [__DynamicallyInvokable]
                public void Dispose()
                {
                }

                //
                // Summary:
                //     Advances the enumerator to the next element of the System.Collections.Generic.Dictionary`2.ValueCollection.
                //
                // Returns:
                //     true if the enumerator was successfully advanced to the next element; false if
                //     the enumerator has passed the end of the collection.
                //
                // Exceptions:
                //   T:System.InvalidOperationException:
                //     The collection was modified after the enumerator was created.
                [__DynamicallyInvokable]
                public bool MoveNext()
                {
                    if (version != dictionary.version)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    }

                    while ((uint)index < (uint)dictionary.count)
                    {
                        if (dictionary.entries[index].hashCode >= 0)
                        {
                            currentValue = dictionary.entries[index].value;
                            index++;
                            return true;
                        }

                        index++;
                    }

                    index = dictionary.count + 1;
                    currentValue = default(TValue);
                    return false;
                }

                //
                // Summary:
                //     Sets the enumerator to its initial position, which is before the first element
                //     in the collection.
                //
                // Exceptions:
                //   T:System.InvalidOperationException:
                //     The collection was modified after the enumerator was created.
                [__DynamicallyInvokable]
                void IEnumerator.Reset()
                {
                    if (version != dictionary.version)
                    {
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    }

                    index = 0;
                    currentValue = default(TValue);
                }
            }

            private Dictionary<TKey, TValue> dictionary;

            //
            // Summary:
            //     Gets the number of elements contained in the System.Collections.Generic.Dictionary`2.ValueCollection.
            //
            // Returns:
            //     The number of elements contained in the System.Collections.Generic.Dictionary`2.ValueCollection.
            [__DynamicallyInvokable]
            public int Count
            {
                [__DynamicallyInvokable]
                get
                {
                    return dictionary.Count;
                }
            }

            //
            // Summary:
            //     Gets a value indicating whether the System.Collections.Generic.ICollection`1
            //     is read-only.
            //
            // Returns:
            //     true if the System.Collections.Generic.ICollection`1 is read-only; otherwise,
            //     false. In the default implementation of System.Collections.Generic.Dictionary`2.ValueCollection,
            //     this property always returns true.
            [__DynamicallyInvokable]
            bool ICollection<TValue>.IsReadOnly
            {
                [__DynamicallyInvokable]
                get
                {
                    return true;
                }
            }

            //
            // Summary:
            //     Gets a value indicating whether access to the System.Collections.ICollection
            //     is synchronized (thread safe).
            //
            // Returns:
            //     true if access to the System.Collections.ICollection is synchronized (thread
            //     safe); otherwise, false. In the default implementation of System.Collections.Generic.Dictionary`2.ValueCollection,
            //     this property always returns false.
            [__DynamicallyInvokable]
            bool ICollection.IsSynchronized
            {
                [__DynamicallyInvokable]
                get
                {
                    return false;
                }
            }

            //
            // Summary:
            //     Gets an object that can be used to synchronize access to the System.Collections.ICollection.
            //
            // Returns:
            //     An object that can be used to synchronize access to the System.Collections.ICollection.
            //     In the default implementation of System.Collections.Generic.Dictionary`2.ValueCollection,
            //     this property always returns the current instance.
            [__DynamicallyInvokable]
            object ICollection.SyncRoot
            {
                [__DynamicallyInvokable]
                get
                {
                    return ((ICollection)dictionary).SyncRoot;
                }
            }

            //
            // Summary:
            //     Initializes a new instance of the System.Collections.Generic.Dictionary`2.ValueCollection
            //     class that reflects the values in the specified System.Collections.Generic.Dictionary`2.
            //
            // Parameters:
            //   dictionary:
            //     The System.Collections.Generic.Dictionary`2 whose values are reflected in the
            //     new System.Collections.Generic.Dictionary`2.ValueCollection.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     dictionary is null.
            [__DynamicallyInvokable]
            public ValueCollection(Dictionary<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                }

                this.dictionary = dictionary;
            }

            //
            // Summary:
            //     Returns an enumerator that iterates through the System.Collections.Generic.Dictionary`2.ValueCollection.
            //
            // Returns:
            //     A System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator for the
            //     System.Collections.Generic.Dictionary`2.ValueCollection.
            [__DynamicallyInvokable]
            public Enumerator GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            //
            // Summary:
            //     Copies the System.Collections.Generic.Dictionary`2.ValueCollection elements to
            //     an existing one-dimensional System.Array, starting at the specified array index.
            //
            // Parameters:
            //   array:
            //     The one-dimensional System.Array that is the destination of the elements copied
            //     from System.Collections.Generic.Dictionary`2.ValueCollection. The System.Array
            //     must have zero-based indexing.
            //
            //   index:
            //     The zero-based index in array at which copying begins.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     array is null.
            //
            //   T:System.ArgumentOutOfRangeException:
            //     index is less than zero.
            //
            //   T:System.ArgumentException:
            //     The number of elements in the source System.Collections.Generic.Dictionary`2.ValueCollection
            //     is greater than the available space from index to the end of the destination
            //     array.
            [__DynamicallyInvokable]
            public void CopyTo(TValue[] array, int index)
            {
                if (array == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (index < 0 || index > array.Length)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                }

                if (array.Length - index < dictionary.Count)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                int count = dictionary.count;
                Entry[] entries = dictionary.entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0)
                    {
                        array[index++] = entries[i].value;
                    }
                }
            }

            //
            // Summary:
            //     Adds an item to the System.Collections.Generic.ICollection`1. This implementation
            //     always throws System.NotSupportedException.
            //
            // Parameters:
            //   item:
            //     The object to add to the System.Collections.Generic.ICollection`1.
            //
            // Exceptions:
            //   T:System.NotSupportedException:
            //     Always thrown.
            [__DynamicallyInvokable]
            void ICollection<TValue>.Add(TValue item)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
            }

            //
            // Summary:
            //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection`1.
            //     This implementation always throws System.NotSupportedException.
            //
            // Parameters:
            //   item:
            //     The object to remove from the System.Collections.Generic.ICollection`1.
            //
            // Returns:
            //     true if item was successfully removed from the System.Collections.Generic.ICollection`1;
            //     otherwise, false. This method also returns false if item was not found in the
            //     original System.Collections.Generic.ICollection`1.
            //
            // Exceptions:
            //   T:System.NotSupportedException:
            //     Always thrown.
            [__DynamicallyInvokable]
            bool ICollection<TValue>.Remove(TValue item)
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
                return false;
            }

            //
            // Summary:
            //     Removes all items from the System.Collections.Generic.ICollection`1. This implementation
            //     always throws System.NotSupportedException.
            //
            // Exceptions:
            //   T:System.NotSupportedException:
            //     Always thrown.
            [__DynamicallyInvokable]
            void ICollection<TValue>.Clear()
            {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
            }

            //
            // Summary:
            //     Determines whether the System.Collections.Generic.ICollection`1 contains a specific
            //     value.
            //
            // Parameters:
            //   item:
            //     The object to locate in the System.Collections.Generic.ICollection`1.
            //
            // Returns:
            //     true if item is found in the System.Collections.Generic.ICollection`1; otherwise,
            //     false.
            [__DynamicallyInvokable]
            bool ICollection<TValue>.Contains(TValue item)
            {
                return dictionary.ContainsValue(item);
            }

            //
            // Summary:
            //     Returns an enumerator that iterates through a collection.
            //
            // Returns:
            //     An System.Collections.Generic.IEnumerator`1 that can be used to iterate through
            //     the collection.
            [__DynamicallyInvokable]
            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            //
            // Summary:
            //     Returns an enumerator that iterates through a collection.
            //
            // Returns:
            //     An System.Collections.IEnumerator that can be used to iterate through the collection.
            [__DynamicallyInvokable]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            //
            // Summary:
            //     Copies the elements of the System.Collections.ICollection to an System.Array,
            //     starting at a particular System.Array index.
            //
            // Parameters:
            //   array:
            //     The one-dimensional System.Array that is the destination of the elements copied
            //     from System.Collections.ICollection. The System.Array must have zero-based indexing.
            //
            //   index:
            //     The zero-based index in array at which copying begins.
            //
            // Exceptions:
            //   T:System.ArgumentNullException:
            //     array is null.
            //
            //   T:System.ArgumentOutOfRangeException:
            //     index is less than zero.
            //
            //   T:System.ArgumentException:
            //     array is multidimensional. -or- array does not have zero-based indexing. -or-
            //     The number of elements in the source System.Collections.ICollection is greater
            //     than the available space from index to the end of the destination array. -or-
            //     The type of the source System.Collections.ICollection cannot be cast automatically
            //     to the type of the destination array.
            [__DynamicallyInvokable]
            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (array.Rank != 1)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                }

                if (array.GetLowerBound(0) != 0)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                }

                if (index < 0 || index > array.Length)
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                }

                if (array.Length - index < dictionary.Count)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                TValue[] array2 = array as TValue[];
                if (array2 != null)
                {
                    CopyTo(array2, index);
                    return;
                }

                object[] array3 = array as object[];
                if (array3 == null)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }

                int count = dictionary.count;
                Entry[] entries = dictionary.entries;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (entries[i].hashCode >= 0)
                        {
                            array3[index++] = entries[i].value;
                        }
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }
        }

        private int[] buckets;

        private Entry[] entries;

        private int count;

        private int version;

        private int freeList;

        private int freeCount;

        private IEqualityComparer<TKey> comparer;

        private KeyCollection keys;

        private ValueCollection values;

        private object _syncRoot;

        private const string VersionName = "Version";

        private const string HashSizeName = "HashSize";

        private const string KeyValuePairsName = "KeyValuePairs";

        private const string ComparerName = "Comparer";

        //
        // Summary:
        //     Gets the System.Collections.Generic.IEqualityComparer`1 that is used to determine
        //     equality of keys for the dictionary.
        //
        // Returns:
        //     The System.Collections.Generic.IEqualityComparer`1 generic interface implementation
        //     that is used to determine equality of keys for the current System.Collections.Generic.Dictionary`2
        //     and to provide hash values for the keys.
        [__DynamicallyInvokable]
        public IEqualityComparer<TKey> Comparer
        {
            [__DynamicallyInvokable]
            get
            {
                return comparer;
            }
        }

        //
        // Summary:
        //     Gets the number of key/value pairs contained in the System.Collections.Generic.Dictionary`2.
        //
        // Returns:
        //     The number of key/value pairs contained in the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        public int Count
        {
            [__DynamicallyInvokable]
            get
            {
                return count - freeCount;
            }
        }

        //
        // Summary:
        //     Gets a collection containing the keys in the System.Collections.Generic.Dictionary`2.
        //
        // Returns:
        //     A System.Collections.Generic.Dictionary`2.KeyCollection containing the keys in
        //     the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        public KeyCollection Keys
        {
            [__DynamicallyInvokable]
            get
            {
                if (keys == null)
                {
                    keys = new KeyCollection(this);
                }

                return keys;
            }
        }

        [__DynamicallyInvokable]
        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            [__DynamicallyInvokable]
            get
            {
                if (keys == null)
                {
                    keys = new KeyCollection(this);
                }

                return keys;
            }
        }

        [__DynamicallyInvokable]
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            [__DynamicallyInvokable]
            get
            {
                if (keys == null)
                {
                    keys = new KeyCollection(this);
                }

                return keys;
            }
        }

        //
        // Summary:
        //     Gets a collection containing the values in the System.Collections.Generic.Dictionary`2.
        //
        // Returns:
        //     A System.Collections.Generic.Dictionary`2.ValueCollection containing the values
        //     in the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        public ValueCollection Values
        {
            [__DynamicallyInvokable]
            get
            {
                if (values == null)
                {
                    values = new ValueCollection(this);
                }

                return values;
            }
        }

        [__DynamicallyInvokable]
        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            [__DynamicallyInvokable]
            get
            {
                if (values == null)
                {
                    values = new ValueCollection(this);
                }

                return values;
            }
        }

        [__DynamicallyInvokable]
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            [__DynamicallyInvokable]
            get
            {
                if (values == null)
                {
                    values = new ValueCollection(this);
                }

                return values;
            }
        }

        //
        // Summary:
        //     Gets or sets the value associated with the specified key.
        //
        // Parameters:
        //   key:
        //     The key of the value to get or set.
        //
        // Returns:
        //     The value associated with the specified key. If the specified key is not found,
        //     a get operation throws a System.Collections.Generic.KeyNotFoundException, and
        //     a set operation creates a new element with the specified key.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        //
        //   T:System.Collections.Generic.KeyNotFoundException:
        //     The property is retrieved and key does not exist in the collection.
        [__DynamicallyInvokable]
        public TValue this[TKey key]
        {
            [__DynamicallyInvokable]
            get
            {
                int num = FindEntry(key);
                if (num >= 0)
                {
                    return entries[num].value;
                }

                ThrowHelper.ThrowKeyNotFoundException();
                return default(TValue);
            }
            [__DynamicallyInvokable]
            set
            {
                Insert(key, value, add: false);
            }
        }

        [__DynamicallyInvokable]
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            [__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //
        // Summary:
        //     Gets a value that indicates whether access to the System.Collections.ICollection
        //     is synchronized (thread safe).
        //
        // Returns:
        //     true if access to the System.Collections.ICollection is synchronized (thread
        //     safe); otherwise, false. In the default implementation of System.Collections.Generic.Dictionary`2,
        //     this property always returns false.
        [__DynamicallyInvokable]
        bool ICollection.IsSynchronized
        {
            [__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //
        // Summary:
        //     Gets an object that can be used to synchronize access to the System.Collections.ICollection.
        //
        // Returns:
        //     An object that can be used to synchronize access to the System.Collections.ICollection.
        [__DynamicallyInvokable]
        object ICollection.SyncRoot
        {
            [__DynamicallyInvokable]
            get
            {
                if (_syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), (object)null);
                }

                return _syncRoot;
            }
        }

        //
        // Summary:
        //     Gets a value that indicates whether the System.Collections.IDictionary has a
        //     fixed size.
        //
        // Returns:
        //     true if the System.Collections.IDictionary has a fixed size; otherwise, false.
        //     In the default implementation of System.Collections.Generic.Dictionary`2, this
        //     property always returns false.
        [__DynamicallyInvokable]
        bool IDictionary.IsFixedSize
        {
            [__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //
        // Summary:
        //     Gets a value that indicates whether the System.Collections.IDictionary is read-only.
        //
        // Returns:
        //     true if the System.Collections.IDictionary is read-only; otherwise, false. In
        //     the default implementation of System.Collections.Generic.Dictionary`2, this property
        //     always returns false.
        [__DynamicallyInvokable]
        bool IDictionary.IsReadOnly
        {
            [__DynamicallyInvokable]
            get
            {
                return false;
            }
        }

        //
        // Summary:
        //     Gets an System.Collections.ICollection containing the keys of the System.Collections.IDictionary.
        //
        // Returns:
        //     An System.Collections.ICollection containing the keys of the System.Collections.IDictionary.
        [__DynamicallyInvokable]
        ICollection IDictionary.Keys
        {
            [__DynamicallyInvokable]
            get
            {
                return Keys;
            }
        }

        //
        // Summary:
        //     Gets an System.Collections.ICollection containing the values in the System.Collections.IDictionary.
        //
        // Returns:
        //     An System.Collections.ICollection containing the values in the System.Collections.IDictionary.
        [__DynamicallyInvokable]
        ICollection IDictionary.Values
        {
            [__DynamicallyInvokable]
            get
            {
                return Values;
            }
        }

        //
        // Summary:
        //     Gets or sets the value with the specified key.
        //
        // Parameters:
        //   key:
        //     The key of the value to get.
        //
        // Returns:
        //     The value associated with the specified key, or null if key is not in the dictionary
        //     or key is of a type that is not assignable to the key type TKey of the System.Collections.Generic.Dictionary`2.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        //
        //   T:System.ArgumentException:
        //     A value is being assigned, and key is of a type that is not assignable to the
        //     key type TKey of the System.Collections.Generic.Dictionary`2. -or- A value is
        //     being assigned, and value is of a type that is not assignable to the value type
        //     TValue of the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        object IDictionary.this[object key]
        {
            [__DynamicallyInvokable]
            get
            {
                if (IsCompatibleKey(key))
                {
                    int num = FindEntry((TKey)key);
                    if (num >= 0)
                    {
                        return entries[num].value;
                    }
                }

                return null;
            }
            [__DynamicallyInvokable]
            set
            {
                if (key == null)
                {
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
                }

                ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
                try
                {
                    TKey key2 = (TKey)key;
                    try
                    {
                        this[key2] = (TValue)value;
                    }
                    catch (InvalidCastException)
                    {
                        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                    }
                }
                catch (InvalidCastException)
                {
                    ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
                }
            }
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that is empty, has the default initial capacity, and uses the default equality
        //     comparer for the key type.
        [__DynamicallyInvokable]
        public Dictionary()
            : this(0, (IEqualityComparer<TKey>)null)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that is empty, has the specified initial capacity, and uses the default equality
        //     comparer for the key type.
        //
        // Parameters:
        //   capacity:
        //     The initial number of elements that the System.Collections.Generic.Dictionary`2
        //     can contain.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     capacity is less than 0.
        [__DynamicallyInvokable]
        public Dictionary(int capacity)
            : this(capacity, (IEqualityComparer<TKey>)null)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that is empty, has the default initial capacity, and uses the specified System.Collections.Generic.IEqualityComparer`1.
        //
        // Parameters:
        //   comparer:
        //     The System.Collections.Generic.IEqualityComparer`1 implementation to use when
        //     comparing keys, or null to use the default System.Collections.Generic.EqualityComparer`1
        //     for the type of the key.
        [__DynamicallyInvokable]
        public Dictionary(IEqualityComparer<TKey> comparer)
            : this(0, comparer)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that is empty, has the specified initial capacity, and uses the specified System.Collections.Generic.IEqualityComparer`1.
        //
        // Parameters:
        //   capacity:
        //     The initial number of elements that the System.Collections.Generic.Dictionary`2
        //     can contain.
        //
        //   comparer:
        //     The System.Collections.Generic.IEqualityComparer`1 implementation to use when
        //     comparing keys, or null to use the default System.Collections.Generic.EqualityComparer`1
        //     for the type of the key.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     capacity is less than 0.
        [__DynamicallyInvokable]
        public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            }

            if (capacity > 0)
            {
                Initialize(capacity);
            }

            this.comparer = (comparer ?? EqualityComparer<TKey>.Default);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that contains elements copied from the specified System.Collections.Generic.IDictionary`2
        //     and uses the default equality comparer for the key type.
        //
        // Parameters:
        //   dictionary:
        //     The System.Collections.Generic.IDictionary`2 whose elements are copied to the
        //     new System.Collections.Generic.Dictionary`2.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     dictionary is null.
        //
        //   T:System.ArgumentException:
        //     dictionary contains one or more duplicate keys.
        [__DynamicallyInvokable]
        public Dictionary(IDictionary<TKey, TValue> dictionary)
            : this(dictionary, (IEqualityComparer<TKey>)null)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     that contains elements copied from the specified System.Collections.Generic.IDictionary`2
        //     and uses the specified System.Collections.Generic.IEqualityComparer`1.
        //
        // Parameters:
        //   dictionary:
        //     The System.Collections.Generic.IDictionary`2 whose elements are copied to the
        //     new System.Collections.Generic.Dictionary`2.
        //
        //   comparer:
        //     The System.Collections.Generic.IEqualityComparer`1 implementation to use when
        //     comparing keys, or null to use the default System.Collections.Generic.EqualityComparer`1
        //     for the type of the key.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     dictionary is null.
        //
        //   T:System.ArgumentException:
        //     dictionary contains one or more duplicate keys.
        [__DynamicallyInvokable]
        public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : this(dictionary?.Count ?? 0, comparer)
        {
            if (dictionary == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
            }

            foreach (KeyValuePair<TKey, TValue> item in dictionary)
            {
                Add(item.Key, item.Value);
            }
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.Dictionary`2 class
        //     with serialized data.
        //
        // Parameters:
        //   info:
        //     A System.Runtime.Serialization.SerializationInfo object containing the information
        //     required to serialize the System.Collections.Generic.Dictionary`2.
        //
        //   context:
        //     A System.Runtime.Serialization.StreamingContext structure containing the source
        //     and destination of the serialized stream associated with the System.Collections.Generic.Dictionary`2.
        protected Dictionary(SerializationInfo info, StreamingContext context)
        {
            HashHelpers.SerializationInfoTable.Add(this, info);
        }

        //
        // Summary:
        //     Adds the specified key and value to the dictionary.
        //
        // Parameters:
        //   key:
        //     The key of the element to add.
        //
        //   value:
        //     The value of the element to add. The value can be null for reference types.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        //
        //   T:System.ArgumentException:
        //     An element with the same key already exists in the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        public void Add(TKey key, TValue value)
        {
            Insert(key, value, add: true);
        }

        [__DynamicallyInvokable]
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair.Key, keyValuePair.Value);
        }

        [__DynamicallyInvokable]
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int num = FindEntry(keyValuePair.Key);
            if (num >= 0 && EqualityComparer<TValue>.Default.Equals(entries[num].value, keyValuePair.Value))
            {
                return true;
            }

            return false;
        }

        [__DynamicallyInvokable]
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int num = FindEntry(keyValuePair.Key);
            if (num >= 0 && EqualityComparer<TValue>.Default.Equals(entries[num].value, keyValuePair.Value))
            {
                Remove(keyValuePair.Key);
                return true;
            }

            return false;
        }

        //
        // Summary:
        //     Removes all keys and values from the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        public void Clear()
        {
            if (count > 0)
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = -1;
                }

                Array.Clear(entries, 0, count);
                freeList = -1;
                count = 0;
                freeCount = 0;
                version++;
            }
        }

        //
        // Summary:
        //     Determines whether the System.Collections.Generic.Dictionary`2 contains the specified
        //     key.
        //
        // Parameters:
        //   key:
        //     The key to locate in the System.Collections.Generic.Dictionary`2.
        //
        // Returns:
        //     true if the System.Collections.Generic.Dictionary`2 contains an element with
        //     the specified key; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        [__DynamicallyInvokable]
        public bool ContainsKey(TKey key)
        {
            return FindEntry(key) >= 0;
        }

        //
        // Summary:
        //     Determines whether the System.Collections.Generic.Dictionary`2 contains a specific
        //     value.
        //
        // Parameters:
        //   value:
        //     The value to locate in the System.Collections.Generic.Dictionary`2. The value
        //     can be null for reference types.
        //
        // Returns:
        //     true if the System.Collections.Generic.Dictionary`2 contains an element with
        //     the specified value; otherwise, false.
        [__DynamicallyInvokable]
        public bool ContainsValue(TValue value)
        {
            if (value == null)
            {
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0 && entries[i].value == null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
                for (int j = 0; j < count; j++)
                {
                    if (entries[j].hashCode >= 0 && @default.Equals(entries[j].value, value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (index < 0 || index > array.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (array.Length - index < Count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            int num = count;
            Entry[] array2 = entries;
            for (int i = 0; i < num; i++)
            {
                if (array2[i].hashCode >= 0)
                {
                    array[index++] = new KeyValuePair<TKey, TValue>(array2[i].key, array2[i].value);
                }
            }
        }

        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.Generic.Dictionary`2.
        //
        // Returns:
        //     A System.Collections.Generic.Dictionary`2.Enumerator structure for the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this, 2);
        }

        [__DynamicallyInvokable]
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this, 2);
        }

        //
        // Summary:
        //     Implements the System.Runtime.Serialization.ISerializable interface and returns
        //     the data needed to serialize the System.Collections.Generic.Dictionary`2 instance.
        //
        // Parameters:
        //   info:
        //     A System.Runtime.Serialization.SerializationInfo object that contains the information
        //     required to serialize the System.Collections.Generic.Dictionary`2 instance.
        //
        //   context:
        //     A System.Runtime.Serialization.StreamingContext structure that contains the source
        //     and destination of the serialized stream associated with the System.Collections.Generic.Dictionary`2
        //     instance.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     info is null.
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
            }

            info.AddValue("Version", version);
            info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization(comparer), typeof(IEqualityComparer<TKey>));
            info.AddValue("HashSize", (buckets != null) ? buckets.Length : 0);
            if (buckets != null)
            {
                KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[Count];
                CopyTo(array, 0);
                info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
            }
        }

        private int FindEntry(TKey key)
        {
            if (key == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (buckets != null)
            {
                int num = comparer.GetHashCode(key) & int.MaxValue;
                for (int num2 = buckets[num % buckets.Length]; num2 >= 0; num2 = entries[num2].next)
                {
                    if (entries[num2].hashCode == num && comparer.Equals(entries[num2].key, key))
                    {
                        return num2;
                    }
                }
            }

            return -1;
        }

        private void Initialize(int capacity)
        {
            int prime = HashHelpers.GetPrime(capacity);
            buckets = new int[prime];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }

            entries = new Entry[prime];
            freeList = -1;
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            if (key == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (buckets == null)
            {
                Initialize(0);
            }

            int num = comparer.GetHashCode(key) & int.MaxValue;
            int num2 = num % buckets.Length;
            int num3 = 0;
            for (int num4 = buckets[num2]; num4 >= 0; num4 = entries[num4].next)
            {
                if (entries[num4].hashCode == num && comparer.Equals(entries[num4].key, key))
                {
                    if (add)
                    {
                        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
                    }

                    entries[num4].value = value;
                    version++;
                    return;
                }

                num3++;
            }

            int num5;
            if (freeCount > 0)
            {
                num5 = freeList;
                freeList = entries[num5].next;
                freeCount--;
            }
            else
            {
                if (count == entries.Length)
                {
                    Resize();
                    num2 = num % buckets.Length;
                }

                num5 = count;
                count++;
            }

            entries[num5].hashCode = num;
            entries[num5].next = buckets[num2];
            entries[num5].key = key;
            entries[num5].value = value;
            buckets[num2] = num5;
            version++;
            if (num3 > 100 && HashHelpers.IsWellKnownEqualityComparer(comparer))
            {
                comparer = (IEqualityComparer<TKey>)HashHelpers.GetRandomizedEqualityComparer(comparer);
                Resize(entries.Length, forceNewHashCodes: true);
            }
        }

        //
        // Summary:
        //     Implements the System.Runtime.Serialization.ISerializable interface and raises
        //     the deserialization event when the deserialization is complete.
        //
        // Parameters:
        //   sender:
        //     The source of the deserialization event.
        //
        // Exceptions:
        //   T:System.Runtime.Serialization.SerializationException:
        //     The System.Runtime.Serialization.SerializationInfo object associated with the
        //     current System.Collections.Generic.Dictionary`2 instance is invalid.
        public virtual void OnDeserialization(object sender)
        {
            HashHelpers.SerializationInfoTable.TryGetValue(this, out SerializationInfo value);
            if (value == null)
            {
                return;
            }

            int @int = value.GetInt32("Version");
            int int2 = value.GetInt32("HashSize");
            comparer = (IEqualityComparer<TKey>)value.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
            if (int2 != 0)
            {
                buckets = new int[int2];
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = -1;
                }

                entries = new Entry[int2];
                freeList = -1;
                KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])value.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
                if (array == null)
                {
                    ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
                }

                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j].Key == null)
                    {
                        ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
                    }

                    Insert(array[j].Key, array[j].Value, add: true);
                }
            }
            else
            {
                buckets = null;
            }

            version = @int;
            HashHelpers.SerializationInfoTable.Remove(this);
        }

        private void Resize()
        {
            Resize(HashHelpers.ExpandPrime(count), forceNewHashCodes: false);
        }

        private void Resize(int newSize, bool forceNewHashCodes)
        {
            int[] array = new int[newSize];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = -1;
            }

            Entry[] array2 = new Entry[newSize];
            Array.Copy(entries, 0, array2, 0, count);
            if (forceNewHashCodes)
            {
                for (int j = 0; j < count; j++)
                {
                    if (array2[j].hashCode != -1)
                    {
                        array2[j].hashCode = (comparer.GetHashCode(array2[j].key) & int.MaxValue);
                    }
                }
            }

            for (int k = 0; k < count; k++)
            {
                if (array2[k].hashCode >= 0)
                {
                    int num = array2[k].hashCode % newSize;
                    array2[k].next = array[num];
                    array[num] = k;
                }
            }

            buckets = array;
            entries = array2;
        }

        //
        // Summary:
        //     Removes the value with the specified key from the System.Collections.Generic.Dictionary`2.
        //
        // Parameters:
        //   key:
        //     The key of the element to remove.
        //
        // Returns:
        //     true if the element is successfully found and removed; otherwise, false. This
        //     method returns false if key is not found in the System.Collections.Generic.Dictionary`2.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        [__DynamicallyInvokable]
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (buckets != null)
            {
                int num = comparer.GetHashCode(key) & int.MaxValue;
                int num2 = num % buckets.Length;
                int num3 = -1;
                for (int num4 = buckets[num2]; num4 >= 0; num4 = entries[num4].next)
                {
                    if (entries[num4].hashCode == num && comparer.Equals(entries[num4].key, key))
                    {
                        if (num3 < 0)
                        {
                            buckets[num2] = entries[num4].next;
                        }
                        else
                        {
                            entries[num3].next = entries[num4].next;
                        }

                        entries[num4].hashCode = -1;
                        entries[num4].next = freeList;
                        entries[num4].key = default(TKey);
                        entries[num4].value = default(TValue);
                        freeList = num4;
                        freeCount++;
                        version++;
                        return true;
                    }

                    num3 = num4;
                }
            }

            return false;
        }

        //
        // Summary:
        //     Gets the value associated with the specified key.
        //
        // Parameters:
        //   key:
        //     The key of the value to get.
        //
        //   value:
        //     When this method returns, contains the value associated with the specified key,
        //     if the key is found; otherwise, the default value for the type of the value parameter.
        //     This parameter is passed uninitialized.
        //
        // Returns:
        //     true if the System.Collections.Generic.Dictionary`2 contains an element with
        //     the specified key; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        [__DynamicallyInvokable]
        public bool TryGetValue(TKey key, out TValue value)
        {
            int num = FindEntry(key);
            if (num >= 0)
            {
                value = entries[num].value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        internal TValue GetValueOrDefault(TKey key)
        {
            int num = FindEntry(key);
            if (num >= 0)
            {
                return entries[num].value;
            }

            return default(TValue);
        }

        [__DynamicallyInvokable]
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            CopyTo(array, index);
        }

        //
        // Summary:
        //     Copies the elements of the System.Collections.Generic.ICollection`1 to an array,
        //     starting at the specified array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional array that is the destination of the elements copied from
        //     System.Collections.Generic.ICollection`1. The array must have zero-based indexing.
        //
        //   index:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0.
        //
        //   T:System.ArgumentException:
        //     array is multidimensional. -or- array does not have zero-based indexing. -or-
        //     The number of elements in the source System.Collections.Generic.ICollection`1
        //     is greater than the available space from index to the end of the destination
        //     array. -or- The type of the source System.Collections.Generic.ICollection`1 cannot
        //     be cast automatically to the type of the destination array.
        [__DynamicallyInvokable]
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (array.Rank != 1)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }

            if (array.GetLowerBound(0) != 0)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            }

            if (index < 0 || index > array.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (array.Length - index < Count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
            if (array2 != null)
            {
                CopyTo(array2, index);
                return;
            }

            if (array is DictionaryEntry[])
            {
                DictionaryEntry[] array3 = array as DictionaryEntry[];
                Entry[] array4 = entries;
                for (int i = 0; i < count; i++)
                {
                    if (array4[i].hashCode >= 0)
                    {
                        array3[index++] = new DictionaryEntry(array4[i].key, array4[i].value);
                    }
                }

                return;
            }

            object[] array5 = array as object[];
            if (array5 == null)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
            }

            try
            {
                int num = count;
                Entry[] array6 = entries;
                for (int j = 0; j < num; j++)
                {
                    if (array6[j].hashCode >= 0)
                    {
                        array5[index++] = new KeyValuePair<TKey, TValue>(array6[j].key, array6[j].value);
                    }
                }
            }
            catch (ArrayTypeMismatchException)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
            }
        }

        //
        // Summary:
        //     Returns an enumerator that iterates through the collection.
        //
        // Returns:
        //     An System.Collections.IEnumerator that can be used to iterate through the collection.
        [__DynamicallyInvokable]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this, 2);
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            return key is TKey;
        }

        //
        // Summary:
        //     Adds the specified key and value to the dictionary.
        //
        // Parameters:
        //   key:
        //     The object to use as the key.
        //
        //   value:
        //     The object to use as the value.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        //
        //   T:System.ArgumentException:
        //     key is of a type that is not assignable to the key type TKey of the System.Collections.Generic.Dictionary`2.
        //     -or- value is of a type that is not assignable to TValue, the type of values
        //     in the System.Collections.Generic.Dictionary`2. -or- A value with the same key
        //     already exists in the System.Collections.Generic.Dictionary`2.
        [__DynamicallyInvokable]
        void IDictionary.Add(object key, object value)
        {
            if (key == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
            try
            {
                TKey key2 = (TKey)key;
                try
                {
                    Add(key2, (TValue)value);
                }
                catch (InvalidCastException)
                {
                    ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                }
            }
            catch (InvalidCastException)
            {
                ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
            }
        }

        //
        // Summary:
        //     Determines whether the System.Collections.IDictionary contains an element with
        //     the specified key.
        //
        // Parameters:
        //   key:
        //     The key to locate in the System.Collections.IDictionary.
        //
        // Returns:
        //     true if the System.Collections.IDictionary contains an element with the specified
        //     key; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        [__DynamicallyInvokable]
        bool IDictionary.Contains(object key)
        {
            if (IsCompatibleKey(key))
            {
                return ContainsKey((TKey)key);
            }

            return false;
        }

        //
        // Summary:
        //     Returns an System.Collections.IDictionaryEnumerator for the System.Collections.IDictionary.
        //
        // Returns:
        //     An System.Collections.IDictionaryEnumerator for the System.Collections.IDictionary.
        [__DynamicallyInvokable]
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new Enumerator(this, 1);
        }

        //
        // Summary:
        //     Removes the element with the specified key from the System.Collections.IDictionary.
        //
        // Parameters:
        //   key:
        //     The key of the element to remove.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     key is null.
        [__DynamicallyInvokable]
        void IDictionary.Remove(object key)
        {
            if (IsCompatibleKey(key))
            {
                Remove((TKey)key);
            }
        }
    }
}
#if false // Decompilation log
'10' items in cache
#endif
