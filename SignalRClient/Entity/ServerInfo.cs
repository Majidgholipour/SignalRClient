using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class ServerInfo
    {
        public string Domain { get; set; }
        public string LocalIP { get; set; }
        public string HostName { get; set; }
        public string ServerName { get; set; }
        public virtual ICollection<ApplicationServers> ApplicationServers { get; set; }
    }
}
