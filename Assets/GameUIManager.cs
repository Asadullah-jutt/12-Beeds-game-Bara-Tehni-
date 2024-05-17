using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    // Start is called before the first frame update
public GameObject gameObject;
public GameObject gameObject2;

public TMP_Text tMP_Text;

    void Start()
    {

        if (PlayerPrefs.HasKey("GameState"))
        {
            string storedString = PlayerPrefs.GetString("GameState");
            PlayerPrefs.DeleteKey("GameState");
            if(storedString != "ingame")
            {
                Debug.Log("" + storedString);
                tMP_Text.text = storedString;
                gameObject2.SetActive(true);
                return ;
            }
               
        }
        gameObject.SetActive(true);


    }
}
