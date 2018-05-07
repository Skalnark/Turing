using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCellContent : MonoBehaviour {

    TextMesh text;

    private void Start()
    {
        text = GetComponent<TextMesh>();
    }

    void Update () {
        try
        {
            if (GameObject.FindGameObjectWithTag("actualCell") != null)
                text.text = GameObject.FindGameObjectWithTag("actualCell").GetComponent<TextMesh>().text;
            else
                text.text = "";
        }
        catch
        {

        }
	}
}
