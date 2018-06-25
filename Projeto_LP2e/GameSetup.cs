using System;
namespace Projeto_LP2e
{
    public class GameSetup
    {
        public int X { get; } = -1;
        public int Y { get; }
        public int NHumans { get; }
        public int NZombies { get; }
        public int NPlayHumans { get; }
        public int NPlayZombies { get; }
        public int MaxTurns { get; }

        public GameSetup(string[] args)
        {
            int verify = 0;

            for (int i = 0; i < args.Length; i+=2)
            {
                switch(args[i])
                {
                    case "-x":
                        X = Convert.ToInt32(args[i + 1]);
                        verify += 1 << 0;
                        break;
                    case "-y":
                        Y = Convert.ToInt32(args[i + 1]);
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
                }
            }

            if (verify != 0x7f)
            {
                throw new ArgumentException($"Invalid command line argument ({verify:x}).");
            }
        }
    }
}
