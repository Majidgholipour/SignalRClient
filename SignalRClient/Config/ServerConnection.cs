using Client.Entity;
using Client.Services;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Threading;
using static Client.JsonConvertor;

namespace Client.Config
{
    public class ServerConnection
    {
        public Boolean OpenConnect()
        {
            try
            {
                //ResourceService resourceInfo = new ResourceService();
                //ServerEventLogService eventLog = new ServerEventLogService();
                //string str = "";

                ServerConfig cnnObject = JsonConvertor.ReadFromJson(@"..\..\ServerConfiguration.json");


                var connection = new HubConnection(cnnObject.Server.ConncetionURL);
                var myHub = connection.CreateHubProxy(cnnObject.Server.HubName);
                int counter = 0;
                connection.Start().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {

                        //Console.WriteLine("There was an error opening the connection:{0}",task.Exception.GetBaseException());
                        Console.WriteLine("Fail connecting to "+ cnnObject.Server.ConncetionURL);
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Try to reconnting....");
                        Console.ReadLine();
                        if (counter < 3)
                        {
                            Thread.Sleep(Utility.Config.Server.ReconnectSleepTime);
                            Thread.CurrentThread.IsBackground = true;
                            counter++;
                            //OpenConnect();
                        }
                        else
                        {
                            //Todo Log
                        }
                        //OpenConnect();

                    }
                    else
                    {
                        Console.WriteLine("Connected");
                    }

                }).Wait();


                //myHub.Invoke<string>("Send", "Start connecting from ... ").ContinueWith(task =>
                //{
                //    if (task.IsFaulted)
                //    {
                //        Console.WriteLine("There was an error calling send: {0}",
                //                          task.Exception.GetBaseException());
                //    }
                //    else
                //    {


                //        while (true)
                //        {
                //            //myHub.Invoke<string>("CPUUsage", resourceInfo.getCpuUsage().ToString()).Wait();
                //            //myHub.Invoke<string>("RAMUsage", resourceInfo.getMemoryUsage().ToString()).Wait();
                //            //myHub.Invoke<string>("HDDInfo", resourceInfo.getHDDInfo().ToString()).Wait();

                //            str = eventLog.getEventLog();

                //            myHub.Invoke<string>("EventLog", str).Wait();

                //            Thread.Sleep(cnnObject.Server.SleepTime);
                //        }

                //    }
                //});

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
