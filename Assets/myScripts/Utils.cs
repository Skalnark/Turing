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

    private IEnumerator currentGuiCoroutine;


    private bool showWait = false;
    private bool showInfo = false;
    public GUISkin warning;

    void Start()
    {
        tm = gameSystem.GetComponent<MachineGear>().SimulatedMachine();
        reader = GameObject.FindGameObjectWithTag("reader");
    }

    void Update()
    {
        OnGUI();
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

    public static void ClearTape()
    {
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("cellTape"))
        {
            Destroy(ob);
        }
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("actualCell"))
        {
            Destroy(ob);
        }
    }

    public static void InputToTape(InputField input, GameObject cellTapePrefab)
    {
        ClearTape();

        TuringMachine tm = GameObject.FindGameObjectWithTag("GameController").GetComponent<TuringMachine>();

        GameObject.FindGameObjectWithTag("processButton").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("startMachineButton").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("tapeInput").GetComponent<Dropdown>().interactable = true;
        GameObject.FindGameObjectWithTag("StepButtonLight").GetComponent<Light>().intensity = 2.5f;

        GameObject.FindGameObjectWithTag("StartButtonLight").GetComponent<Light>().color = Color.green;

        char[] cellsInChar = input.GetComponent<InputField>().text.ToCharArray();


        foreach (char c in cellsInChar)
        {
            if (!tm.GetAlphabet().LookForSymbol(c))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<Utils>().showWait = true;
                return;
            }
        }

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

    public void Info()
    {
        showInfo = true;
        StartCoroutine(InfoPopUp());
    }

    void OnGUI()
    {

        TuringMachine tm = GameObject.FindGameObjectWithTag("GameController").GetComponent<MachineGear>().SimulatedMachine();
        if (showWait)
        {
            try
            {
                GUI.Box(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 30, 200, 60), "Input rejected", warning.box);
            }
            catch { }

            StartCoroutine(InputErrorPopUp());
        }
        if (showInfo)
        {
            try
            {
                GUI.Box(new Rect(130, 150, Screen.width / 3, Screen.height / 3), tm.GetDescription(), warning.box);
            }
            catch { }

            StartCoroutine(InfoPopUp());
        }
    }

    IEnumerator InputErrorPopUp()
    {
        yield return new WaitForSeconds(1.5f);
        showWait = false;
    }
    IEnumerator InfoPopUp()
    {
        yield return new WaitForSeconds(4f);
        showInfo = false;
    }
}