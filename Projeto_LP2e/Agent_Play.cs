using System;
namespace Projeto_LP2e
{
    public class Agent_Play : Agent 
    {
        public Type Type { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsPlayable { get; } = true;

        public Agent_Play(Type type, int row, int col, int id)
        {
            Type = type;
            Row = row;
            Col = col;
            Id = id;

        }

        public override string ToString()
        {
            string state = null;
            if (Type == Type.Human)
            {
                state = "H";
            }
            else if (Type == Type.Zombie)
            {
                state = "Z";
            }

            return $"{state}{Id:x2}";
        }

        public override void Move()
        {
            string move = null;

            do
            {
                move = Console.ReadLine().ToLower();
            }
            while (move != "w" && move != "a" && move != "s" && move != "d");
           

            switch (move)
            {
                case "w":
                    Row -= 1;
                    break;
                case "a":
                    Col -= 1;
                    break;
                case "s":
                    Row += 1;
                    break;
                case "d":
                    Col += 1;
                    break;
            }
        }
    }
} 