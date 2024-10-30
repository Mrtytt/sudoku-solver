namespace Program
{
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