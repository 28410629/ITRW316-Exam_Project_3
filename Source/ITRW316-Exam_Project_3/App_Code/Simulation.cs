using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    public Simulation(int pageCountPhysical, int pageCountSecondary)
    {
        secondaryAllowed = pageCountSecondary;
        physicalAllowed = pageCountPhysical;
        programAllowed = (int)((pageCountPhysical + pageCountSecondary) * 1.2);
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

    private void readProgram(int index)
    {
        try
        {
            Program program = programs.ElementAt(index);

            // check memory if cant find it is page fault
            if (program.getMemoryStatus())
            {
                log.logSuccessfulPageRead(); // read from memory
                // report read successful
            }
            else
            {
                if (program.getDroppedStatus())
                {
                    log.logFailedPageRead(); // unavailable, it was dropped in swap
                    // report read failed, dropped
                }
                else
                {
                    log.logPageFaults();
                    // run move to physical
                    if (physicalCounter < physicalAllowed)
                    {
                        // remove from list
                        programs.RemoveAt(index);
                        // set allocated to physical
                        program.setMemoryStatus(true);
                        // add to end of list
                        programs.Add(program);
                    }
                    else
                    {
                        // variables 
                        bool continuePhysicalSearch = true;
                        bool continueSecondarySearch = true;
                        int programIndex = 0;
                        // search for oldest program in physical
                        for (int i = 0; i < programs.Count; i++)
                        {
                            if (continuePhysicalSearch)
                            {
                                if (!programs.ElementAt(i).getDroppedStatus())
                                {
                                    if (programs.ElementAt(i).getMemoryStatus())
                                    {
                                        continuePhysicalSearch = false;
                                        programIndex = i;
                                    }
                                }
                            }
                        }
                    }
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
                // variables 
                bool continuePhysicalSearch = true;


                // search for oldest program in physical

                // check space in secondary
                if (secondaryCounter < secondaryAllowed)
                {
                    // add program to secondary
                    programs.ElementAt(programIndex).setMemoryStatus(false);
                }
                else
                {

                }
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