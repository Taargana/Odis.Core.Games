namespace Odis.Core.Games
{
    /// <summary>
    /// Updatable Elements
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Update method that is called by the server loop
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);
    }
}