using System;
using System.Collections;
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
    private LinkedList<char> alph; ///O ETeimoso

    public Alphabet()
    {
        alph = new LinkedList<char>();
    }

    //To insert a new symbol and increment the lastKey
    public bool InsertSymbol(char c)
    {
        if (!LookForSymbol(c))
        {
            alph.AddLast(c);
            return true;
        }
        else
        {
            return false;
        }
    }

    public LinkedList<char> GetAlphabet()
    {
        return alph;
    }

    public bool LookForSymbol(char c)
    {
        return alph.Contains(c);
    }
}