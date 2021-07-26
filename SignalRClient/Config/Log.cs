using Client.Services;
using System;
using System.IO;
using System.Reflection;

namespace Client.Config
{
    public static class Log
    {
        public static void CreateLog(string strLog)
        {
            try
            {
                ServerInfoService serverInfoService = new ServerInfoService();
                string fileName = Utility.Config.Server.LogPath + DateTime.Now.Date.ToString("yyyy-mm-dd") + ".txt";
                string srvInfo = string.Empty;
                if (!Directory.Exists(Utility.Config.Server.LogPath))
                {
                    Directory.CreateDirectory(Utility.Config.Server.LogPath);
                }

                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }
                var serverinfo = serverInfoService.getServerInfo();

                srvInfo += serverinfo.ServerName + " - " + serverinfo.LocalIP+" -- ";

                strLog += "--" + DateTime.Now.Date.ToString("yyyy-mm-dd hh:mm:ss") + Environment.NewLine;

                strLog = srvInfo + strLog;
                File.AppendAllText(fileName, strLog);
            }
            catch (Exception)
            {

            }

        }


    }
}
