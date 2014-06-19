using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Odis.Core.Games.Communication
{
    [Export(typeof(IModule))]
    public class CommunicationModule : IModule
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEntity> Entities
        {
            get
            {
                yield return new SocketEntity("DefaultSocket").AddComponent(new SocketComponent());
            }
        }
    }
}
