using System;
using System.Collections.Generic;
using System.Text;

namespace REghZyFramework.Arrays
{
    /// <summary>
    ///     An extension to the normal Dictionary, But where <typeparamref name="TKey"/> returns a <see cref="HashSet{TValue}"/> rather than a single value.
    /// <para>
    ///     When adding to the same <typeparamref name="TKey"/>, it will fetch the <see cref="HashSet{TValue}"/> at the given key, and add the 
    ///     <typeparamref name="TValue"/> to that. If the <see cref="HashSet{TValue}"/> doesn't exist, it's created
    /// </para>
    /// <para>
    ///     When removing, you have the <see cref="Remove(TKey, TValue)"/> function to remove a <typeparamref name="TValue"/> from the 
    ///     <see cref="HashSet{TValue}"/> at the given <typeparamref name="TKey"/>. Same with the <see cref="ContainsValue(TKey, TValue)"/> function
    /// </para>
    /// </summary>
    /// <typeparam name="TKey">The key</typeparam>
    /// <typeparam name="TValue">The map's value</typeparam>
    public class Multimap<TKey, TValue> : Dictionary<TKey, HashSet<TValue>>
    {
        /// <summary>
        /// Adds a value to the map which the key points to. If the map doesn't exist, it is created
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (!TryGetValue(key, out HashSet<TValue> container))
            {
                container = new HashSet<TValue>();
                base.Add(key, container);
            }
            container.Add(value);
        }

        /// <summary>
        /// Checks if the map at the given key contains the given value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(TKey key, TValue value)
        {
            if (TryGetValue(key, out HashSet<TValue> values))
            {
                return values.Contains(value);
            }
            return false;
        }

        /// <summary>
        /// Removes a value from the map which key points to. Removes the entire map (which key points to) if there's no values left in it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(TKey key, TValue value)
        {
            if (TryGetValue(key, out HashSet<TValue> values))
            {
                bool removed = values.Remove(value);
                if (values.Count == 0)
                {
                    return base.Remove(key) && removed;
                }
            }
            return false;
        }
    }
}
