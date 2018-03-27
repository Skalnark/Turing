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

  
 namespace TM{

 	public static class Constants
	{
    	public const int NORMAL = 0;
    	public const int FINAL = 1;
    	public const int INITIAL = 2;
	}
     
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

   /* The following class holds the generic class
	* that works as the transactional function for
	* the TM work. It will be part of the State
	* class.
	*/
	class GamaFunction
	{
	    private int input, output; //the In and Out characters for our function
	    private bool side; //True for Right, False for Left
	    private State state; //The state tha will be called by our function

	    //default constructor
	    public GamaFunction(int input, int output, State state, bool side){
	    	this.input = input;
	    	this.output = output;
	    	this.state = state;
	    	this.side = side;
	    }

	    //Getters and setters
	    public int read(){
	    	return input;
	    }

	    public int write(){
	    	return output;
	    }

	    public bool goTo(){
	    	return side;
	    }
	    public State nextState(){
	    	return state;
	    }
	}

   /* The following class simulates the states of our TM. 
	* It will have the transactional functions and a 
	* indicator if it is a final or acceptance state.
	*/
	class State
	{
	    private GamaFunction[] func; //Will hold the transactional functions for this state
	    private int stateIdentity;

	    public State(GamaFunction[] func){
	    	this.func = func;
	    	stateIdentity = Constants.NORMAL; //The state will aways be declared as a normal state
	    }

	    public void defineIdentity(int i){
	    	stateIdentity = i;
	    }

	    public int identity(){
	    	return stateIdentity;
	    }

	    public GamaFunction function(int n){
	    	return func[n];
	    }

	    //So this method will process any input til it reach
	    //the last symbol from the input
	    //we'll need to re-indent the code
	    public void process(
	    					int[] tape, //the array of keys for the hash of our tape
	    					int index, //the current symbol to be processed
	    					State state, //the current TM's state
	    					int n //The number for the recursivity function
						   )
		{
            //If the current function have an output for the input
            if (state.function(n).read() == tape[index]) {
                //TODO process the input
                process(tape, index, state.function(n).nextState(), 0); //This will run the next process
            }
            else if (tape[index] == 0) {
                if (state.identity() == Constants.FINAL)
                {
                    //TODO accept
                }
                else {
                    //TODO reject
                }
            }
            else {
                process(tape, index, state, n + 1); //seek on the next function
            }
	    }
	}

	/* This is the last class for our TM. It will hold all
	* the states instances and his functions. This is the
	* principal class on this file.
	*/
	class TuringMachine
	{
		State[] states;
	    //TODO Principal Class
	}
}