using System;
using System.Text;

namespace Projeto_LP2e
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

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
