using System;
using System.Reflection.Metadata;

namespace Program
{

    /// <summary>
    /// a board with calculation mechanisms in it (?)
    /// </summary>
    public class Sudoku
    {
        public const int MORALE_BOOST = 2;
        public const int CONSTANT_COST = 5*5;

        public Board board;

        public Board[] possibility_boards = new Board[Board.BOARD_SIZE];

        public Board possibility_boards_combined;

        public Move obvious_move = new Move();


        public int EmptyCellCount;
        public int EmptyCellCountSquared;

        public Sudoku(Board board)
        {
            this.board = board;
            CalcEmptyCellCount();
            PossibilityBoardBetter();
        }

        public int Squared(int val)
        {
            return val * val;
        }
        public void CalcEmptyCellCount()
        {
            int counter = 0;
            for (int i = 0; i < Board.BOARD_SIZE; i++)
               for (int j = 0; j < Board.BOARD_SIZE; j++)
                    if (board.board_data[i,j] == 0)
                        counter++;
            EmptyCellCount = counter;
            EmptyCellCountSquared = counter*counter;
        }

        /// <summary>
        /// f(n) = g(n) + h(n)
        /// </summary>
        public int EvalFunction(int i, int j, int num)
        {
            return CostFunc() + HeuristicFunc(i, j, num);
        }

        /// <summary>
        /// g(n) = constant
        /// </summary>
        public int CostFunc() { return CONSTANT_COST; }

        /// <summary>
        ///  h(n)
        /// </summary>
        public int HeuristicFunc(int i,int j, int num)
        {
            // if move is 100
            //      h(n) = empty cell count^2 - full cell count^2 - morale
            // else
            //      h(n) = possibility of the moves that can be placed there^2 + empty cell count^2


            if (obvious_move.isSame(i,j,num))
                return EmptyCellCountSquared - Squared(Board.BOARD_SIZE) - MORALE_BOOST;
            else
                return Squared(possibility_boards[num-1].board_data[i,j]) + EmptyCellCountSquared;

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
                    if (possibility_boards_combined.board_data[i, j] == 1)
                    {
                        for (int k = 0; k < Board.BOARD_SIZE; k++)
                        {
                            if (possibility_boards[k].board_data[i, j] == 1)
                            {
                                foundmove = true;
                                Move move = new Move(j, i, k + 1);
                                System.Console.WriteLine("ADDING MOVE i{0} j{1} k{2} ", i, j, k + 1);
                                move.PrintMove();
                                obvious_move = move;
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

            PossibilityBoardCombined();

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
                        repeatance = CheckBoxForRepeatance(i, j, possibility_boards[k], debug);

                        // update the possibilityboard for next check

                        if (repeatance.Item1 == PrintMode.none)
                            continue;

                        else if (repeatance.Item1 == PrintMode.def)
                        {
                            // add 100% move to list
                            Move move = new Move(repeatance.Item2.Item2, repeatance.Item2.Item1, k + 1);
                            if (debug)
                                move.PrintMove();
                            obvious_move = move;

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
                                possibility_boards[debug_num].PrintBoard(x: 0, print_mode: PrintMode.select_numbers);
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
                possibility_boards_combined.PrintBoard(x: 0, print_mode: PrintMode.select_numbers);
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
        public (PrintMode, (int, int)) CheckBoxForRepeatance(int box_i, int box_j, Board board, bool debug = false)
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
                return (PrintMode.def, BoxtoReal(box_i, box_j, int_arr[0], debug));
            }
            else if (counter == 2)
            {
                // check for horizontal
                if (int_arr[0] / 3 == int_arr[1] / 3)
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
                    return (PrintMode.vertical, BoxtoReal(box_i, box_j, int_arr[0], debug));
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
                    return (PrintMode.horizontal, BoxtoReal(box_i, box_j, int_arr[0], debug));
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

                    return (PrintMode.vertical, BoxtoReal(box_i, box_j, int_arr[0], debug));
                }
            }


            if (debug)
                System.Console.WriteLine();




            return (PrintMode.none, (-1, -1));
        }

        /// <summary>
        /// uses int box value and boxes i, j values to find its original placement
        /// </summary>
        /// <returns></returns>
        public static (int, int) BoxtoReal(int box_i, int box_j, int in_box_number, bool debug = false)
        {
            // int startRow = box_l;
            // in: 0,0 , 3

            // out: 1, 0

            // PIN: COME HERE IF YOU FORGET ARRANGEMENTS

            // ij0 1 2   yx 0 1 2
            // 0 . . .   0
            // 1 @ . .   1
            // 2 . . .   2

            if (debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("({0},{1}) {2}", box_i, box_j, in_box_number);
                System.Console.WriteLine("({0},{1})", box_i * 3 + in_box_number / 3, box_j * 3 + in_box_number % 3);
                Console.ResetColor();
            }

            return (box_i * 3 + in_box_number / 3, box_j * 3 + in_box_number % 3);
        }

        public static bool IsSolved(Board board)
        {
            
            return false;
        }

    }
}