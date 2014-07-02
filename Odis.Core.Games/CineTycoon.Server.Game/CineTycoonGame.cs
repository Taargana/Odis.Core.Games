using System;
using System.Linq;
using System.Text;
using Odis.Core.Games;
using Odis.Core.Games.Components;

namespace CineTycoon.Server.GameApplication
{
    public class CineTycoonGame : Game
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Console.WriteLine("CineTycoonGame; fps: {0}", GameManager.ServerUpdateRateCounter.AverageFramesPerSecond);
            gameTime.ExecuteEachTime(new TimeSpan(0, 0, 0, 5, 0), () => Console.WriteLine("Messages: {0}", this.GameManager.MessageCollection.Aggregate(new StringBuilder(), (builder, message) => builder.Append(message.RawData), builder => builder.ToString())));
        }
    }
}
