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

  /* The following class is the tape's alphabet: 
   * A generic class that will hold any alphabet
   * necessary for the simulated Turing Machine.
   */
	class Alphabet
	{
	    private Dictionary<int, char> alph; ///O ETeimoso
	    private int index;

	    public Alphabet()
	    {
	        alph = new Dictionary<int, char>();
	        //our "blank character" will be aways saved with key = 0
	        alph.Add(0, ' ');
	        index = 0;
	    }

	    //To insert a new symbol and increment the lastKey
	    public void insertSymbol(char c)
	    {
	        alph.Add(++index, c);
	    }

	    //To get the symbol through the key
	    public char getSymbol(int n)
	    {
	        char value;
	        alph.TryGetValue(n, out value);
	        return value;
	    }
	}

	/*
	* the following class holds the generic class
	* that works as the transactional function for
	* the TM work. It will be part of the State
	* class.
	*/
	class GamaFunction
	{
	    //TODO Transactional Function
	}

	/* The following class simulates the states of our TM. 
	* It will have the transactional functions and a 
	* indicator if it is a final or acceptance state.
	*/
	class State
	{
	    //TODO States
	}

	/* This is the last class for our TM. It will hold all
	* the states instances and his functions. This is the
	* principal class on this file.
	*/
	class TuringMachine
	{
	    //TODO Principal Class
	}