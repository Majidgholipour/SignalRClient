using Client.Config.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class ApplicationLog 
    {
        public string Message { get; set; }
        public string ApplicationName { get; set; }
        public DateTime GeneratedDate { get; set; }
        public LogType LogType { get; set; }
        public virtual ICollection<ApplicationServers> ApplicationServers { get; set; }
    }
}
