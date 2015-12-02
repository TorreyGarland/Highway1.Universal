namespace Highway1.Universal.Collections
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Windows.Foundation.Collections;
    using Windows.Foundation.Metadata;

    /// <summary>Observable dictionary class.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [WebHostHidden]
    public class ObservableDictionary<TKey, TValue> : IObservableMap<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Dictionary<TKey, TValue> _dictionary;

        #endregion

        #region Properties

        TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key]
            => this[key];

        /// <summary>Gets or sets the <see cref="TValue"/> with the specified key.</summary>
        /// <value>The <see cref="TValue"/>.</value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set
            {
                _dictionary[key] = value;
                OnMapChanged(CollectionChange.ItemChanged, key);
            }
        }

        /// <summary>Gets or sets the count.</summary>
        /// <value>The count.</value>
        public int Count
            => _dictionary.Count;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
            => false;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
            => Keys;

        /// <summary>Gets or sets the keys.</summary>
        /// <value>The keys.</value>
        public ICollection<TKey> Keys
            => _dictionary.Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
            => Values;

        /// <summary>Gets or sets the values.</summary>
        /// <value>The values.</value>
        public ICollection<TValue> Values
            => _dictionary.Values;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        public ObservableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ObservableDictionary(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, nameof(capacity));
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, nameof(dictionary));
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, nameof(capacity));
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, nameof(dictionary));
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>Adds the specified item.</summary>
        /// <param name="item">The item.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
            => Add(item.Key, item.Value);

        /// <summary>Adds the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            OnMapChanged(CollectionChange.ItemInserted, key);
        }

        /// <summary>Clears this instance.</summary>
        public void Clear()
        {
            var keys = Keys.ToArray();
            _dictionary.Clear();
            keys.Each(key => MapChanged?.Invoke(this, MapChangedEventArgs<TKey>.Create(key, CollectionChange.ItemRemoved)));
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
            => _dictionary.Contains(item);

        bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key)
            => ContainsKey(key);

        /// <summary>Determines whether the specified key contains key.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
            => _dictionary.ContainsKey(key);

        /// <summary>Copies to.</summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var arraySize = array.Length;
            foreach (var pair in _dictionary)
            {
                if (arrayIndex >= arraySize)
                    break;
                array[arrayIndex++] = pair;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>Gets the enumerator.</summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => _dictionary.GetEnumerator();

        /// <summary>
        /// Called when [map changed].
        /// </summary>
        /// <param name="collectionChange">The collection change.</param>
        /// <param name="key">The key.</param>
        protected virtual void OnMapChanged(CollectionChange collectionChange, TKey key = default(TKey))
            => MapChanged?.Invoke(this, MapChangedEventArgs<TKey>.Create(key, collectionChange));

        /// <summary>Removes the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
            => Remove(item.Key);

        /// <summary>Removes the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (_dictionary.Remove(key))
            {
                OnMapChanged(CollectionChange.ItemRemoved, key);
                return true;
            }
            return false;
        }

        bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
            => TryGetValue(key, out value);

        /// <summary>Tries the get value.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
            => _dictionary.TryGetValue(key, out value);

        #endregion

        #region Events

        /// <summary>Occurs when [map changed].</summary>
        public event MapChangedEventHandler<TKey, TValue> MapChanged;

        #endregion

    }

}