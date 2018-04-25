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
 */


/*
 *This script will make our machine glow, 
 *dance and make weird noises when 
 *processing the tape
 */
public class MachineGear : MonoBehaviour {

	public GameObject cellTapePrefab;
	public GameObject machine;
    public GameObject greenLight, redLight;
    public TuringMachine tm;
    public Alphabet alph; ///O Eteimoso
    public State[] states;

    private void Update()
    {
        
    }

    public void OnInputMove(bool side){
		if(side){
			machine.transform.position += new Vector3(1.5f, 0, 0);
		}
		else{
			machine.transform.position += new Vector3(-1.5f, 0, 0);
		}
	}

	public void OnSymbolInsert(GameObject infiniteTape, int key){
		//to make our "infinite" tape look really endless like Dream of Them
		infiniteTape.transform.localScale += new Vector3(1.5f, 0, 0);

		GameObject cellTape = Instantiate(cellTapePrefab);
		cellTape.GetComponent<TextMesh>().text = "" + alph.getSymbol(key);
	}

    public TuringMachine BuildMachineFromDescription(string description)
    {
        TuringMachine tm = new TuringMachine();
        Alphabet alphabet = new Alphabet();
        State[] states;
        string name = null;
        int i = 0;
        //To extract the name of the machine
        while (!description[i].Equals("#"))
        {
            name += description[i];
            description.Remove(i);
            i++;
        }

        description.Remove(i);
        i = 0;
        tm.SetName(name);

        //To extract the alphabet
        string symb = null;
        while (!description[i].Equals("#"))
        {
            if(!description[i].Equals(","))
            {
                symb += description[i];
            }
            else
            {
                alph.insertSymbol(symb);
                symb = null;
            }
            description.Remove(i);
            i++;
        }
        i = 0;

        string value = description[0] + "";
        description.Remove(0, 1); //to remove the stateNumber and the "#"
        int nStates = int.Parse(value);
        states = new State[nStates];
        
        //TODO
        //to extract the functions from each state
        while (!description[i].Equals("#"))
        {
            if (description[i].Equals("("))
            {
                while (!description[i].Equals(")")){
                    
                }
            }
        }

        tm.DefineAlphabet(alph);


        return tm;
    }
}