﻿using System;
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
    private string userReadReport = "";

    public Simulation(int pageCountPhysical, int pageCountSecondary)
    {
        secondaryAllowed = pageCountSecondary;
        physicalAllowed = pageCountPhysical;
        programAllowed = (int)((pageCountPhysical + pageCountSecondary) * 1.2);
    }

    public void userReadFunction(int programID)
    {
        readProgram(programID);
        // report via default page function
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