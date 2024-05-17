using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineCreateRoomHanlder : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField tMP_InputField ;
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        string s ;
        s = tMP_InputField.text;
        Debug.Log("Text : "+s);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogError("Error: No internet connection.");
            // Handle the error (e.g., display an error message to the user)
            return;
        }
        bool keycreated =  UIMongodb.Instance.init(s);

        if(keycreated== true)
        {
            Debug.Log("Waiting for the other Player to join");
             tMP_InputField.interactable = false;

        // Set text
            tMP_InputField.text = "Waiting for the other player to join";

            Invoke("check2Player",5f);

        }
        else
            tMP_InputField.text = "Change the Key";   

    }

    void check2Player()
    {
        
        UIMongodb.Instance.Check2PlayerStatus();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
