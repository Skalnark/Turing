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
	public Alphabet alph; ///O ETeimoso


	public void onInputMove(bool side){
		if(side){
			machine.transform.position += new Vector3(1.5f, 0, 0);
		}
		else{
			machine.transform.position += new Vector3(-1.5f, 0, 0);
		}
	}

	public void onSymbolInsert(GameObject infiniteTape, int key){
		//to make our "infinite" tape look really endless like Dream of Them
		infiniteTape.transform.localScale += new Vector3(1.5f, 0, 0);

		GameObject cellTape = Instantiate(cellTapePrefab);
		cellTape.GetComponent<TextMesh>().text = "" + alph.getSymbol(key);
	}
}