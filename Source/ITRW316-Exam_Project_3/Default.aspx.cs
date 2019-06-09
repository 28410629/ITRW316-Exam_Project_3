using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    // server details variables
    ServerDetails server = new ServerDetails();

    // simulation variables
    private List<string[]> programs = new List<string[]>();
    /* string array of programs content:
     * 0 : id
     * 1 : program name
     * 2 : in memory? T or F
     * 3 : is dropped? T or F
     */

    private LogSystem log = new LogSystem();
    private Random _random = new Random();
    private int programAllowed = 0;
    private int programCounter = 0;
    private int physicalAllowed = 0;
    private int physicalCounter = 0;
    private int secondaryAllowed = 0;
    private int secondaryCounter = 0;
    private string userReadReport = "";
    private bool pagesCalculated = false;

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

        if (!pagesCalculated)
            ButtonStart.Enabled = false;
    }

    

    public void updateLabels() // server details
    {
        LabelServerDetailsDateTime.Text = "Details retrieved on " + DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToLongTimeString();
        LabelPhysicalMemory.Text = (server.getFreePhysicalMemory() / 1024).ToString() + " MB";
        LabelPhysicalMemoryTotal.Text = (server.getTotalVisibleMemorySize() / 1024).ToString() + " MB";
        LabelVirtualMemory.Text = (server.getFreeVirtualMemory() / 1024).ToString() + " MB";
        LabelVirtualMemoryTotal.Text = (server.getTotalVirtualMemorySize() / 1024).ToString() + " MB";
        LabelOSName.Text = server.getOSCaption();  // display operating system caption
        LabelOSArchitecture.Text = server.getOSArchitecture(); // display operating system architecture.
    }

    protected void ButtonCalculate_Click(object sender, EventArgs e)
    {
        if (!validateTextboxes())
        {}
        else
        {
            labelOSValidation.ForeColor = System.Drawing.Color.Black;
            labelOSValidation.Text = "...";

            labelPageFrameValidation.ForeColor = System.Drawing.Color.Black;
            labelPageFrameValidation.Text = "..."; ;

            labelMemoryValidation.ForeColor = System.Drawing.Color.Black;
            labelMemoryValidation.Text = "..."; ;

            userReservedSize = (Convert.ToInt64(TextBoxSizeOS.Text));
            userPageSize = (Convert.ToInt64(TextBoxSizePage.Text));
            userSimulationSize = (server.getFreePhysicalMemory() / 1024) - userReservedSize;
            calculatePages();

            pagesCalculated = true;
            ButtonStart.Enabled = true;
        }
    }

    public bool validateTextboxes()
    {
        if (TextBoxSizeOS.Text == "")
        {
            labelOSValidation.ForeColor = System.Drawing.Color.Red;
            labelOSValidation.Text = "Enter a value";
            return false;

        }
        else if (TextBoxSizePage.Text == "")
        {
            labelPageFrameValidation.ForeColor = System.Drawing.Color.Red;
            labelPageFrameValidation.Text = "Enter a value";
            return false;
        }
        else if (textboxMemoryPercentage.Text == "")
        {
            labelMemoryValidation.ForeColor = System.Drawing.Color.Red;
            labelMemoryValidation.Text = "Enter a value";
            return false;
        }
        else if ((Convert.ToInt64(TextBoxSizeOS.Text) < ((0.1 * server.getFreePhysicalMemory()) / 1024) || Convert.ToInt64(TextBoxSizeOS.Text) > ((0.5 * server.getFreePhysicalMemory()) / 1024)))
        {
            labelOSValidation.ForeColor = System.Drawing.Color.Red;
            labelOSValidation.Text = "Invalid Entry: OS must be larger than " + ((0.1 * server.getFreePhysicalMemory()) / 1024) + " and smaller than " + ((0.25 * server.getFreePhysicalMemory()) / 1024);
            return false;
        }

        else if (((Convert.ToInt64(TextBoxSizePage.Text)) < 10 || (Convert.ToInt64(TextBoxSizePage.Text)) > 150) && TextBoxSizePage.Text == "")
        {
            labelPageFrameValidation.ForeColor = System.Drawing.Color.Red;
            labelPageFrameValidation.Text = "Invalid Entry";
            return false;
        }
        else if (((Convert.ToInt64(textboxMemoryPercentage.Text)) < 20 || (Convert.ToInt64(textboxMemoryPercentage.Text)) > 80) && textboxMemoryPercentage.Text == "")
        {
            labelMemoryValidation.ForeColor = System.Drawing.Color.Red;
            labelMemoryValidation.Text = "Invalid Entry";
            return false;
        }
        else
            return true;
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

        LabelSimulationSize.Text = ((server.getFreePhysicalMemory() / 1024) - (userReservedSize)).ToString() + " MB";
    }

    public void setLists(List<string[]> list)
    {
        DropDownListProgramsPhysical.Items.Clear();
        DropDownListProgramsSecondary.Items.Clear();
        DropDownListProgramsRead.Items.Clear();

        int counterP = 0;
        int counterS = 0;

        list.Sort((x, y) => (Convert.ToInt32(x[0])).CompareTo(Convert.ToInt32(y[0])));

        for (int i = 0; i < list.Count; i++)
        {
            if (list.ElementAt(i)[2] == "T" && list.ElementAt(i)[3] == "F")
            {
                DropDownListProgramsPhysical.Items.Add(list.ElementAt(i)[1]);
                counterP++;
            }
            if (list.ElementAt(i)[2] == "F" && list.ElementAt(i)[3] == "F")
            {
                DropDownListProgramsSecondary.Items.Add(list.ElementAt(i)[1]);
                counterS++;
            }
            DropDownListProgramsRead.Items.Add(list.ElementAt(i)[1]);
        }
        LabelListInPhysical.Text = "(" + counterP + ")";
        LabelListInSecondary.Text = "(" + counterS + ")";
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
            setSimulation(Convert.ToInt32(LabelPageCountMemory.Text), Convert.ToInt32(labelPageCountStorage.Text));
            runSimulation();
        }
        catch (Exception)
        {
        }
        
        
    }

    protected void ButtonSearchPage_Click(object sender, EventArgs e)
    {
        programs = (List<string[]>)Session["programList"];
        log = (LogSystem)Session["logObject"];
        // read serialised data from app_data
        userReadFunction(DropDownListProgramsRead.SelectedValue);
    }

    public void setSimulation(int pageCountPhysical, int pageCountSecondary)
    {
        secondaryAllowed = pageCountSecondary;
        physicalAllowed = pageCountPhysical;
        programAllowed = (int)((pageCountPhysical + pageCountSecondary) * 1.2); // 20% more programs are created to simulate page drops as well
    }

    public void setProgramsList(List<string[]> program)
    {
        programs = program;
    }

    public void userReadFunction(string programName)
    {
        readProgram(programName);
        setReadStatus(userReadReport);
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
            readProgram(programs.ElementAt(_random.Next(programs.Count))[1]);
        }
        else
        {
            runSimulation();
        }
    }

    private int getIndex(string programName)
    {
        for (int i = 0; i < programs.Count; i++)
        {
            if (programs.ElementAt(i)[1] == programName)
            {
                return i;
            }
        }
        return -1;
    }

    private void readProgram(string programName)
    {
        try
        {
            int index = getIndex(programName);
            string[] program = programs.ElementAt(index);

            // check memory if cant find it is page fault
            if (program[2] == "T")
            {
                // statistics
                log.logSuccessfulPageRead();
                setUserReadReport("Succesfully read page from physical memory!");
            }
            else
            {
                // statistics
                log.logPageFaults();
                if (program[3] == "T")
                {
                    // statistics
                    log.logFailedPageRead();
                    setUserReadReport("Unsuccesfully read, page was dropped from secondary storage!");
                }
                else
                {
                    // statistics
                    log.logSuccessfulPageRead();
                    log.logPageFaultsResolved();
                    // run move to physical
                    moveToPhysical(index);
                    // report
                    setUserReadReport("Page fault occured, resolving.\nSuccesfully read page from physical memory!");
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
        try
        {
            setCounters();
            if (programCounter <= programAllowed)
            {
                string[] program = new string[4];
                program[0] = programCounter.ToString();
                program[1] = "Page " + programCounter;
                program[2] = "T";
                program[3] = "F";
                // stats
                log.logProgramAdded();
                // check if physical has space
                if (!(physicalCounter < physicalAllowed))
                {
                    // move oldest in physical to secondary
                    moveToSecondary(findOldestPhysical());
                }
                // add to physical
                programs.Add(program);
                // stats
                log.logPageAdd();
                // run simulation
                runSimulation();
            }
            else
            {
                finishSimulation();
            }
        }
        catch (Exception)
        {
        }
    }

    public void finishSimulation()
    {
        Session["programList"] = programs;
        Session["logObject"] = log;
        setLists(programs);
        setStatistics(log);
        setSimulationStatus("Simulation is finished, you can use read function!", System.Drawing.Color.Green);
    }

    public void setCounters()
    {
        int T = 0;
        int P = 0;
        int S = 0;
        string[] arr;
        for (int i = 0; i < programs.Count; i++)
        {
            T++;
            arr = programs.ElementAt(i);
            if (arr[2] == "T" && arr[3] == "F")
            {
                P++;
            }
            if (arr[2] == "F" && arr[3] == "F")
            {
                S++;
            }
        }
        programCounter = T;
        physicalCounter = P;
        secondaryCounter = S;
    }

    private void assignToMemory(string[] program, bool isPhysical)
    {
        // set allocated to physical or secondary
        if (isPhysical)
        {
            program[2] = "T";
        }
        else
        {
            program[2] = "F";
        }
        // add to end of list
        programs.Add(program);
    }

    private void moveToSecondary(int programIndex)
    {
        try
        {
            setCounters();
            // make copy
            string[] program = programs.ElementAt(programIndex);
            // remove original from list
            programs.RemoveAt(programIndex);
            // check space in secondary
            if (!(secondaryCounter < secondaryAllowed))
            {
                // drop oldest in secondary
                programs.ElementAt(findOldestSecondary())[3] = "T";
                log.logPageDrop();
            }
            // add to secondary
            assignToMemory(program, false);
            // statistics
            log.logPageSwap();
        }
        catch (Exception)
        {
        }
    }

    private void moveToPhysical(int programIndex)
    {
        try
        {
            setCounters();
            // make a copy
            string[] program = programs.ElementAt(programIndex);
            // remove original from list
            programs.RemoveAt(programIndex);
            // check space in secondary
            if (!(physicalCounter < physicalAllowed))
            {
                // move oldest in physical to secondary
                moveToSecondary(findOldestPhysical());
            }
            // add to physical
            assignToMemory(program, true);
            // statistics
            log.logPageUnswap();
        }
        catch (Exception)
        {
        }
    }

    private int findOldestSecondary()
    {
        try
        {
            // search for oldest program in secondary
            for (int j = 0; j < programs.Count; j++)
            {
                if (programs.ElementAt(j)[3] == "F")
                {
                    if (programs.ElementAt(j)[2] == "F")
                    {
                        return j;
                    }
                }
            }
            return -1;
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
            // search for oldest program in physical
            for (int i = 0; i < programs.Count; i++)
            {
                if (programs.ElementAt(i)[3] == "F")
                {
                    if (programs.ElementAt(i)[2] == "T")
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        catch (Exception)
        {
            return -1;
        }
    }
}