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

/* The following class simulates the states of our TM. 
* It will have the transactional functions and a 
* indicator if it is a final or acceptance state.
*/
public class State
{
    private string stateName;
    private DeltaFunction[] func; //Will hold the transactional functions for this state
    private int stateIdentity;

    public State(DeltaFunction[] func, string stateName){
        this.stateName = stateName;
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

    public DeltaFunction function(int n){
    	return func[n];
    }
    public int numberOfFunctions()
    {
        return func.Length;
    }

    public string processDeepness(int deepness)
    {
        if (deepness == 40)
        {
            return "Is this algorithm decidable?";
        }
        else if (deepness == 100)
        {
            return "This simulation uses recursive functions that may use too much memory if you run a undecidable algorithm for too much steps";
            //TODO function that glow the "stop" button
        }
        else if (deepness == 200)
        {
            return "Our machine won't crash, your's I can't guarantee";
            //TODO function that glow the "stop" button
        }
        else return null;
    }

    //So this method will process any input til it reach
    //the last symbol from the input
    public void process(string tape, int index, State state, int n, int deepness)
	{
        processDeepness(deepness);

        //If the current function have an output for the input
        if (state.function(n).getInput() == tape[index]) {
            //TODO process the input
            process(tape, index, state.function(n).getNextState(), 0, deepness); //This will run the next process
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
            process(tape, index, state, n + 1, deepness+1); //seek on the next function
        }
    }
}
