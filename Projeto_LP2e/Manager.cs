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
            for (int i = 0; i < gs.MaxTurns; i++)
            {
                shuffle.ShuffleAgents(w.agents);
                render.View(w.grid);

                foreach(Agent a in w.agents)
                {
                    Console.WriteLine(a.Id);
                }

                Console.ReadKey();
            }
        }
    }
}
