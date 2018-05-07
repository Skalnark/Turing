using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOutputDisplay : MonoBehaviour {

    void Update()
    {

        OutputUpdate();
    }
    public  void OutputUpdate()
    {
        try
        {
            if (GameObject.FindGameObjectWithTag("stateDisplay").GetComponent<TextMesh>().text != "")
            {
                TuringMachine tm = GameObject.FindGameObjectWithTag("GameController").GetComponent<TuringMachine>();
                DeltaFunction df = tm.StateByIndex(
                    int.Parse(GameObject.FindGameObjectWithTag("stateDisplay").GetComponent<TextMesh>().text)).LookForFunction(
                        char.Parse(GameObject.FindGameObjectWithTag("inputDisplay").GetComponent<TextMesh>().text));

                GetComponent<TextMesh>().text = df.getOutput() + "";
            }
        }
        catch { }
    }
}
