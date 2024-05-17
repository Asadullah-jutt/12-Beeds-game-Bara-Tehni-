using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void OnMouseEnter()
    {
        Block blockScript = gameObject.GetComponent<Block>();

        if (blockScript != null)
        {
            beed beedScript = GetComponentInChildren<beed>();

            if (beedScript != null)
            {
                if(beedScript.GetTeamNum() ==GameManager.turn.GetTurn())
                {
                    string IJ = gameObject.name;
                    int i = IJ[0] - 48 ;
                    int j = IJ[1] - 48 ;

                // Log or use the parent name as needed
                // Debug.Log("Parent name of beed1: " + i + " , " + j );
                    GameManager.GetPossibleIndexMoves(i,j);
                }
            }
            else
            {
                // Debug.Log("No Beed here.");
            }

            
        }
       
    }
    void OnMouseExit()
    {
        Block blockScript = gameObject.GetComponent<Block>();

        if (blockScript != null)
        {
            beed beedScript = GetComponentInChildren<beed>();

            if (beedScript != null)
            {
                if(beedScript.GetTeamNum() ==GameManager.turn.GetTurn())
                {
                    string IJ = gameObject.name;
                    int i = IJ[0] - 48 ;
                    int j = IJ[1] - 48 ;

                    // Debug.Log("Parent name of beed1: " + i + " , " + j );
                    GameManager.GetPossibleIndexMovesOFF(i,j);
                }
            }
            else
            {
                // Debug.Log("No Beed here.");
            }

            
        }
    }
    void OnMouseDown()
    {
        Block blockScript = gameObject.GetComponent<Block>();

        if (blockScript != null)
        {
            beed beedScript = GetComponentInChildren<beed>();

            if (beedScript != null)
            {
                if(beedScript.GetTeamNum() ==GameManager.turn.GetTurn())
                {
                    string IJ = gameObject.name;
                    int i = IJ[0] - 48 ;
                    int j = IJ[1] - 48 ;

                    // Log or use the parent name as needed
                    // Debug.Log("Parent name of beed1: " + i + " , " + j );
                    GameManager.Selectedbeed(i,j);
                }
            }
            else if(beedScript == null)
            {
                // Debug.Log("No Beed here.");
                string IJ = gameObject.name;
                int i = IJ[0] - 48 ;
                int j = IJ[1] - 48 ;

                // Log or use the parent name as needed
                // Debug.Log("No Beed here: " + i + " , " + j );
                GameManager.Selectedblock(i,j);
            }
            

            
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
