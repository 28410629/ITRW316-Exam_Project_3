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
    // system values
    public long freePhysicalMemory = 0; // KB
    public long freeVirtualMemory = 0; // KB
    public long totalVisibleMemorySize = 0; // KB
    public long totalVirtualMemorySize = 0; // KB
    public string caption = "";  // display operating system caption
    public string osArchitecture = ""; // display operating system architecture.

    // results from
    ManagementObjectCollection results = null;

    // user input values
    public long userReservedSize = 0;
    public long userPageSize = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        getSystemInformation();
        assignRawValues();
        updateLabels();
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

    public void updateLabels()
    {
        LabelPhysicalMemory.Text = (freePhysicalMemory / 1024).ToString() + " MB";
        LabelPhysicalMemoryTotal.Text = (totalVisibleMemorySize / 1024).ToString() + " MB" ; 
        LabelVirtualMemory.Text = (freeVirtualMemory / 1024).ToString() + " MB";
        LabelVirtualMemoryTotal.Text = (totalVirtualMemorySize / 1024).ToString() + " MB";
        LabelOSName.Text = caption;  // display operating system caption
        LabelOSArchitecture.Text = osArchitecture; // display operating system architecture.
        LabelSimulationSize.Text = ((freePhysicalMemory / 1024) - (userReservedSize)).ToString() + " MB";
    }

    protected void ButtonCalculate_Click(object sender, EventArgs e)
    {
        userReservedSize = (Convert.ToInt64(TextBoxSizeOS.Text));
        updateLabels();
    }
}