namespace Program
{
    public class Solver
    {
        public Fringe fringe = new Fringe();
        
        public bool isSolved = false;

        public void GenerateSuccessor()
        {
            State bestState = fringe.SelectLowestCost();
            if (Sudoku.IsSolved(bestState.board))
            {
                
            }

            // get the current best board
            // check if its solved
            // create all possible moves and add them to fringe
        }

        public void Solve()
        {
            //while is not solved 
            //      generatesuccessor()
        }
    }

    public class State
    {
        public int cost;

        public Board board;

        public Move move;

        public State(int cost, Board board, Move move)
        {
            this.cost = cost;
            this.board = board;
            this.move = move;
        }
    }

    public class Fringe
    {
        public List<State> fringe = new List<State>();

        public void AddState(State state)
        {
            fringe.Add(state);
        }

        /// <summary>
        /// returns the lowest cost state to expand
        /// </summary>
        /// <returns></returns>
        public State SelectLowestCost()
        {
            State res = fringe[0];
            int cost = fringe[0].cost;
            int index = 0;

            for (int i = 1; i < fringe.Count; i++)
            {
               if (fringe[i].cost < cost)
               {
                    res = fringe[i];
                    index = i;
               }
            }

            fringe.RemoveAt(index);
            return res;
        }
    }
    
}