using System;
using System.Collections.Generic;
using Odis.Core.Games.Components;

namespace Odis.Core.Games
{
    /// <summary>
    /// GameServer that hosts the server
    /// </summary>
    public class GameServer
    {
        #region Events
        /// <summary>
        /// Occurs when server is starting
        /// </summary>
        public event EventHandler ServerStarting;

        /// <summary>
        /// Occurs when server is starting
        /// </summary>
        protected virtual void OnServerStarting()
        {
            EventHandler handler = ServerStarting;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when server is started
        /// </summary>
        public event EventHandler ServerStarted;


        /// <summary>
        /// Occurs when server is started
        /// </summary>
        protected virtual void OnServerStarted()
        {
            EventHandler handler = ServerStarted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when server is stopping
        /// </summary>
        public event EventHandler ServerStopping;

        /// <summary>
        /// Occurs when server is stopping
        /// </summary>
        protected virtual void OnServerStopping()
        {
            EventHandler handler = ServerStopping;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when server is stopped;
        /// </summary>
        public event EventHandler ServerStopped;

        /// <summary>
        /// Occurs when server is stopped;
        /// </summary>
        protected virtual void OnServerStopped()
        {
            EventHandler handler = ServerStopped;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// AnyElement that compose the server
        /// </summary>
        private List<IComponent> components = new List<IComponent>();

        private GameManager gameManager;
        /// <summary>
        /// Current gme instance
        /// </summary>
        public Game CurrentGame { get; private set; }

        /// <summary>
        /// Game server constructor
        /// </summary>
        /// <param name="game"></param>
        public GameServer(Game game)
        {
            CurrentGame = game;
        }

        private GameServer()
        {
            
        }

        /// <summary>
        /// Current game server state
        /// </summary>
        public GameServerState GameServerState { get; private set; }

        /// <summary>
        /// Start the game server
        /// </summary>
        public void Start()
        {

            #region game parameters

            GameServerState = GameServerState.Starting;
            OnServerStarting();
            //frequence of loop refresh
            int gameLoopFrequency = 60;
            #endregion

            gameManager = new GameManager(this);

            GameServerState = GameServerState.Started;
            OnServerStarted();

            while (GameServerState == GameServerState.Started)
            {
                //I start by checking if I can do the loop or not (depends on the choosen frequency)
                if (GameTime.Instance.Elapsed.TotalMilliseconds < GameTime.Instance.LoopDuration.Current + (double) Math.Floor((decimal) (1000/gameLoopFrequency))) continue;

                //I update current Game
                gameManager.Update(GameTime.Instance);

                //I add a new elapsed time in the GameTime buffer
                GameTime.Instance.LoopDuration.Add(GameTime.Instance.Elapsed.TotalMilliseconds);
            }

            GameServerState = GameServerState.Stopping;
            OnServerStopping();

            GameServerState = GameServerState.Stopped;
            OnServerStopped();
        }
    }
}
