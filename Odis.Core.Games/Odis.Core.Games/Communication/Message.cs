using System;

namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// tcp/ip message
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// Contenu du Message
        /// </summary>
        public String RawData { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        public Message(string content)
        {
            RawData = content;
        }
    }
}