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
    public Simulation sim;

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

    public void setLists(List<Program> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list.ElementAt(i).getMemoryStatus() && !list.ElementAt(i).getDroppedStatus())
            {
                DropDownListProgramsPhysical.Items.Add(list.ElementAt(i).getName());
            }
            if (!list.ElementAt(i).getMemoryStatus() && !list.ElementAt(i).getDroppedStatus())
            {
                DropDownListProgramsSecondary.Items.Add(list.ElementAt(i).getName());
            }
            DropDownListProgramsRead.Items.Add(list.ElementAt(i).getName());
        }
    }

    public void setStatistics(LogSystem log)
    {
        LabelTotalPrograms.Text = log.getTotalPrograms();
        LabelTotalDroppedPages.Text = log.getTotalDroppedPages();
        LabelTotalFailedPageReads.Text = log.getTotalPageReadsFailed();
        LabelTotalPageFaults.Text = log.getTotalPageFaults();
        LabelTotalPageFaultsResolved.Text = log.getTotalPageFaultsResolved();
        LabelTotalPageReads.Text = log.getTotalPageReads();
        LabelTotalSuccesfulPageReads.Text = log.getTotalPageReadsSuccesful();
        LabelTotalSwappedPages.Text = log.getTotalSwappedPages();
        LabelTotalUnswappedPages.Text = log.getTotalUnswappedPages();
    }

    public void setReadStatus(string val)
    {
        LabelReadStatus.Text = val;
    }

    public void setSimulation(Simulation sim)
    {
        this.sim = sim;
    }

    public void setSimulationStatus(string val, System.Drawing.Color color)
    {
        LabelSimulationStatus.Text = val;
        LabelSimulationStatus.ForeColor = color;
    }

    protected void ButtonStart_Click(object sender, EventArgs e)
    {
        try
        {
            LabelSimulationStatus.Text = "Simulation is in progress, please wait.";
            LabelSimulationStatus.ForeColor = System.Drawing.Color.Yellow;
            simulation = new Simulation(Convert.ToInt32(LabelPageCountMemory.Text), Convert.ToInt32(labelPageCountStorage.Text), this);
            simulation.runSimulation();
        }
        catch (Exception)
        {
        }
    }

    protected void ButtonSearchPage_Click(object sender, EventArgs e)
    {
        sim.userReadFunction(DropDownListProgramsRead.SelectedValue);
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
        programAllowed = (int)((pageCountPhysical + pageCountSecondary) * 1.2); // 20% more programs are created to simulate page drops as well
        _mainPage = main;
    }

    public void userReadFunction(string programName)
    {
        readProgram(programName);
        _mainPage.setReadStatus(userReadReport);
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
            readProgram(programs.ElementAt(_random.Next(programs.Count)).getName());
        }
        else
        {
            runSimulation();
        }
    }

    private int getIndex(string programName)
    {
        int index = -1;
        for (int i = 0; i < programs.Count; i++)
        {
            if (programs.ElementAt(i).getName() == programName)
            {
                index = i;
                return index;
            }
        }
        return index;
    }

    private void readProgram(string programName)
    {
        try
        {
            int index = getIndex(programName);
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
                // statistics
                log.logPageFaults();
                if (program.getDroppedStatus())
                {
                    // statistics
                    log.logFailedPageRead();
                    setUserReadReport("Unsuccesfully read, program was dropped from secondary storage!");
                }
                else
                {
                    // statistics
                    log.logSuccessfulPageRead();
                    log.logPageFaultsResolved();
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
            // stats
            log.logProgramAdded();

            // check if physical has space
            if (physicalCounter < physicalAllowed)
            {
                // add to physical
                programs.Add(program);
                physicalCounter++;
                // stats
                log.logPageAdd();
            }
            else
            {
                // move oldest in physical to secondary
                moveToSecondary(findOldestPhysical());
                // add to physical
                programs.Add(program);
                // stats
                log.logPageAdd();
            }
            // run simulation
            runSimulation();
        }
        else
        {
            _mainPage.setLists(programs);
            _mainPage.setStatistics(log);
            _mainPage.setSimulationStatus("Simulation is finished, you can use read function!", System.Drawing.Color.Green);
            _mainPage.setSimulation(this);
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
                secondaryCounter++;
                log.logPageSwap();
            }
            else
            {
                // drop oldest in secondary
                programs.ElementAt(findOldestSecondary()).setDroppedStatus(true);
                log.logPageDrop();
                // add to secondary
                assignToMemory(program, programIndex, false);
                log.logPageSwap();
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
                log.logPageUnswap();
            }
            else
            {
                // move oldest in physical to secondary
                moveToSecondary(findOldestPhysical());
                // add to physical
                assignToMemory(program, programIndex, true);
                log.logPageUnswap();
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