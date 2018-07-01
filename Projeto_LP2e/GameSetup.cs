using System;
namespace Projeto_LP2e
{
    /// <summary>
    /// Game setup. Class that recieve the arguments passed on the command line
    /// to setup the game.
    /// </summary>
    [Serializable]
    public class GameSetup
    {
        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row { get; }
        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <value>The col.</value>
        public int Col { get; }
        /// <summary>
        /// Gets the number of humans.
        /// </summary>
        /// <value>The NHumans.</value>
        public int NHumans { get; }
        /// <summary>
        /// Gets the number of zombies.
        /// </summary>
        /// <value>The NZombies.</value>
        public int NZombies { get; }
        /// <summary>
        /// Gets the number of playeble humans.
        /// </summary>
        /// <value>The NPlay humans.</value>
        public int NPlayHumans { get; }
        /// <summary>
        /// Gets the number of playable zombies.
        /// </summary>
        /// <value>The NP lay zombies.</value>
        public int NPlayZombies { get; }
        /// <summary>
        /// Gets the max turns.
        /// </summary>
        /// <value>The max turns.</value>
        public int MaxTurns { get; }
        /// <summary>
        /// Gets or sets the savefile.
        /// </summary>
        /// <value>The savefile.</value>
        public string Savefile { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Projeto_LP2e.GameSetup"/> class.
        /// Construter that recieves the diferent command line  arguments. 
        /// </summary>
        /// <param name="args">Arguments.</param>
        public GameSetup(string[] args)
        {
            //variable to verify wrong or missing command line arguments.
            int verify = 0;

            // go throw the recieved command line argumens two in two.
            for (int i = 0; i < args.Length; i+=2)
            {
                //select the diferent configuration options.
                switch(args[i])
                {
                    case "-x":
                        Row = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 0;
                        break;
                    case "-y":
                        Col = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 1;
                        break;
                    case "-z":
                        NZombies = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 2;
                        break;
                    case "-h":
                        NHumans = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 3;
                        break;
                    case "-H":
                        NPlayHumans = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 4;
                        break;
                    case "-Z":
                        NPlayZombies = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 5;
                        break;
                    case "-t":
                        MaxTurns = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 6;
                        break;
                    case "-s":
                        Savefile = args[i + 1];
                        break;
                    default:
                        throw new ArgumentException($"Invalid Argument ({args[i]})");
                }
            }
            // if verify is diferent than 127(in hexadecimal), give an error
            // message showing the wrong or missing command line aguments.
            if (verify != 0x7f)
            {
                //error that show the wrong or missing command line aguments.
                throw new ArgumentException($"Invalid command line argument ({verify:x}).");
            }
        }
    }
}
