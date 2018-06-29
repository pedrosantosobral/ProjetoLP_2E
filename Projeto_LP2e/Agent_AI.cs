using System;
namespace Projeto_LP2e
{
    public class Agent_AI : Agent
    {
        public Agent_AI(Type type, int row, int col, int id)
        {
            Type = type;
            Row = row;
            Col = col;
            Id = id;

        }

        public override void Move(string move)
        {
            move = move.ToLower();

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

        public override string ToString()
        {
            string state = null;
            if (Type == Type.Human)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                state = "\u0426";
            }
            else if (Type == Type.Zombie)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                state = "\u0466";
            }

            return $"{state}{Id:x2}";
        }

	}
}
