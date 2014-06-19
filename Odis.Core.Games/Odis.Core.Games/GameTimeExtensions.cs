using System;

namespace Odis.Core.Games
{
    /// <summary>
    /// Extension classe to the gametime
    /// </summary>
    public static class GameTimeExtensions
    {
        private static TimeSpan previousExecution = default(TimeSpan);
        /// <summary>
        /// Execute an action every n time
        /// </summary>
        /// <param name="gametime">current gametime</param>
        /// <param name="elapseBetweenCall">time between two calls</param>
        /// <param name="action">Action to execute</param>
        public static void ExecuteEachTime(this GameTime gametime, TimeSpan elapseBetweenCall, Action action)
        {
            if (previousExecution == default(TimeSpan))
            {
                action.Invoke();
                previousExecution = gametime.Elapsed;
            }
            else
            {
                if (gametime.Elapsed - previousExecution < elapseBetweenCall) return;
                action.Invoke();
                previousExecution = gametime.Elapsed;
            }
        }

        private static int currentFrameNumber = 0;
        private static Boolean start = true;
        /// <summary>
        /// Execute an action every n frame
        /// </summary>
        /// <param name="gameTime">current gametime</param>
        /// <param name="frameNumber">frame number before execute action</param>
        /// <param name="action">action to execute</param>
        public static void ExecuteEachFrame(this GameTime gameTime, int frameNumber, Action action)
        {
            if (currentFrameNumber == 0 && start)
            {
                action.Invoke();
                currentFrameNumber++;
                start = false;
            }
            else
            {
                if (currentFrameNumber == frameNumber)
                {
                    action.Invoke();
                    currentFrameNumber = 0;
                }
                else
                {
                    currentFrameNumber++;
                }
            }
        }

        private static int currentFrameNumber2 = 0;
        private static Boolean start2 = true;
        /// <summary>
        /// Execute an action every n frame
        /// </summary>
        /// <param name="gameTime">current gametime</param>
        /// <param name="frameNumber">frame number before execute action</param>
        /// <param name="action">action to execute</param>
        /// <param name="actionParameter">action to execute</param>
        public static void ExecuteEachFrame<T>(this GameTime gameTime, int frameNumber, Action<T> action, T actionParameter)
        {
            if (currentFrameNumber2 == 0 && start2)
            {
                action.Invoke(actionParameter);
                currentFrameNumber2++;
                start2 = false;
            }
            else
            {
                if (currentFrameNumber2 == frameNumber)
                {
                    action.Invoke(actionParameter);
                    currentFrameNumber2 = 0;
                }
                else
                {
                    currentFrameNumber2++;
                }
            }
        }


        private static TimeSpan previousExecution2 = default(TimeSpan);
        /// <summary>
        /// Execute an action every n time
        /// </summary>
        /// <param name="gametime">current gametime</param>
        /// <param name="elapseBetweenCall">time between two calls</param>
        /// <param name="action">Action to execute</param>
        /// <param name="actionParameter">Action to execute</param>
        public static void ExecuteEachTime<T>(this GameTime gametime, TimeSpan elapseBetweenCall, Action<T> action, T actionParameter)
        {
            if (previousExecution2 == default(TimeSpan))
            {
                action.Invoke(actionParameter);
                previousExecution2 = gametime.Elapsed;
            }
            else
            {
                if (gametime.Elapsed - previousExecution2 < elapseBetweenCall) return;
                action.Invoke(actionParameter);
                previousExecution2 = gametime.Elapsed;
            }
        }
    }
}
