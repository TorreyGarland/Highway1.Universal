namespace Highway1.Universal
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>Collection extensions class/module.</summary>
    public static class CollectionExtensions
    {

        /// <summary>Adds the many.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="enumerable">The enumerable.</param>
        public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(collection != null, nameof(collection));
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            enumerable.Each(collection.Add);
        }

        /// <summary>Clears the and add many.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="enumerable">The enumerable.</param>
        public static void ClearAndAddMany<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(collection != null, nameof(collection));
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            collection.Clear();
            collection.AddMany(enumerable);
        }

        /// <summary>Distincts the specified equals.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="equals">The equals.</param>
        /// <param name="hashCode">The hash code.</param>
        /// <returns></returns>
        [Pure]
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> enumerable, Func<T, T, bool> equals, Func<T, int> hashCode = null)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Requires<ArgumentNullException>(equals != null, nameof(equals));
            return enumerable.Distinct(new DelegateEqualityComparer<T>(equals, hashCode));
        }

        /// <summary>Eaches the specified action.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        /// <param name="continueIfItemIsNull">if set to <c>true</c> [continue if item is null].</param>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action, bool continueIfItemIsNull = true)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));
            foreach (var item in enumerable)
            {
                if (ReferenceEquals(item, null) && continueIfItemIsNull)
                    continue;
                action(item);
            }
        }

        /// <summary>Joineds the specified separator.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        [Pure]
        public static string Joined<T>(this IEnumerable<T> enumerable, string separator = null)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<string>() != null, nameof(CollectionExtensions.Joined));
            return string.Join(separator, enumerable);
        }

        /// <summary>To the collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        [Pure]
        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<Collection<T>>() != null, nameof(CollectionExtensions.ToCollection));
            return new Collection<T>(enumerable.ToList());
        }

        /// <summary>To the hash set.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <returns></returns>
        [Pure]
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> equalityComparer = null)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<HashSet<T>>() != null, nameof(CollectionExtensions.ToHashSet));
            return new HashSet<T>(enumerable, equalityComparer);
        }

        /// <summary>To the linked list.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        [Pure]
        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<LinkedList<T>>() != null, nameof(CollectionExtensions.ToLinkedList));
            return new LinkedList<T>(enumerable);
        }

        /// <summary>To the queue.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        [Pure]
        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<Queue<T>>() != null, nameof(CollectionExtensions.ToQueue));
            return new Queue<T>(enumerable);
        }

        /// <summary>To the observable collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        [Pure]
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<ObservableCollection<T>>() != null, nameof(CollectionExtensions.ToObservableCollection));
            return new ObservableCollection<T>(enumerable);
        }

        /// <summary>To the read only observable collection.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        [Pure]
        public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<ReadOnlyObservableCollection<T>>() != null, nameof(CollectionExtensions.ToReadOnlyObservableCollection));
            return new ReadOnlyObservableCollection<T>(enumerable.ToObservableCollection());
        }

        /// <summary>To the sorted dictionary.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        [Pure]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, nameof(dictionary));
            Contract.Ensures(Contract.Result<SortedDictionary<TKey, TValue>>() != null, nameof(CollectionExtensions.ToSortedDictionary));
            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>To the sorted list.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        [Pure]
        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, nameof(dictionary));
            Contract.Ensures(Contract.Result<SortedList<TKey, TValue>>() != null, nameof(CollectionExtensions.ToSortedList));
            return new SortedList<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>To the sorted set.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        [Pure]
        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> enumerable, IComparer<T> comparer = null)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<SortedSet<T>>() != null, nameof(CollectionExtensions.ToSortedSet));
            return new SortedSet<T>(enumerable, comparer);
        }

        /// <summary>To the stack.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        [Pure]
        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            Contract.Requires<ArgumentNullException>(enumerable != null, nameof(enumerable));
            Contract.Ensures(Contract.Result<Stack<T>>() != null, nameof(CollectionExtensions.ToStack));
            return new Stack<T>(enumerable);
        }

    }

}