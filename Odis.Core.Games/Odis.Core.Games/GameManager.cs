using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Odis.Core.Games.Communication;
using Odis.Core.Games.Components;

namespace Odis.Core.Games
{
    /// <summary>
    /// GameManager that is responsible for game instance management
    /// </summary>
    public class GameManager : IUpdatable
    {
        /// <summary>
        /// Get Mod folder path
        /// </summary>
        public String ModPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Mods");
            }
        }
        //todo changer ça en mettant une liste capable d'affecter l'instance du GameManager au IGameManagerComponent
        /// <summary>
        /// Any component hosted by the GameManager
        /// </summary>
        public List<IGameManagerComponent> GameManagerComponent { get; private set; }

        [Import(typeof(IScriptManager))]
        public IScriptManager ScriptManager { get; set; }

        /// <summary>
        /// Current Game Manager
        /// </summary>
        /// <param name="gameServer"></param>
        public GameManager(GameServer gameServer)
        {
            bool isExists = Directory.Exists(ModPath);

            if (!isExists)
            {
                Directory.CreateDirectory(ModPath);
            }

            MessageCollection = new MessageCollection();

            //I add an update rate counter
            ServerUpdateRateCounter = new ServerUpdateRateCounter();
            GameManagerComponent = new List<IGameManagerComponent> { ServerUpdateRateCounter, gameServer.CurrentGame};

            gameServer.CurrentGame.Initialize();

            //Call to get all the entities
            Compose();

            foreach (IModule module in Modules)
            {
                foreach (KeyValuePair<String, String> script in module.Scripts)
                {
                    ScriptManager.ScriptCollection.Add(script.Key, script.Value);
                }
                gameServer.CurrentGame.AddRangeEntities(module.Entities.ToArray());
            }

            foreach (KeyValuePair<string, IEntity> entity in gameServer.CurrentGame.Entities)
            {
                entity.Value.GetType().GetProperty("GameManager").SetValue(
                    entity.Value, this);
            }


            //todo supprimer la boucle ci-dessous après la création de la liste de IGameManagerComponent
            foreach (IGameManagerComponent gameManagerComponent in GameManagerComponent)
            {
                gameManagerComponent.GameManager = this;
            }
        }

        /// <summary>
        /// Game Manager
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (IGameManagerComponent gameManagerComponent in GameManagerComponent)
            {
                gameManagerComponent.Update(gameTime);
            }
        }

        /// <summary>
        /// Component that exposes infos about gameupdate loop
        /// </summary>
        public ServerUpdateRateCounter ServerUpdateRateCounter { get; private set; }

        /// <summary>
        /// All Modules that game executes 
        /// </summary>
        [ImportMany]
        public IEnumerable<IModule> Modules { get; private set; }

        /// <summary>
        /// Message collection exchange with client / server
        /// </summary>
        public MessageCollection MessageCollection { get; private set; }

        private CompositionContainer _container;
        private void Compose()
        { 
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(ModPath));
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(GetType().Assembly.Location)));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
