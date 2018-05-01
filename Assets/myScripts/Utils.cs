using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    public Alphabet alph;
    public TuringMachine tm;
    public int tapecellnumb;

    public static IEnumerator Wait(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }

    public static IEnumerator Blink(GameObject light, int time)
    {
        light.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        light.SetActive(false);
    }

    public static void WriteOnDisplay(TextMesh display, TextMesh source)
    {
        display.text = source.text;
    }

    //fazer uma função que receberá uma string como input, através do inputField
    //processar a string, checando se o alfabeto existe
    //enquanto isso colocar as celulas no tape
     public static void InputToTape(string input, GameObject cellTapePrefab)
    {
        char[] cellsInChar = input.ToCharArray();
        //checa se cada celular existe no alfabeto
        
        //cria a primeira celula vazia
        GameObject beginingCell = Instantiate(cellTapePrefab);
        beginingCell.GetComponentInChildren<TextMesh>().fontSize = 20;
        beginingCell.GetComponentInChildren<TextMesh>().text = "Empty";
        
        beginingCell.transform.position = new Vector3(-1.5f, 0, 0);

        //cria as celulas da fita
        for (int i = 0; i < cellsInChar.Length; i++)
        {
            GameObject cell = Instantiate(cellTapePrefab);
            cell.GetComponentInChildren<TextMesh>().text = "" + cellsInChar[i];
            cell.transform.position = new Vector3((1.5f * i), 0, 0);
        }

        //cria as ultima celula vazia
        GameObject endingCell = Instantiate(cellTapePrefab);
        endingCell.GetComponentInChildren<TextMesh>().fontSize = 20;
        endingCell.GetComponentInChildren<TextMesh>().text = "Empty";
        endingCell.transform.position = new Vector3(1.5f + ((cellsInChar.Length-1) * 1.5f), 0, 0);
    }
}