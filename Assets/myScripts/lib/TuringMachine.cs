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

/* This is the last class for our TM. It will hold all
* the states instances and his functions. This is the
* principal class on this file.
*/
public class TuringMachine
{
    string name;
    string description;

	State[] states;
    Alphabet alph = new Alphabet();

    public TuringMachine(string name, Alphabet alph, State[] states, string description)
    {
        this.name = name;
        this.alph = alph;
        this.states = states;
        this.description = description;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
    public bool SearchForElement(int n, string c)
    {
        if (alph.getSymbol(n) == c) return true;
        else return false;
    }
    //This function will set the alphabet to the required patterns
    public void DefineAlphabet(Alphabet alph)
    {
        this.alph = alph;
    }
    //This function will map the symbols on their respective keys on our hash,
    //so we'll be able to use the alphabet's hash numbers instead of their symbols
    public string SetInputFormat(String[] s)
    {
        string str = null;
        return str;
    }

    //to process the input tape
    public void StartMachine(String[] t)
    {
        string tape = SetInputFormat(t);
        State initial = null;

        foreach(State q in states)
        {
            if (q.Identity() == Constants.INITIAL)
            {
                initial = q;
                break;
            }
        }

        initial.process(tape, 0, initial, 0, 0);
    }
    
    public string toString()
    {
        string text = name;
        text += "#";

        for(int i = 0; i < alph.length(); i++)
        {
            text += alph.getSymbol(i) + ",";
        }

        text += "#";

        text += states.Length;

        text += "#";

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].Identity() == Constants.INITIAL)
            {
                text += i;
                break;
            }
        }

        text += "#";

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].Identity() == Constants.FINAL)
            {
                text += i;
                break;
            }
        }

        text += "#";

        for(int i = 0; i < states.Length; i++)
        {
            text += states[i].toString() + ";";
        }

        text.Remove(text.Length);

        text += "#";

        text += description;

        return text;
    }
}