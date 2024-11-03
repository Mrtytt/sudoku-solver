using System;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading;

namespace SudokuSolver
{

    public class SudokuSolver
    {
        private int[,] puzzle;
        private PriorityQueue<int[,],int> openSet;

        public SudokuSolver(int[,] initialPuzzle)
        {
            puzzle = initialPuzzle;
            openSet = new PriorityQueue<int[,], int>();
        }

        public void Solve()
        {
            long l = Stopwatch.GetTimestamp();

            int[,] initialState = puzzle;
            openSet.Enqueue(initialState, 0);
            
            while (openSet.Count > 0)
            {
                int[,] current = openSet.Dequeue();

                if (IsSolved(current))
                {
                    PrintSolution(current);
                    System.Console.WriteLine("Solved in {0}",Stopwatch.GetElapsedTime(l).TotalMilliseconds);
                    return;
                }

                GetNeighbors(current,ref openSet);
                /*
                foreach (var neighbor in GetNeighbors(current))
                {
                    openSet.Enqueue(neighbor, 0);
                }
                */
            }

            Console.WriteLine("No solution found.");
        }

        private bool IsSolved(int[,] board)
        {
            // Check if there are any zeros left (meaning incomplete board)
            foreach (var cell in board)
            {
                if (cell == 0) return false;
            }
            return true;
        }

        private IEnumerable<int[,]> GetNeighbors(int[,] state, ref PriorityQueue<int[,],int> pq)
        {
            var neighbors = new List<int[,]>();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (state[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsValid(state, row, col, num))
                            {
                                int[,] newBoard = CopyBoard(state);
                                newBoard[row, col] = num;
                                
                                pq.Enqueue(newBoard, CalculateCost(newBoard));
                                
                                //neighbors.Add(newBoard);
                            }
                        }
                        return neighbors;
                    }
                }
            }
            return neighbors;
        }

        private bool IsValid(int[,] board, int row, int col, int num)
        {
            for (int i = 0; i < 9; i++)
                if (board[row, i] == num || board[i, col] == num ||
                    board[row - row % 3 + i / 3, col - col % 3 + i % 3] == num)
                    return false;
            return true;
        }

        private int CalculateCost(int[,] board)
        {
            int emptyCells = 0;
            foreach (var cell in board)
            {
                if (cell == 0) emptyCells++;
            }
            return emptyCells; // Fewer empty cells means closer to solution
        }

        private int[,] CopyBoard(int[,] board)
        {
            int[,] newBoard = new int[9, 9];
            Array.Copy(board, newBoard, board.Length);
            return newBoard;
        }

        private void PrintSolution(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    // Usage
    class Program
    {
        static void Main()
        {
            int[,] puzzle = {
            {0, 0, 9, 0, 0, 0, 0, 0, 2},
            {8, 7, 5, 0, 0, 0, 0, 0, 0},
            {0, 0, 1, 0, 0, 0, 3, 0, 9},
            {0, 0, 0, 0, 0, 0, 7, 0, 0},
            {0, 0, 0, 5, 0, 7, 0, 9, 0},
            {1, 0, 0, 8, 0, 0, 5, 0, 0},
            {4, 0, 0, 0, 0, 9, 0, 0, 0},
            {0, 0, 0, 0, 3, 0, 0, 4, 6},
            {0, 8, 0, 0, 1, 0, 0, 0, 0}
        };

            SudokuSolver solver = new SudokuSolver(puzzle);
            solver.Solve();
        }
    }

}