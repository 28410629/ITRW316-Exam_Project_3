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

    public long pageAmountInMemory = 0;
    public long pageAmountInStorage = 0;

    public long userSimulationSize = 0;
    public long reservedForMemory = 0;
    public long reservedForStorage = 0;

    public double percentageInMemory = 0.00;
    public double percentageInStorage = 0.00;

    protected void Page_Load(object sender, EventArgs e)
    {
        server.getSystemInformation();
        server.assignRawValues();
        updateLabels();
    }

    public void updateLabels()
    {
        LabelPhysicalMemory.Text = (server.getFreePhysicalMemory() / 1024).ToString() + " MB";
        LabelPhysicalMemoryTotal.Text = (server.getTotalVisibleMemorySize() / 1024).ToString() + " MB" ; 
        LabelVirtualMemory.Text = (server.getFreeVirtualMemory() / 1024).ToString() + " MB";
        LabelVirtualMemoryTotal.Text = (server.getTotalVirtualMemorySize() / 1024).ToString() + " MB";
        LabelOSName.Text = server.getOSCaption();  // display operating system caption
        LabelOSArchitecture.Text = server.getOSArchitecture(); // display operating system architecture.
        LabelSimulationSize.Text = ((server.getFreePhysicalMemory() / 1024) - (userReservedSize)).ToString() + " MB";

        
    }

    protected void ButtonCalculate_Click(object sender, EventArgs e)
    {
        userReservedSize = (Convert.ToInt64(TextBoxSizeOS.Text));
        userPageSize = (Convert.ToInt64(TextBoxSizePage.Text));
        userSimulationSize = (server.getFreePhysicalMemory() / 1024) - userReservedSize;
        calculatePages();
        updateLabels();
        
    }

    public void calculatePages()
    {
        percentageInMemory = Convert.ToInt64(textboxMemoryPercentage.Text);
        percentageInStorage = 100 - percentageInMemory;
        labelStoragePercentage.Text = percentageInStorage.ToString();

        reservedForMemory = Convert.ToInt64(userSimulationSize * (percentageInMemory / 100));
        labelMemorySimulation.Text = reservedForMemory.ToString();

        reservedForStorage = userSimulationSize - reservedForMemory;
        labelStorageSimulation.Text = reservedForStorage.ToString();

        pageAmountInMemory = reservedForMemory / userPageSize;
        LabelPageCountMemory.Text = pageAmountInMemory.ToString();

        pageAmountInStorage = reservedForStorage / userPageSize;
        labelPageCountStorage.Text = pageAmountInStorage.ToString();
    }
}