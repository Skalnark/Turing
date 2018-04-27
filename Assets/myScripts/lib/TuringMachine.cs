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
    public string SetInputFormat(string s)
    {
        // TODO: Fix this function 
        char[] separators = { ' ', ',' };
        string[] stringInput = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        int[] normalInput = new int[stringInput.Length + 1];

        for (int j = 0; j < stringInput.Length; j++)
        {
            bool notFound = true;

            for (int i = 1; i <= alph.length(); i++)
            {
                if (stringInput[j] == alph.getSymbol(i))
                {
                    normalInput[normalInput.Length] = i;
                    notFound = false;
                    break;
                }
            }

            if (notFound) return "symbol not found"; //breaks the function to return the error

        }
        normalInput[stringInput.Length] = Constants.WHITESPACE; //the last element is the whitespace
        return stringInput + "";
    }

    //to process the input tape
    public void StartMachine(string tape)
    {
        tape = SetInputFormat(tape);
        State initial = null;
        State final = null;


        foreach(State q in states)
        {
            if (q.Identity() == Constants.INITIAL) initial = q;

            else if (q.Identity() == Constants.FINAL) final = q;

            if (final != null && initial != null) break;
        }

        initial.process(tape, 0, initial, 0, 0);
    }
    
}