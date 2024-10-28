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
            sudoku.board.PrintBoard();
            System.Console.WriteLine("cannot be 1 if 0");
            sudoku.PossibilityBoardForNum(1).PrintBoard(x: 0, print_mode: PrintMode.select_numbers);

            //Board.PrintArr(sudoku.PossibilityBoardForNum(7).BoxArr(5, 2),PrintMode.box);

            //System.Console.WriteLine("cannot be 9 if 0");
            //sudoku.PossibilityBoardForNum(9).PrintBoard(x: 0, print_mode: PrintMode.select_numbers);

            //System.Console.WriteLine("how many possibilities there are");
            //sudoku.PossibilityBoardCombined().PrintBoard(x: 1, y: 0, print_mode: PrintMode.dual, highlight_color2: ConsoleColor.DarkGreen);

            System.Console.WriteLine("##### 1");
            sudoku.PossibilityBoardBetter();
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y,move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();

            }



            System.Console.WriteLine("##### 2");
            sudoku.PossibilityBoardBetter(debug: false,debug2:false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 3");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 4");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 5");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }
            
            System.Console.WriteLine("##### 6");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 7");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 8");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 9");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 10");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            System.Console.WriteLine("##### 11");
            sudoku.PossibilityBoardBetter(debug: false, debug2: false);
            foreach (Move move in sudoku.moves_100)
            {
                System.Console.WriteLine("MOVING");
                move.PrintMove();
                sudoku.board.board_data[move.y, move.x] = move.value;
                sudoku.moves_100 = new List<Move>();
                sudoku.board.PrintBoard();
            }

            sudoku.possibility_boards_combined.PrintBoard();
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
                    board_data[i, j] = val;
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
        public void PrintBoard(int x = -1, int y = -1, PrintMode print_mode = PrintMode.none,
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
                        PrintPos(i, j);
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
                        else if (board_data[y, x] == board_data[i, j])
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
            int val = board_data[i, j];
            if (val == 0)
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
                    res.board_data[i, j] = board1.board_data[i, j] + board2.board_data[i, j];
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
                    PrintVal(arr[i], true);
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

        public void PrintMove()
        {
            System.Console.WriteLine("({0},{1}) {2}", x, y, value);
        }

    }

    public class Sudoku
    {

        public Board board;

        public Board[] possibility_boards = new Board[Board.BOARD_SIZE];
        public Board possibility_boards_combined;

        public List<Move> moves_100 = new List<Move>();

        public Sudoku(Board board)
        {
            this.board = board;
            this.PossibilityBoardCombined();
        }

        /// <summary>
        /// finds next move
        /// </summary>
        public Move FindNextMove()
        {

            /// create the possiblitiy board to see possible moves
            /// create the mock board to calculate values

            return new Move();
        }





        /// <summary>
        /// f(n) = g(n) + h(n)
        /// </summary>
        public int EvalFunction()
        {
            return CostFunc() + HeuristicFunc();
        }

        /// <summary>
        ///  h(n)
        /// </summary>
        public int HeuristicFunc()
        {
            return 0;
        }

        /// <summary>
        /// creates an mock board of evaluation numbers for h(n)
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Board MockBoardForNum(int num) { return new Board(); }


        public Move[] CreateLoneArr()
        {
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
        public Board PossibilityBoardForNum(int num)
        {

            Board res = new Board(1);

            // set already available numbers to 0 
            // todo export to another function to use later
            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                {
                    if (board.board_data[i, j] > 0)
                    {
                        res.board_data[i, j] = 0;
                    }
                }
            }


            // get the numbers into moves arrray 
            for (int i = 0; i < Board.BOARD_SIZE; i++)
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                    if (board.board_data[i, j] == num)
                    {

                        // set horizontal 0 same j
                        for (int i_in = 0; i_in < Board.BOARD_SIZE; i_in++)
                        {
                            res.board_data[i_in, j] = 0;
                        }
                        // set vertical 0 same i
                        for (int j_in = 0; j_in < Board.BOARD_SIZE; j_in++)
                        {
                            res.board_data[i, j_in] = 0;
                        }
                        // set box 0
                        for (int i_in = 0; i_in < Board.BOARD_SIZE; i_in++)
                        {
                            for (int j_in = 0; j_in < Board.BOARD_SIZE; j_in++)
                            {
                                int box_i = i / 3;
                                int box_j = j / 3;

                                int box_i_in = i_in / 3;
                                int box_j_in = j_in / 3;

                                if (box_i == box_i_in && box_j == box_j_in)
                                    res.board_data[i_in, j_in] = 0;
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
        public Board PossibilityBoardCombined()
        {

            Board res = new Board();

            for (int i = 1; i < Board.BOARD_SIZE + 1; i++)
            {
                res = Board.SumBoards(res, PossibilityBoardForNum(i));
            }

            possibility_boards_combined = res;

            // check for possibility 1 and add to move_100

            bool foundmove = false;
            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                {   
                    if (possibility_boards_combined.board_data[i,j] == 1)
                    {
                        for (int k = 0; k < Board.BOARD_SIZE; k++)
                        {
                            if (possibility_boards[k].board_data[i, j] == 1)
                            {
                                foundmove = true;
                                Move move = new Move(j, i, k + 1);
                                System.Console.WriteLine("ADDING MOVE i{0} j{1} k{2} ",i,j,k+1);
                                move.PrintMove();
                                moves_100.Add(move);
                            }
                            if (foundmove)
                                break;
                        }
                    }
                    if (foundmove)
                        break;
                }
                if (foundmove)
                    break;
            }

            return res;
        }

        /// <summary>
        /// creates better possibility boards
        /// </summary>
        /// <returns></returns>
        public void PossibilityBoardBetter(bool debug = false, bool debug2 = false)
        {


            (PrintMode, (int, int)) repeatance;
            /// use repeatance
            for (int k = 0; k < Board.BOARD_SIZE; k++)
            {
                int debug_num = 1;
                if (debug)
                    if (k != debug_num)
                        continue;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        bool vertical = false;
                        bool horizontal = false;
                        if (debug)
                            System.Console.WriteLine("{0},{1}", i * 3, j * 3);
                        repeatance = CheckBoxForRepeatance(i, j, possibility_boards[k],debug);

                        // update the possibilityboard for next check

                        if (repeatance.Item1 == PrintMode.none)
                            continue;

                        else if(repeatance.Item1 == PrintMode.def)
                        {
                            // add 100% move to list
                            Move move = new Move(repeatance.Item2.Item2, repeatance.Item2.Item1, k + 1);
                            if (debug)
                                move.PrintMove();
                            moves_100.Add(move);

                            vertical = true;
                            horizontal = true;
                        }
                        else if (repeatance.Item1 == PrintMode.horizontal)
                            horizontal = true;
                        else if (repeatance.Item1 == PrintMode.vertical)
                            vertical = true;

                        
                        if (horizontal)
                        {
                            if (debug)
                            {
                                System.Console.WriteLine("horizontal repeatance clearing at row {0} ", repeatance.Item2.Item1);
                                possibility_boards[debug_num].PrintBoard(x: 0, print_mode: PrintMode.select_numbers);
                            }

                            for (int j_in = 0; j_in < Board.BOARD_SIZE; j_in++)
                            {
                                if (j_in / 3 == repeatance.Item2.Item1 / 3)
                                    continue;

                                if (debug)
                                    System.Console.WriteLine("removed 1 at column {0}", j_in);

                                possibility_boards[k].board_data[repeatance.Item2.Item1, j_in] = 0;
                            }

                            if (debug)
                                possibility_boards[debug_num].PrintBoard(x: 0,print_mode:PrintMode.select_numbers);
                        }
                        if (vertical)
                        {
                            if (debug)
                            {
                                System.Console.WriteLine("vertical repeatance clearing at column {0} ", repeatance.Item2.Item2);
                                possibility_boards[debug_num].PrintBoard(x: 0, print_mode: PrintMode.select_numbers);
                            }

                            for (int i_in = 0; i_in < Board.BOARD_SIZE; i_in++)
                            {
                                if (i_in / 3 == repeatance.Item2.Item1 / 3)
                                    continue;
                                possibility_boards[k].board_data[i_in, repeatance.Item2.Item2] = 0;
                            }
                            if (debug)
                                possibility_boards[debug_num].PrintBoard(x: 0, print_mode: PrintMode.select_numbers);
                        }
                    }
                }
            }


            // check next until the end

            // do for all numbers 
            if (debug2)
            {
                for (int i = 0; i < Board.BOARD_SIZE; i++)
                {
                    possibility_boards[i].PrintBoard(x: 0, y: 1, print_mode: PrintMode.dual, highlight_color2: ConsoleColor.DarkGreen);
                }
            }
            
            this.PossibilityBoardCombined();
            //possibility_boards[6].PrintBoard(x: 0,y: 1, print_mode: PrintMode.dual,highlight_color2:ConsoleColor.DarkGreen);
            if (debug2)
            {
                possibility_boards_combined.PrintBoard(x:0,print_mode:PrintMode.select_numbers);
            }
        }


        /// <summary>
        /// checks in box for horizontal or vertical placement if it founds returns the resutl
        /// PrintMode.def for lonelynums
        /// PrintMode.vertical , int for vertical repeatance and the col number of vertical repeatance
        /// PrintMode.horizotnal , int for horizotnal repeatance and the row number of horizotnal repeatance
        /// if it founds more than one repeatance it defaults to none
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public (PrintMode, (int,int)) CheckBoxForRepeatance(int box_i, int box_j, Board board,bool debug = false)
        {
 

            int[] box;
            box = board.BoxArr(box_i * 3, box_j * 3);
            if (debug)
                Board.PrintArr(box, PrintMode.box);


            List<int> ints = new List<int>(); 
            // count numbers and assign them to an arr for later use with their respective positions
            int counter = 0;
            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                int num = box[i];
                if (num == 1)
                {
                    ints.Add(i);
                    counter++;
                }
            }

            int[] int_arr = ints.ToArray();


            // check for lonely number
            if (counter == 1)
            {
                if (debug)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    System.Console.WriteLine("Lonely Number");
                    Console.ResetColor();
                }
                return (PrintMode.def, BoxtoReal(box_i,box_j, int_arr[0],debug));
            }
            else if (counter == 2)
            {
                // check for horizontal
                if(int_arr[0]/3 == int_arr[1] / 3)
                {
                    if (debug)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        System.Console.WriteLine("Horizontal at {0}", int_arr[0]);
                        Console.ResetColor();
                    }
                    return (PrintMode.horizontal, BoxtoReal(box_i, box_j, int_arr[0], debug));
                }
                // check for vertical
                if (int_arr[0] % 3 == int_arr[1] % 3)
                {
                    if (debug)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        System.Console.WriteLine("Vertical at {0}", int_arr[0]);
                        Console.ResetColor();
                    }
                    return (PrintMode.vertical, BoxtoReal(box_i, box_j, int_arr[0],debug));
                }
            }
            else if (counter == 3)
            {
                // check for horizontal
                if (int_arr[0] / 3 == int_arr[1] / 3 && int_arr[0] / 3 == int_arr[2] / 3)
                {
                    if (debug)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        System.Console.WriteLine("Horizontal at {0}", int_arr[0]);
                        Console.ResetColor();
                    }
                    return (PrintMode.horizontal, BoxtoReal(box_i, box_j, int_arr[0],debug));
                }
                // check for vertical
                if (int_arr[0] % 3 == int_arr[1] % 3 && int_arr[0] % 3 == int_arr[2] % 3)
                {
                    if (debug)
                    {
                         Console.ForegroundColor = ConsoleColor.DarkYellow;
                        System.Console.WriteLine("Vertical at {0}", int_arr[0]);
                        Console.ResetColor();
                    }

                    return (PrintMode.vertical, BoxtoReal(box_i, box_j, int_arr[0],debug));
                }
            }


            if (debug)
                System.Console.WriteLine();
                



            return (PrintMode.none, (-1,-1));
        }

        /// <summary>
        /// uses int box value and boxes i, j values to find its original placement
        /// </summary>
        /// <returns></returns>
        public static (int,int) BoxtoReal(int box_i, int box_j, int in_box_number, bool debug = false)
        {
            //int startRow = box_l;
            // in: 0,0 , 3

            // ij0 1 2   yx 0 1 2
            // 0 . . .   0
            // 1 @ . .   1
            // 2 . . .   2

            // out: 1, 0

            if (debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("({0},{1}) {2}", box_i,  box_j,  in_box_number);
                System.Console.WriteLine("({0},{1})",box_i * 3 + in_box_number / 3, box_j * 3 + in_box_number % 3);
                Console.ResetColor();
            }

            return (box_i*3 + in_box_number / 3 , box_j * 3 + in_box_number % 3);
        }

        /// <summary>
        /// g(n) = constant
        /// </summary>
        public int CostFunc() { return 1; }

    }
}