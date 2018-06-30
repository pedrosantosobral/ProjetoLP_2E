using System;
namespace Projeto_LP2e
{
    /// <summary>
    /// Class that creates the game world and populates the grid.
    /// </summary>
    [Serializable]
    public class World
    {
        /// <summary>
        /// The grid.
        /// </summary>
        public IGameObject[,] grid;
        /// <summary>
        /// The agents.
        /// </summary>
        public Agent[] agents;
        /// <summary>
        /// The random generator.
        /// </summary>
        private Random rand = new Random();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Projeto_LP2e.World"/> class.
        /// Constructor that puts the agent in the grid.
        /// </summary>
        /// <param name="gs">Gs.</param>
        public World(GameSetup gs)
        {
            //Creates a new grid.
            grid = new IGameObject[gs.Row , gs.Col];
            //Creates the total agents.
            agents = new Agent[gs.NZombies + gs.NHumans];
            //Calls the function to populate the grid.
            PopulateGrid();
            //Row and column variables.
            int row, col;
            //Variable that count the playable agents.
            int playableCount = 0;

            //Go trough all the human agents.
            for (int i = 0; i < gs.NHumans; i++)
            {
                //while the founded position is an agent, keep searching for an
                //empty position.
                do
                {
                    row = rand.Next(0, gs.Row - 1);
                    col = rand.Next(0, gs.Col - 1);
                }
                while (grid[row, col] is Agent);

                //Creates AI agents of the Human type
                if (playableCount >= gs.NPlayHumans)
                {
                    grid[row, col] = new Agent_AI(Type.Human, row, col, i);
                }
                //Creates playable agents of human type and increments the
                //playableCount variable by one for each agent created.
                else
                {
                    grid[row, col] = new Agent_Play(Type.Human, row, col, i);
                    playableCount++;
                }
            }
            //Sets the playableCount to 0.
            playableCount = 0;

            //Go through all the Zombie agents.
            for (int i = gs.NHumans ; i < gs.NHumans + gs.NZombies; i++)
            {
                //while the founded position is an agent, keep searching for an
                //empty position.
                do
                {
                    row = rand.Next(0, gs.Row - 1);
                    col = rand.Next(0, gs.Col - 1);
                }
                while (grid[row, col] is Agent);

                //Creates AI agents of the Zombie type
                if (playableCount >= gs.NPlayZombies)
                {
                    grid[row, col] = new Agent_AI(Type.Zombie, row, col, i);
                }
                //Creates playable agents of zombie type and increments the
                //playableCount variable by one for each agent created.
                else
                {
                    grid[row, col] = new Agent_Play(Type.Zombie, row, col, i);
                    playableCount++;
                }
            }
            //Call the method to populate array to shuffle.
            PopulateArrayToShuffle();

        }
        /// <summary>
        /// Populates the array to shuffle.
        /// </summary>
        public void PopulateArrayToShuffle()
        {
            //Index variable
            int index = 0;

            //Go through all the game objects in the grid.
            foreach(IGameObject go in grid)
            {
                //If a Agent is found.
                if(go is Agent)
                {
                    //Inserts an agent in the array. 
                    agents[index] = go as Agent;
                    //Increments the index variable by one.
                    index++;
                }
            }
        }
        /// <summary>
        /// Populates the grid with emptys.
        /// </summary>
        public void PopulateGrid()
        {
            //Go through the grid lines.
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                //Go through the grid columns.
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new Empty();  
                }
            }
        }
    }
}
