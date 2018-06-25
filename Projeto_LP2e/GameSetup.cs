using System;
namespace Projeto_LP2e
{
    public class GameSetup
    {
        public string X { get; set}
        public string Y { get; set}
        public string NHumans { get; set}
        public string NZombies { get; set}
        public string NPlayHumans { get; set}
        public string NPlayZombies { get; set}
        public string MaxTurns { get; set}

        public GameSetup(string[] args)
        {
            for (int i = 0; i < args.Length; i+=2)
            {
                switch(args[i])
                {
                    case "-x":
                        X = args[i + 1];
                        break;
                    case "-y":
                        Y = args[i + 1];
                        break;
                    case "-z":
                        NZombies = args[i + 1];
                        break;
                    case "-h":
                        NHumans = args[i + 1];
                        break;
                    case "-H":
                        NPlayHumans = args[i + 1];
                        break;
                    case "-Z":
                        NPlayZombies = args[i + 1];
                        break;
                    case "-t":
                        MaxTurns = args[i + 1];
                        break;
                }
            }
        }
    }
}
