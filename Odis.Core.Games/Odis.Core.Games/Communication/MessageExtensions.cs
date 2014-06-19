namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// Extesnion class for messages
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// try to change the message type
        /// </summary>
        /// <typeparam name="T">type expected</typeparam>
        /// <param name="message">current message</param>
        /// <returns>return null if cast doesn't work</returns>
        public static T TryCast<T>(this IMessage message) where T: class, IMessage
        {
            return message as T;
        }
    }
}
