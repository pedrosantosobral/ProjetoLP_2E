using System;
using System.Text;
using System.IO;

namespace Projeto_LP2e
{
    /// <summary>
    /// Class Program.
    /// The class that starts the game.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts
        /// and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            //For unicode support.
            Console.OutputEncoding = Encoding.UTF8;

            //To trow an error if the program dont run properly.
            try
            {
                //Creates a new game setup.
                GameSetup gs = new GameSetup(args);
                //Creates a new game Manager.
                Manager manager = new Manager(gs);
                //Checks if there is a saved game file avaliable.
                if (gs.Savefile != null && File.Exists(gs.Savefile) &&
                    new FileInfo(gs.Savefile).Length != 0)
                {
                    //loads the saved game
                    manager.LoadGame();
                }
                //Starts the game.
                manager.Play();
            } 
            catch(Exception e)
            {
                Console.WriteLine("Zombie game had an error: " + e.Message); 
            } 
        }
    }
}
