using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnChanger : MonoBehaviour
{
    // Start is called before the first frame update
    private int turn = 1 ;

    private int count = 0 ;

    public void SetTurn(int turn_)
    {
        if(turn_ == 1 || turn_ == 2)
        {
            turn = turn_ ;
        }
        count = 1 ;
    }
    public void SetCount(int turn_)
    {
      count = turn_ ;
    }

    public void ToggleTurn()
    {
        if(turn == 1)
            turn++ ;
        else
            turn-- ;
        count++;
    }

    public int GetTurn()
    {
        return turn ; 
    }
    public int GetCount()
    {
        return count ;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
