using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetExpectedMoves : MonoBehaviour
{
    int[][] visited = new int[5][]; 
    List<List<(int, int)>> allPaths = new List<List<(int, int)>>();

    int previ= 0,prevj = 0;
    // Start is called before the first frame update
    void Allocate()
    {
        for (int i = 0; i < 5; i++)
            visited[i] = new int[5];
    }
    private void MakeZero()
    {
        for (int i = 0; i < 5; i++)
            visited[i] = new int[5];
        // Allocate();
        for(int i = 0 ; i < 5 ; i++)
            for(int j = 0 ; j < 5 ;j++)
                visited[i][j] = 0 ;
    }
    private bool IsValidIndex(int i, int j)
    {
        return i >= 0 && i < 5 && j >= 0 && j < 5 && visited[i][j] == 0;
    
    }
    private bool Isvalid(int ii , int jj , int i , int j)
    {
        int diffi = ii - i ;
        int diffj = jj - j ;
    //    return true;
        if(diffi == -2 || diffi == 2 || diffi == 0)
            if(diffj == -2 || diffj == 2 || diffj == 0 )
                return true ;
        return false ;
    }
    public List<List<(int, int)>> GetExpectedMovess(int[][] arr, int i, int j)
    {
        
       
        
            allPaths.Clear();
            previ = i ;
            prevj = j ;
        
        MakeZero();
        List<(int, int)> currentPath = new List<(int, int)>();

        currentPath.Add((i, j));

        // arr[i][j] = 0 ;
        Helper(arr, i, j, arr[i][j], 1,currentPath);
        // Console.WriteLine("Total paths explored: " + allPaths.Count);

        // if(i == 0 && j == 0)
        // {
        //     Debug.Log(i + " , " + j + " === " + currentPath.Count );
        // }

        // for (int k = 0; k < allPaths.Count; ++k)
        // {
        //     if (allPaths[k].Count == 2)
        //         continue;
        //     for (int m = 0; m < allPaths[k].Count - 1; ++m)
        //     {
        //         if (!Isvalid(allPaths[k][m].Item1, allPaths[k][m].Item2, allPaths[k][m + 1].Item1, allPaths[k][m + 1].Item2) )
        //         {
        //             allPaths[k].RemoveAt(m);
        //         }
        //     }
        // }
        // for (int k = 0; k < allPaths.Count; ++k)
        // {
        //     // Debug.Log("Path " + (k + 1) + ": " + allPaths[k].Count);
        //     for (int m = 0; m < allPaths[k].Count; ++m)
        //     {
        //         // Debug.Log("(" + allPaths[k][m].Item1 + ", " + allPaths[k][m].Item2 + ") ");
        //     }
        //     Console.WriteLine();
        // }
        // List<List<(int, int)>> temp = new List<List<(int, int)>>();
        return allPaths ;
    }
    void Helper(int[][] board, int i, int j, int val, int first, List<(int, int)> currentPath)
    {
        // Check the adjacent cells
      
        if (IsValidIndex(i - 1, j))
        {
            // Check if the cell is empty
            if (board[i - 1][ j] == 0)
            {
                // Check if it's the second step of the move
                if (first == 2)
                {
                    allPaths.Add(new List<(int, int)>(currentPath));
                    // return;
                }
                else
                {
                
                    if (first == 1&& currentPath.Count == 1)
                    {

                // Mark the cell as visited
                        visited[i - 1][j] = 1;

                    // Update the current path
                        currentPath.Add((i - 1, j));

                    // Print the cell coordinates
                    // Console.WriteLine($"{i - 1}, {j}");

                    // Check if it's the first step of the move
                    
                        allPaths.Add(new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    // return;
                    }
                }
            }
            else if (val != board[i - 1][ j]&& board[i-1][j]!= 0)
            {
                // Check if the cell is valid for a potential second step
                if (IsValidIndex(i - 2, j) && board[i - 2][ j] == 0)
                {
                    visited[i - 2][ j] = 1;
                    currentPath.Add((i - 2, j));
                    // Console.WriteLine($"{i - 2}, {j}");
                    Helper(board, i - 2, j, val, 2, new List<(int, int)>(currentPath));
                    currentPath.RemoveAt(currentPath.Count-1);
                }
            }
        }
        //!

        if (IsValidIndex(i + 1, j))
        {
            if (board[i + 1][ j] == 0)
            {
                if (first == 2)
                {
                    allPaths.Add(new List<(int, int)>(currentPath));
                    // return;
                }
                else
                {
                
                    if (first == 1&& currentPath.Count == 1)
                    {

                        visited[i + 1][ j] = 1;
                        currentPath.Add((i + 1, j));
                    // Console.WriteLine($"{i + 1}, {j}");
                    // if (first == 1)
                    // {
                        allPaths.Add(new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    // return;
                    }
                }
            }
            else if (val != board[i + 1][ j]&& board[i+1][j]!= 0)
            {
                if (IsValidIndex(i + 2 ,j) && board[i + 2][ j] == 0)
                {
                    visited[i + 2][ j] = 1;
                    currentPath.Add((i + 2, j));
                    // Console.WriteLine($"{i + 2}, {j}");
                    Helper(board, i + 2, j, val, 2, new List<(int, int)>(currentPath));
                    currentPath.RemoveAt(currentPath.Count-1);
                }
            }
        }
        //!
          if (IsValidIndex(i , j - 1))
        {
            if (board[i ][ j-1] == 0)
            {
                if (first == 2)
                {
                    allPaths.Add(new List<(int, int)>(currentPath));
                    // return;
                }
                else
                {
                
                    if (first == 1&& currentPath.Count == 1)
                    {
                        visited[i][ j-1] = 1;
                        currentPath.Add((i, j-1));
                        // Console.WriteLine($"{i}, {j-1}");

                        allPaths.Add(new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                        // return;
                    }
                }
            }
            else if (val != board[i ][ j-1]&& board[i][j-1]!= 0)
            {
                if (IsValidIndex(i , j-2) && board[i ][ j-2] == 0)
                {
                    visited[i][ j-2] = 1;
                    currentPath.Add((i , j-2));
                    // Console.WriteLine($"{i }, {j-2}");
                    Helper(board, i , j-2, val, 2, new List<(int, int)>(currentPath));
                    currentPath.RemoveAt(currentPath.Count-1);
                }
            }
        }
        //!
        if (IsValidIndex(i , j + 1))
        {
            if (board[i ][ j + 1] == 0)
            {
                if (first == 2)
                {
                    allPaths.Add(new List<(int, int)>(currentPath));
                    // return;
                }
                else
                {
                
                    if (first == 1&& currentPath.Count == 1)
                    {

                        visited[i ][ j + 1] = 1;
                        currentPath.Add((i, j + 1));
                        // Console.WriteLine($"{i}, {j + 1}");

                        allPaths.Add(new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    // return;
                  }
                }
            }
            else if (val != board[i ][ j + 1]&& board[i][j+1]!= 0)
            {
                if (IsValidIndex(i , j + 2) && board[i ][ j + 2] == 0)
                {
                    visited[i][ j + 2] = 1;
                    currentPath.Add((i , j + 2));
                    // Console.WriteLine($"{i }, {j + 2}");
                    Helper(board, i , j + 2, val, 2, new List<(int, int)>(currentPath));
                    currentPath.RemoveAt(currentPath.Count-1);
                }
            }
        }
       
        
        //!
            // Special cases
        if ((i + j) % 2 == 0)
        {
            if (IsValidIndex(i - 1, j + 1))
            {
                if (board[i - 1][ j + 1] == 0)
                {
                    if(first == 2)
                    {
                        allPaths.Add(new List<(int, int)>(currentPath));
                        // return;
                    }
                    else
                    {
                
                        if (first == 1&& currentPath.Count == 1)
                        {

                            visited[i - 1][ j + 1] = 1;
                        // Console.WriteLine($"{i - 1}, {j + 1}");
                            currentPath.Add((i - 1, j + 1));
                        // if(first == 1)
                        // {
                            allPaths.Add(new List<(int, int)>(currentPath));
                            currentPath.RemoveAt(currentPath.Count-1);
                        // return;
                        }
                    }
                }
                else if (val != board[i - 1][ j + 1]&& board[i-1][j+1]!= 0)
                {
                    if (IsValidIndex(i - 2, j + 2) && board[i - 2][ j + 2] == 0)
                    {
                        visited[i - 2][j + 2] = 1;
                        // Console.WriteLine($"{i - 2}, {j + 2}");
                        currentPath.Add((i - 2, j + 2));
                        Helper(board, i - 2, j + 2, val, 2, new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    }
                }
            }
            if (IsValidIndex(i + 1, j + 1))
            {
                if (board[i + 1][ j + 1] == 0 && first == 1)
                {
                    if(first == 2)
                    {
                        allPaths.Add(new List<(int, int)>(currentPath));
                        // return;
                    }
                    else
                    {
                
                        if (first == 1&& currentPath.Count == 1)
                        {

                            visited[i + 1][ j + 1] = 1;
                            // Console.WriteLine($"{i + 1}, {j + 1}");
                            currentPath.Add((i + 1, j + 1));
                        // if(first == 1)
                        // {
                            allPaths.Add(new List<(int, int)>(currentPath));
                            currentPath.RemoveAt(currentPath.Count-1);
                        // return;
                        }
                    }
                    
                }
                else if (val != board[i + 1][ j + 1]&& board[i+1][j+1]!= 0)
                {
                    if (IsValidIndex(i + 2, j + 2) && board[i + 2][ j + 2] == 0)
                    {
                        visited[i + 2][ j + 2] = 1;
                        currentPath.Add((i + 2, j + 2));
                //   /      Console.WriteLine($"{i + 2}, {j + 2}");
                        Helper(board, i + 2, j + 2, val, 2, new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    }
                }
            }
            if (IsValidIndex(i + 1, j - 1))
            {
                if (board[i + 1][ j - 1] == 0 && first == 1)
                {
                    if(first == 2)
                    {
                        allPaths.Add(new List<(int, int)>(currentPath));
                        // return;
                    }

                    else
                    {
                
                        if (first == 1&& currentPath.Count == 1)
                        {
                            visited[i + 1][ j - 1] = 1;
                            // Console.WriteLine($"{i + 1}, {j - 1}");
                            currentPath.Add((i + 1, j - 1));
                        
                            allPaths.Add(new List<(int, int)>(currentPath));
                            currentPath.RemoveAt(currentPath.Count-1);
                        // return;
                        }
                    }
                }
                else if (val != board[i + 1][ j - 1]&& board[i+1][j-1]!= 0)
                {
                    if (IsValidIndex(i + 2, j - 2) && board[i + 2][ j - 2] == 0)
                    {
                        visited[i + 2][ j - 2] = 1;
                        currentPath.Add((i + 2, j - 2));
                        // Console.WriteLine($"{i + 2}, {j - 2}");
                        Helper(board, i + 2, j - 2, val, 2, new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    }
                }
            }
            if (IsValidIndex(i - 1, j - 1))
            {
                if (board[i - 1][j - 1] == 0 && first == 1)
                {
                    if (first == 2)
                    {
                        allPaths.Add(new List<(int, int)>(currentPath));
                        // return;
                    }
                    else
                    {
                
                        if (first == 1&& currentPath.Count == 1)
                        {


                            visited[i - 1][ j - 1] = 1;
                            // Console.WriteLine($"{i - 1}, {j - 1}");
                            currentPath.Add((i - 1, j - 1));
                        // if (first == 1)
                        // {
                            allPaths.Add(new List<(int, int)>(currentPath));
                            currentPath.RemoveAt(currentPath.Count-1);
                        // return;
                        }
                    }
                }
                else if (val != board[i - 1][ j - 1]&& board[i-1][j-1]!= 0)
                {
                    if (IsValidIndex(i - 2, j - 2) && board[i - 2][ j - 2] == 0)
                    {
                        visited[i - 2][ j - 2] = 1;
                        currentPath.Add((i - 2, j - 2));
                        // Console.WriteLine($"{i - 2}, {j - 2}");
                        Helper(board, i - 2, j - 2, val, 2, new List<(int, int)>(currentPath));
                        currentPath.RemoveAt(currentPath.Count-1);
                    }
                }
            }
        }
        // if (allPaths.Count == 0)
        // {
            // allPaths.Add(new List<(int, int)>(currentPath));
        // }
        // else
        // {
            // if (allPaths[allPaths.Count - 1].Count < currentPath.Count)
            // {
                allPaths.Add(new List<(int, int)>(currentPath));
            // }
        // }

    }













    // Update is called once per frame
    void Update()
    {
        
    }
}
