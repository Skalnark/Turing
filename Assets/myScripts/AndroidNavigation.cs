using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AndroidNavigation : MonoBehaviour {
    
    public void GoToMachine()
    {
        SceneManager.LoadScene("mainSceneAndroid");
    }
    public void GoToDescription()
    {
        SceneManager.LoadScene("InsertDescriptionSceneAndroid");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("homeSceneAndroid");
    }
}
