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
            }
            else
            {
                if (program.getDroppedStatus())
                {
                    log.logFailedPageRead(); // unavailable, it was dropped in swap
                }
                else
                {
                    log.logPageFaults();
                    // run move to physical
                    log.logPageFaultsResolved();
                    log.logSuccessfulPageRead();
                }
            }
          
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
            }
            else
            {
                // move first in physical to secondary
                for (int i = 0; i < programs.Count; i++)
                {
                    programs.ElementAt(i);
                }
            }

            
            // run simulation
        }
        else
        {
           // done
        }
    }
}