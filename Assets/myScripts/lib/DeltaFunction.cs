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

/* The following class holds the generic class
* that works as the transactional function for
* the TM work. It will be part of the State
* class.
*/
public class DeltaFunction
{
    private int input, output; //the In and Out characters for our function
    private string side; //True for Right, False for Left
    private State state; //The state tha will be called by our function

    //default constructor
    public DeltaFunction(int input, int output, State state, string side){
    	this.input = input;
    	this.output = output;
    	this.state = state;
    	this.side = side;
    }

    public int getInput(){
    	return input;
    }

    public int getOutput(){
    	return output;
    }

    public string getSide(){
    	return side;
    }
    public State getNextState(){
    	return state;
    }


    public string toString()
    {
        string description = null;

        description = input + "," + output + "," + side + "," + state.Name();

        return description;
    }
}