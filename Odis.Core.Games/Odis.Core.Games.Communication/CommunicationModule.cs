using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// This Module exposes communication Entity
    /// </summary>
    [Export(typeof(IModule))]
    public class CommunicationModule : IModule
    {
        public void Initialize()
        {
        }

        /// <summary>
        /// All the entities of the module
        /// </summary>
        public IEnumerable<IEntity> Entities
        {
            get
            {
                yield return new SocketEntity("DefaultSocket", 8587).AddComponent(new SocketComponent());
                yield return new SocketEntity("DefaultWebSocket").AddComponent(new WebSocketComponent());
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
