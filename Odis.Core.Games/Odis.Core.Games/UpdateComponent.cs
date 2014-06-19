namespace Odis.Core.Games
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UpdateComponent : IUpdateComponent
    {
        /// <summary>
        /// Initialize the UpdateComponent
        /// </summary>
        public virtual void Initialize()
        {
            
        }

        /// <summary>
        /// Entity that own the component
        /// </summary>
        public IEntity Entity { get; set; }
        /// <summary>
        /// Update the component
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}