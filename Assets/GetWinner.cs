using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetWinner : MonoBehaviour
{
    // Start is called before the first frame update


    public static int checkwinner(int[][] board )
    {
        int count1 = 0 ;
        int count2 = 0 ;

        for(int i = 0 ; i < 5 ; i++)
        {
            for(int j=0 ; j < 5 ; j++)
            {
                if(board[i][j] == 1)
                     count1++;
                else if(board[i][j]== 2)
                    count2++;
            }
        }

        if(count1==0)
            return 1 ;
        if(count2 == 0)
            return 2 ;
        return 0 ;

    }
    
}
