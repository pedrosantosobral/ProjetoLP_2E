using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Projeto_LP2e
{
    /// <summary>
    /// Game manager.
    /// </summary>
    [Serializable]
    public class Manager : ISerializable
    {
        /// <summary>
        /// The game setup instance variable.
        /// </summary>
        private readonly GameSetup gs;
        /// <summary>
        /// The Render.
        /// </summary>
        private readonly Render render;
        /// <summary>
        /// The Shuffle.
        /// </summary>
        private readonly Shuffle shuffle = new Shuffle();
        /// <summary>
        /// The World
        /// </summary>
        private World w;
        /// <summary>
        /// The turn counter.
        /// </summary>
        private int turn = 1;
        /// <summary>
        /// The loaded game variable.
        /// </summary>
        private bool loadedGame = false;
        /// <summary>
        /// The number of humans.
        /// </summary>
        private int nhumans;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Projeto_LP2e.Manager"/> class.
        /// </summary>
        /// <param name="gs">Gs.</param>
        public Manager(GameSetup gs)
        {
            this.gs = gs;
            render = new Render(gs, w);
            w = new World(gs);
            //variable that recieves the value of variable NHumans 
            //passed on the comand line arguments
            nhumans = gs.NHumans;
        }
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Projeto_LP2e.Manager"/> class.
        /// Constructor to make save game.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <param name="context">Context.</param>
        public Manager(SerializationInfo info, StreamingContext context)
        {
            w = (World)info.GetValue("World", typeof(World));
            turn = (int)info.GetValue("Turn", typeof(int));
            nhumans = (int)info.GetValue("NHumans", typeof(int));
            loadedGame = true;
        }
        /// <summary>
        /// Gameloop.
        /// </summary>
        public void Play ()
        {
            //Initiate the move variable with null.
            string move = null;
            //Render the game view.
            render.View(w.grid, turn);

            //Go through the turns.
            for (int i = 0; i < gs.MaxTurns; i++)
            {
                if (loadedGame)
                {
                    loadedGame = false;
                }
                else
                {
                    shuffle.ShuffleAgents(w.agents);
                }

                if(gs.Savefile != null && turn > 1)
                {
                    SaveGame();
                }
                //Go through all the agents.
                foreach (Agent ag in w.agents)
                {
                    //variables to save old positions.
                    int oldCol = ag.Col;
                    int oldRow = ag.Row;

                    //Show the legend.
                    render.ShowLegend();
                    //Show playing Agent. 
                    render.ShowPlayingAgent(ag);
                    //Calls the method to check possible directions.
                    CheckPossibleDirections(ag);

                    //If agent is Playable.
                    if (ag is Agent_Play)
                    {
                        //Set move variable to null.
                        move = null;

                        //while the user chooses a string in upercases,
                        //set to lowercases.
                        do
                        {
                            move = Console.ReadLine().ToLower();
                        }
                        while (move != "w" && move != "a" && move != "s" && move != "d");
                        //Move the agent.
                        ag.Move(move);
                    }
                    //If agent is AI controled
                    else
                    {
                        //Calls the function to check nearest agent.
                        CheckNearestAgent(ag);
                    }
                    //Calls the function to restrict position.
                    RestrictPosition(ag);
                    //Place an agent.
                    PlaceAgent(ag, oldCol, oldRow);

                    //Check if there are no more humans on the grid.
                    if (nhumans == 0)
                    {
                        //Show Zombies win message.
                        render.ShowZombieWinMessage();

                        Environment.Exit(1);

                    }
                    //Render the game view.
                    render.View(w.grid, turn);
                }
                //Increments the turn variable by one.
                turn++;
                //Render the game view.
                render.View(w.grid, turn);

                //If the turns are over
                if (turn > gs.MaxTurns)
                {
                    //go throug all the agents in the world.
                    foreach (Agent ag in w.agents)
                    {
                        //if it finds a human
                        if (ag.Type == Type.Human)
                        {
                            //Show Human win message.
                            render.ShowHumanWinMessage();
                            Environment.Exit(1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to restrict the agents positions inside the grid.
        /// </summary>
        /// <param name="ag">Ag.</param>
        public void RestrictPosition(Agent ag)
        {
            // if in an agent is found outside the grid in left side, place the
            // agent in the first column.
            if (ag.Col < 0) ag.Col = 0;
            // if in an agent is found outside the grid in right side, place the
            // agent in the last column.
            if (ag.Col > gs.Col - 1) ag.Col = gs.Col - 1 ;
            // if in an agent is found outside the grid on the top, place the
            // agent in the first row.
            if (ag.Row < 0) ag.Row = 0;
            // if in an agent is found outside the grid on the bottom, place the
            // agent in the last row.
            if (ag.Row > gs.Row - 1) ag.Row = gs.Row - 1;

        }
        /// <summary>
        /// Places the agent in the grid and makes infection.
        /// </summary>
        /// <param name="ag">Ag.</param>
        /// <param name="oldCol">Old col.</param>
        /// <param name="oldRow">Old row.</param>
        public void PlaceAgent(Agent ag, int oldCol, int oldRow)
        {
            //Variables to save the destination coordinates.
            int destRow = ag.Row;
            int destCol = ag.Col;

            //if the destination position is an Agent
            if (w.grid[destRow, destCol] is Agent)
            {
                //Return to old position(stays in the same place)
                ag.Col = oldCol;
                ag.Row = oldRow;

                //If a zombie moves to a place where it founds a human,
                //the human turns into a zombie (infection) 
                if ((ag.Type == Type.Zombie) && (w.grid[destRow, destCol]
                                                 as Agent).Type == Type.Human)
                {
                    (w.grid[destRow, destCol] as Agent).Type = Type.Zombie;
                    nhumans--;
                }
            }
            //places the agent in the new coordinates and add an Empty game object
            //in the old ones.
            else
            {
                w.grid[destRow, destCol] = ag;
                w.grid[oldRow, oldCol] = new Empty();
            }
        }
        /// <summary>
        /// Method that calls the render to show possible directions.
        /// </summary>
        /// <param name="ag">Ag.</param>
        public void CheckPossibleDirections(Agent ag)
        {
            bool[] directions;
            string dir = null;

            if (ag is Agent_Play)
            {
                directions = RetrievePossibleDirections(ag);

                if (directions[0])
                {
                    dir = "North";
                    render.ShowPossibleDirections(w.grid[ag.Row - 1, ag.Col], dir);
                }

                if (directions[1])
                {
                    dir = "West";
                    render.ShowPossibleDirections(w.grid[ag.Row, ag.Col - 1], dir);
                }

                if (directions[2])
                {
                    dir = "South";
                    render.ShowPossibleDirections(w.grid[ag.Row + 1, ag.Col], dir);
                }

                if (directions[3])
                {
                    dir = "East";
                    render.ShowPossibleDirections(w.grid[ag.Row, ag.Col + 1], dir);
                }

                render.ShowWayToGoQuestion();
                CheckPossibleMovements(ag, directions);
            }
            else
            {
                //Show the press any key to continue message.
                render.ShowKeyMessage();
            }

        }
        /// <summary>
        /// Retrieves the possible directions.
        /// </summary>
        /// <returns>The possible directions.</returns>
        /// <param name="ag">Ag.</param>
        public bool[] RetrievePossibleDirections(Agent ag)
        {
            bool[] directions = new bool[4];
            //w (north)
            directions[0] = (ag.Row - 1 >= 0);
            // a (west)
            directions[1] = (ag.Col - 1 >= 0);
            //s (south)
            directions[2] = (ag.Row + 1 < gs.Row);
            // d (east)
            directions[3] = (ag.Col + 1 < gs.Col);

            return directions;
        }
        /// <summary>
        /// Checks the possible movements that the player can do.
        /// </summary>
        /// <param name="ag">Ag.</param>
        /// <param name="directions">Directions.</param>
        public void CheckPossibleMovements(Agent ag, bool[] directions)
        {
            if (directions[0] && (w.grid[ag.Row - 1, ag.Col]) is Empty)
            {
                render.ShowPossibleMovements('W');
            }
            if (directions[1] && (w.grid[ag.Row, ag.Col - 1]) is Empty)
            {
                render.ShowPossibleMovements('A');
            }
            if (directions[2] && (w.grid[ag.Row + 1, ag.Col]) is Empty)
            {
                render.ShowPossibleMovements('S');
            }
            if (directions[3] && (w.grid[ag.Row, ag.Col + 1]) is Empty)
            {
                render.ShowPossibleMovements('D');
            }
        }
        /// <summary>
        /// Checks the nearest agent.
        /// </summary>
        /// <param name="ag">Ag.</param>
        public void CheckNearestAgent(Agent ag)
        {
            //radius variable
            int radius;
            //destination coordinates
            int destRow;
            int destCol;
            int FLAG = 0;
            //max search variable
            int max_search;

            // Find max radius
            if (gs.Row > gs.Col) max_search = gs.Row;
            else max_search = gs.Col;

            // Von Neumann neighborhood
            for (radius = 1; radius <= max_search; radius++)
            {
                if (FLAG == 1) break;
                for (destRow = -radius; destRow <= radius; destRow++)
                {
                    if (FLAG == 1) break;
                    for (destCol = -radius; destCol <= radius; destCol++)
                    {

                        if (Distance_vn(ag.Row, ag.Col, ag.Row + destRow, ag.Col
                                        + destCol) == radius)
                        {
                            int rowCoord = ag.Row + destRow;
                            int colCoord = ag.Col + destCol;

                            // Verification for the bottom. if the position is 
                            //outside the grid set to last row.
                            if (rowCoord >= gs.Row) rowCoord = gs.Row - 1;
                            // Verification for the top. if the position is 
                            //outside the grid set to first row.
                            else if (rowCoord < 0) rowCoord = 0;
                            // Verification for the right side. if the position is 
                            //outside the grid set to last collumn.
                            if (colCoord >= gs.Col) colCoord = gs.Col - 1;
                            // Verification for the left side. if the position is 
                            //outside the grid set to first collumn
                            else if (colCoord < 0) colCoord = 0;

                            if (w.grid[rowCoord, colCoord] is Agent)
                            {
                                if ((ag.Type == Type.Human) &&
                                 ((w.grid[rowCoord, colCoord] as Agent).Type == Type.Zombie))
                                {
                                    Console.WriteLine($"Nearest Zombie ID:" + 
                                     $" {(w.grid[rowCoord, colCoord] as Agent).Id}");
                                    HumanMove(ag as Agent_AI, w.grid[rowCoord, colCoord] as Agent);
                                    FLAG = 1;
                                    break;
                                }
                                else if ((ag.Type == Type.Zombie) &&
                                        ((w.grid[rowCoord, colCoord] as Agent).Type == Type.Human))
                                {
                                    Console.WriteLine($"Nearest Human ID:" +
                                     $" {(w.grid[rowCoord, colCoord] as Agent).Id}");
                                    ZombieMove(ag as Agent_AI, w.grid[rowCoord, colCoord] as Agent);
                                    FLAG = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            Console.ReadKey();
        }

        // Function that returns distance between 2 coordinates
        public int Distance_vn(int row1, int col1, int row2, int col2)
        {
            int rowdist = Math.Abs(row2 - row1);
            int coldist = Math.Abs(col2 - col1);
            int dist = rowdist + coldist;

            return dist;
        }

        /// <summary>
        /// Humans movement.
        /// </summary>
        /// <param name="ag">Ag.</param>
        /// <param name="enemyAg">Enemy ag.</param>
        public void HumanMove(Agent_AI ag, Agent enemyAg)
        {
            if (enemyAg.Row > ag.Row) ag.Move("W");
            else if (enemyAg.Row < ag.Row) ag.Move("S");
            else if (enemyAg.Col > ag.Col) ag.Move("A");
            else if (enemyAg.Col < ag.Col) ag.Move("D");
        }
        /// <summary>
        /// Zombie movement.
        /// </summary>
        /// <param name="ag">Ag.</param>
        /// <param name="enemyAg">Enemy ag.</param>
        public void ZombieMove(Agent_AI ag, Agent enemyAg)
        {
            if (enemyAg.Row > ag.Row) ag.Move("S");
            else if (enemyAg.Row < ag.Row) ag.Move("W");
            else if (enemyAg.Col > ag.Col) ag.Move("D");
            else if (enemyAg.Col < ag.Col) ag.Move("A");
        }
        /// <summary>
        /// Method that saves the game.
        /// </summary>
        public void SaveGame()
        {
            Stream stream = File.Open(gs.Savefile, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, w);
            bf.Serialize(stream, turn);
            bf.Serialize(stream, nhumans);
            stream.Close();
        }
        /// <summary>
        /// Method that loads the game.
        /// </summary>
        public void LoadGame()
        {
            Stream stream = File.Open(gs.Savefile, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            w = (World)bf.Deserialize(stream);
            turn = (int)bf.Deserialize(stream);
            nhumans = (int)bf.Deserialize(stream);
            stream.Close(); 
        }
        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <param name="context">Context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("World", w);
            info.AddValue("Turn", turn);
            info.AddValue("NHumans", nhumans);
        }
    }


}
