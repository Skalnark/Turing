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
    private List<DeltaFunction> func; //Will hold the transactional functions for this state
    private int stateIdentity;
    
    //Default Constructor
    public State()
    {
         this.stateIdentity = Constants.NORMAL;
    }
    public State(DeltaFunction[] func){
    	foreach(DeltaFunction function in func)
        {
            this.func.Add(function);
        }
    	stateIdentity = Constants.NORMAL; //The state will aways be declared as a normal state
    }

    public void DefineIdentity(int i){
    	stateIdentity = i;
    }

    public int Identity(){
    	return stateIdentity;
    }

    public bool HaveFunctions()
    {
        try
        {
            if (func.Count != 0) return true;
        }
        catch { }

        return false;
    }
    public DeltaFunction DFunction(int n){
    	return func[n];
    }

    public void SetFunctions(List<DeltaFunction> df)
    {
        this.func = df;
    }

    public string toString()
    {
        string text = null;

        for(int i = 0; i < func.Count; i++)
        {
            text += func[i].getInput() + ",";
            text += func[i].getOutput() + ",";
            text += func[i].getSide() + ",";
            text += func[i].getNextState() + "|";
        }
        return text;
    }
    
    public int NumberOfFunctions()
    {
        if (func != null) return func.Count;
        else return 0;
    }

    public DeltaFunction LookForFunction(char input)
    {
        foreach(DeltaFunction df in func)
        {
            if (df.getInput() == input) return df;
        }

        return null;
    }

    public List<DeltaFunction> StateFunctions()
    {
        return func;
    }
}
