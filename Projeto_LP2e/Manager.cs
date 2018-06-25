using System;
namespace Projeto_LP2e
{
    public class Manager
    {
        private readonly GameSetup gs;

        public Manager(GameSetup gs)
        {
            this.gs = gs;
        }

        public void Play ()
        {
            Console.WriteLine(gs.X);
            Console.WriteLine(gs.Y);
            Console.WriteLine(gs.NZombies);
            Console.WriteLine(gs.NHumans);
            Console.WriteLine(gs.NPlayZombies);
            Console.WriteLine(gs.NPlayHumans);
            Console.WriteLine(gs.MaxTurns);

            World w = new World(gs);
        }
    }
}
