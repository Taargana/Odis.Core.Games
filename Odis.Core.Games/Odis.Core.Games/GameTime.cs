using System;
using System.Diagnostics;
using Odis.Core.Games.Arrays;

namespace Odis.Core.Games
{
    /// <summary>
    /// GameTime class that manage the gametime
    /// </summary>
    public class GameTime
    {
        /// <summary>
        /// instance of the singleton
        /// </summary>
        private static GameTime instance = null;
        /// <summary>
        /// Get loopDuration buffer
        /// </summary>
        public BufferedState<double> LoopDuration { get; private set; }
        private static Stopwatch currentGametime;

        /// <summary>
        /// Get GameTime instance
        /// </summary>
        public static GameTime Instance
        {
            get
            {
                return instance ?? (instance = new GameTime());
            }
        }
        private GameTime()
        {
            currentGametime = new Stopwatch();
            currentGametime.Start();
            LoopDuration = new BufferedState<double>();
            LoopDuration.Add(0);
        }

        /// <summary>
        /// Get elapsed game time
        /// </summary>
        public TimeSpan Elapsed
        {
            get
            {
                return currentGametime.Elapsed;
            }
        }

        /// <summary>
        /// Get previous loop duration
        /// </summary>
        public TimeSpan PreviousLoopElapsed { get { return new TimeSpan(0, 0, 0, 0, (int)(LoopDuration.Current - LoopDuration.Previous)); } }
    }
}