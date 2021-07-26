using Client.Config;
using Client.Config.Enums;
using Client.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
   public class ServiceAvailability
    {
        public string  GetServiceAvailability(ServiceAvalibilityType serviceAvalibilityType)
        {
            try
            {
                ServerInfoService serverInfoService = new ServerInfoService();
                ServiceAvailabilityInfo availabilityInfo = new ServiceAvailabilityInfo();

                var service = serverInfoService.getServerInfo();
                availabilityInfo.Domain = service.Domain;
                availabilityInfo.HostName= service.HostName;
                availabilityInfo.LocalIP= service.LocalIP;
                availabilityInfo.ServerName= service.ServerName;
                availabilityInfo.serviceAvalibilityType = serviceAvalibilityType;


                return JsonConvertor.DataToJsan(availabilityInfo);
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return "";
            }
        }
    }
}
