using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odis.Core.Games.Script
{
    [Export(typeof(IScriptManager))]
    public class ScriptManager : IScriptManager
    {
        /// <summary>
        /// Get all game script
        /// </summary>
        public ScriptCollection ScriptCollection  { get; private set; }
        private static ScriptManager instance = null;
        public static ScriptManager Instance
        {
            get
            {
                return instance ?? (instance = new ScriptManager());
            }
        }

        /// <summary>
        /// Script manager
        /// </summary>
        public ScriptManager()
        {
            ScriptCollection = new ScriptCollection();

        }
    }
}
