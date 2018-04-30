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
    private DeltaFunction[] func; //Will hold the transactional functions for this state
    private int stateIdentity;
    
    //Default Constructor
    public State(string name)
    {
        this.stateIdentity = Constants.NORMAL;
    }
    public State(DeltaFunction[] func){
    	this.func = func;
    	stateIdentity = Constants.NORMAL; //The state will aways be declared as a normal state
    }

    public void DefineIdentity(int i){
    	stateIdentity = i;
    }

    public int Identity(){
    	return stateIdentity;
    }

    public DeltaFunction DFunction(int n){
    	return func[n];
    }
    public int NumberOfFunctions()
    {
        return func.Length;
    }

    public string toString()
    {
        string text = null;

        for(int i = 0; i < func.Length; i++)
        {
            text += func[i].getInput() + ",";
            text += func[i].getOutput() + ",";
            text += func[i].getSide() + ",";
            text += func[i].getNextState() + "|";
        }
        return text;
    }

    public string ProcessDeepness(int deepness)
    {
        if (deepness == 100)
        {
            return "Is this algorithm decidable?";
        }
        else if (deepness == 500)
        {
            return "This simulation use recursive functions that may use too much memory if you run an algorithm for too much steps";
            //TODO function that glow the "stop" button
        }
        else if (deepness == 1000)
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
        ProcessDeepness(deepness);

        //If the current function have an output for the input
        if (state.DFunction(n).getInput() == tape[index]) {
            //TODO process the input
            process(tape, index, state.DFunction(n).getNextState(), 0, deepness); //This will run the next process
        }
        else if (tape[index] == 0) {
            if (state.Identity() == Constants.FINAL)
            {
                //TODO accept
            }
            else {
                //TODO reject
            }
        }
        else {
            process(tape, index, state, n + 1, deepness+1); //seek on the next function
            //TODO se o estado não processar a entrada, tratar a exceção
        }
    }
}
