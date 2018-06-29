using System;
namespace Projeto_LP2e
{
    [Serializable]
    public class Agent_Play : Agent 
    {

        public Agent_Play(Type type, int row, int col, int id)
        {
            Type = type;
            Row = row;
            Col = col;
            Id = id;

        }

        public override void Move(string move)
        {          
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
                state = "\u0429";
            }
            else if (Type == Type.Zombie)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                state = "\u04C1";
            }

            return $"{state}{Id:x2}";
        }
    }
} 