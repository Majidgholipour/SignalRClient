using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class EventLogInfo
    {
        public string MachineName { get; set; }
        public int Index { get; set; }
        public string Category { get; set; }
        public short CategoryNumber { get; set; }
        public int EventID { get; set; }
        public int EntryType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public long InstanceId { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeWritten { get; set; }
        public string UserName { get; set; }
        public ICollection<EventLogData> eventLogData { get; set; }
        public ICollection<EventLogReplacementStrings> eventLogReplacementStrings { get; set; }
        public ServerInfo serverInfo { get; set; }
    }
}
