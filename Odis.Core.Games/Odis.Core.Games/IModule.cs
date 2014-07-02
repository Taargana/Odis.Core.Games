using System;
using System.Collections.Generic;

namespace Odis.Core.Games
{
    /// <summary>
    /// Used to create Module
    /// </summary>
    public interface IModule : IInitializer
    {
        /// <summary>
        /// All the entities of the module
        /// </summary>
        IEnumerable<IEntity> Entities { get; }

        /// <summary>
        /// All scripts that module uses
        /// </summary>
        IEnumerable<KeyValuePair<String, String>> Scripts { get; }
    }
}
