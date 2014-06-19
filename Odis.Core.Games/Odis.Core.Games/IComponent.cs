namespace Odis.Core.Games
{
    /// <summary>
    /// Define a component
    /// </summary>
    public interface IComponent : IInitializer
    {
        /// <summary>
        /// Entity that own the component
        /// </summary>
        IEntity Entity { get; set; }
    }
}