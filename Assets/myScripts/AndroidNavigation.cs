using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidNavigation : MonoBehaviour
{

    public void GoToMachine()
    {
        SceneManager.LoadScene("mainAndroidScene");
    }
    public void GoToDescription()
    {
        SceneManager.LoadScene("InsertDescriptionSceneAndroid");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("homeAndroidScene");
    }
}
