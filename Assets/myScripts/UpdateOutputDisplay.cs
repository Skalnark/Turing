using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOutputDisplay : MonoBehaviour {

    int index;
    string input;
    TuringMachine tm;
    DeltaFunction df;
    void Update()
    {

        OutputUpdate();
    }
    public  void OutputUpdate()
    {
        try
        {
            tm = GameObject.FindGameObjectWithTag("GameController").GetComponent<TuringMachine>();

            index = int.Parse(GameObject.FindGameObjectWithTag("stateDisplay").GetComponent<TextMesh>().text);

            input = GameObject.FindGameObjectWithTag("readDisplay").GetComponent<TextMesh>().text;

            if (input.Equals("")) df = tm.StateByIndex(index).LookForFunction('Ø');
            else df = tm.StateByIndex(index).LookForFunction(char.Parse(input));

            if (df.getOutput().Equals('Ø')) GetComponent<TextMesh>().text = "";
            else GetComponent<TextMesh>().text = df.getOutput() + "";
            
        }
        catch { }
    }
}
