
using Client.Config.Enums;
using Client.Entity;
using Client.Services;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Reflection;
using System.Threading;

namespace Client.Config
{
    public class CreateHubConnection
    {
        private int counter = 0;
        HubConnection connection = new HubConnection(Utility.Config.Server.ConncetionURL);
        public ConnectionResult OpenConnection()
        {
            var connectionHub = connection.CreateHubProxy(Utility.Config.Server.HubName);

            ConnectionRepo repo = new ConnectionRepo();
            try
            {
                connection.Start().ContinueWith(task =>
                {
                    if (connection.State == ConnectionState.Disconnected && task.IsFaulted)
                    {
                        Console.WriteLine("Fail connecting to " + Utility.Config.Server.ConncetionURL + " Try to reconnting. Please wait");

                        if (counter < 3)
                        {
                            Thread.Sleep(Utility.Config.Server.ReconnectSleepTime);
                            Thread.CurrentThread.IsBackground = true;
                            counter++;
                            OpenConnection();

                        }
                        else
                        {
                            throw new Exception("Unable to communicate with route server");
                        }
                    }
                    else if (connection.State == ConnectionState.Connected)
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine("------------Connected to " + Utility.Config.Server.ConncetionURL);
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine("------------Press X key for exit-----------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------");

                    }
                }).Wait();


                connectionHub.On<string>("addMessage", param =>
                {
                    Console.WriteLine(param);
                });
                return repo.AddConnectionResult(connectionHub, connection.State, "Connected");
            }
            catch (Exception ex)
            {
                Log.CreateLog("Fail connecting to " + Utility.Config.Server.ConncetionURL + ex.Message);
                return repo.AddConnectionResult(null, connection.State, "Not Connected");
            }

        }

        public void SendMesage(IHubProxy proxy, int sleepTime, int input, bool isSingleMessage = false, string appData = "")
        {
            try
            {
                string data = string.Empty;

                data = getServiceAvalibility(ServiceAvalibilityType.FirstConnect);
                proxy.Invoke<string>("Receive", ApplicationInput.ServiceAvlibiliy, data).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                        Log.CreateLog(routPath + " -- " + task.Exception.Message);
                    }
                    else
                    {
                        Console.WriteLine("Send service avalibility information is saved at " + DateTime.Now);
                        Console.WriteLine("------------------------------------------------------------------");
                        while (true)
                        {

                            switch (input)
                            {
                                case (int)ApplicationInput.ApplicationInfo:
                                    {
                                        try
                                        {
                                            data = getApplicationLogService();
                                            if (data.Length > 10)
                                                proxy.Invoke<string>("Receive", ApplicationInput.ApplicationInfo, data).Wait();
                                            Console.WriteLine("Send Applicarion information is saved at " + DateTime.Now);
                                            Console.WriteLine("------------------------------------------------------------------");
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                                            Log.CreateLog(routPath + " -- " + ex.Message);
                                            break;
                                        }

                                    }
                                case (int)ApplicationInput.EventLog:
                                    {
                                        try
                                        {
                                            data = getEventLogService();
                                            if (data.Length > 10)
                                                proxy.Invoke<string>("Receive", ApplicationInput.EventLog, data).Wait();
                                            Console.WriteLine("Send EventLog information is saved at " + DateTime.Now);
                                            Console.WriteLine("------------------------------------------------------------------");
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                                            Log.CreateLog(routPath + " -- " + ex.Message);
                                            break;
                                        }
                                    }
                                case (int)ApplicationInput.ResourceInfo:
                                    {
                                        try
                                        {
                                            data = getResourceService();
                                            if (data.Length > 10)
                                                proxy.Invoke<string>("Receive", ApplicationInput.ResourceInfo, data).Wait();
                                            Console.WriteLine("Send Resource information is saved at " + DateTime.Now);
                                            Console.WriteLine("------------------------------------------------------------------");
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                                            Log.CreateLog(routPath + " -- " + ex.Message);
                                            break;
                                        }


                                    }
                                case (int)ApplicationInput.ServiceAvlibiliy:
                                    {
                                        try
                                        {
                                            data = getServiceAvalibility(ServiceAvalibilityType.FirstConnect);
                                            if (data.Length > 10)
                                                proxy.Invoke<string>("Receive", ApplicationInput.ServiceAvlibiliy, data).Wait();
                                            Console.WriteLine("Send Resource information is saved at " + DateTime.Now);
                                            Console.WriteLine("------------------------------------------------------------------");
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                                            Log.CreateLog(routPath + " -- " + ex.Message);
                                            break;
                                        }
                                    }
                            }

                            if (isSingleMessage)
                                break;
                            Thread.Sleep(sleepTime);
                            Thread.CurrentThread.IsBackground = true;
                        }

                    }
                });
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                //if (connection.State == ConnectionState.Disconnected)
                //    OpenConnection();
            }
        }

        public void ReceiveMessage(string str)
        {
            Console.WriteLine("message from server is: " + str);
        }

        public void ReceiveMessage(IHubProxy proxy, int sleepTime)
        {
            try
            {
                string data = string.Empty;
                while (true)
                {
                    try
                    {
                        data = getServiceAvalibility(ServiceAvalibilityType.RuntimeConnect);
                        if (data.Length > 10)
                            proxy.Invoke<string>("Receive", ApplicationInput.ApplicationInfo, data).Wait();
                        Console.WriteLine("Send Applicarion information is saved at " + DateTime.Now);
                        Console.WriteLine("------------------------------------------------------------------");
                        Thread.Sleep(sleepTime);
                        Thread.CurrentThread.IsBackground = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                        Log.CreateLog(routPath + " -- " + ex.Message);
                        break;
                    }

                }
            }

            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);

                //if (connection.State == ConnectionState.Disconnected)
                //    OpenConnection();
            }
        }

        private string getResourceService()
        {
            ResourceService service = new ResourceService();
            return JsonConvertor.DataToJsan(service.GetResouceInfo());
        }
        private string getEventLogService()
        {
            ServerEventLogService service = new ServerEventLogService();
            return service.getEventLog();
        }

        private string getApplicationLogService()
        {
            return new ApplicationLogService().GetApplicationLog("test", LogType.Warning, "this message is a test");
        }

        private string getServiceAvalibility(ServiceAvalibilityType serviceAvalibilityType)
        {
            return new ServiceAvailability().GetServiceAvailability(serviceAvalibilityType);
        }
    }
}
