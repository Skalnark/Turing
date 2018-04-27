using System;
using System.Collections.Generic;
using UnityEngine;

/* ********************************************
 * Graphic Simulator for Turing Machines
 * 
 * Darcio Basilio
 * Universidade Federal da Paraiba
 * March 26, 2018
 * 
 * Copyright (c) 2018
 * All rights reserved.
 * 
 * This software is open source
 * Please read the LICENSE for terms
 * https://github.com/Skalnark/TuringMachines
 * 
 */
  
public class Alphabet
{
    private Dictionary<int, string> alph; ///O ETeimoso
    private int index;

    public Alphabet()
    {
        alph = new Dictionary<int, string>();
        //our "blank character" will be aways saved with key = 0
        alph.Add(0, " ");
        index = 0;
    }

    //To insert a new symbol and increment the lastKey
    public int insertSymbol(string c)
    {
        if (alph.ContainsValue(c))
        {
            return -1;
        }
        else
        {
            alph.Add(++index, c);
            return index;
        }
    }
    //To get the symbol through the key
    public string getSymbol(int n)
    {
        string value;
        alph.TryGetValue(n, out value);
        return value;
    }

    public int length()
    {
        return index;
    }

    public int getSymbolKey(string value)
    {
        for(int i = 0; i < index; i++)
        {
            if (alph[i].Equals(value))
            {
                return i;
            }
        }
        return int.Parse(null);
    }
}