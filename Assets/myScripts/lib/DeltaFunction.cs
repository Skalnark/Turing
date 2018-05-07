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
    private char input, output; //the In and Out characters for our function
    private char side; //R for Right, L for Left, S for Stay
    private int state; //The state tha will be called by our function

    //default constructor
    public DeltaFunction() {
    }
    public DeltaFunction(char input, char output, char side, int state)
    {
    	this.input = input;
    	this.output = output;
    	this.state = state;
    	this.side = side;
    }

    public char getInput(){
    	return input;
    }

    public char getOutput(){
    	return output;
    }

    public char getSide(){
    	return side;
    }
    public int getNextState(){
    	return state;
    }

    public new string ToString()
    {
        string description = null;

        description += input + ",";
        description += output + ",";
        description += side + ",";
        description += state;

        return description;
    }
}