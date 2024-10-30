namespace Program
{
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
            // Not yet implemented
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
}