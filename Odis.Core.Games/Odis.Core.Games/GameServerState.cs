namespace Odis.Core.Games
{
    /// <summary>
    /// All game server states
    /// </summary>
    public enum GameServerState
    {
        /// <summary>
        /// Occurs when server is stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// Occurs when server is stopping
        /// </summary>
        Stopping,
        /// <summary>
        /// Occurs when server is starting
        /// </summary>
        Starting,
        /// <summary>
        /// Occurs when server is started
        /// </summary>
        Started,
    }
}