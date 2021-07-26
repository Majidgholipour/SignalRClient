using Client.Config;
using Client.Entity;
using Microsoft.TeamFoundation.Common.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Client.Services
{
    public class ResourceService
    {

        ManagementObjectSearcher cpuCounter;
        ServerInfoService service = new ServerInfoService();
        
        NetworkInfoService ethernetService = new NetworkInfoService();
        public ResouceInfo GetResouceInfo()
        {
            try
            {
                ResouceInfo info = new ResouceInfo();
                info.CPUInfo = getCpuUsage();
                info.DiskInfo = getDiskInfo();
                info.MemoryInfo = getMemoryUsage();
                info.ServerInfo = service.getServerInfo();
                info.ethernetInfo = ethernetService.getEthernetInfo();
                return info;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return new ResouceInfo();
            }
        }
        private DiskInfo getDiskInfo()
        {
            try
            {


                string driveInfo = string.Empty;
                DiskInfo info = new DiskInfo();

                long totalSize = 0;
                long totalFreesize = 0;
                long availableFreeSpace = 0;
                DriveInfo[] drives = DriveInfo.GetDrives();
                var mydrive = new driveInfo();
                var lstdrive = new List<driveInfo>();
                var mydriveDetail = new driveDetailInfo();



                foreach (DriveInfo drive in drives)
                {

                    mydrive = new driveInfo();
                    mydriveDetail = new driveDetailInfo();
                    if (drive.IsReady)
                    {
                        totalSize += drive.TotalSize;
                        totalFreesize += drive.TotalFreeSpace;
                        availableFreeSpace += drive.AvailableFreeSpace;

                        mydriveDetail.AvailableFreeSpace = drive.AvailableFreeSpace;
                        mydriveDetail.DriveFormat = drive.DriveFormat;
                        mydriveDetail.DriveType = drive.DriveType.ToString();
                        mydriveDetail.IsReady = drive.IsReady;
                        //mydriveDetail.Name= drive.Name;
                        mydriveDetail.TotalFreeSpace = drive.TotalFreeSpace;
                        mydriveDetail.TotalSize = drive.TotalSize;
                        mydriveDetail.VolumeLabel = drive.VolumeLabel;

                        mydrive.Name = drive.Name;
                        mydrive.driveDetail = mydriveDetail;

                        lstdrive.Add(mydrive);
                    }

                }

                driveInfo = JsonConvertor.DataToJsan(lstdrive);

                info.AvailableFreeSpace = availableFreeSpace;
                info.TotalFreesize = totalFreesize;
                info.TotalSize = totalSize;
                info.DrivesInfo = driveInfo.ToString().Replace(@"\\", @"\");

                return info;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return new DiskInfo();
            }
        }
        private CPUInfo getCpuUsage()
        {
            try
            {

                PerformanceCounter pc;

                pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                cpuCounter = new ManagementObjectSearcher(@"\root\CIMV2",
                          "SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name=\"_Total\"");

                ManagementObjectCollection moc = cpuCounter.Get();
                ManagementObject item = moc.Cast<ManagementObject>().First();

                CPUInfo info = new CPUInfo();
                info.UsagePresent = (float)(Convert.ToDecimal(item["PercentIdleTime"].ToString()));
                info.CountOfCore = getCPUCore();
                info.Speed = getCPUClockSpeed();
                info.CountOfProcessoer = getCountOfProcessor();

                return info;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return new CPUInfo();
            }
        }
        private int getCPUCore()
        {
            try
            {

                int coreCount = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                {
                    coreCount += int.Parse(item["NumberOfCores"].ToString());
                }
                return coreCount;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return 0;
            }
        }
        private uint getCPUClockSpeed()
        {
            try
            {
                uint clockSpeed = 0;
                var searcher = new ManagementObjectSearcher(
                   "select MaxClockSpeed from Win32_Processor");
                foreach (var item in searcher.Get())
                {
                    clockSpeed = (uint)item["MaxClockSpeed"];
                }
                return clockSpeed;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return 0;
            }

        }
        private int getCountOfProcessor()
        {
            try
            {
                int count = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                {
                    count = int.Parse(item["NumberOfProcessors"].ToString());
                }
                return count;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return 0;
            }
        }
        private MemoryInfo getMemoryUsage()
        {
            try
            {
                MemoryInfo info = new MemoryInfo();

                Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
                Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
                decimal percentFree = ((decimal)phav / (decimal)tot) * 100;
                decimal percentOccupied = 100 - percentFree;
                info.Avialible = phav;
                info.Size = tot;
                info.Usage = percentOccupied;
                info.InUse = info.Size - info.Avialible;



                string prcName = Process.GetCurrentProcess().ProcessName;
                var counter = new PerformanceCounter("Process", "Working Set - Private", prcName);
                info.Commited = counter.RawValue / 1024;


                return info;
            }
            catch (Exception ex)
            {
                string routPath = this.GetType().Name + " - " + MethodBase.GetCurrentMethod().Name;
                Log.CreateLog(routPath + " -- " + ex.Message);
                return new MemoryInfo();
            }

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(NativeMethods.MEMORYSTATUSEX));
            }
        }
        private static class PerformanceInfo
        {
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

            [StructLayout(LayoutKind.Sequential)]
            public struct PerformanceInformation
            {
                public int Size;
                public IntPtr CommitTotal;
                public IntPtr CommitLimit;
                public IntPtr CommitPeak;
                public IntPtr PhysicalTotal;
                public IntPtr PhysicalAvailable;
                public IntPtr SystemCache;
                public IntPtr KernelTotal;
                public IntPtr KernelPaged;
                public IntPtr KernelNonPaged;
                public IntPtr PageSize;
                public int HandlesCount;
                public int ProcessCount;
                public int ThreadCount;
            }

            public static Int64 GetPhysicalAvailableMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
                }
                else
                {
                    return -1;
                }

            }

            public static Int64 GetTotalMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
                }
                else
                {
                    return -1;
                }

            }
        }
    }
}

