using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Odis.Core.Games.Arrays
{
    /// <summary>
    /// Implements a circular buffer in an array by redefining array's accessors
    /// </summary>
    public class CircularArray<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Stores the underlying items
        /// </summary>
        T[] items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CircularArray class
        /// </summary>
        /// <param name="size">The size of the array</param>
        public CircularArray(int size)
        {
            this.SetSize(size);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the capacity of the underlying array
        /// </summary>
        public int Capacity
        {
            get { return this.items.Length; }
        }

        /// <summary>
        /// Gets or sets the array at an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The object of type T at the modulated index</returns>
        public T this[int index]
        {
            get { return this.items[((index % this.Capacity) + this.Capacity) % this.Capacity]; }
            set { this.items[((index % this.Capacity) + this.Capacity) % this.Capacity] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clear the array
        /// </summary>
        public void Clear()
        {
            this.SetSize(this.Capacity);
        }

        /// <summary>
        /// Set the size of the internal buffer
        /// </summary>
        /// <param name="size">The size, in elements count, of the array</param>
        private void SetSize(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("The size of the list cannot be negative or equals to zero");
            }

            this.items = new T[size];
        }

        #endregion

        /// <summary>
        /// Get the buffered enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.items.AsEnumerable().GetEnumerator();
        }

        /// <summary>
        /// Get the buffered enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
