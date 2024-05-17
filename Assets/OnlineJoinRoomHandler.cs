using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class OnlineJoinRoomHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField tMP_InputField ;

    void OnMouseDown()
    {
        string s ;
        s = tMP_InputField.text;
        Debug.Log("Text : "+s);
        bool keycreated =  UIMongodb.Instance.Check1PlayerStatus(s);

        if(keycreated== true)
        {
            tMP_InputField.interactable = false;
        }
        else
        { 
            tMP_InputField.text = "Key Does not Exist , Check the Key Again";
            tMP_InputField.interactable = false;
            Invoke("makeactive",4f);
        }
           
    }

    void makeactive()
    {
        tMP_InputField.interactable = true;
        tMP_InputField.text = "";
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
