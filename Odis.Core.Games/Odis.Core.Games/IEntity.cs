using System;
using System.Collections.Generic;

namespace Odis.Core.Games
{
    /// <summary>
    /// An entity
    /// </summary>
    public interface IEntity : IInitializer
    {
        /// <summary>
        /// Components own by the entity
        /// </summary>
        IList<IUpdateComponent> Components { get; }

        /// <summary>
        /// Name Of the Entity
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Add a new component to the entity
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        IEntity AddComponent(IUpdateComponent component);

        /// <summary>
        /// Current Game Manager
        /// </summary>
        GameManager GameManager { get; }
    }
}
