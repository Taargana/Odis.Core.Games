using System;
using System.Collections.Generic;

namespace Odis.Core.Games.Components
{
    /// <summary>
    /// Classe de jeu qu'il faut overrider pour pouvoir créer un jeu
    /// </summary>
    public abstract class Game : IGameManagerComponent, IInitializer
    {
        /// <summary>
        /// Get any entities of the game
        /// </summary>
        public IDictionary<String, IEntity> Entities { get; private set; }

        /// <summary>
        /// Update game logic
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<String, IEntity> entity in Entities)
            {
                foreach (IUpdateComponent component in entity.Value.Components)
                {
                    component.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Current Game Manager
        /// </summary>
        public GameManager GameManager { get; set; }

        /// <summary>
        /// Add an entity to the entity collection
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity.Name,entity);
        }

        /// <summary>
        /// Add entities to the entity collection
        /// </summary>
        /// <param name="entities"></param>
        public void AddRangeEntities(params IEntity[] entities)
        {
            foreach (IEntity entity in entities)
            {
                foreach (IUpdateComponent updateComponent in entity.Components)
                {
                    updateComponent.Initialize();
                }
                if (!Entities.ContainsKey(entity.Name))
                {
                    Entities.Add(entity.Name,entity);
                }
            }
        }

        /// <summary>
        /// Initialize the Game
        /// </summary>
        public void Initialize()
        {
            Entities = new Dictionary<string, IEntity>();
        }
    }
}
