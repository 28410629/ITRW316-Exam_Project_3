using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Program
{
    private int _id;
    private string _name;
    private bool _inMemory = true;
    private bool _dropped = false;

    public Program(int id, string name)
    {
        setID(id);
        setName(name);
    }

    public int getID()
    {
        return _id;
    }

    public string getName()
    {
        return _name;
    }

    public bool getMemoryStatus()
    {
        return _inMemory; // true = physical, false = secondary
    }

    public bool getDroppedStatus()
    {
        return _dropped; 
    }

    private void setID(int val)
    {
        _id = val;
    }

    private void setName(string val)
    {
       _name = val;
    }

    public void setMemoryStatus(bool val)
    {
        _inMemory = val; // true = physical, false = secondary
    }

    public void setDroppedStatus(bool val)
    {
        _dropped = val;
    }
}