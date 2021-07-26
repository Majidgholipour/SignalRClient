using Client.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    public class ResouceInfo
    {
        public CPUInfo CPUInfo { get; set; }
        public MemoryInfo MemoryInfo { get; set; }
        public DiskInfo DiskInfo { get; set; }
        public ServerInfo ServerInfo { get; set; }
        public EthernetInfo ethernetInfo { get; set; }
    }

    public class DiskInfo : BaseEntity
    {
        public long TotalSize { get; set; }
        public long TotalFreesize { get; set; }
        public long AvailableFreeSpace { get; set; }
        public string DrivesInfo { get; set; }
    }

    public class driveInfo
    {
        public string Name { get; set; }
        public driveDetailInfo driveDetail { get; set; }
    }
    public class driveDetailInfo
    {
        public string DriveType { get; set; }
        public string DriveFormat { get; set; }
        public bool IsReady { get; set; }
        public long AvailableFreeSpace { get; set; }
        public long TotalFreeSpace { get; set; }
        public long TotalSize { get; set; }
        public string VolumeLabel { get; set; }
    }
    public class CPUInfo : BaseEntity
    {
        public float UsagePresent { get; set; }
        public int CountOfCore { get; set; }
        public float Speed { get; set; }
        public int CountOfProcessoer { get; set; }
    }
    public class MemoryInfo : BaseEntity
    {
        public long Size { get; set; }
        public decimal Usage { get; set; }
        public long InUse { get; set; }
        public long Avialible { get; set; }
        public long Cached { get; set; }
        public long Commited { get; set; }
    }
    public class EthernetInfo : BaseEntity
    {
        public string AdaptorName { get; set; }
        public string DomainName { get; set; }
        public string ConnectionType { get; set; }
        public string IPv4Address { get; set; }
        public string IPv6Address { get; set; }
        public float Send { get; set; }
        public float Receive { get; set; }
    }

}
