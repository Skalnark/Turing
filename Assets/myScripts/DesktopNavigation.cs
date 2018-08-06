using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DesktopNavigation : MonoBehaviour {
    
    public void GoToMachine()
    {
        SceneManager.LoadScene("mainScene");
    }
    public void GoToDescription()
    {
        SceneManager.LoadScene("InsertDescriptionScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("homeScene");
    }
}
