using System;
namespace Projeto_LP2e
{
    public class Render
    {
        private readonly GameSetup gs;
        private readonly World w;

        public Render(GameSetup gs, World w)
        {
            this.gs = gs;
            this.w = w;
        }
        public void View(IGameObject[,] grid, int turn)
        {
            string state = null;

            Console.Clear();
            Console.WriteLine($"Actual turn: {turn}");
            Console.WriteLine();
            for (int i = 0; i < grid.GetLength(0);i++)
            {
                for (int j = 0; j < grid.GetLength(1);j++)
                {
                    state = State(grid[i, j]);
                    Console.Write(state);
                    Console.ResetColor();
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

        public void ShowLegend()
        {
            Console.WriteLine("Legend:");
            Console.WriteLine("Human: AI - \u0426 | Playable - \u0429");
            Console.WriteLine("Zombie: AI - \u0466 | Playable - \u04C1");
            Console.WriteLine();
        }

        public void ShowPlayingAgent(Agent ag)
        {
            if(ag is Agent_AI)
            {
                Console.Write($"* Next to Play: ");
                Console.Write($"{ag}");
                Console.ResetColor();
                Console.WriteLine(" (AI Controlled)");
            }
            else
            {
                Console.Write($"* Next to Play: ");
                Console.Write($"{ag}");
                Console.ResetColor();
                Console.WriteLine(" (Player Controlled)");
            }
        }

        public void ShowPossibleDirections(IGameObject go, string dir)
        {
            if(go is Agent_AI)
            {
                Console.WriteLine($"\t- There is a {((Agent_AI)go).Type} " +
                                  $"{((Agent_AI)go).Id:x2} to the {dir} (AI).");
            }
            else if (go is Agent_Play)
            {
                Console.WriteLine($"\t- There is a {((Agent_Play)go).Type} " +
                                  $"{((Agent_Play)go).Id:x2} to the {dir} (Playable).");
            }
            else if (go is Empty)
            {
                Console.WriteLine($"\t- The path is free to the {dir}");
            }
        }

        public void ShowWayToGoQuestion()
        {
            Console.WriteLine("* Which way to go?");
        }

        public void ShowPossibleMovements(char input)
        {
            if (input == 'W') Console.Write(" W (North),");
            if (input == 'A') Console.Write(" A (West),");
            if (input == 'S') Console.Write(" S (South),");
            if (input == 'D') Console.Write(" D (East),");
            Console.WriteLine();
        }

        public void ShowKeyMessage()
        {
            Console.WriteLine("Press any key to continue...");
        }

        public void ShowHumanWinMessage()
        {
            Console.Clear();
            Console.WriteLine("Humans have survived!");
        }

        public void ShowZombieWinMessage()
        {
            Console.Clear();
            Console.WriteLine("Too bad. Zombies took over.");
        }
    }
}
