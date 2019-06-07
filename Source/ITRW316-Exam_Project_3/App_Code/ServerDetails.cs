using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;

/// <summary>
/// Summary description for ServerDetails
/// </summary>
public class ServerDetails
{
    // system values
    public long freePhysicalMemory = 0; // KB
    public long freeVirtualMemory = 0; // KB
    public long totalVisibleMemorySize = 0; // KB
    public long totalVirtualMemorySize = 0; // KB
    public string caption = "";  // display operating system caption
    public string osArchitecture = ""; // display operating system architecture.

    // results from
    ManagementObjectCollection results = null;
    public ServerDetails()
    {
    }
    
    public void getSystemInformation()
    {
        ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
        results = searcher.Get();
    }

    public void assignRawValues()
    {
        foreach (ManagementObject result in results)
        {
            // physical memory details
            freePhysicalMemory = Convert.ToInt64(result["FreePhysicalMemory"]);
            totalVisibleMemorySize = Convert.ToInt64(result["TotalVisibleMemorySize"]);

            // virtual memory details
            freeVirtualMemory = Convert.ToInt64(result["FreeVirtualMemory"]);
            totalVirtualMemorySize = Convert.ToInt64(result["TotalVirtualMemorySize"]);

            // other system details
            caption = result["Caption"].ToString(); // os name
            osArchitecture = result["OSArchitecture"].ToString(); // os architecture
        }
    }
}

