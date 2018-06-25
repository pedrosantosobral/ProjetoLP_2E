using System;
namespace Projeto_LP2e
{
    public class World
    {
        private IGameObject[,] grid;
        private IGameObject[] agents;
        private Random rand = new Random();

        public World(GameSetup gs)
        {
            grid = new IGameObject[gs.Row , gs.Col];
            agents = new IGameObject[gs.NZombies + gs.NHumans];
            int row, col;

            for (int i = 0; i < gs.NHumans; i++)
            {
                do
                {
                    row = rand.Next(0, gs.Row - 1);
                    col = rand.Next(0, gs.Col - 1);
                } 
                while (grid[row, col] is Agent_AI  || grid[row, col] is Agent_Play);
            }

        }
        public void PopulateGrid()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    
                }
            }
        }
    }
}
