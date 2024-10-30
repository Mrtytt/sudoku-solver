using System.Diagnostics;

namespace Program
{
    public class App
    {
        public static void Main()
        {
            Board board = new Board();
            board.LoadBoard();
            board.PrintBoard();

            Solver solver = new Solver(board);
            solver.solution.PrintBoard();
        }
    }
}