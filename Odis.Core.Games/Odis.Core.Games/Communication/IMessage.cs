using System;

namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// Define a standard message exchande with socket
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Content of the message
        /// </summary>
        String RawData { get; }
    }

    /// <summary>
    /// Generic Message
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessage<T> : IMessage
    {
        /// <summary>
        /// Strong typed message dataItem
        /// </summary>
        T DataItem { get; }
    }
}
