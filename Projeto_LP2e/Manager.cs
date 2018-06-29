using System;
namespace Projeto_LP2e
{
    public class Manager
    {
        private readonly GameSetup gs;
        private readonly Render render;
        private readonly Shuffle shuffle = new Shuffle();
        private World w;

        public Manager(GameSetup gs)
        {
            this.gs = gs;
            render = new Render(gs, w);
        }

        public void Play ()
        {
            w = new World(gs);
            render.View(w.grid);

            for (int i = 0; i < gs.MaxTurns; i++)
            {
                shuffle.ShuffleAgents(w.agents);

                foreach (Agent ag in w.agents)
                {
                    int oldCol = ag.Col;
                    int oldRow = ag.Row;

                    render.ShowPlayingAgent(ag);
                    CheckPossibleDirections(ag);
                    ag.Move();
                    RestrictPosition(ag);
                    PlaceAgent(ag, oldCol, oldRow);
                    render.View(w.grid);


                }

                Console.ReadKey();
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

            CheckPossibleMovements(ag , directions);

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
    }


}
