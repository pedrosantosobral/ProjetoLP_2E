using System;

namespace Projeto_LP2e
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameSetup gs = new GameSetup(args);
                Manager manager = new Manager(gs);
                manager.Play();
            } 
            catch(Exception e)
            {
                Console.WriteLine("Zombie game had an error: " + e.Message); 
            }
        }
    }
}
