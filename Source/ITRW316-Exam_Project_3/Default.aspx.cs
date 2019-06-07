using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Management;   //This namespace is used to work with WMI classes. For using this namespace add reference of System.Management.dll .

public partial class _Default : Page
{
    // raw values are in KB
    public long freePhysicalMemory = 0; 
    public long freeVirtualMemory = 0;
    public long totalVisibleMemorySize = 0;
    public long totalVirtualMemorySize = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
        ManagementObjectCollection results = searcher.Get();

        foreach (ManagementObject result in results)
        {
            // physical memory details
            freePhysicalMemory = Convert.ToInt64(result["FreePhysicalMemory"]);
            totalVisibleMemorySize = Convert.ToInt64(result["TotalVisibleMemorySize"]);

            // virtual memory details
            freeVirtualMemory = Convert.ToInt64(result["FreeVirtualMemory"]);
            totalVirtualMemorySize = Convert.ToInt64(result["TotalVirtualMemorySize"]);
            
            // assign values to labels
            LabelSystemMemory.Text = (freePhysicalMemory / 1024).ToString() + " MB";
        }
    }

    public long SizeOS = 6;

    protected void TextBoxSizeOS_TextChanged(object sender, EventArgs e)
    {
        
        
    }

    public void updateLabels()
    {
        LabelOSSize.Text = SizeOS.ToString();
        LabelSimulationSize.Text = ((freePhysicalMemory / 1024) - (SizeOS)).ToString();
    }

    protected void ButtonCalculate_Click(object sender, EventArgs e)
    {
        SizeOS = (Convert.ToInt64(TextBoxSizeOS.Text));
        updateLabels();
    }
}

//public class SystemInfo
//{
//    public void getOperatingSystemInfo()
//    {
//        Console.WriteLine("Displaying operating system info....\n");
//        //Create an object of ManagementObjectSearcher class and pass query as parameter.
//        ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
//        foreach (ManagementObject managementObject in mos.Get())
//        {
//            if (managementObject["Caption"] != null)
//            {
//                Console.WriteLine("Operating System Name  :  " + managementObject["Caption"].ToString());   //Display operating system caption
//            }
//            if (managementObject["OSArchitecture"] != null)
//            {
//                Console.WriteLine("Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
//            }
//            if (managementObject["CSDVersion"] != null)
//            {
//                Console.WriteLine("Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString());     //Display operating system version.
//            }
//        }
//    }

//    public void getProcessorInfo()
//    {
//        Console.WriteLine("\n\nDisplaying Processor Name....");
//        RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

//        if (processor_name != null)
//        {
//            if (processor_name.GetValue("ProcessorNameString") != null)
//            {
//                Console.WriteLine(processor_name.GetValue("ProcessorNameString"));   //Display processor ingo.
//            }
//        }
//    }
//}