using System;
using System.Collections.Generic;
using System.Linq;

namespace Odis.Core.Games.Components
{
    /// <summary>
    /// Count many values about the game loop
    /// </summary>
    public class ServerUpdateRateCounter : IGameManagerComponent
    {
        /// <summary>
        /// Total frames since the beginnning
        /// </summary>
        public long TotalFrames { get; private set; }
        /// <summary>
        /// Total spend seconds
        /// </summary>
        public double TotalSeconds { get; private set; }

        /// <summary>
        /// Average frame number by second
        /// </summary>
        public double AverageFramesPerSecond { get; private set; }

        /// <summary>
        /// Get frame per second for current game loop execution
        /// </summary>
        public double CurrentFramesPerSecond { get; private set; }

        /// <summary>
        /// sample number userd by buffer to smooth loop count
        /// </summary>
        public const int MAXIMUM_SAMPLES = 100;

        /// <summary>
        /// buffer used to store samples
        /// </summary>
        private Queue<double> _sampleBuffer = new Queue<double>();

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="gametime"></param>
        public void Update(GameTime gametime)
        {
            //get the number of frame per second by taking the previous loop time elapsed
            CurrentFramesPerSecond = Math.Round(1 / gametime.PreviousLoopElapsed.TotalSeconds);

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += gametime.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Current Game Manager
        /// </summary>
        public GameManager GameManager { get; set; }
    }
}
