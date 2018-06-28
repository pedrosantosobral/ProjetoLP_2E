using System;
namespace Projeto_LP2e
{
    public class Render
    {
        public void View(IGameObject[,] grid)
        {
            string state = null;
            for (int i = 0; i < grid.GetLength(0);i++)
            {
                for (int j = 0; j < grid.GetLength(1);j++)
                {
                    state = State(grid[i, j]);
                    Console.Write(state);
                    Console.Write("\t");
                }
                Console.Write("\n\n\n");
            }
          
        }
        public string State(IGameObject go)
        {
            string state = null;
            if (go is Empty) state = ".";
            else if (go is Agent_Play) state = $"{((Agent_Play)go)}";
            else if (go is Agent_AI) state = $"{((Agent_AI)go)}";

            return state;
        }

        public void ShowPlayingAgent(Agent ag)
        {
            if(ag is Agent_AI)
            {
                Console.WriteLine($"* Next to Play: {ag} (AI Controlled)");
            }
            else
            {
                Console.WriteLine($"* Next to Play: {ag} (Player Controlled)");
            }
        }
        public void ShowPossibleDirections(Agent ag)
        {
            
        }
    }
}
