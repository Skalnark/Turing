using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
public class MachineGear : MonoBehaviour
{
    string description;
    public float speed = 0.4f;

    public Text velocity;

    public GameObject cellTapePrefab;
    public GameObject machine;
    public GameObject greenLight, redLight;
    public GameObject startButtonLight;

    public TuringMachine tm;
    
    public InputField input;

    private IEnumerator currentStopCoroutine;
    private IEnumerator currentProccessing;
    private IEnumerator initialcorroutine;
    private IEnumerator initialCoroutine;

    void Start()
    {
        try
        {
            description = System.IO.File.ReadAllText("Assets/mt.txt");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        tm = GetComponent<TuringMachine>();

        LoadMachine();
        
    }
    void Update()
    {
        UpdateSpeed();
    }

    public void ProcessState()
    {
        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = false;

        int actualState = int.Parse(GameObject.FindGameObjectWithTag("stateDisplay").GetComponent<TextMesh>().text);
        ArrayList result = tm.ProcessCell(tm.StateByIndex(actualState), actualState);

        if ((bool)result[0])
        {
            GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = true;
            return;
        }
        else
        {
            if (currentStopCoroutine != null) StopCoroutine(currentStopCoroutine);

            currentStopCoroutine = tm.StopMachine(tm.StateByIndex((int)result[1]));

            StartCoroutine(currentStopCoroutine);
        }

    }


    public void UpdateSpeed()
    {

            if (velocity.text == "1")
            {
                speed = 1;
            }
            else if (velocity.text == "2")
            {
                speed = 0.75f;
            }
            else
            {
                speed = 0.4f;
            }
    }

    public void IncrementVelocity()
    {
        if (velocity.text == "1")
        {
            velocity.text = "2";
        }
        else if (velocity.text == "2")
        {
            velocity.text = "3";
        }
    }
    public void DecrementVelocity()
    {
        if (velocity.text == "3")
        {
            velocity.text = "2";
        }
        else if (velocity.text == "2")
        {
            velocity.text = "1";
        }
    }

    public TuringMachine SimulatedMachine()
    {
        return tm;
    }

    public void LoadMachine()
    {
        tm = BuildMachineFromDescription(tm, description);
        Debug.Log("machine loaded");

        Utils.WriteOnDisplay("stateDisplay", tm.InitialStateIndex() + "");
    }

    public void StartMachine()
    {
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = false;

        startButtonLight.GetComponent<Light>().color = Color.green;

        ArrayList initial = new ArrayList();

        initial.Add(true);
        initial.Add(tm.InitialStateIndex());
        
        initialCoroutine = StartProcessing(initial);

        StartCoroutine(initialCoroutine);

    }

    public IEnumerator StartProcessing(ArrayList result)
    {
        
        yield return new WaitForSeconds(speed);
        if ((bool)result[0])
        {
            if (currentProccessing != null) StopCoroutine(currentProccessing);
            currentProccessing = StartProcessing(tm.ProcessCell(tm.StateByIndex((int)result[1]), (int) result[1]));
            StartCoroutine(currentProccessing);
        }
        else
        {
            if (currentStopCoroutine != null) StopCoroutine(currentStopCoroutine);

            currentStopCoroutine = tm.StopMachine(tm.StateByIndex((int)result[1]));

            StartCoroutine(currentStopCoroutine);
        }
    }

    public void OnInputMove(bool side)
    {
        if (side)
        {
            machine.transform.position += new Vector3(1.5f, 0, 0);
        }
        else
        {
            machine.transform.position += new Vector3(-1.5f, 0, 0);
        }
    }

    public TuringMachine BuildMachineFromDescription(TuringMachine tm, string d)
    {
        Alphabet alph = new Alphabet(); ///O ETeimoso

        //Split the string in components
        String[] description = d.Split('#');

        string name = description[0];
        string machineDescription = description[6];

        //Split the Alphabet in symbols
        char[] symbols = description[1].ToCharArray();

        //Split the delta functions in functions from each state
        String[] deltaFunctions = description[5].Split(';');


        int initial = int.Parse(description[3]);
        int final = int.Parse(description[4]);

        State[] states = new State[int.Parse(description[2])];

        for (int i = 0; i < states.Length; i++)
        {
            states[i] = new State();
        }

        foreach (char c in symbols)
        {
            alph.InsertSymbol(c);
        }


        for (int i = 0; i < deltaFunctions.Length; i++)
        {
            if (!deltaFunctions[i].Equals("void"))
            {
                String[] functions = deltaFunctions[i].Split('|');
                List<DeltaFunction> df = new List<DeltaFunction>();
                
                for (int j = 0; j < functions.Length; j++)
                {
                    string f = functions[j];
                    char input = f[0];
                    char output = f[2];
                    char side = f[4];
                    char sState = f[6];
                    
                    int state;
                    
                    state = sState - '0';

                    DeltaFunction DF = new DeltaFunction(input, output, side, state);

                    df.Add(DF);
                    states[i].SetFunctions(df);
                }
            }
        }
        states[initial].DefineIdentity(Constants.INITIAL);
        states[final].DefineIdentity(Constants.FINAL);

        List<State> s = new List<State>();
        for (int i = 0; i < states.Length; i++)
        {
            s.Add(states[i]);
        }

        tm.InstanciateMachine(name, alph, s, machineDescription, cellTapePrefab, redLight, greenLight);

        return tm;
    }


    public void ReceiveTape()
    {
        LoadMachine();
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("cellTape"))
        {
            Destroy(ob);
        }
        try
        {
            Utils.InputToTape(input, cellTapePrefab);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        input.GetComponent<InputField>().text = "";

    }

    public void ReadCellDebug()
    {
        GameObject reader = GameObject.FindGameObjectWithTag("actualCell");
        Debug.Log(reader.GetComponent<TextMesh>());
    }
    
}