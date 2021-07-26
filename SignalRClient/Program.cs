using Client.Config;
using Client.Config.Enums;
using Client.Services;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Threading;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                CreateHubConnection cls = new CreateHubConnection();
                var cnn = cls.OpenConnection();
                if (cnn.connectionState == ConnectionState.Disconnected)
                {
                    Console.WriteLine("Unable to communicate with route server");
                    Log.CreateLog("Unable to communicate with route server");
                }
                else
                {
                    cls.SendMesage(cnn.proxy, Utility.Config.Server.ResourceSleepTime, (int)ApplicationInput.ResourceInfo);
                    //cls.SendMesage(cnn.proxy, Utility.Config.Server.EventSleepTime, (int)ApplicationInput.EventLog);
                    //cls.ReceiveMessage(cnn.proxy, Utility.Config.Server.ServiceAvalibilitySleepTime);
                    //cls.SendMesage(cnn.proxy, 3000, (int)ApplicationInput.EventLog);
                }

            }
            catch (Exception ex)
            {
                Log.CreateLog(ex.Message);
            }


            Console.Read();
        }

    }
}