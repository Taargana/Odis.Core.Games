using System;
using System.Collections.Generic;

namespace Odis.Core.Games
{
    /// <summary>
    /// An Entity
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// Instanciate an entity
        /// </summary>
        /// <param name="entityName"></param>
        public Entity(String entityName)
        {
            this.Initialize();
            Name = entityName;
        }
        /// <summary>
        /// Initialize the entity
        /// </summary>
        public virtual void Initialize()
        {
            Components = new List<IUpdateComponent>();
        }

        /// <summary>
        /// All the own components of the entity
        /// </summary>
        public IList<IUpdateComponent> Components { get; private set; }

        /// <summary>
        /// Name of the entity
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Add a component to the entity
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public IEntity AddComponent(IUpdateComponent component)
        {
            component.Entity = this;
            Components.Add(component);
            return this;
        }

        /// <summary>
        /// Current GameManager
        /// </summary>
        public GameManager GameManager { get; protected set; }
    }
}