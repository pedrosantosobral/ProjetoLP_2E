using System;
namespace Projeto_LP2e
{
    public class GameSetup
    {
        public string X { get; }
        public string Y { get; }
        public string NHumans { get; }
        public string NZombies { get; }
        public string NPlayHumans { get; }
        public string NPlayZombies { get; }
        public string MaxTurns { get; }

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
