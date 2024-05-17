using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using MongoDB.Driver;

// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIPlayer 
{
    public BeedData[] beedData = new BeedData[12] ; 
    public int idx ;
    int AIteamvalue = 1 ;
    public void SetAIplayerValue(int val)
    {
        AIteamvalue = val ;
    }
    public void GiveAIBeedsBestMoves()
    {
        GiveAIBeedsBestMoveshelper(GameManager.GetBoardValues());
    }

    public List<(int, int)> FindBestMove()
    {
        return Minmaxhelper(GameManager.GetBoardValues());
    }

    void GiveAIBeedsBestMoveshelper(int[][] board)
    {
        for (int i = 0; i < beedData.Length; i++)
        {
            beedData[i] = new BeedData();
        }
        // beedData = new BeedData[12] ;
        idx = 0 ;
        for(int i = 0 ; i < 5 ; i++)
        {
            for(int j = 0 ; j < 5 ; j++)
            {
                if(board[i][j] == AIteamvalue)
                {
                    if(beedData[idx] == null)
                        Debug.Log("00000000000");
                    else
                    {
                        beedData[idx].Store(i,j,board) ;
                        idx++;
                    }
                }
            }
        }

    }

    public List<(int, int)> Minmaxhelper(int[][] board,int val = 1)
    {

        for (int i = 0; i < beedData.Length; i++)
        {
            beedData[i] = new BeedData();
        }
        // beedData = new BeedData[12] ;
        idx = 0 ;
        
        for(int i = 0 ; i < 5 ; i++)
        {
            for(int j = 0 ; j < 5 ; j++)
            {
                if(board[i][j] == val)
                {
                    if(beedData[idx] == null)
                        Debug.Log("00000000000");
                    else
                    {
                        beedData[idx].Store(i,j,board) ;
                        idx++;
                    }
                }
            }
        }



        if(idx == 0)
        {   
            return null;
        }
        int bestmoveidx = -1 ;
        int bestmovepathidx = - 1 ;

        Minmax minmax = new Minmax();
        // List<(int,int)> values = minmax.GetBestMove(board,3,1);


        // for(int i = 0; i < values.Count; i++)
        // {
        //     Debug.Log(values[i].Item1 + " --- " + values[i].Item2);
        // }

        int localdeadmovecount = 0 ;
        for(int i = 0 ; i < idx ; i++)
        {
            // Debug.Log(beedData[i].i + " , " + beedData[i].j);
            if(beedData[i].hasDeadMove == 1 )
            {

                // Debug.Log(beedData[i].i+" === " +beedData[i].j +" , "+ beedData[i].allPaths.Count);
                // beedData[i].allPaths
                if(beedData[i].maxmovescount > localdeadmovecount)
                {
                    bestmoveidx = i ;

                //+ " , "+ beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx][0].item2);
                    bestmovepathidx = beedData[i].maxmovesidx ;
                }
               
                // Debug.Log(beedData[bestmoveidx].allPaths[1][0].Item1 + " , "+beedData[bestmoveidx].allPaths[1][0].Item2 + " ===... " + bestmoveidx );
            }
            

        }
        if(idx == 0)
            return null;
        if(bestmoveidx == -1)
        {
            int i = 0 ;
            while(true)
            {
                i++;
                bestmoveidx = Random.Range(0, idx);
                if(i==13)
                {
             
                    if(beedData[0].allPaths[0] == null)
                        return null ;
                    return beedData[0].allPaths[0];
                }
                if (beedData != null) {
                    // Debug.Log("beedData is not null");

                    if (bestmoveidx >= 0 && bestmoveidx < beedData.Length) {
                        var bestMove = beedData[bestmoveidx];
                        // Debug.Log("bestMove accessed");

                        if (bestMove.allPaths != null) {
                            // Debug.Log("allPaths is not null");

                            if (bestMove.maxmovesidx >= 0 && bestMove.maxmovesidx < bestMove.allPaths.Count) {
                                var path = bestMove.allPaths[bestMove.maxmovesidx];
                                // Debug.Log("path accessed");

                                if (path != null && path.Count > 1) {
                                    // Debug.Log("path is valid");
                                    break;
                                } 
                                else if(i >= 10)
                                {
                                    if (path != null && path.Count > 0) {
                                    Debug.Log("path is valid");
                                    break;
                                } 
                                }
                                else {
                                    // Debug.LogError("Path is null or empty");
                                }
                            } else {
                                // Debug.LogError("Invalid maxmovesidx");
                            }
                        } else {
                            // Debug.LogError("allPaths is null");
                        }
                    } else {
                        // Debug.LogError("Invalid bestmoveidx");
                    }
                } else {
                    // Debug.LogError("beedData is null");
                }


            }

        }

        // Debug.Log(bestmoveidx + " ===== " + beedData[bestmoveidx].maxmovesidx + " , "+idx);
        // Debug.Log(beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx][0].Item1 + " , "+beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx][0].Item2  );//+ " , "+ beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx][0].item2);

        // Debug.Log(beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx][0].Item1 + " , "+beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx][0].Item1 );
        return beedData[bestmoveidx].allPaths[beedData[bestmoveidx].maxmovesidx] ;


    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}

public class BeedData 
{
    public int i , j ;
    public bool placed ;

    public int hasDeadMove = 0;

    public int maxmovesidx = 0 ;

    public int maxmovescount = 0 ;

    

    public List<List<(int, int)>> allPaths = new List<List<(int, int)>>();

    private bool hasdeadmove(int ii , int jj , int i , int j)
    {
        int diffi = ii - i ;
        int diffj = jj - j ;
    //    return true;
        if(diffi == -2 || diffi == 2 || diffi == 0)
            if(diffj == -2 || diffj == 2 || diffj == 0 )
                return true ;
        return false ;
    }

    
    public void Store( int ii , int jj , int[][] board )
    {

        i = ii ;
        j = jj ;
        placed = true ;
        // GetExpectedMoves getExpectedMoves = new GetExpectedMoves() ;
        List<List<(int, int)>> tempallPaths = new List<List<(int, int)>>();
        GetExpectedMoves getExpectedMoves = new GetExpectedMoves();
        tempallPaths = getExpectedMoves.GetExpectedMovess(board,i,j);
        allPaths = tempallPaths ;
        // int havingdeadmove = 0 ;
        // Debug.Log(i+ " , "+ j);
        // Debug.Log("Counttt = " + allPaths.Count );
        if(allPaths.Count > 0)
        {
            // maxmoves = allPaths[0].Count ;
            maxmovesidx = -1 ;

            int localmax = 1 ; 

            for (int k = 0; k < allPaths.Count ; k++)
            {
                if(allPaths[k].Count > 1)
                {
                if(hasdeadmove(allPaths[k][0].Item1, allPaths[k][0].Item2, allPaths[k][1].Item1, allPaths[k][1].Item2))
                {
                    if(allPaths[k].Count > localmax)
                    {
                    localmax = allPaths[k].Count ;
                    maxmovesidx = k ;
                    hasDeadMove = 1 ;
                    }
                }
                }
            }
            maxmovescount = localmax ;

            
            if(maxmovesidx == -1)
            {
                int raniidx ;//ranjidx ;
                int i = 0 ;
                while(true)
                {

                    raniidx = Random.Range(0, allPaths.Count);
                    if(allPaths[raniidx].Count >1)
                    {
                        maxmovesidx = raniidx ;
                        break;
                    }
                    i++;
                    if(i == 3)
                    {
                        maxmovesidx = raniidx ;
                        break;
                    }


                }

                hasDeadMove = 0;
            
        } 
    }
    }


}

public class MoveNode
{
    public List<MoveNode> Moves ;//= new List<MoveNode>();
    public int value { get; set; }
    public List<(int,int)> move;
    public int[][] board ;

    // public int playerid = 2 ; 

    // public int AIteamid = 1 ;

    

    public MoveNode(int[][] b , List<(int,int)> mov )
    {
        Moves = new List<MoveNode>();
        board = GiveNewBoard(b,null);
        value = -1 ;
    }   

    public void SetNode(int[][] b , List<(int,int)> mov)
    {
        move = new List<(int,int)>(mov) ;
        board = GiveNewBoard(b , move );
    }

    public void addChild(MoveNode moveNode)
    {
        Moves.Add( moveNode );
    }

    private bool isdiagonal1_1(int ii, int jj, int i, int j)
    {
        if(ii == i + 1 || ii == i-1)
        {
            if(jj == j + 1 || jj == j-1)
                return true;
        }
        
        return false ;
    }

    void MoveBeadFromTo(int[][] boardvalues ,int ii, int jj, int i, int j)
    {
        // Get the child object from places[ii][jj]
        if(boardvalues[i][j] == 0)
        {
            int mi = (ii + i) / 2;
            int mj = (jj + j) / 2;

            if((mi != ii || mj != jj ) && boardvalues[mi][mj] != 0 && isdiagonal1_1(ii,jj,i,j)== false)
            {

                boardvalues[mi][mj] = 0;
            }
        

            if (boardvalues[i][j] == 0)
            {
                // Move the child object to places[i][j]
               
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

    public int [][] GiveNewBoard(int[][] board ,List<(int, int)> moves )
    {
        int[][] newboardValues = new int[5][]
        {
        new int[] {board[0][0],board[0][1],board[0][2],board[0][3],board[0][4]},
        new int[] {board[1][0],board[1][1],board[1][2],board[1][3],board[1][4]},
        new int[] {board[2][0],board[2][1],board[2][2],board[2][3],board[2][4]},
        new int[] {board[3][0],board[3][1],board[3][2],board[3][3],board[3][4]},
        new int[] {board[4][0],board[4][1],board[4][2],board[4][3],board[4][4]}
        
        };

        if(moves == null)
            return newboardValues;

        for(int i = 0; i < moves.Count -1 ; i++)
        {
            MoveBeadFromTo(newboardValues,moves[i].Item1,moves[i].Item2,moves[i+1].Item1,moves[i+1].Item2);
        }

        return newboardValues;

    }

    public void setValue(int val)
    {
        value = val ;
    }


}



public class tempMinimax
{
    public MoveNode root ;

    public int playerid = 2 ; 

    public int AIteamid = 1 ;

    public tempMinimax()
    {
        root = new MoveNode(GameManager.GetBoardValues() ,null);
        SetLevel(root,AIteamid);

        

        for(int i = 0 ; i < root.Moves.Count ; i++)
        {
            SetLevel(root.Moves[i],playerid);
            for(int j = 0 ; j < root.Moves[i].Moves.Count ; j++)
            {
                SetLevel(root.Moves[i].Moves[j],AIteamid,2);
            }
        
        }
    }

    public void SetLevel(MoveNode node , int id , int lvl = 0)
    {
        List<List<(int, int)>> tempallPaths = new List<List<(int, int)>>();
        GetExpectedMoves getExpectedMoves = new GetExpectedMoves();
        
    
        for(int i = 0 ; i < 5 ; i++)
        {
            for(int j = 0 ; j < 5 ; j++)
            {
                if(node.board[i][j] == id)
                {
                    tempallPaths = getExpectedMoves.GetExpectedMovess(node.board,i,j);
                    

                    for(int k = 0; k < tempallPaths.Count; k++)
                    {
                        // Debug.Log(tempallPaths[k][tempallPaths[k].Count-1].Item1 + " , "+ tempallPaths[k][tempallPaths[k].Count-1].Item2);
                        MoveNode temp = new MoveNode(node.board,new List<(int, int)> (tempallPaths[k]));
                        if(lvl == 2)
                            temp.setValue(tempallPaths[k].Count);
                        node.addChild(temp);
                    }
                }
            }
        }

        // Debug.Log("Asad = " + id + " == "+ node.Moves.Count);
    }

    void MinMax()
    {
        for(int i = 0 ; i < root.Moves.Count ; i++)
        {
            SetLevel(root.Moves[i],playerid);
            for(int j = 0 ; j < root.Moves[i].Moves.Count ; j++)
            {   
            }
        }
    }





}

public class Minimax
{
    public List<(int,int)> MinimaxAlgorithm(MoveNode Node, bool isMaxPlayer, int depth, int alpha, int beta)
    {
        if (depth == 0)
        {
            // BeedMove temp = EvaluateBoard(board);
            return new List<(int, int)>(Node.move); // Return board evaluation at the specified depth
        }

        int bestResult = isMaxPlayer ? int.MinValue : int.MaxValue;
        List<(int,int)> bestMove  = null;

        List<MoveNode> possibleMoves = Node.Moves;

        foreach (MoveNode move in possibleMoves)
        {
            // if(move.move == null)
            //     continue;
            // Simulate the move
            // In a real implementation, apply and undo the move to evaluate
            List<(int,int)> result = MinimaxAlgorithm(move, !isMaxPlayer, depth - 1, alpha, beta);

            if (isMaxPlayer)
            {
                if (result.Count > bestResult)
                {
                    bestResult = result.Count;
                    bestMove = new List<(int, int)>(result);
                }
                alpha = System.Math.Max(alpha, bestResult);
            }
            else
            {
                if (result.Count < bestResult)
                {
                    bestResult = result.Count;
                    bestMove = new List<(int, int)>(result);
                }
                beta = System.Math.Min(beta, bestResult);
            }

            if (alpha >= beta) break; // Alpha-beta pruning
        }

        return new List<(int, int)>(bestMove);
    }

    // Example evaluation function for the chess board (simplified)
    // public BeedMove EvaluateBoard(int[][] board)
    // {
    //     // Simple evaluation returning a constant value
    //     // A real implementation would analyze the board and return a heuristic value
        
    // }
}

