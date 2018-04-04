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

        //This last one is for the alphabet key
        public const int WHITESPACE = 0;
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

        public int length()
        {
            return index;
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


        public string funcDescription()
        {
            string description = null;

            description = input + ";" + output + ";" + side + ";" + state.name();

            return description;
        }
    }

   /* The following class simulates the states of our TM. 
	* It will have the transactional functions and a 
	* indicator if it is a final or acceptance state.
	*/
	class State
	{
        private string stateName;
	    private GamaFunction[] func; //Will hold the transactional functions for this state
	    private int stateIdentity;

	    public State(GamaFunction[] func, string s){
            stateName = s;
	    	this.func = func;
	    	stateIdentity = Constants.NORMAL; //The state will aways be declared as a normal state
	    }

	    public void defineIdentity(int i){
	    	stateIdentity = i;
	    }

        public void setName(string n)
        {
            stateName = n;
        }
        
        public string name()
        {
            return stateName;
        }
	    public int identity(){
	    	return stateIdentity;
	    }

	    public GamaFunction function(int n){
	    	return func[n];
	    }
        public int numberOfFunctions()
        {
            return func.Length;
        }

	    //So this method will process any input til it reach
	    //the last symbol from the input
	    public void process(string tape, int index, State state, int n )
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
        Alphabet alph = new Alphabet();

        public bool searchForElement(int n, char c)
        {
            if (alph.getSymbol(n) == c) return true;
            else return false;
        }
        //This function will set the alphabet to the required patterns
        public string defineAlphabet(string s)
        {
            char[] input = s.ToCharArray();

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
            char[] charInput;
            charInput = s.ToCharArray();

            int[] normalInput = new int[charInput.Length + 1];

            for (int j = 0; j < charInput.Length; j++)
            {
                bool notFound = true;

                for (int i = 1; i <= alph.length(); i++)
                {
                    if (charInput[j] == alph.getSymbol(i))
                    {
                        normalInput[normalInput.Length] = i;
                        notFound = false;
                        break;
                    }
                }

                if (notFound) return "symbol not found"; //breaks the function to return the error

            }
            normalInput[charInput.Length] = Constants.WHITESPACE; //the last element is the whitespace
            return charInput + "";
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

        public string toString()
        {
            string description = "";
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

            description += "#";

            foreach(State s in states)
            {
                description += s.name() + ": ";
                for(int i = 0; i < s.numberOfFunctions(); i++)
                {
                    description += "("+ s.function(i).funcDescription() + "); ";
                }
            }

            description += "#" + initialState + "#" + finalState;

            return description;
        }
	}
}