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
}