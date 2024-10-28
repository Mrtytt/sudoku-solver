using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace SudokuGame
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
            System.Console.WriteLine("cannot be 7 if 0");
            sudoku.PossibilityBoardForNum(7).PrintBoard(x: 0, print_mode: PrintMode.select_numbers);

            //Board.PrintArr(sudoku.PossibilityBoardForNum(7).BoxArr(5, 2),PrintMode.box);
            
            //System.Console.WriteLine("cannot be 9 if 0");
            //sudoku.PossibilityBoardForNum(9).PrintBoard(x: 0, print_mode: PrintMode.select_numbers);

            //System.Console.WriteLine("how many possibilities there are");
            sudoku.PossibilityBoardCombined().PrintBoard(x: 1, y: 0, print_mode: PrintMode.dual, highlight_color2: ConsoleColor.DarkGreen);


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

    public class Board
    {

        public const int BOARD_SIZE = 9; 
        public const string FILE_PATH = "board.txt";

        public const char EMPTY_BOX_CHAR = '.';

        public int[,] board_data = new int[BOARD_SIZE, BOARD_SIZE];


        /// <summary>
        /// creates a board and fills it with 0es
        /// </summary>
        public Board(int val = 0)
        {
            for (int i = 0; i < BOARD_SIZE; i++)
                for (int j = 0; j < BOARD_SIZE; j++)
                    board_data[i,j] = val;
        }

        public void LoadBoard(string file_path = "boards/board")
        {
            using (StreamReader reader = new StreamReader(file_path))
            {
                string line;

                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nums = line.Split(' ');

                    int j = 0;
                    foreach (string num in nums)
                    {
                        if (num.Contains('.'))
                            board_data[i, j] = 0;
                        else
                            board_data[i, j] = Convert.ToInt32(num);
                        j++;
                    }

                    i++;
                }
            }
        }
        public void SaveBoard(string name)
        {

        }

        /// <summary>
        /// prints the board can highlight by given x, y for next move
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="boardcolor"></param>
        public void PrintBoard( int x = -1,int y = -1, PrintMode print_mode = PrintMode.none,
                                ConsoleColor border_color = ConsoleColor.DarkYellow,
                                ConsoleColor highlight_color = ConsoleColor.DarkRed,
                                ConsoleColor highlight_color2 = ConsoleColor.Red
                                )
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if (i == 3 || i == 6)
                {
                    Console.ForegroundColor = border_color;
                    for (int j = 0; j < BOARD_SIZE + 2; j++)
                        if (j == 3 || j == 7)
                            Console.Write("+ ");
                        else
                            Console.Write("- ");
                    System.Console.WriteLine();
                    Console.ResetColor();
                }
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (j == 3 || j == 6)
                    {
                        Console.ForegroundColor = border_color;
                        Console.Write("| ");
                        Console.ResetColor();
                    }
                    if (print_mode == PrintMode.none)
                       PrintPos(i,j);
                    else if (print_mode == PrintMode.def)
                    {
                        if (i == y && j == x)
                            PrintPos(i, j, highlight_color);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.horizontal)
                    {
                        if (i == y && j == x)
                            PrintPos(i, j, highlight_color);
                        else if (j == x)
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.vertical)
                    {
                        if (i == y && j == x)
                            PrintPos(i, j, highlight_color);
                        else if (i == y)
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.cross)
                    {
                        if (i == y && j == x)
                            PrintPos(i, j, highlight_color);
                        else if (i == y)
                            PrintPos(i, j, highlight_color2);
                        else if (j == x)
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.box)
                    { 
                        int box_x = x / 3;
                        int box_y = y / 3;

                        int box_i = i / 3;
                        int box_j = j / 3;

                        if (i == y && j == x)
                            PrintPos(i, j, highlight_color);
                        else if (box_i == box_y && box_j == box_x)
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.same_numbers)
                    {
                        if (i == y && j == x)
                            PrintPos(i, j, highlight_color);
                        else if (board_data[y,x] == board_data[i, j])
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.select_numbers)
                    {

                        if (x == board_data[i, j])
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                    else if (print_mode == PrintMode.dual)
                    {

                        if (x == board_data[i, j])
                            PrintPos(i, j, highlight_color);
                        else if (y == board_data[i, j])
                            PrintPos(i, j, highlight_color2);
                        else
                            PrintPos(i, j);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// prints position
        /// </summary>
        /// <param name="i"></param>
        /// <param name="y"></param>
        public void PrintPos(int i, int j, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            int val = board_data[i,j];
            if(val == 0)
                Console.Write(EMPTY_BOX_CHAR + " ");
            else
                Console.Write(val + " ");
            Console.ResetColor();
        }

        public static Board SumBoards(Board board1, Board board2)
        {
            Board res = new Board();

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    res.board_data[i,j] = board1.board_data[i, j] + board2.board_data[i, j];
                }
            }

            return res;
        }


        /// <summary>
        /// returns the row as an arr to further controls
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int[] RowArr(int i, int j)
        {
            int[] res = new int[BOARD_SIZE];
            for (int i_in = 0; i_in < BOARD_SIZE; i_in++)
            {
                res[i_in] = board_data[i_in, j];
            }
            return res;
        }

        /// <summary>
        /// returns the col as an arr to further controls
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param> 
        /// <returns></returns>
        public int[] ColArr(int i, int j)
        {
            int[] res = new int[BOARD_SIZE];
            for (int j_in = 0; j_in < BOARD_SIZE; j_in++)
            {
                res[j_in] = board_data[i, j_in];
            }
            return res;
        }

        /// <summary>
        /// returns the box as an arr to further controls
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int[] BoxArr(int i, int j)
        {
            int[] res = new int[BOARD_SIZE];
            int counter = 0;
            for (int i_in = 0; i_in < Board.BOARD_SIZE; i_in++)
            {
                for (int j_in = 0; j_in < Board.BOARD_SIZE; j_in++)
                {
                    int box_i = i / 3;
                    int box_j = j / 3;

                    int box_i_in = i_in / 3;
                    int box_j_in = j_in / 3;

                    if (box_i == box_i_in && box_j == box_j_in)
                        res[counter++] = board_data[i_in, j_in];
                }
            }
            return res;
        }

        public static void PrintArr(int[] arr, PrintMode print_mode)
        {
            if (print_mode == PrintMode.horizontal)
                for (int i = 0; i < BOARD_SIZE; i++)
                    PrintVal(arr[i]);
            if (print_mode == PrintMode.vertical)
                for (int i = 0; i < BOARD_SIZE; i++)
                    PrintVal(arr[i],true);
            if (print_mode == PrintMode.box)
                for (int i = 0; i < BOARD_SIZE; i++)
                {
                    if (i == 3 || i == 6)
                        System.Console.WriteLine();
                    PrintVal(arr[i]);
                }
            System.Console.WriteLine();
        }

        public static void PrintVal(int val, bool next_line = false, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            if (val == 0)
                Console.Write(EMPTY_BOX_CHAR + " ");
            else
                Console.Write(val + " ");
            Console.ResetColor();

            if (next_line)
                Console.WriteLine();
        }


    }

    public class Move {
        int x; // j
        int y; // i
        int value;

        public Move(int x = 0, int y = 0, int value = 0)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public void PrintMove()
        {
            System.Console.WriteLine( "({0},{1}) {2}", x, y, value);
        }

    }

    public class Sudoku {

        Board board;

        Board[] possibility_boards = new Board[Board.BOARD_SIZE];
        Board possibility_boards_combined;

        public Sudoku(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// finds next move
        /// </summary>
        public Move FindNextMove() {

            /// create the possiblitiy board to see possible moves
            /// create the mock board to calculate values
            
            return new Move();
        }





        /// <summary>
        /// f(n) = g(n) + h(n)
        /// </summary>
        public int EvalFunction() { return 0;}

        /// <summary>
        ///  h(n)
        /// </summary>
        public int HeuristicFunc() { return 0;}

        /// <summary>
        /// creates an mock board of evaluation numbers for h(n)
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Board MockBoardForNum(int num) { return new Board(); }


        public Move[] CreateLoneArr() {
            List<Move> moves = new List<Move>();

            // select number
                // check rows
                // check cols
                // check  

            return moves.ToArray();
        }

        /// <summary>
        /// creates an possibility board of evaluation numbers for h(n)
        /// 0 means cant place 1 means placeable
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Board PossibilityBoardForNum(int num) {

            Board res = new Board(1);

            // set already available numbers to 0 
            // todo export to another function to use later
            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                {
                    if (board.board_data[i,j] > 0)
                    {
                        res.board_data[i,j] = 0;
                    }
                }
            }


            // get the numbers into moves arrray 
            for (int i = 0; i < Board.BOARD_SIZE; i++)
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                    if (board.board_data[i,j] == num)
                    {

                        // set horizontal 0 same j
                        for (int i_in = 0; i_in < Board.BOARD_SIZE; i_in++)
                        {
                            res.board_data[i_in,j] = 0;
                        }
                        // set vertical 0 same i
                        for (int j_in = 0; j_in < Board.BOARD_SIZE; j_in++)
                        {
                            res.board_data[i, j_in] = 0;
                        }
                        // set box 0
                        for (int i_in = 0; i_in <  Board.BOARD_SIZE; i_in++)
                        {
                            for (int j_in = 0; j_in <  Board.BOARD_SIZE; j_in++)
                            {
                                int box_i = i / 3;
                                int box_j = j / 3;

                                int box_i_in = i_in / 3;
                                int box_j_in = j_in / 3;

                                if (box_i == box_i_in && box_j == box_j_in)
                                    res.board_data[i_in,j_in] = 0;
                            }
                        }
                        
                    }

            possibility_boards[num - 1] = res;
            return res;
        }

        /// <summary>
        /// creates an possibility board of evaluation numbers for h(n)
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Board PossibilityBoardCombined() { 

            Board res = new Board();

            for (int i = 1; i < Board.BOARD_SIZE + 1; i++)
            {
                res = Board.SumBoards(res,PossibilityBoardForNum(i));
            }

            possibility_boards_combined = res;
            return res;
        }

        /// <summary>
        /// creates better possibility boards
        /// </summary>
        /// <returns></returns>
        public Board PossibilityBoardBetter()
        {
            /// use repeatance
            CheckBoxForRepeatance(0,0,new Board());

            return new Board();
        }


        /// <summary>
        /// checks in box for horizontal or vertical placement if it founds returns the resutl
        /// PrintMode.def for lonelynums
        /// PrintMode.vertical , int for vertical repatance and the col number of vertical repeatance
        /// PrintMode.horizotnal , int for horizotnal repatance and the row number of horizotnal repeatance
        /// if it founds more than one repeatance it defaults to none
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public (PrintMode, int) CheckBoxForRepeatance(int box_i, int box_j, Board board)
        {
            // check for lonely number
            // check for horizontal
            // check for vertical

            return (PrintMode.none, -1);
        }

        /// <summary>
        /// g(n) = constant
        /// </summary>
        public int CostFunc() { return 1;}

    }
}