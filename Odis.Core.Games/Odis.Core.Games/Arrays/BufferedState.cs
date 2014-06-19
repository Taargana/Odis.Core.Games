namespace Odis.Core.Games.Arrays
{
    /// <summary>
    /// Implements a double state History
    /// </summary>
    public class BufferedState<T> : History<T>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BufferedState class
        /// </summary>
        public BufferedState()
            : base(2)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the previous value
        /// </summary>
        public T Previous
        {
            get { return this.Preceding(1); }
        }

        #endregion

        #region Methods

        #endregion
    }
}
