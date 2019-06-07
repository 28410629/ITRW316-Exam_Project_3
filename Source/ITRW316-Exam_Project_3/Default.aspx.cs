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

    private Simulation simulation;

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

    public void setPhysicalList(List<Program> list)
    {
        LabelSimulationStatus.Text = "It works!!!!";
    }

    protected void ButtonStart_Click(object sender, EventArgs e)
    {
        simulation = new Simulation((int)(reservedForMemory / userPageSize),(int)(reservedForStorage / userPageSize), this);
    }

    protected void ButtonSearchPage_Click(object sender, EventArgs e)
    {
        if (simulation != null)
        {
            simulation.userReadFunction(0);
        }
    }
}

public class Simulation
{
    private List<Program> programs = new List<Program>();
    private LogSystem log = new LogSystem();
    private Random _random = new Random();
    private int programAllowed = 0;
    private int programCounter = 0;
    private int physicalAllowed = 0;
    private int physicalCounter = 0;
    private int secondaryAllowed = 0;
    private int secondaryCounter = 0;
    private string userReadReport = "";

    _Default _mainPage;

    public Simulation(int pageCountPhysical, int pageCountSecondary, _Default main)
    {
        secondaryAllowed = pageCountSecondary;
        physicalAllowed = pageCountPhysical;
        programAllowed = (int)((pageCountPhysical + pageCountSecondary) * 1.2);
        _mainPage = main;
    }

    public void userReadFunction(int programID)
    {
        // readProgram(programID);
        // report via default page function
        _mainPage.setPhysicalList(programs);

    }

    private void setUserReadReport(string val)
    {
        userReadReport = val;
    }

    public void runSimulation()
    {
        if (_random.Next(101) > 20)
        {
            read(); // 80% chance to read
        }
        else
        {
            write(); // 20% chance to write
        }
    }

    private void read()
    {
        if (programs.Count > 0)
        {
            log.logPageRead();
            readProgram(_random.Next(programs.Count));
        }
        else
        {
            runSimulation();
        }
    }

    private int getIndex(int programID)
    {
        int index = -1;
        for (int i = 0; i < programs.Count; i++)
        {
            if (programs.ElementAt(i).getID() == programID)
            {
                index = i;
                return index;
            }
        }
        return index;
    }

    private void readProgram(int ID)
    {
        try
        {
            int index = getIndex(ID);
            Program program = programs.ElementAt(index);

            // check memory if cant find it is page fault
            if (program.getMemoryStatus())
            {
                // statistics
                log.logSuccessfulPageRead();
                setUserReadReport("Succesfully read program from physical memory!");
            }
            else
            {
                if (program.getDroppedStatus())
                {
                    // statistics
                    log.logFailedPageRead();
                    setUserReadReport("Unsuccesfully read, program was dropped from secondary storage!");
                }
                else
                {
                    // statistics
                    log.logPageFaultsResolved();
                    log.logSuccessfulPageRead();
                    log.logPageFaults();
                    // run move to physical
                    moveToPhysical(index);
                    // report
                    setUserReadReport("Page fault occured, resolving.\nSuccesfully read program from physical memory!");
                }
            }
            runSimulation();
        }
        catch (Exception)
        {
        }
    }

    private void write()
    {
        if (programCounter < programAllowed)
        {
            // make program
            Program program = new Program(programCounter, "Program " + programCounter);
            programCounter++;

            // check if physical has space
            if (physicalCounter < physicalAllowed)
            {
                // add to physical
                programs.Add(program);
            }
            else
            {
                // move oldest in physical to secondary
                moveToSecondary(findOldestPhysical());
                // add to physical
                programs.Add(program);
            }
            // run simulation
            runSimulation();
        }
    }

    private void assignToMemory(Program program, int originalIndex, bool isPhysical)
    {
        // remove from list
        programs.RemoveAt(originalIndex);
        // set allocated to physical or secondary
        program.setMemoryStatus(isPhysical);
        // add to end of list
        programs.Add(program);
    }

    private void moveToSecondary(int programIndex)
    {
        try
        {
            Program program = programs.ElementAt(programIndex);
            // check space in secondary
            if (secondaryCounter < secondaryAllowed)
            {
                assignToMemory(program, programIndex, false);
            }
            else
            {
                // drop oldest in secondary
                programs.ElementAt(findOldestSecondary()).setDroppedStatus(true);
                // add to secondary
                assignToMemory(program, programIndex, false);
            }
        }
        catch (Exception)
        {
        }
    }

    private void moveToPhysical(int programIndex)
    {
        try
        {
            Program program = programs.ElementAt(programIndex);
            // check space in secondary
            if (physicalCounter < physicalAllowed)
            {
                assignToMemory(program, programIndex, true);
            }
            else
            {
                // move oldest in physical to secondary
                moveToSecondary(findOldestPhysical());
                // add to physical
                assignToMemory(program, programIndex, true);
            }
        }
        catch (Exception)
        {
        }
    }

    private int findOldestSecondary()
    {
        try
        {
            int programIndex = -1;
            // search for oldest program in secondary
            for (int j = 0; j < programs.Count; j++)
            {
                if (!programs.ElementAt(j).getDroppedStatus())
                {
                    if (!programs.ElementAt(j).getMemoryStatus())
                    {
                        programIndex = j;
                        return programIndex;
                    }
                }
            }
            return programIndex;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    private int findOldestPhysical()
    {
        try
        {
            int programIndex = -1;
            // search for oldest program in physical
            for (int i = 0; i < programs.Count; i++)
            {
                if (!programs.ElementAt(i).getDroppedStatus())
                {
                    if (programs.ElementAt(i).getMemoryStatus())
                    {
                        programIndex = i;
                        return programIndex;
                    }
                }
            }
            return programIndex;
        }
        catch (Exception)
        {
            return -1;
        }
    }
}