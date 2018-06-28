using System;
namespace Projeto_LP2e
{
    public class Manager
    {
        private readonly GameSetup gs;
        private readonly Render render = new Render();
        private readonly Shuffle shuffle = new Shuffle();
        private World w;

        public Manager(GameSetup gs)
        {
            this.gs = gs;
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
    }


}
