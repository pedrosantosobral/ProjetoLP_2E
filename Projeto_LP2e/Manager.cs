using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Projeto_LP2e
{
    [Serializable]
    public class Manager : ISerializable
    {
        private readonly GameSetup gs;
        private readonly Render render;
        private readonly Shuffle shuffle = new Shuffle();
        private World w;

        public Manager(GameSetup gs)
        {
            if(File.Exists(gs.Savefile))
            {
                LoadGame();
            }
            this.gs = gs;
            render = new Render(gs, w);
        }

        public Manager(SerializationInfo info, StreamingContext context)
        {
            w = (World)info.GetValue("World", typeof(World));  
        }
        public void Play ()
        {
            w = new World(gs);
            string move = null;
            int turn = 1;
            render.View(w.grid, turn);

            for (int i = 0; i < gs.MaxTurns; i++)
            {
                if(gs.Savefile != null)
                {
                    SaveGame();
                }
                shuffle.ShuffleAgents(w.agents);

                foreach (Agent ag in w.agents)
                {
                    int oldCol = ag.Col;
                    int oldRow = ag.Row;

                    render.ShowLegend();
                    render.ShowPlayingAgent(ag);
                    CheckPossibleDirections(ag);
                    
                    if (ag is Agent_Play)
                    {
                        move = null;

                        do
                        {
                            move = Console.ReadLine().ToLower();
                        }
                        while (move != "w" && move != "a" && move != "s" && move != "d");
                        ag.Move(move);
                    }
                    else
                    {
                        CheckNearestAgent(ag);
                    }

                    RestrictPosition(ag);
                    PlaceAgent(ag, oldCol, oldRow);
                    render.View(w.grid, turn);
                }
                turn++;
                render.View(w.grid, turn);
            }
        }

        public void RestrictPosition(Agent ag)
        {
            if (ag.Col < 0) ag.Col = 0;
            if (ag.Col > gs.Col - 1) ag.Col = gs.Col - 1 ;
            if (ag.Row < 0) ag.Row = 0;
            if (ag.Row > gs.Row - 1) ag.Row = gs.Row - 1;

        }

        public void PlaceAgent(Agent ag, int oldCol, int oldRow)
        {
            int destRow = ag.Row;
            int destCol = ag.Col;

            if (w.grid[destRow, destCol] is Agent)
            {
                ag.Col = oldCol;
                ag.Row = oldRow;

                if ((ag.Type == Type.Zombie) && (w.grid[destRow, destCol]
                                                 as Agent).Type == Type.Human)
                {
                    (w.grid[destRow, destCol] as Agent).Type = Type.Zombie;
                }
            }
            else
            {
                w.grid[destRow, destCol] = ag;
                w.grid[oldRow, oldCol] = new Empty();
            }
        }

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
                render.ShowKeyMessage();
            }

        }

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

        public void CheckNearestAgent(Agent ag)
        {
            int radius;
            int destRow;
            int destCol;
            int FLAG = 0;
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

                        if (Distance_vn(ag.Row, ag.Col, ag.Row + destRow, ag.Col + destCol) == radius)
                        {
                            int rowCoord = ag.Row + destRow;
                            int colCoord = ag.Col + destCol;

                            // Verifications for positions off grid
                            if (rowCoord >= gs.Row) rowCoord = gs.Row - 1;
                            else if (rowCoord < 0) rowCoord = 0;
                            if (colCoord >= gs.Col) colCoord = gs.Col - 1;
                            else if (colCoord < 0) colCoord = 0;

                            if (w.grid[rowCoord, colCoord] is Agent)
                            {
                                if ((ag.Type == Type.Human) && ((w.grid[rowCoord, colCoord] as Agent).Type == Type.Zombie))
                                {
                                    Console.WriteLine($"Nearest Zombie ID: {(w.grid[rowCoord, colCoord] as Agent).Id}");
                                    HumanMove(ag as Agent_AI, w.grid[rowCoord, colCoord] as Agent);
                                    FLAG = 1;
                                    break;
                                }
                                else if ((ag.Type == Type.Zombie) && ((w.grid[rowCoord, colCoord] as Agent).Type == Type.Human))
                                {
                                    Console.WriteLine($"Nearest Human ID: {(w.grid[rowCoord, colCoord] as Agent).Id}");
                                    ZombieMove(ag as Agent_AI, w.grid[rowCoord, colCoord] as Agent);
                                    FLAG = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"{radius}");
            Console.ReadKey();
        }

        /* Function that returns distance between 2 coordinates */
        public int Distance_vn(int row1, int col1, int row2, int col2)
        {
            int rowdist = Math.Abs(row2 - row1);
            int coldist = Math.Abs(col2 - col1);
            int dist = rowdist + coldist;

            return dist;
        }

        public void HumanMove(Agent_AI ag, Agent enemyAg)
        {
            if (enemyAg.Row > ag.Row) ag.Move("W");
            else if (enemyAg.Row < ag.Row) ag.Move("S");
            else if (enemyAg.Col > ag.Col) ag.Move("A");
            else if (enemyAg.Col < ag.Col) ag.Move("D");
        }

        public void ZombieMove(Agent_AI ag, Agent enemyAg)
        {
            if (enemyAg.Row > ag.Row) ag.Move("S");
            else if (enemyAg.Row < ag.Row) ag.Move("W");
            else if (enemyAg.Col > ag.Col) ag.Move("D");
            else if (enemyAg.Col < ag.Col) ag.Move("A");
        }
        public void SaveGame()
        {
            Stream stream = File.Open(gs.Savefile, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, w);
            stream.Close();
        }
        public void LoadGame()
        {
            Stream stream = File.Open(gs.Savefile, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            w = (World)bf.Deserialize(stream);
            stream.Close(); 
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("World", w);
        }
    }


}
