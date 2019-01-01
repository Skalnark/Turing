using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
    public List<String> names;
    public string nome;
    public float speed;
    public int machineNumber = 0;

    public Text velocity;

    public GameObject cellTapePrefab;
    public GameObject machine;
    public GameObject greenLight, redLight;

    public TuringMachine tm;
    public List<MachineList> tms = new List<MachineList>();

    public InputField input;

    private InputField inputField;

    private Button processButton;
    private Button startMachineButton;
    
    private TextMesh stateDisplay;

    private Image stepButtonLight;
    private Image startButtonLight;

    private IEnumerator currentStopCoroutine;
    private IEnumerator currentProccessing;
    private IEnumerator initialcorroutine;
    private IEnumerator initialCoroutine;
    
    private bool showStop;
    public GUISkin warning;
    public Dropdown dd;

    void Start()
    {
        tm = GetComponent<TuringMachine>();
        inputField = GameObject.FindGameObjectWithTag("input").GetComponent<InputField>();
        startMachineButton = GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>();
        stepButtonLight = GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Image>();
        startButtonLight = GameObject.FindGameObjectWithTag("StartButtonLight").GetComponent<Image>();
        processButton = GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>();
        stateDisplay = GameObject.FindGameObjectWithTag("stateDisplay").GetComponent<TextMesh>();
        /*
        if(File.Exists(Application.persistentDataPath + "/Machines/machines.txt"))
            LoadAllMachines(File.ReadAllText(Application.persistentDataPath + "/Machines/machines.txt"));
        else
        {
            File.Create(Application.persistentDataPath + "/Machines/machines.txt");
        }*/

        if(!Directory.Exists(Application.persistentDataPath + "\\Machines"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "\\Machines");

            SaveMachine.WriteIt(Application.persistentDataPath + "\\Machines\\machines.txt", Constants.DEFAULTDESCRIPTIONS());
        }

        LoadAllMachines(File.ReadAllText(Application.persistentDataPath + "\\Machines\\machines.txt"));
        LoadOptions();
        LoadMachine();
    }

    public void LoadOptions()
    {
        dd.ClearOptions();
        names.Sort();
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        foreach(string s in names)
        {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = s;
            list.Add(op);
        }

        dd.AddOptions(list);
    }

    public void ProcessState()
    {
        inputField.interactable = false;
        stepButtonLight.color = Color.gray;
        processButton.interactable = false;
        startMachineButton.interactable = false;

        int actualState = tm.InitialStateIndex();
        string input = null;
        try
        {
            input = "" + int.Parse(stateDisplay.text);
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
        FindMachine(dd.captionText.text);
        Utils.WriteOnDisplay("stateDisplay", tm.InitialStateIndex() + "");
    }

    public void StartMachine()
    {
        dd.interactable = false;

        processButton.interactable = false;
        stepButtonLight.color = Color.gray;
        startMachineButton.interactable = false;
        
        startButtonLight.color = Color.blue;

        ArrayList initial = new ArrayList();

        initial.Add(true);
        initial.Add(tm.InitialStateIndex());

        initialCoroutine = StartProcessing(initial);

        StartCoroutine(initialCoroutine);

    }

    public IEnumerator StartProcessing(ArrayList result)
    {
        
        yield return new WaitForSeconds(speed/2);
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
            
            startButtonLight.color = Color.red;

            StartCoroutine(currentStopCoroutine);
        }

        yield return new WaitForSeconds(speed / 2);
    }

    public void LoadAllMachines(string description)
    {
        description = RemoveString(description, Environment.NewLine);
        description = RemoveString(description, "{");
        description = RemoveString(description, "\t");

        List<String> refine = new List<string>();
        
        foreach(String m in description.Split('}'))
        {
            if (m.Length > 1) refine.Add(m);
        }

        String[] allMachines = refine.ToArray();

        foreach (String m in allMachines)
        {
            String[] desc = m.Split('#');

            string machineName = desc[0];
            Alphabet alph = new Alphabet();
            string mDesc = desc[4];

            foreach (char c in desc[1]) {
                alph.InsertSymbol(c);
            }

            int nStates = int.Parse(desc[2]);
            List<State> states = new List<State>();

            desc[4] = ReplaceString(desc[4], "def", ":");

            String[] rawDFunctions = desc[4].Split(':');
            
            for(int i = 0; i < rawDFunctions.Length; i++)
            {
                rawDFunctions[i] = RemoveString(rawDFunctions[i], " ");
            }

            foreach(string dFuncString in rawDFunctions) {
                List<DeltaFunction> functions = new List<DeltaFunction>();

                if (dFuncString.Length > 2)
                {
                    if (dFuncString.Contains("void"))
                    {
                        states.Add(new State());
                    }
                    else
                    {
                        string[] atomicFuncString = dFuncString.Split(';');
                        
                        foreach (string function in atomicFuncString)
                        {
                            if (!function.Contains("void") && function.Length > 3)
                            {
                                string[] aChar = function.Split(',');
                                List<char> fchar = new List<char>() ;

                                foreach (string s in aChar)
                                {
                                    if(s.Length == 1)
                                    fchar.Add(char.Parse(s));
                                }
                                
                                DeltaFunction df = new DeltaFunction(fchar[0], fchar[1], fchar[2], int.Parse(fchar[3] + ""));

                                functions.Add(df);
                            }
                        }
                        states.Add(new State(functions));
                    }
                }
            }

            if (desc[2].Contains(","))
            {
                string[] finalStates = desc[2].Split(',');

                foreach (string s in finalStates)
                {
                    states[int.Parse(s)].DefineIdentity(Constants.FINAL);
                }
            }
            else
            {
                states[int.Parse(desc[2])].DefineIdentity(Constants.FINAL);
            }

            states[int.Parse(desc[1])].DefineInitial();

            names.Add(machineName);
            
            tms.Add(new MachineList(alph, states, machineName, mDesc));
            
        }
    }


    public void ReceiveTape()
    {
        LoadMachine();

            Utils.InputToTape(input, cellTapePrefab);
        
        input.GetComponent<InputField>().text = "";

    }

    public IEnumerator WaitMachine(float time)
    {
        processButton.interactable = true;
        stepButtonLight.color = Color.cyan;

        yield return new WaitForSeconds(time);
    }

    public void Stop()
    {
        try
        {
            StopCoroutine(currentProccessing);
            FindMachine(dd.captionText.text);
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
        dd.interactable = true;
        startMachineButton.interactable = true;
        processButton.interactable = true;

        stepButtonLight.color = Color.cyan;

        startButtonLight.color = Color.red;

        LoadMachine();
    }

    public void FindMachine(string seek)
    {
        foreach(MachineList t in tms)
        {
            if (t.mName.Equals(seek))
            {
                tm.InstantiateMachine(t);
                break;
            }
        }
    }

    public static string RemoveString(string description, string content)
    {
        Regex rgx = new Regex(content);

        char[] arraychar = rgx.Replace(description, " ").ToCharArray();
        description = "";

        foreach (char c in arraychar)
        {
            if (c != ' ' && c != '{')
            {
                description += c;
            }
        }

        return description;
    }


    public static string ReplaceString(string description, string content, string replacement)
    {
        Regex rgx = new Regex(content);

        char[] arraychar = rgx.Replace(description, replacement).ToCharArray();
        description = "";

        foreach (char c in arraychar)
        {
            if (c != ' ' && c != '{')
            {
                description += c;
            }
        }

        return description;
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
