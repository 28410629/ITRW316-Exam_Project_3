using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogSystem
/// </summary>
public class LogSystem
{
    private int totalPrograms = 0;
    private int totalPageReads = 0;
    private int totalPageReadsSuccesful = 0;
    private int totalPageReadsFailed = 0;
    private int totalPageFaults = 0;
    private int totalPageFaultsResolved = 0;
    private int totalUnswappedPages = 0;
    private int totalHitsTLB = 0;
    private int totalMissesTLB = 0;
    private int totalSwappedPages = 0;
    private int totalPages = 0;
    private int totalDroppedPages = 0;
    private double totalPagesFragAmount = 0;
    private double totalPagesFragSize = 0;

    public LogSystem()
    {
    }

    public void logFramentation(int pages, int programSize)
    {
        totalPagesFragAmount += (double)pages;
        totalPagesFragSize += (double)programSize;
    }

    public void logProgramAdded()
    {
        totalPrograms++;
    }

    public void logPageRead()
    {
        totalPageReads++;
    }

    public void logTLBHit()
    {
        totalHitsTLB++;
    }

    public void logTLBMiss()
    {
        totalMissesTLB++;
    }

    public void logSuccessfulPageRead()
    {
        totalPageReadsSuccesful++;
    }

    public void logFailedPageRead()
    {
        totalPageReadsFailed++;
    }

    public void logPageFaultsResolved()
    {
        totalPageFaultsResolved++;
    }

    public void logPageFaults()
    {
        totalPageFaults++;
    }

    public void logPageDrop()
    {
        totalDroppedPages++;
    }

    public void logPageSwap()
    {
        totalSwappedPages++;
    }

    public void logPageAdd()
    {
        totalPages++;
    }

    public void logPageUnswap()
    {
        totalUnswappedPages++;
    }


    public string getTotalPrograms()
    {
        return totalPrograms.ToString();
    }

    public string getTotalPages()
    {
        return totalPages.ToString();
    }

    public string getTotalSwappedPages()
    {
        return totalSwappedPages.ToString();
    }

    public string getTotalUnswappedPages()
    {
        return totalUnswappedPages.ToString();
    }
    public string getTotalDroppedPages()
    {
        return totalDroppedPages.ToString();
    }
    public string getTotalPageReads()
    {
        return totalPageReads.ToString();
    }
    public string getTotalPageReadsSuccesful()
    {
        return totalPageReadsSuccesful.ToString();
    }
    public string getTotalPageReadsFailed()
    {
        return totalPageReadsFailed.ToString();
    }

    public string getTotalPageFaults()
    {
        return totalPageFaults.ToString();
    }
    public string getTotalPageFaultsResolved()
    {
        return totalPageFaultsResolved.ToString();
    }
}