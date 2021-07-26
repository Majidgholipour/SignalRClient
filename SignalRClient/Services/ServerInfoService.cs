using Client.Config;
using Client.Entity;
using System;
using System.Reflection;

namespace Client.Services
{
    public class ServerInfoService
    {
        public ServerInfo getServerInfo()
        {
            try
            {
                string LocalIp = string.Empty;
                string Domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                string Host = System.Net.Dns.GetHostName();

                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    return null;
                }
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (System.Net.IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        LocalIp = ip.ToString();
                        break;
                    }
                }

                ServerInfo info = new ServerInfo();
                info.Domain = Domain;
                info.HostName = Host;
                info.LocalIP = LocalIp;
                info.ServerName = System.Environment.MachineName;

                return info;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return new ServerInfo();
            }

        }
    }
}
