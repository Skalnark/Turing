using System;
using System.Collections;
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
 * 
 */

/* This is the last class for our TM. It will hold all
* the states instances and his functions. This is the
* principal class on this file.
*/
public class TuringMachine : MonoBehaviour
{
    public float speed = 10;

    string tmName;
    string description;

	List<State> states;
    Alphabet alph = new Alphabet();
    public GameObject cellTapePrefab;
    public GameObject crossLight;
    public GameObject correctLight;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject stayStill;

    IEnumerator currentStopCoroutine;
    IEnumerator currentMoveMachineCoroutine;
    IEnumerator currentBlinkCoroutine;

    public void InstantiateMachine(MachineList tm)
    {
        this.tmName = tm.mName;
        this.alph = tm.alph;
        this.states = tm.states;
        this.description = tm.description;
    }

    public int InitialStateIndex()
    {
        int index = -1;

        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].Initial())
            {
                return i;
            }
        }
        return index;
    }

    public int FinalStateIndex()
    {
        int index = -1;
        for(int i = 0; i < states.Count; i++)
        {
            if(states[i].Identity() == Constants.FINAL)
            {
                index = i;
                return index;
            }
        }
        return index;
    }

    public State StateByIndex(int n)
    {
        return states[n];
    }

    public int NumberOfStates()
    {
        if (states != null) return states.Count;
        else return 0;
    }

    public string GetName()
    {
        return tmName;
    }

    public Alphabet GetAlphabet()
    {
        return alph;
    }
    
    public string GetDescription()
    {
        return description;
    }

    public List<State> GetStates()
    {
        return states;
    }

    //This function will set the alphabet to the required patterns
    public void DefineAlphabet(Alphabet alph)
    {
        this.alph = alph;
    }

    public ArrayList ProcessCell(State state, int index)
    {
        ArrayList whatReturn = new ArrayList();
        GameObject cellTape = GameObject.FindGameObjectWithTag("actualCell");

        if (state.HaveFunctions())
        {
            if (cellTape != null)
            {
                DeltaFunction df = state.LookForFunction(char.Parse(cellTape.GetComponent<TextMesh>().text));
                if (df != null)
                {
                    index = df.getNextState();
                    Utils.WriteOnDisplay("stateDisplay", index + "");
                    if (df.getOutput() == 'v')
                    {
                        try
                        {
                            Destroy(cellTape);
                        }
                        catch { }
                    }
                    else
                    {
                        cellTape.GetComponent<TextMesh>().text = df.getOutput() + "";
                    }

                    if (currentMoveMachineCoroutine != null) StopCoroutine(currentMoveMachineCoroutine);

                    currentMoveMachineCoroutine = MoveMachine(df.getSide(), GameObject.FindGameObjectWithTag("TuringMachine").transform.position);
                    StartCoroutine(currentMoveMachineCoroutine);
                    whatReturn.Add(true);
                    whatReturn.Add(index);
                    return whatReturn;
                }
            }
            else
            {
                DeltaFunction df = state.LookForFunction('v');
                if (df != null)
                {
                    index = df.getNextState();
                    Utils.WriteOnDisplay("stateDisplay", index + "");
                    if (df.getOutput() == 'v')
                    {
                        try
                        {
                            Destroy(cellTape);
                        }
                        catch { }

                        if (currentMoveMachineCoroutine != null) StopCoroutine(currentMoveMachineCoroutine);

                        currentMoveMachineCoroutine = MoveMachine(df.getSide(), GameObject.FindGameObjectWithTag("TuringMachine").transform.position);
                        StartCoroutine(currentMoveMachineCoroutine);

                        whatReturn.Add(true);
                        whatReturn.Add(index);
                        return whatReturn;
                    }
                    else
                    {
                        Utils.InstantiateCell(df.getOutput(), cellTapePrefab);

                        if (currentMoveMachineCoroutine != null) StopCoroutine(currentMoveMachineCoroutine);

                        currentMoveMachineCoroutine = MoveMachine(df.getSide(), GameObject.FindGameObjectWithTag("TuringMachine").transform.position);
                        StartCoroutine(currentMoveMachineCoroutine);
                        whatReturn.Add(true);
                        whatReturn.Add(index);
                        return whatReturn;
                    }
                    
                }
                else
                {
                    whatReturn.Add(false);
                    whatReturn.Add(index);
                    return whatReturn;
                }
            }
        }
        whatReturn.Add(false);
        whatReturn.Add(index);
        return whatReturn;
    }

    public IEnumerator StopMachine(State state)
    {
        Utils.WriteOnDisplay("stateDisplay", InitialStateIndex() + "");
        if (state.Identity() == Constants.FINAL)
        {
            correctLight.SetActive(true);
            yield return new WaitForSeconds(3);
            correctLight.SetActive(false);
        }
        else
        {
            crossLight.SetActive(true);
            yield return new WaitForSeconds(3);
            crossLight.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("input").GetComponent<InputField>().interactable = true;

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        stayStill.SetActive(false);

        
        StopAllCoroutines();
    }
    
    IEnumerator MoveMachine(char side, Vector3 target)
    {
        GameObject machine = GameObject.FindGameObjectWithTag("TuringMachine");

        target.x -= (target.x % 1.5f);

        if (side == 'R')
        {

            if (currentBlinkCoroutine != null) StopCoroutine(currentBlinkCoroutine);
            currentBlinkCoroutine = BlinkRight();
            StartCoroutine(currentBlinkCoroutine);
            machine.transform.position = target;
            target.x += 1.5f;
            while(machine.transform.position.x < target.x)
            {
                machine.transform.position += new Vector3(Time.deltaTime*speed, 0, 0);
                yield return null;
            }
        }

        else if(side == 'L')
        {

            if (currentBlinkCoroutine != null) StopCoroutine(currentBlinkCoroutine);
            currentBlinkCoroutine = BlinkLeft();
            StartCoroutine(currentBlinkCoroutine);
            machine.transform.position = target;
            target.x -= 1.5f;
            while (machine.transform.position.x > target.x)
            {
                machine.transform.position -= new Vector3(Time.deltaTime*speed, 0, 0);
                yield return null;
            }
        }
        else
        {
            if (currentBlinkCoroutine != null) StopCoroutine(currentBlinkCoroutine);
            currentBlinkCoroutine = BlinkStay();
            StartCoroutine(currentBlinkCoroutine);
        }

        machine.transform.position = target;

        yield return new WaitForSeconds(0.2f);
    }
    
    IEnumerator BlinkRight()
    {
        rightArrow.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        rightArrow.SetActive(false);
    }
    IEnumerator BlinkLeft()
    {
        leftArrow.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        leftArrow.SetActive(false);
    }
    IEnumerator BlinkStay()
    {
        stayStill.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        stayStill.SetActive(false);
    }

    new public string ToString()
    {
        string completeDescription = name + "#";

        foreach(char c in alph.GetAlphabet())
        {
            completeDescription += c;
        }

        completeDescription += "#" + states.Count + "#";

        completeDescription += InitialStateIndex() + "#";

        completeDescription += FinalStateIndex() + "#";

        for(int i = 0; i < states.Count; i++)
        {
            if(states[i].NumberOfFunctions() > 0)
            {
                for(int j = 0; i < states[i].NumberOfFunctions(); i++)
                {
                    completeDescription += states[i].DFunction(j).getInput() + ",";
                    completeDescription += states[i].DFunction(j).getOutput() + ",";

                    completeDescription += states[i].DFunction(j).getSide() + ",";

                    completeDescription += states[i].DFunction(j).getNextState();

                    if (states[i].NumberOfFunctions() != j)
                        completeDescription += "|";
                }

                if (states[i].NumberOfFunctions() != i)
                    completeDescription += ";";
            }
            else
            {
                completeDescription += "void;";
            }
        }

        completeDescription += "#" + description;

        return completeDescription;
    }
}