using System.Collections.Concurrent;

namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// Collection of messages
    /// </summary>
    public class MessageCollection : ConcurrentQueue<IMessage>
    {
    }
}
