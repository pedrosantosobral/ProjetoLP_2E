using System;
namespace Projeto_LP2e
{
    public class World
    {
        public IGameObject[,] grid;
        public Agent[] agents;
        private Random rand = new Random();

        public World(GameSetup gs)
        {
            grid = new IGameObject[gs.Row , gs.Col];
            agents = new Agent[gs.NZombies + gs.NHumans];
            PopulateGrid();
            int row, col;
            int playableCount = 0;

            for (int i = 0; i < gs.NHumans; i++)
            {
                do
                {
                    row = rand.Next(0, gs.Row - 1);
                    col = rand.Next(0, gs.Col - 1);
                }
                while (grid[row, col] is Agent);

                if (playableCount >= gs.NPlayHumans)
                {
                    grid[row, col] = new Agent_AI(Type.Human, row, col, i);
                }
                else
                {
                    grid[row, col] = new Agent_Play(Type.Human, row, col, i);
                }
            }

            for (int i = gs.NHumans ; i < gs.NHumans + gs.NZombies; i++)
            {
                do
                {
                    row = rand.Next(0, gs.Row - 1);
                    col = rand.Next(0, gs.Col - 1);
                }
                while (grid[row, col] is Agent);

                if (playableCount >= gs.NPlayZombies)
                {
                    grid[row, col] = new Agent_AI(Type.Zombie, row, col, i);
                }
                else
                {
                    grid[row, col] = new Agent_Play(Type.Zombie, row, col, i);
                }
            }
            PopulateArrayToShuffle();

        }
        public void PopulateArrayToShuffle()
        {
            int index = 0;
            foreach(IGameObject go in grid)
            {
                if(go is Agent)
                {
                    agents[index] = go as Agent;
                    index++;
                }
            }
        }
        public void PopulateGrid()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new Empty();  
                }
            }
        }
    }
}
