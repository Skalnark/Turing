using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InsertDescription : MonoBehaviour
{

    public InputField textField;
    public TextMesh displayer;
    public TextMesh location;
    IEnumerator error;

    private void Start()
    {
        error = Error();

        if (!Directory.Exists(Application.persistentDataPath + "/Machines"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Machines");

            if (!File.Exists(Application.persistentDataPath + "/Machines/machines.txt"))
            {
                File.Create(Application.persistentDataPath + "/Machines/machines.txt");
            }
            
        }

        textField.text = File.ReadAllText(Application.persistentDataPath + "/Machines/machines.txt");
    }

    public void Update()
    {
        location.text = "location: " + Application.persistentDataPath;
    }

    public void Clear()
    {
        textField.text = "";
    }

    public void TryInsert()
    {
        
        string desc = File.ReadAllText(Application.persistentDataPath + "/Machines/machines.txt") + textField.text;
        
        try
        {
            try
            {
                LoadMachine(desc);
                
                textField.text = "";
                
                SaveMachine.WriteIt(Application.persistentDataPath + "/Machines/machines.txt", desc);
            }
            catch(Exception e)
            {
                Debug.Log(e);
                displayer.text = "ERROR";
                if (error != null) StopCoroutine(error);
                StartCoroutine(error);
            }
        }
        catch { }
    }

    IEnumerator Error()
    {
        yield return new WaitForSeconds(3);
        displayer.text = "Go to https://github.com/Skalnark/Turing for more informations";
    }

    public void LoadMachine(string description)
    {
        description = MachineGear.RemoveString(description, Environment.NewLine);
        description = MachineGear.RemoveString(description, "{");
        description = MachineGear.RemoveString(description, "\t");

        List<String> refine = new List<string>();

        foreach (String m in description.Split('}'))
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

            foreach (char c in desc[1])
            {
                alph.InsertSymbol(c);
            }

            int nStates = int.Parse(desc[2]);
            List<State> states = new List<State>();

            desc[4] = MachineGear.ReplaceString(desc[4], "def", ":");

            String[] rawDFunctions = desc[4].Split(':');

            for (int i = 0; i < rawDFunctions.Length; i++)
            {
                rawDFunctions[i] = MachineGear.RemoveString(rawDFunctions[i], " ");
            }

            foreach (string dFuncString in rawDFunctions)
            {
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
                                List<char> fchar = new List<char>();

                                foreach (string s in aChar)
                                {
                                    if (s.Length == 1)
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
            states[int.Parse(desc[2])].DefineIdentity(Constants.FINAL);
            states[int.Parse(desc[1])].DefineInitial();
        }
    }
    
}
