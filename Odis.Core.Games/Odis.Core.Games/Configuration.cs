using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odis.Core.Games
{
    /// <summary>
    /// Configuration classe
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        static Configuration()
        {
            LoadConfiguration();
        }

        /// <summary>
        /// Load configuration
        /// http://blog.danskingdom.com/adding-and-accessing-custom-sections-in-your-c-app-config/
        /// </summary>
        private static void LoadConfiguration()
        {
            //I'm looking for the socket ports configuration
            Ports = new Dictionary<string, int>();
            NameValueCollection SocketPorts = ConfigurationManager.GetSection("SocketPorts") as NameValueCollection;
            if (SocketPorts != null)
            {
                //if i find the section, i want to populate the dictionary
                foreach (var serverKey in SocketPorts.AllKeys)
                {
                    //I look for the values
                    String[] values = SocketPorts.GetValues(serverKey);
                    if (values == null) continue;
                    //I 've found the value
                    string serverValue = values.FirstOrDefault();
                    //I had it to the ports value
                    Ports.Add(serverKey, Convert.ToInt32(serverValue));
                }
            }
        }

        /// <summary>
        /// Reference all ports by name and port value
        /// </summary>
        public static Dictionary<String, int> Ports { get; private set; }
    }
}
