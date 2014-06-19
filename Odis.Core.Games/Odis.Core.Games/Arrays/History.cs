using System;

namespace Odis.Core.Games.Arrays
{
    /// <summary>
    /// Implements a generic data history
    /// </summary>
    public class History<T> : CircularArray<T>
    {
        #region Fields

        /// <summary>
        /// Stores the count of the elements that already have been stored
        /// in the array
        /// </summary>
        private int count;

        /// <summary>
        /// Stores the internal pointer, to know where to insert the next
        /// value in the array
        /// </summary>
        int pointer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the History class
        /// </summary>
        /// <param name="size">The size of the history</param>
        public History(int size)
            : base(size)
        {
            this.pointer = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements in the array that can
        /// be recovered.
        /// <remarks>This value can not be over the size of the array</remarks>
        /// </summary>
        public int Count
        {
            get { return this.count; }
        }

        /// <summary>
        /// Gets the last item added to the History
        /// </summary>
        public T Current
        {
            get { return this[this.pointer - 1]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add an item to the history
        /// </summary>
        /// <param name="item">The item to add to the history</param>
        public virtual void Add(T item)
        {
            // We add the item to the current index pointer
            this[this.pointer] = item;

            // We increment the pointer by one to prepair the next entry
            this.pointer++;

            // If the pointer is greater than the capacity, decrement it by Capacity.
            // This operation is not really necessary as this[int] is taking care
            // of modulo operations but not doing this could lead to overflow exceptions
            // when running history for a long time.
            if (this.pointer >= this.Capacity)
            {
                this.pointer -= this.Capacity;
            }

            // Update the value of count property, if it is under the Capacity.
            // We do not need to increment it if the full capacity has been reached
            // because we are also removing the last element in this case.
            if (this.count < this.Capacity)
            {
                this.count++;
            }
        }

        /// <summary>
        /// Get the value that was stored some frames ago
        /// </summary>
        ///<param name="value">The number of frame to look back</param>
        public T Preceding(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("value can't be under or equals to 0.");
            }

            // The history doesn't store enough items, so return the default value for T
            if (value >= this.Count)
            {
                return default(T);
            }

            return (this[this.pointer - 1 - value]);
        }

        #endregion
    }
}
