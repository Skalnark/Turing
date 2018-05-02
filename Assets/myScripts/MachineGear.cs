using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text textInput;

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

    public TuringMachine BuildMachineFromDescription(string d)
    {
        Alphabet alph = new Alphabet(); ///O ETeimoso

        //Split the string in components
        String[] description = d.Split('#'); 
        //Split the Alphabet in symbols
        String[] symbols = description[1].Split(','); 
        //Split the delta functions in functions from each state
        String[] deltaFunctions = description[5].Split(';'); 

        string name = description[0];
        string machineDescription = description[6];

        int initial = int.Parse(description[3]);
        int final = int.Parse(description[4]);

        State[] states = new State[int.Parse(description[2])];

        foreach(string s in symbols)
        {
            alph.insertSymbol(s);
        }

        
        for(int i = 0; i < deltaFunctions.Length; i++)
        {
            String[] functions = deltaFunctions[i].Split('|');
            DeltaFunction[] df = new DeltaFunction[functions.Length];
            
            for(int j = 0; j < functions.Length; j++)
            {
                String[] function = functions[j].Split(',');
                df[i] = new DeltaFunction(
                    alph.getSymbolKey(function[0]), //Symbol readed
                    alph.getSymbolKey(function[1]), //Symbol to write
                    functions[2], //The side wich the "machine's head" should go
                    states[int.Parse(function[3])]); //The state to go
            }

            states[i] = new State(df);
        }

        states[initial].DefineIdentity(Constants.INITIAL);
        states[final].DefineIdentity(Constants.FINAL);

        tm = new TuringMachine(name, alph, states, machineDescription);

        return tm;
    }
    
    public void WriteMachine(TuringMachine turingMachine)
    {
        string thisname = name.Replace(' ', '\0');
        try
        {
            System.IO.File.WriteAllText(thisname + ".txt", turingMachine.toString());
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    
    public void ReceiveTape() 
    {
        try
        {
            String input = textInput.text;
            Utils.InputToTape(input, cellTapePrefab);
        }catch(System.Exception e)
        {
            Debug.Log(e);
        }
        textInput.text = "";
        
    }   
}