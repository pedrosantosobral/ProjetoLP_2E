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
                    ag.Move();
                    render.View(w.grid);
           
                }

                Console.ReadKey();
            }
        }

        public void RestrictPosition()
        {
            
        }
    }
}
