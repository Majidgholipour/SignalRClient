using Client.Config.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class ServiceAvailabilityInfo
    {
        public ServiceAvailabilityInfo()
        {
            GenerateDateTime = DateTime.Now;
        }

        public string Domain { get; set; }
        public string LocalIP { get; set; }
        public string HostName { get; set; }
        public string ServerName { get; set; }
        public ServiceAvalibilityType  serviceAvalibilityType { get; set; }
        public DateTime GenerateDateTime { get; set; }
    }
}
