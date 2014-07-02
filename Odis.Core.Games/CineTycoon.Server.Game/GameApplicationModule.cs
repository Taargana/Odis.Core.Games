using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Odis.Core.Games;

namespace CineTycoon.Server.GameApplication
{
    [Export(typeof(IModule))]
    public class GameApplicationModule : IModule
    {
        public void Initialize()
        {
        }

        public IEnumerable<IEntity> Entities
        {
            get
            {
                yield return new Entity("X-Wing");
            }
        }

        #region IModule Members


        public IEnumerable<KeyValuePair<string, string>> Scripts
        {
            get { return new List<KeyValuePair<string, string>>(); }
        }

        #endregion
    }
}
