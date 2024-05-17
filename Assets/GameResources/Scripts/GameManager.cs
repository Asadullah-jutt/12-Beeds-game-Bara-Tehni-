using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MongoDB.Bson;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] tempplaces;
    public static GameObject[][] places = new GameObject[5][];
    public static int[][] boardvalues = new int[5][];
    private static bool IsAnySelected = false ;
    public GameObject beed1;
    public GameObject beed2;
    public static TurnChanger turn = new TurnChanger();
    // private static GetExpectedMoves GetMoves = new GetExpectedMoves();
    public  GameObject tempremove ;
    public static GameObject remove ;
    static List<List<(int, int)>>  allPathss ; 
    static AIPlayer aIPlayer = new AIPlayer();
    static int playerid = 0 ;
    static bool VS_PC = false;
    static GetExpectedMoves getExpectedMoves = new GetExpectedMoves();
    static bool isOnline = false ;

    private static GameManager instance;

    int completed = 0 ;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance=new GameManager();
            }
            return instance;
        }
    }


    void Start()
    {
        turn.SetTurn(1);
        PlayerPrefs.SetString("GameState", "ingame");

        if(PlayerPrefs.GetInt("VS", 1)==5) // Online Host
        {
            playerid = 1 ;
            isOnline = true ;
        }
        else if(PlayerPrefs.GetInt("VS", 1)==4)  // Online Client
        {
            // turn.SetCount(2);
            playerid = 2 ;
            isOnline = true ;
        }
        else if(PlayerPrefs.GetInt("VS", 1)==1) // VS AI
        {
            playerid = 2 ;
            VS_PC = true;
            turn.SetTurn(2);
        }
        else if(PlayerPrefs.GetInt("VS", 1)==3) // VS Offlineplayer
        {
            playerid = 0 ;
            VS_PC = false;
            turn.SetTurn(1);
        }
        
        for (int i = 0; i < 5; i++)
            boardvalues[i] = new int[5];
        for (int i = 0; i < 5; i++)
            places[i] = new GameObject[5];

        remove = tempremove ;

        for(int i = 0 ; i < 5 ; i++)
        {
            for(int j = 0 ; j < 5 ; j++)
            {
                 places[i][j] = tempplaces[(i*5)+j]  ; 
            }
        }
        for(int i = 0 ; i < 2 ; i++)
        {
            for(int j = 0 ; j < 5 ; j++)
            {
                Transform placeTransform = places[i][j].transform;

            // Instantiate beed1Prefab as a new game object
                GameObject instantiatedBeed1 = Instantiate(beed1, placeTransform);
                boardvalues[i][j] = 1 ;

                // Optionally reset the local position and rotation of the instantiatedBeed1 (if needed)
                instantiatedBeed1.transform.localPosition = Vector3.zero;
                instantiatedBeed1.transform.localRotation = Quaternion.identity;
                instantiatedBeed1.transform.localScale = new Vector3(0.3f, 3f, 0.3f);
            }
        }
            for(int j = 0 ; j < 2 ; j++)
            {
                Transform placeTransform = places[2][j].transform;

            // Instantiate beed1Prefab as a new game object
                GameObject instantiatedBeed1 = Instantiate(beed1, placeTransform);
                boardvalues[2][j] = 1 ;

                // Optionally reset the local position and rotation of the instantiatedBeed1 (if needed)
                instantiatedBeed1.transform.localPosition = Vector3.zero;
                instantiatedBeed1.transform.localRotation = Quaternion.identity;
                instantiatedBeed1.transform.localScale = new Vector3(0.3f, 3f, 0.3f);
            }
        boardvalues[2][2] = 0 ;

        for(int j = 3 ; j < 5 ; j++)
        {
            Transform placeTransform = places[2][j].transform;

        // Instantiate beed1Prefab as a new game object
            GameObject instantiatedBeed1 = Instantiate(beed2, placeTransform);
            boardvalues[2][j] = 2 ;

            // Optionally reset the local position and rotation of the instantiatedBeed1 (if needed)
            instantiatedBeed1.transform.localPosition = Vector3.zero;
            instantiatedBeed1.transform.localRotation = Quaternion.identity;
            instantiatedBeed1.transform.localScale = new Vector3(0.3f, 3f, 0.3f);
        }
        for(int i = 3 ; i < 5 ; i++)
        {
            for(int j = 0 ; j < 5 ; j++)
            {
                Transform placeTransform = places[i][j].transform;

            // Instantiate beed1Prefab as a new game object
                GameObject instantiatedBeed1 = Instantiate(beed2, placeTransform);
                boardvalues[i][j] = 2 ;

                // Optionally reset the local position and rotation of the instantiatedBeed1 (if needed)
                instantiatedBeed1.transform.localPosition = Vector3.zero;
                instantiatedBeed1.transform.localRotation = Quaternion.identity;
                instantiatedBeed1.transform.localScale = new Vector3(0.3f, 3f, 0.3f);
            }
        }
        if(PlayerPrefs.GetInt("VS", 1)==4)
        {
            MongoDBManager.Instance.checknextturn();
            Debug.Log("checking");
            // playerid = 2 ;
        }

        
    }

    // public static int indexer = 0 ;
    // public static List<(int,int)> movee ;

    public static void SetBoardValues(BsonArray array)
    {

        List<(int,int)> dbpath = new List<(int,int)> ();

        for(int i = 0; i < array.Count; i = i+2)
        {
            dbpath.Add ((array[i].AsInt32,array[i+1].AsInt32));
        }
        // indexer = 0 ;
        // movee = dbpath ;
        // movebeed();
             
        for(int l = 0 ; l < dbpath.Count - 1  ; l++)
        {
            Debug.Log(dbpath[l].Item1+" , "+dbpath[l].Item2+" ==> "+dbpath[l+1].Item1+" , "+dbpath[l+1].Item2);
            MoveBeadFromTo(dbpath[l].Item1,dbpath[l].Item2,dbpath[l+1].Item1,dbpath[l+1].Item2);
        }
        Debug.Log("Changedddd");
        turn.ToggleTurn(); 

    }

    

    // private static void movebeed()
    // {
    //     MoveBeadFromTo(movee[indexer].Item1,movee[indexer].Item2,movee[indexer+1].Item1,movee[indexer+1].Item2);
    //     indexer++;
                
    //     if(indexer < movee.Count - 1)
    //     {
    //         Debug.Log("sfdsf");
    //         Instance.Invoke("movebeed",4.5f);
    //     }
    //     else
    //     {
    //         Debug.Log("Changedddd");
    //         indexer = 0 ;
    //         movee = null;
    //     }

    // }
    public static void PrintBoard(int[][] board)
    {
        for (int i = 0; i < board.Length; i++) // Loop through each row
        {
            string rowString = string.Empty; // Create a string to represent the row

            for (int j = 0; j < board[i].Length; j++) // Loop through each column
            {
                rowString += board[i][j] + " "; // Append the element and a space
            }

            Debug.Log(rowString); // Print the row to the Unity console
        }
    }


    public static int[][] GetBoardValues()
    {
        return boardvalues ;
    }

    private static bool IsValidIndex(int i, int j)
    {
        return i >= 0 && i < 5 && j >= 0 && j < 5;
    } 

    public static void GetPossibleIndexMoves(int i , int j)
    {
        if(IsAnySelected == false && (turn.GetTurn() == playerid || playerid == 0))
        {

           allPathss = getExpectedMoves.GetExpectedMovess(boardvalues,i,j) ;
           for (int k = 0; k < allPathss.Count; ++k)
            {
                // Debug.Log("Path " + (k + 1) + ": " + allPathss[k].Count);
                for (int m = 1; m < allPathss[k].Count; ++m)
                {
                    // Debug.Log("(" + allPathss[k][m].Item1 + ", " + allPathss[k][m].Item2 + ") ");
                    ActivateHighlighters(allPathss[k][m].Item1,allPathss[k][m].Item2);
                }
                // Console.WriteLine();
            }
        }
    }
    public static void GetPossibleIndexMovesOFF(int i , int j)
    {
        if(IsAnySelected == false && (turn.GetTurn() == playerid || playerid == 0)) 
        {
         allPathss = getExpectedMoves.GetExpectedMovess(boardvalues,i,j) ;
           for (int k = 0; k < allPathss.Count; ++k)
            {
                // Console.Write("Path " + (k + 1) + ": ");
                for (int m = 1; m < allPathss[k].Count; ++m)
                {
                    // Console.Write("(" + allPaths[k][m].Item1 + ", " + allPaths[k][m].Item2 + ") ");
                    DisableHighlighters(allPathss[k][m].Item1,allPathss[k][m].Item2);
                }
                // Console.WriteLine();
            }
        }
    }
    // Start is called before the first frame update

   
    public static void  ActivateHighlighters(int i , int j)
    {
        
        //! Find the "Highlighter" child within each place
        Transform highlighterTransform = places[i][j].transform.Find("Highlighter");

        if (highlighterTransform != null)
        {
            // Activate the "Highlighter" child
            highlighterTransform.gameObject.SetActive(true);
        }
                    
    }

    public static void DisableAllHighlighters()
    {
        for (int i = 0; i < 5 ; i++)
        {
            for(int j = 0;j<5 ; j++)
            {
                 DisableHighlighters(i,j);
            }
        }

    }
    public static void DisableHighlighters(int i , int j)
    {
        
        Transform highlighterTransform = places[i][j].transform.Find("Highlighter");

        if (highlighterTransform != null)
        {
            // Activate the "Highlighter" child
            highlighterTransform.gameObject.SetActive(false);
        }
                    
    }
    // bool clickTOGGLE = false;
    private static int ii , jj ;
    public static void Selectedbeed(int i , int j)
    {
        if(playerid != 0)
        {
            if(turn.GetTurn() != playerid )
            return;
        }

        if((ii == i) && (jj == j))
        {
            IsAnySelected = false ;
            GetPossibleIndexMovesOFF(ii , jj);
            // Debug.Log("aya");
            return ;
        }
        // Debug.Log("ayaaaaaa" + ii + " , " + jj);
        IsAnySelected = false ;
        GetPossibleIndexMovesOFF(ii , jj);
        ii = i ;
        jj = j ;
        // Debug.Log("ayaaaaaa");
        GetPossibleIndexMoves(ii , jj);
        // if(IsValidIndex(ii,jj))
            IsAnySelected = true ;
    }
    static List<(int,int)> FindMovepath(int ii , int jj,int i , int j)
    {
       allPathss = getExpectedMoves.GetExpectedMovess(boardvalues,ii,jj) ;

        // Debug.Log("count = " + allPathss.Count);
       for(int l = 0 ; l < allPathss.Count ; l++)
       {
            // Debug.Log("ababababa");
            // for (int m = 0 ; m < allPathss[l].Count ; m++)
            // {
            //     Debug.Log(allPathss[l][m].Item1 + " , "+allPathss[l][m].Item2 );
            // }
            // Debug.Log("1234567890");
       }
        int idx = -1 , idx_j = -1;
        // Debug.Log(allPathss[k].Count);
         for (int k = 0; k < allPathss.Count; k++)
            {
                //  Debug.Log(allPathss.Count+" Path " + (k + 1) + ":" + allPathss[k].Count);
                // Debug.Log(allPathss[k][0].Item1 + "lllllllllll , "+allPathss[k][0].Item2 );
                
                for (int l = 0  ; l < allPathss.Count; l++)
                {
                    int m = allPathss[l].Count - k - 1  ;
                    if(m > 0 )
                    {
                        // Debug.Log(allPathss[l][m].Item1 + "lllllllllll , "+allPathss[l][m].Item2 );
                        // Debug.Log( i+ " ,jnjnjnjn " + j);
                        if(allPathss[l][m].Item1== i && allPathss[l][m].Item2 == j)
                        {
                            // Debug.Log( k+ " ,jnjnjnjn " + m);
                            idx= l ;
                            idx_j = m ;
                            // Debug.Log( idx+ "rrrrrrr" + idx_j);
                            break;
                        }
                    }

        
                }
                // Console.WriteLine();
            }
        List<(int,int)> path = new List<(int,int)>();
        for(int l = 0 ; l <= idx_j ; l++)
        {
            path.Add((allPathss[idx][l].Item1,allPathss[idx][l].Item2));
        }
        // Debug.Log(path.Count + " , gfgfffh");
        return path ;
    }

   





    public static void Selectedblock(int i , int j)
    {
        if(IsAnySelected == true && (turn.GetTurn() == playerid || playerid == 0))
        {
            //! Find the "Highlighter" child within each place
            Transform highlighterTransform = places[i][j].transform.Find("Highlighter");

            if(highlighterTransform.gameObject.activeSelf)
            {
                
                List<(int,int)> path = FindMovepath(ii,jj,i,j);
                // Debug.Log(path.Count);
                if(path!= null)
                {
                    for(int l = 0 ; l < path.Count - 1  ; l++)
                    {

                        // Debug.Log(path[l].Item1+" , "+path[l].Item2+" ==> "+path[l+1].Item1+" , "+path[l+1].Item2);
                        MoveBeadFromTo(path[l].Item1,path[l].Item2,path[l+1].Item1,path[l+1].Item2);
                    }
                    // indexer = 0 ;
                    // movee = path ;
                    // movebeed();
                
                    if(turn.GetTurn() == playerid && isOnline == true )
                    {
                        // Debug.Log(turn.GetCount());
                        MongoDBManager.Instance.InsertInGameData(turn.GetCount(),path);
                        
                    }
                    DisableAllHighlighters();
                    turn.ToggleTurn();
                    if(VS_PC == true)
                    {
                        if(turn.GetTurn() == 1)
                        {
                            // Debug.Log("turn = " + turn.GetTurn());
                            
                            List<(int,int)> AIpath = aIPlayer.FindBestMove();
                            if(AIpath!=null)
                            {
                                // DisableHighlighters(AIpath[0].Item1,AIpath[0].Item2);
                                // indexer = 0 ;
                                // movee = AIpath ;
                                // movebeed();
                                for(int l = 0 ; l < AIpath.Count - 1  ; l++)
                                {
                                    // Debug.Log(AIpath[l].Item1+" , "+AIpath[l].Item2+" ==> "+AIpath[l+1].Item1+" , "+AIpath[l+1].Item2);
                                    MoveBeadFromTo(AIpath[l].Item1,AIpath[l].Item2,AIpath[l+1].Item1,AIpath[l+1].Item2);
                                }
                                turn.ToggleTurn();
                                // DisableHighlighters(AIpath[0].Item1,AIpath[0].Item2);
                                DisableAllHighlighters();
                            }

                            // Debug.Log(AIpath[0].Item1 + " jhgfdsdfgf, " + AIpath[0].Item2) ;
                        }
                    }
                }

                // DisableHighlighters(ii,jj);
                // Debug.Log(ii + " , "+ jj + "==> " + i + " , " + j );
                
            }


            
        }
        int hasWon = GetWinner.checkwinner(boardvalues);
        if(hasWon!= 0)
        {
            if(hasWon == 1)
            {
                Debug.Log("Red Lost");
                PlayerPrefs.SetString("GameState","Blue has Won" );
            }
            if(hasWon == 2)
            {
                Debug.Log("Blue Lost");
                PlayerPrefs.SetString("GameState","Red has Won" );
            }

            SceneManager.LoadScene(0);
        }

        
        
        // if(IsValidIndex(ii,jj))
        IsAnySelected = false ;
        GetPossibleIndexMovesOFF(ii , jj);
        // ii = -1 ;
        // jj = -1 ;
        

    }
    private static bool isdiagonal1_1(int ii, int jj, int i, int j)
    {
        if(ii == i + 1 || ii == i-1)
        {
            if(jj == j + 1 || jj == j-1)
                return true;
        }
        return false ;
    }
    
    
    static void MoveBeadFromTo(int ii, int jj, int i, int j)
    {
        // Get the child object from places[ii][jj]
        if(boardvalues[i][j] == 0)
        {
            GameObject beadToMove = GetSecondChildObject(places[ii][jj]);
            int mi = (ii + i) / 2;
            int mj = (jj + j) / 2;

            if((mi != ii || mj != jj ) && boardvalues[mi][mj] != 0 && isdiagonal1_1(ii,jj,i,j)== false)
            {
                GameObject tempbeadToMove = GetSecondChildObject(places[mi][mj]);
                Destroy(tempbeadToMove.transform.gameObject);
                boardvalues[mi][mj] = 0;
            }
        

            if (beadToMove != null && boardvalues[i][j] == 0)
            {
                // BeadMover.StartCoroutine(BeadMover.MoveBead(beadToMove, places[i][j].transform, duration));
                // BeadMoverSingleton.Instance.StartCoroutine(BeadMoverSingleton.Instance.MoveBead(beadToMove, places[i][j].transform, 4));

                // System.Threading.Thread.Sleep(4000);
                

                beadToMove.transform.SetParent(places[i][j].transform);
                // Optionally, reset its local position and rotation if needed
                beadToMove.transform.localPosition = Vector3.zero;
                beadToMove.transform.localRotation = Quaternion.identity;
                // Debug.Log("Moved Bead from places[" + ii + "][" + jj + "] to places[" + i + "][" + j + "]");
                boardvalues[i][j] = boardvalues[ii][jj] ;
                boardvalues[ii][jj] = 0;
                // Debug.Log("turn = " + turn.GetTurn());  
            }
            else
            {
                // Debug.LogWarning("No second child found on places[" + ii + "][" + jj + "]");
            }
        }
    }

    static GameObject GetSecondChildObject(GameObject parent)
    {
        // Check if the parent has at least two children
        if (parent.transform.childCount >= 2)
        {
            // Return the second child
            return parent.transform.GetChild(1).gameObject;
        }
        else
        {
            return null; // No second child found
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}


public class BeadMoverSingleton : MonoBehaviour
{
    private static BeadMoverSingleton instance;

    int completed = 0 ;

    public static BeadMoverSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("BeadMoverSingleton");
                instance = go.AddComponent<BeadMoverSingleton>();
                DontDestroyOnLoad(go); // Keep the instance alive across scenes
            }
            return instance;
        }
    }

    public IEnumerator MoveBead(GameObject bead, Transform target, float duration)
    {
        completed = 0 ;
        Vector3 start = bead.transform.position;
        Vector3 end = target.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            bead.transform.position = Vector3.Lerp(start, end, t); // Smoothly move toward the target
            yield return null; // Wait until the next frame
        }
        completed = 1 ;
        bead.transform.position = end; // Ensure the final position is accurate
    }
    public void check()
    {
        if(completed == 0)
        {
            Invoke("check",2f);
        }
        else
            completed = 1 ;

        Debug.Log("dsfdsf");
        

    }

}