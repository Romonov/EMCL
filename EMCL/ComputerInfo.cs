using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections;

namespace EMCL
{
    class ComputerInfo
    {
        /// <summary>
        /// 获取操作系统版本
        /// </summary>
        /// <returns>操作系统版本</returns>
        public static string GetOSVersion()
        {
            string str = "Error";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_OperatingSystem.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    str = m[WindowsAPIKeys.Name.ToString()].ToString().Split('|')[0].Replace("Microsoft Windows ", "Win");
                    break;
                }
            }
            catch { }
            return str;
        }

        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        public static string GetMemoryTotal()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher();
            searcher.Query = new SelectQuery(WindowsAPIType.Win32_PhysicalMemory.ToString(), "",
                new string[] { WindowsAPIKeys.Capacity.ToString() });
            ManagementObjectCollection collection = searcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            long capacity = 0;
            while (em.MoveNext())
            {
                ManagementBaseObject baseObj = em.Current;
                if (baseObj.Properties[WindowsAPIKeys.Capacity.ToString()].Value != null)
                {
                    try
                    {
                        capacity += long.Parse(baseObj.Properties[WindowsAPIKeys.Capacity.ToString()].Value.ToString());
                    }
                    catch
                    {
                        return "Error";
                    }
                }
            }
            return ByteToMB((double)capacity, 1024.0);
        }

        /// <summary>  
        /// 获取内存可用大小
        /// </summary>
        /// <returns>内存可用大小（单位M）</returns>  
        public static string GetMemoryAvailable()
        {
            long available = 0;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher();
            searcher.Query = new SelectQuery(WindowsAPIType.Win32_PerfFormattedData_PerfOS_Memory.ToString(), "",
                new string[] { WindowsAPIKeys.AvailableMBytes.ToString() });
            ManagementObjectCollection collection = searcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            while (em.MoveNext())
            {
                ManagementBaseObject baseObj = em.Current;
                if (baseObj.Properties[WindowsAPIKeys.AvailableMBytes.ToString()].Value != null)
                {
                    try
                    {
                        available += long.Parse(baseObj.Properties[WindowsAPIKeys.AvailableMBytes.ToString()].Value.ToString());
                    }
                    catch
                    {
                        return "Error";
                    }
                }
            }
            return available.ToString();
        }

        /// <summary>  
        /// 转换以Byte为单位的数值为以MB为单位的
        /// </summary>  
        /// <param name="size">字节值</param>  
        /// <param name="mod">除数，硬盘容量除以1000，内存容量除以1024</param>  
        /// <returns>MB数</returns>  
        public static string ByteToMB(double size, double mod)
        {
            size /= mod * mod;
            return Math.Round(size).ToString();
        }

        /// <summary>
        /// 获取CPU型号
        /// </summary>
        /// <returns>CPU型号</returns>
        public static string GetCPU()
        {
            string result = "";
            try
            {
                string str = string.Empty;
                ManagementClass mcCPU = new ManagementClass(WindowsAPIType.Win32_Processor.ToString());
                ManagementObjectCollection mocCPU = mcCPU.GetInstances();
                foreach (ManagementObject m in mocCPU)
                {
                    string name = m[WindowsAPIKeys.Name.ToString()].ToString();
                    string[] parts = name.Split(' ');
                    for (int i = 3; i <= parts.Length; i++)
                    {
                        if (result == "")
                        {
                            result = $"{parts[i]}";
                        }
                        else
                        {
                            if (parts[i] == "@")
                            {
                                break;
                            }
                            result = $"{result} {parts[i]}";
                        }
                    }
                    break;
                }
            }
            catch
            {
                return "Error";
            }
            return result;
        }

        /// <summary>
        /// 获取GPU型号
        /// </summary>
        /// <returns>GPU型号</returns>
        public static string GetGPU()
        {
            string result = "";
            try
            {

                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_VideoController.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    result = m[WindowsAPIKeys.VideoProcessor.ToString()].ToString().Replace("Family", "");
                    break;
                }
            }
            catch
            {

            }
            return result;

        }
    }
    #region WindowsAPI枚举
    /// <summary>
    /// WindowsAPI类型
    /// </summary>
    public enum WindowsAPIType
    {
        /// <summary>
        /// 内存
        /// </summary>
        Win32_PhysicalMemory,
        /// <summary>
        /// 可用内存
        /// </summary>
        Win32_PerfFormattedData_PerfOS_Memory,
        /// <summary>
        /// cpu
        /// </summary>
        Win32_Processor,
        /// <summary>
        /// 硬盘
        /// </summary>
        win32_DiskDrive,
        /// <summary>
        /// 电脑型号
        /// </summary>
        Win32_ComputerSystemProduct,
        /// <summary>
        /// 分辨率
        /// </summary>
        Win32_DesktopMonitor,
        /// <summary>
        /// 显卡
        /// </summary>
        Win32_VideoController,
        /// <summary>
        /// 操作系统
        /// </summary>
        Win32_OperatingSystem

    }

    /// <summary>
    /// WindowsAPI键值
    /// </summary>
    public enum WindowsAPIKeys
    {
        /// <summary>
        /// 名称
        /// </summary>
        Name,
        /// <summary>
        /// 显卡芯片
        /// </summary>
        VideoProcessor,
        /// <summary>
        /// 显存大小
        /// </summary>
        AdapterRAM,
        /// <summary>
        /// 分辨率宽
        /// </summary>
        ScreenWidth,
        /// <summary>
        /// 分辨率高
        /// </summary>
        ScreenHeight,
        /// <summary>
        /// 电脑型号
        /// </summary>
        Version,
        /// <summary>
        /// 硬盘容量
        /// </summary>
        Size,
        /// <summary>
        /// 内存容量
        /// </summary>
        Capacity,
        /// <summary>
        /// 可用内存
        /// </summary>
        AvailableMBytes,
        /// <summary>
        /// cpu核心数
        /// </summary>
        NumberOfCores
    }
    #endregion

    /*
    /// <summary>
    /// 电脑信息类 单例
    /// </summary>
    public class Computer
    {
        private static Computer _instance;
        private static readonly object _lock = new object();
        private Computer()
        { }
        public static Computer CreateComputer()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Computer();
                    }
                }
            }
            return _instance;
        }
        
        /// <summary>
        /// 获取cpu核心数
        /// </summary>
        /// <returns></returns>
        public string GetCPU_Count()
        {
            string str = "查询失败";
            try
            {
                int coreCount = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from " +
 WindowsAPIType.Win32_Processor.ToString()).Get())
                {
                    coreCount += int.Parse(item[WindowsAPIKeys.NumberOfCores.ToString()].ToString());
                }
                if (coreCount == 2)
                {
                    return "双核";
                }
                str = coreCount.ToString() + "核";
            }
            catch
            {

            }
            return str;
        }

        /// <summary>
        /// 获取硬盘容量
        /// </summary>
        public string GetDiskSize()
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.win32_DiskDrive.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    long capacity = Convert.ToInt64(m[WindowsAPIKeys.Size.ToString()].ToString());
                    sb.Append(ToGB(capacity, 1000.0) + "+");
                }
                result = sb.ToString().TrimEnd('+');
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// 电脑型号
        /// </summary>
        public string GetVersion()
        {
            string str = "查询失败";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_ComputerSystemProduct.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    str = m[WindowsAPIKeys.Version.ToString()].ToString(); break;
                }
            }
            catch
            {

            }
            return str;
        }

        /// <summary>
        /// 获取分辨率
        /// </summary>
        public string GetFenbianlv()
        {
            string result = "1920*1080";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_DesktopMonitor.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    result = m[WindowsAPIKeys.ScreenWidth.ToString()].ToString() + "*" +
m[WindowsAPIKeys.ScreenHeight.ToString()].ToString();
                    break;
                }
            }
            catch
            {

            }
            return result;
        }
        
        /// <summary>  
        /// 将字节转换为GB
        /// </summary>  
        /// <param name="size">字节值</param>  
        /// <param name="mod">除数，硬盘除以1000，内存除以1024</param>  
        /// <returns></returns>  
        public static string ToGB(double size, double mod)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }

    }
}
*/
}