using System.Diagnostics;

namespace Program
{

    public class App
    {

        public static void Main()
        {
            Board board = new Board();
            board.LoadBoard();

            //System.Console.WriteLine("board that highlights same numbers as x:8 y:2 (i:2 j:8)");
            //board.PrintBoard(8, 2, PrintMode.horizontal);
            //board.PrintBoard(x: 8, print_mode:PrintMode.select_numbers);

            Sudoku sudoku = new Sudoku(board);
            sudoku.board.PrintBoard();
            System.Console.WriteLine("cannot be 1 if 0");
            sudoku.PossibilityBoardForNum(1).PrintBoard(x: 0, print_mode: PrintMode.select_numbers);

            //Board.PrintArr(sudoku.PossibilityBoardForNum(7).BoxArr(5, 2),PrintMode.box);

            //System.Console.WriteLine("cannot be 9 if 0");
            //sudoku.PossibilityBoardForNum(9).PrintBoard(x: 0, print_mode: PrintMode.select_numbers);

            //System.Console.WriteLine("how many possibilities there are");
            //sudoku.PossibilityBoardCombined().PrintBoard(x: 1, y: 0, print_mode: PrintMode.dual, highlight_color2: ConsoleColor.DarkGreen);
        }

    }

    public enum PrintMode
    {
        none,           //  doesn't highlight
        def,            //  highlights just the number
        dual,           //  highligts two diffrent selected numbers
        horizontal,     //  highlights the row
        vertical,       //  highlights the column
        cross,          //  highlights both row and column
        box,            //  highlights the box
        same_numbers,   //  highlights all the numbers
        select_numbers, //  highlights all the numbers that is selected
        selected_numbers_crosses, // not yet implemented

    }

    

    public class Move
    {
        public int x; // j
        public int y; // i
        public int value;

        public Move(int x = 0, int y = 0, int value = 0)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public void PrintMove(ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            System.Console.WriteLine("({0},{1}) {2}", x, y, value);
            Console.ResetColor();
        }

        public bool isSame(int i, int j, int value)
        {
            if (this.x == j && this.y == i && this.value == value)
                return true;
            return false;
        }

    }

    
}