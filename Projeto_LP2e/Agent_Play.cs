using System;
namespace Projeto_LP2e
{
    public class Agent_Play : Agent 
    {

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
                Console.ForegroundColor = ConsoleColor.Green;
                state = "\u00B1";
            }
            else if (Type == Type.Zombie)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                state = "\u04C1";
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