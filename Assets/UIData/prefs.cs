using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class prefs : MonoBehaviour
{
    // Start is called before the first frame update
    public void Action(int a)
    {
        if(a == 1)
        {
            PlayerPrefs.SetInt("VS", 1); // Store the high score
             // Save PlayerPrefs to disk
        }
        else
            PlayerPrefs.SetInt("VS", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);

    }
}
