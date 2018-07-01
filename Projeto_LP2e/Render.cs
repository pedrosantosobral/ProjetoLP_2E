using System;
namespace Projeto_LP2e
{
    /// <summary>
    /// Render.
    /// Class that prints the entire view of the game.
    /// Includes instructions, agents and messages
    /// </summary>
    public class Render
    {
        /// <summary>
        /// The GameSetup instance variable.
        /// </summary>
        private readonly GameSetup gs;
        /// <summary>
        /// The World instance variable.
        /// </summary>
        private readonly World w;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:Projeto_LP2e.Render"/> class.
        /// Constructor that recives the GameSetup and the World variables.
        /// </summary>
        /// <param name="gs">GameSetup.</param>
        /// <param name="w">World.</param>
        public Render(GameSetup gs, World w)
        {
            this.gs = gs;
            this.w = w;
        }
        /// <summary>
        /// Print the actual turn and the world grid. 
        /// </summary>
        /// <returns>The game visualisation.</returns>
        /// <param name="grid">Grid.</param>
        /// <param name="turn">Turn.</param>
        public void View(IGameObject[,] grid, int turn)
        {
            //set every grid space to null.
            string state = null;
            //Clear the console. 
            Console.Clear();
            //Prints the actual turn.
            Console.WriteLine($"Actual turn: {turn}");
            //Prints a blank line.
            Console.WriteLine();
            //Go trough the world grid lines.
            for (int i = 0; i < grid.GetLength(0);i++)
            {
                //Go trough the world grid columns.
                for (int j = 0; j < grid.GetLength(1);j++)
                {
                    //Saves diferent states in the current position.
                    state = State(grid[i, j]);
                    //Prints the game object State
                    Console.Write(state);
                    //Reset the text color to default.
                    Console.ResetColor();
                    //Prints a tab.
                    Console.Write("\t");
                }
                Console.Write("\n\n\n");
            }
          
        }
        /// <summary>
        /// Contains the diferent agents and empty states.
        /// </summary>
        /// <returns>The state.</returns>
        /// <param name="go">Go.</param>
        public string State(IGameObject go)
        {
            //sets the state to null.
            string state = null;
            //if game object is Empty set state to a dot.
            if (go is Empty) state = ".";
            //if game object is Agent_Play set state to the respective unicode.
            else if (go is Agent_Play) state = $"{((Agent_Play)go)}";
            //if game object is Agent_AI set state to the respective unicode.
            else if (go is Agent_AI) state = $"{((Agent_AI)go)}";

            return state;
        }

        /// <summary>
        /// Prints the legend of the unicode characters.
        /// </summary>
        public void ShowLegend()
        {
            Console.WriteLine("Legend:");
            Console.WriteLine("Human: AI - \u0426 | Playable - \u0429");
            Console.WriteLine("Zombie: AI - \u0466 | Playable - \u04C1");
            Console.WriteLine();
        }
        /// <summary>
        /// Prints the next moving agent and if is AI or Player controlled.
        /// </summary>
        /// <param name="ag">Ag.</param>
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
        /// <summary>
        /// Shows the possible moving directions.
        /// </summary>
        /// <param name="go">go.</param>
        /// <param name="dir">direction.</param>
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
        /// <summary>
        /// Shows the way to go question.
        /// </summary>
        public void ShowWayToGoQuestion()
        {
            Console.WriteLine("* Which way to go?");
        }
        /// <summary>
        /// Shows the possible movements.
        /// </summary>
        /// <param name="input">Input.</param>
        public void ShowPossibleMovements(char input)
        {
            if (input == 'W') Console.Write(" W (North),");
            if (input == 'A') Console.Write(" A (West),");
            if (input == 'S') Console.Write(" S (South),");
            if (input == 'D') Console.Write(" D (East),");
            Console.WriteLine();
        }
        /// <summary>
        /// Shows the "press any key" message to continue the game.
        /// </summary>
        public void ShowKeyMessage()
        {
            Console.WriteLine("Press any key to continue...");
        }
        /// <summary>
        /// Shows the human win message.
        /// </summary>
        public void ShowHumanWinMessage()
        {
            //Clear the console
            Console.Clear();
            Console.WriteLine("Humans have survived!");
        }
        /// <summary>
        /// Shows the zombie win message.
        /// </summary>
        public void ShowZombieWinMessage()
        {
            //Clear the console
            Console.Clear();
            Console.WriteLine("Too bad. Zombies took over.");
        }
    }
}
