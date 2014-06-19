namespace Odis.Core.Games
{
    /// <summary>
    /// Specific component that are used by the GameManager (like Game or Clock manager)
    /// </summary>
    public interface IGameManagerComponent : IUpdatable
    {
        /// <summary>
        /// Current Game Manager
        /// </summary>
        GameManager GameManager { get; set; }
    }
}