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
   public class ApplicationLogService
    {
        public string GetApplicationLog(string appName,LogType logType , string message)
        {
            try
            {
                var app = new ApplicationLog();
                app.ApplicationName = appName;
                app.GeneratedDate = DateTime.Now;
                app.LogType = logType;
                app.Message = message;

                var server = new ServerInfoService().getServerInfo();

                var serverInfo = new ApplicationServers();

                serverInfo.ApplicationLog = app;
                serverInfo.ServerInfo = server;
                
                var str = JsonConvertor.DataToJsan(serverInfo);
                return str;

            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- "+ex.Message);
                return "";
            }
        }
    }
}
