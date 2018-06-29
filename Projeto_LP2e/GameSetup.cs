﻿using System;
namespace Projeto_LP2e
{
    [Serializable]
    public class GameSetup
    {
        public int Row { get; }
        public int Col { get; }
        public int NHumans { get; }
        public int NZombies { get; }
        public int NPlayHumans { get; }
        public int NPlayZombies { get; }
        public int MaxTurns { get; }
        public string Savefile { get; set; } = null;

        public GameSetup(string[] args)
        {
            int verify = 0;

            for (int i = 0; i < args.Length; i+=2)
            {
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

            if (verify != 0x7f)
            {
                throw new ArgumentException($"Invalid command line argument ({verify:x}).");
            }
        }
    }
}
