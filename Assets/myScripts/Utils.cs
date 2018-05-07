using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoBehaviour {
    
    public static TuringMachine tm;
    public int tapecellnumb;
    public static GameObject machine;
    public static GameObject reader;
    public GameObject gameSystem;
    public static float speed = 20;
    void Start()
    {
        tm = gameSystem.GetComponent<MachineGear>().SimulatedMachine();
        reader = GameObject.FindGameObjectWithTag("reader");
    }

    public static IEnumerator MoveMachine(bool side)
    {
        machine = GameObject.FindGameObjectWithTag("TuringMachine");
        Vector3 target = machine.transform.position;
        if (side)
        {
            while (machine.transform.position.x < target.x + 1.5f)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                machine.transform.position += new Vector3(Time.deltaTime / speed, 0, 0);
            }
        }
        else
        {
            while (machine.transform.position.x > target.x - 1.5f)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                machine.transform.position += new Vector3(Time.deltaTime / speed, 0, 0);
            }
        }
    }

    public static IEnumerator Wait(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }

    public static IEnumerator Blink(GameObject light, float time)
    {
        light.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        light.SetActive(false);
    }

    //fazer uma função que receberá uma string como input, através do inputField
    //processar a string, checando se o alfabeto existe
    //enquanto isso colocar as celulas no tape
     public static void InputToTape(InputField input, GameObject cellTapePrefab)
    {

        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = true;
        char[] cellsInChar = input.GetComponent<InputField>().text.ToCharArray();

        //cria as celulas da fita
        for (int i = 0; i < cellsInChar.Length; i++)
        {
            GameObject cell = Instantiate(cellTapePrefab);
            cell.GetComponentInChildren<TextMesh>().text = "" + cellsInChar[i];
            cell.transform.position = new Vector3((1.5f * i), 0, 0);
        }

        GameObject.FindGameObjectWithTag("TuringMachine").transform.position = new Vector3(0, 0, 0);
    }

    public static void InstantiateCell(char input, GameObject cellTapePrefab)
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("TuringMachine").transform.position;
        GameObject cell = Instantiate(cellTapePrefab);
        cell.GetComponentInChildren<TextMesh>().text = "" + input;
        cell.transform.position = pos;
    }

    public static void WriteOnDisplay(string tag, string content)
    {
        if (content == null)
            GameObject.FindGameObjectWithTag(tag).GetComponent<TextMesh>().text = "";
        else
        GameObject.FindGameObjectWithTag(tag).GetComponent<TextMesh>().text = content;
    }
    
    public static char ReadCellText()
    {
        GameObject reader = GameObject.FindGameObjectWithTag("actualCell");
        return char.Parse(reader.GetComponent<TextMesh>().text);
    }

    public static void WriteCellText(char t)
    {
        GameObject reader = GameObject.FindGameObjectWithTag("actualCell");
        reader.GetComponent<TextMesh>().text = t + "";
    }

    public void DebugCellText()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("actualCell").GetComponent<TextMesh>().text);
    }
}