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
    ServerDetails server = new ServerDetails();

    // user input values
    public long userReservedSize = 0;
    public long userPageSize = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        server.getSystemInformation();
        server.assignRawValues();
        updateLabels();
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