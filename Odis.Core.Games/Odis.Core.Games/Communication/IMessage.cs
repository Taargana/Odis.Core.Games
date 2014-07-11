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
    /// <typeparam name="T">the type of the content of the message</typeparam>
    public interface IMessage<out T> : IMessage where T : class, new()
    {
        /// <summary>
        /// Strong typed message dataItem
        /// </summary>
        T DataItem { get; }
    }
}
