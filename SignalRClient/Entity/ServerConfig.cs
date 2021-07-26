using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class ServerConfig
    {
        public ServerConfigItem Server { get; set; }
    }
    public class ServerConfigItem
    {
        public string HubName { get; set; }
        public string ConncetionURL { get; set; }
        public int ResourceSleepTime { get; set; }
        public int EventSleepTime { get; set; }
        public int ApplicarionSleepTime { get; set; }
        public int ServiceAvalibilitySleepTime { get; set; }
        public int ReconnectSleepTime { get; set; }
        public string LogPath { get; set; }

    }
}
