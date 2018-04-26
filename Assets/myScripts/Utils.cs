using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoBehaviour {

    private Alphabet alph;
	private TuringMachine tm;
	public int tapecellnumb;
	public InputField valueInput;


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

}