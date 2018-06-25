using System;
namespace Projeto_LP2e
{
    public class Manager
    {
        private readonly GameSetup gs;
        private readonly Render render = new Render();
        public Manager(GameSetup gs)
        {
            this.gs = gs;
        }

        public void Play ()
        {
            World w = new World(gs);
            render.View(w.grid);
        }
    }
}
