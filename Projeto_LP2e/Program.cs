using System;

namespace Projeto_LP2e
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSetup gs = new GameSetup(args);
            Manager manager = new Manager(gs);
            manager.Play();
        }
    }
}
