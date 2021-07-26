using Client.Config;
using Client.Entity;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;

namespace Client
{
    public class ServerEventLogService
    {
        public string getEventLog()
        {
            try
            {

                string LogData = string.Empty;

                ServerInfoService serverInfo = new ServerInfoService();
                EventLog eventLog = new EventLog();
                EventLogData logData = new EventLogData();
                List<EventLogData> lstlogData = new List<EventLogData>();

                EventLogReplacementStrings eventLogReplacement = new EventLogReplacementStrings();
                List<EventLogReplacementStrings> lstEventLogReplacement = new List<EventLogReplacementStrings>();

                EventLogInfo info = new EventLogInfo();
                List<EventLogInfo> lstEventLog = new List<EventLogInfo>();

                var server = serverInfo.getServerInfo();
                info.serverInfo = server;
                eventLog.Source = "Application";

                var maxIndexID = FileConfig.readFromFile();

                foreach (EventLogEntry entry in eventLog.Entries)
                {
                    if (maxIndexID != 0)
                    {
                        if (entry.Index > maxIndexID)
                        {
                            info = new EventLogInfo();
                            info.Category = entry.Category;
                            info.CategoryNumber = entry.CategoryNumber;
                            //info.Data = entry.Data;
                            foreach (var item in entry.Data)
                            {
                                logData = new EventLogData();
                                logData.Id = System.Guid.NewGuid(); ;
                                logData.Caption = item.ToString();
                                lstlogData.Add(logData);
                            }
                            info.eventLogData = lstlogData;
                            info.EntryType = (int)entry.EntryType;
                            info.EventID = entry.EventID;
                            info.Index = entry.Index;
                            info.InstanceId = entry.InstanceId;
                            info.MachineName = entry.Category;
                            info.Message = entry.Category;
                            //info.ReplacementStrings = entry.ReplacementStrings;

                            foreach (var item in entry.ReplacementStrings)
                            {
                                eventLogReplacement = new EventLogReplacementStrings();
                                eventLogReplacement.Id = System.Guid.NewGuid(); ;
                                eventLogReplacement.Caption = item.ToString();
                                lstEventLogReplacement.Add(eventLogReplacement);
                            }
                            info.eventLogReplacementStrings = lstEventLogReplacement;
                            info.Source = entry.Source;
                            info.TimeGenerated = entry.TimeGenerated;
                            info.TimeWritten = entry.TimeWritten;
                            info.UserName = entry.UserName;
                            info.serverInfo = server;

                            maxIndexID = entry.Index;

                            lstEventLog.Add(info);
                        }
                    }
                    else
                    {
                        var currentdate = DateTime.Now;
                        if (entry.TimeGenerated > currentdate.AddHours(-1))
                        {
                            info = new EventLogInfo();
                            info.Category = entry.Category;
                            info.CategoryNumber = entry.CategoryNumber;
                            //info.Data = entry.Data;
                            foreach (var item in entry.Data)
                            {
                                logData = new EventLogData();
                                logData.Id = System.Guid.NewGuid(); ;
                                logData.Caption = item.ToString();
                                lstlogData.Add(logData);
                            }
                            info.eventLogData = lstlogData;
                            info.EntryType = (int)entry.EntryType;
                            info.EventID = entry.EventID;
                            info.Index = entry.Index;
                            info.InstanceId = entry.InstanceId;
                            info.MachineName = entry.Category;
                            info.Message = entry.Category;
                            //info.ReplacementStrings = entry.ReplacementStrings;

                            foreach (var item in entry.ReplacementStrings)
                            {
                                eventLogReplacement = new EventLogReplacementStrings();
                                eventLogReplacement.Id = System.Guid.NewGuid(); ;
                                eventLogReplacement.Caption = item.ToString();
                                lstEventLogReplacement.Add(eventLogReplacement);
                            }
                            info.eventLogReplacementStrings = lstEventLogReplacement;
                            info.Source = entry.Source;
                            info.TimeGenerated = entry.TimeGenerated;
                            info.TimeWritten = entry.TimeWritten;
                            info.UserName = entry.UserName;
                            maxIndexID = entry.Index;
                            info.serverInfo = server;
                            lstEventLog.Add(info);
                        }
                    } 
                }

                FileConfig.writetoFile(maxIndexID);

                LogData = JsonConvertor.DataToJsan(lstEventLog);
                return LogData;
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