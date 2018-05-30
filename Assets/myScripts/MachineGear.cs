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
    public string nome;
    public float speed;
    public int machineNumber = 0;

    public Text velocity;

    public GameObject cellTapePrefab;
    public GameObject machine;
    public GameObject greenLight, redLight;
    public GameObject startButtonLight;

    public TuringMachine tm;

    public InputField input;
    public InputField input2;

    private IEnumerator currentStopCoroutine;
    private IEnumerator currentProccessing;
    private IEnumerator initialcorroutine;
    private IEnumerator initialCoroutine;

    public List<string> machineDescription;
    private bool showStop;
    public GUISkin warning;

    void Start()
    {
        tm = GetComponent<TuringMachine>();
    }

    public void SearchDescription()
    {
        string path = null;

        try
        {
            path = Application.dataPath + "/Machines/" + nome + ".txt";
            Debug.Log(path);
            description = File.ReadAllText(path);
        }
        catch { }
        tm = null;
        tm = GetComponent<TuringMachine>();

        LoadMachine();
    }

    public void UpdateName()
    {
        nome = input2.text;
    }

    public void ProcessState()
    {
        GameObject.FindGameObjectWithTag("input").GetComponent<InputField>().interactable = false;
        GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Light>().intensity = 0;
        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = false;
        int actualState = tm.InitialStateIndex();
        string input = null;
        try
        {
            input = "" + int.Parse(GameObject.FindGameObjectWithTag("stateDisplay").GetComponent<TextMesh>().text);
        }
        catch {}

        if (input != null) actualState = int.Parse(input);

        ArrayList result = tm.ProcessCell(tm.StateByIndex(actualState), actualState);

        if ((bool)result[0])
        {
            StartCoroutine(WaitMachine(speed/2));
            return;
        }
        else
        {
            if (currentStopCoroutine != null) StopCoroutine(currentStopCoroutine);

            currentStopCoroutine = tm.StopMachine(tm.StateByIndex((int)result[1]));

            StartCoroutine(currentStopCoroutine);
        }

    }

    public void UpdateSpeed(float nowSpeed)
    {
        speed = nowSpeed;
    }

    public TuringMachine SimulatedMachine()
    {
        return tm;
    }

    public void LoadMachine()
    {
        Utils.ClearTape();
        tm = BuildMachineFromDescription(tm, description);
        Debug.Log("machine loaded");

        Utils.WriteOnDisplay("stateDisplay", tm.InitialStateIndex() + "");
    }

    public void StartMachine()
    {
        GameObject.FindGameObjectWithTag("input").GetComponent<InputField>().interactable = false;

        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Light>().intensity = 0;
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = false;

        GameObject.FindGameObjectWithTag("StartButtonLight").GetComponent<Light>().color = Color.blue;

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
            
            GameObject.FindGameObjectWithTag("StartButtonLight").GetComponent<Light>().color = Color.red;
            GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Light>().intensity = 2.5f;

            StartCoroutine(currentStopCoroutine);
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

            Utils.InputToTape(input, cellTapePrefab);
        
        input.GetComponent<InputField>().text = "";

    }

    public void ReadCellDebug()
    {
        GameObject reader = GameObject.FindGameObjectWithTag("actualCell");
        Debug.Log(reader.GetComponent<TextMesh>());
    }

    public void LoadLib()
    {
        try
        {
            LoadDescriptions();
            //description = System.IO.File.ReadAllText("Assets/Machines/infinite.txt");
            LoadMachine(machineNumber);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void LoadMachine(int i)
    {
        //description = System.IO.File.ReadAllText(machineDescription[i]);
        Debug.Log(Application.dataPath + " datapath");
        Debug.Log(Application.streamingAssetsPath + " sdatapath");
        if(Directory.Exists(Application.streamingAssetsPath))
            description = File.ReadAllText(Application.streamingAssetsPath + "/Machines/Amt.txt");
        else
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "Machines");
        }

        tm = BuildMachineFromDescription(tm, description);
        Debug.Log("Machine Loaded");

        Utils.WriteOnDisplay("stateDisplay", tm.InitialStateIndex() + "");
    }

    public void LoadDescriptions()
    {
        string path = Application.dataPath + "/Machines";

        String[] fileEntries = Directory.GetFiles(path);

        for (int i = 0; i < fileEntries.Length; i++)
        {
            if (fileEntries[i].EndsWith(".txt"))
                machineDescription.Add(fileEntries[i]);
        }
    }

    public IEnumerator WaitMachine(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Light>().intensity = 2.5f;
    }

    public void Stop()
    {
        try
        {
            StopCoroutine(currentProccessing);
            tm = BuildMachineFromDescription(tm, "");
        }
        catch { }

        tm.StopMachine(new State());

        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("cellTape"))
        {
            Destroy(ob);
        }
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("actualCell"))
        {
            Destroy(ob);
        }

        StartCoroutine(DestroyWithDelay());

        input.interactable = true;

        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = true;

        GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Light>().intensity = 2.5f;

        GameObject.FindGameObjectWithTag("StartButtonLight").GetComponent<Light>().color = Color.green;

        LoadMachine();
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("cellTape"))
        {
            Destroy(ob);
        }
    }

}