using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minmax 
{

    

    int Minimax(int[][] board, int depth, bool isMaximizingPlayer, int aiTeamValue) {
    if (depth == 0) {
        return 0; 
    }

    var allMoves = GenerateAllMoves(board, isMaximizingPlayer ? aiTeamValue : 2);

    if (isMaximizingPlayer) {
        int maxEval = int.MinValue;
        foreach (var (i, j, moves) in allMoves) {
            foreach (var move in moves) {
                // Debug.Log("Eval = " + move.Count);
                var newBoard = MakeMove(board, move); // Create a copy of the board and apply the move
                int eval = Minimax(newBoard, depth - 1, true, aiTeamValue);
                maxEval = System.Math.Max(maxEval, move.Count);
            }
        }
        return maxEval;
    } else {
        int minEval = int.MaxValue;
        foreach (var (i, j, moves) in allMoves) {
            foreach (var move in moves) {
                var newBoard = MakeMove(board, move); // Create a copy of the board and apply the move
                int eval = Minimax(newBoard, depth - 1, false, 2);
                minEval = System.Math.Min(minEval, move.Count);
            }
        }
        return minEval;
    }
}
    public List<(int, int)> GetBestMove(int[][] board, int depth, int aiTeamValue) 
    {
    List<(int, int)> bestMove = null;
    int bestEval = int.MinValue;

    var allMoves = GenerateAllMoves(board, aiTeamValue);

    Debug.Log(allMoves.Count);

    foreach (var (i, j, moves) in allMoves) {
        foreach (var move in moves) 
        {

            
            var newBoard = MakeMove(board, move); // Apply the move to a copy of the board
            int eval = Minimax(newBoard, depth - 1, true, aiTeamValue);

            if (eval > bestEval) {
                
                bestEval = eval;
                bestMove = new List<(int, int)>(move);
                // Debug.Log(bestMove.Count+"aaaaaaa");
            }
        }
    }
    // Debug.Log(bestMove.Count+"tttt");

    return bestMove;
}

int[][] MakeMove(int[][] board, List<(int, int)> moves) {
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



    List<(int, int, List<List<(int,int)>>)>  GenerateAllMoves(int[][] board, int aiTeamValue) 
    {
        List<(int, int, List<List<(int,int)>>)> allMoves = new List<(int, int, List<List<(int, int)>>)>();
        for (int i = 0; i < board.Length; i++) {
            for (int j = 0; j < board[i].Length; j++) {
                if (board[i][j] == aiTeamValue) {
                    // Generate moves for this piece at (i, j)
                    var moves = GenerateMoves(board, aiTeamValue, i, j); 
                    // Debug.Log("yyyyyy = " + moves.Count);
                    allMoves.Add((i, j, moves));
                }
            }
        }
    return allMoves;
    }
    List<List<(int, int)>> GenerateMoves(int[][] board, int teamValue, int i, int j) {
        var possibleMoves = new List<List<(int, int)>>();

        BeedData beed = new BeedData();
        beed.Store(i,j,board);
        // Implement logic to generate possible moves for this position and team
        return beed.allPaths;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

   
}
