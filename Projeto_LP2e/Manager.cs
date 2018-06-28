using System;
namespace Projeto_LP2e
{
    public class Manager
    {
        private readonly GameSetup gs;
        private readonly Render render = new Render();
        private readonly Shuffle shuffle = new Shuffle();
        public Manager(GameSetup gs)
        {
            this.gs = gs;
        }

        public void Play ()
        {
            World w = new World(gs);
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
                    render.View(w.grid);
                }

                Console.ReadKey();
            }
        }

        public void RestrictPosition(Agent ag)
        {
            if (ag.Col < 0) ag.Col = 0;
            if (ag.Col > ag.Col -1) ag.Col = ag.Col - 1 ;
            if (ag.Row < 0) ag.Row = 0;
            if (ag.Row > ag.Row - 1) ag.Row = ag.Row - 1;

        }

        public void PlaceAgent()
        {
        }
    }


}
