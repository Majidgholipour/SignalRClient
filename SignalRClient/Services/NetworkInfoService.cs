using Client.Config;
using Client.Entity;
using System;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Client.Services
{
    public class NetworkInfoService
    {
        public EthernetInfo getEthernetInfo()
        {
            try
            {
                EthernetInfo ethernet = new EthernetInfo();

                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                    foreach (NetworkInterface ni in interfaces)
                    {
                        ethernet.Send = ni.GetIPv4Statistics().BytesSent;
                        ethernet.Receive = ni.GetIPv4Statistics().BytesReceived;
                        ethernet.AdaptorName = ni.Name;
                        if (ni.NetworkInterfaceType == NetworkInterfaceType.Isdn || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        {

                            foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                            {
                                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                {
                                    ethernet.IPv4Address = ip.Address.ToString();
                                }
                            }
                        }
                        ethernet.ConnectionType = ni.NetworkInterfaceType.ToString();
                        ethernet.AdaptorName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

                    }
                }
                return ethernet;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return new EthernetInfo();
            }

        }
    }
}
