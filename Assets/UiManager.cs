using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    
    public int vstype = 0 ;
    private void OnMouseDown()
    {
        if(vstype == 1)
        {
            PlayerPrefs.SetInt("VS", 1);  // vs AI
        }
        else if(vstype == 2)
        {
            PlayerPrefs.SetInt("VS", 2);
        }
        else if(vstype == 3)
        {
            PlayerPrefs.SetInt("VS", 3);
        }
        else if(vstype == 4)
        {
            Application.Quit();
            return;
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene(1);

    }
}
