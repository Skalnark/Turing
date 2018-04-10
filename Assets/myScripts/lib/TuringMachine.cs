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
class TuringMachine
{
	State[] states;
    Alphabet alph = new Alphabet();

    public bool searchForElement(int n, string c)
    {
        if (alph.getSymbol(n) == c) return true;
        else return false;
    }
    //This function will set the alphabet to the required patterns
    public string defineAlphabet(string s)
    {
        char[] separators = { ' ', ','};
        string[] input = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        //to check if there's any duplicated symbol on the string
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                if (input[i] == input[j])
                {
                    if (i != j)
                    {
                        return "duplicated symbol";
                    }
                }
            }

            alph.insertSymbol(input[i]);
        }

        return "alphabet successfully created";
    }
    //This function will map the symbols on their respective keys on our hash,
    //so we'll be able to use the alphabet's hash numbers instead of their symbols
    public string setInputFormat(string s)
    {
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
    public void startMachine(string tape)
    {
        tape = setInputFormat(tape);
        State initial = null;
        State final = null;


        foreach(State q in states)
        {
            if (q.identity() == Constants.INITIAL) initial = q;

            else if (q.identity() == Constants.FINAL) final = q;

            if (final != null && initial != null) break;
        }

        initial.process(tape, 0, initial, 0);
    }

    //to save the TM's description
    public void saveMachine()
    {
        
    }

    //This function is suposed to process all the elements from our TM and
    //give us the normalized description.
    public string toString()
    {
        string description = null;
        string initialState = null;
        string finalState = null;

        for(int i = 0; i < alph.length(); i++)
        {
            description += alph.getSymbol(i) +" ,";
        }
        description += "#";

        foreach (State s in states)
        {
            description = description + s.name() + " ,";

            if (s.identity() == Constants.INITIAL)
                initialState = s.name();
            if (s.identity() == Constants.FINAL)
                finalState = s.name();
        }
        
        description += "#" + initialState + "#" + finalState + "#";

        foreach (State s in states)
        {
            description += s.name() + ": ";
            for(int i = 0; i < s.numberOfFunctions(); i++)
            {
                description += "("+ s.function(i).toString() + ")";

                if (i != s.numberOfFunctions() - 1)
                    description += ", ";

                else description += "; ";
            }
        }

        return description;
    }
}