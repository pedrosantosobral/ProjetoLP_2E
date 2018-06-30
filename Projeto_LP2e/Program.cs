using System;
using System.Text;
using System.IO;

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
                if (gs.Savefile != null && File.Exists(gs.Savefile) &&
                    new FileInfo(gs.Savefile).Length != 0)
                {
                    manager.LoadGame();
                }
                manager.Play();
            } 
            catch(Exception e)
            {
                Console.WriteLine("Zombie game had an error: " + e.Message); 
            } 
        }
    }
}
