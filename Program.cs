namespace SudokuGame
{
    
    public class App
    {
        public static void Main()
        {
            Sudoku su = new Sudoku();
            su.PrintBoard();
            su.LoadBoard();
            System.Console.WriteLine();
            System.Console.WriteLine();
            su.PrintBoard();
        }
        
    }

    public class Sudoku
    {
        public const int BOARD_SIZE = 9; 
        public const string FILE_PATH = "puzzle.txt";
        public int[,] board_data = new int[BOARD_SIZE, BOARD_SIZE];
        
        public int[] row_data;
        public int[] colum_data;
        public int[] block_data;

        public bool is_valid;
        public bool is_complete;

        public Sudoku()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    board_data[i,j] = i;
                }
            }
        }

        public void LoadBoard(string file_path = "puzzle.txt") {
            using (StreamReader reader = new StreamReader(file_path))
            {
                string line;

                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nums = line.Split('-');

                    int j = 0;
                    foreach (string num in nums)
                    {
                        if (num.Contains(' '))
                            board_data[i,j] = 0;
                        else
                            board_data[i,j] = Convert.ToInt32(num);
                        j++;
                    }

                    i++;       
                }
            }
        }
        public void SaveBoard(string name)
        {

        }
        public void PrintBoard() 
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if (i == 3 || i == 6)
                {
                    for (int j = 0; j < BOARD_SIZE + 2; j++)
                        if(j == 3 || j == 7)
                            Console.Write("+ ");
                        else
                            Console.Write("- ");
                    System.Console.WriteLine();
                }
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (j == 3 || j == 6)
                        Console.Write("| ");
                    Console.Write(board_data[i,j] + " ");
                }        
                Console.WriteLine();        
            }
        }
        public int EvalBoard() { return 0;}


        public void FindNextMove() {}

    }
}