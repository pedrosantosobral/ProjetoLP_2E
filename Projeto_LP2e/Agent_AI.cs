using System;
namespace Projeto_LP2e
{
    public class Agent_AI : Agent
    {
        public Type Type { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsPlayable { get; } = false;

        public Agent_AI(Type type, int row, int col, int id)
        {
            Type = type;
            Row = row;
            Col = col;
            Id = id;

        }

		public override string ToString()
		{
            string state = null;
            if(Type == Type.Human)
            {
                 state = "h";
            }
            else if(Type == Type.Zombie)
            {
                 state = "z";
            }

            return $"{state}{Id:x2}"; 
		}

        public override void Move()
        {
            int randMove = 0;
            char move =' ';
            randMove = rand.Next(0, 4);

            if (randMove == 0) move = 'w';
            else if (randMove == 1) move = 'a';
            else if (randMove == 2) move = 's';
            else if (randMove == 3) move = 'd';

            switch (move)
            {
                case 'w':
                    Row -= 1;
                    break;
                case 'a':
                    Col -= 1;
                    break;
                case 's':
                    Row += 1;
                    break;
                case 'd':
                    Col += 1;
                    break;
            }
            Console.ReadKey();
        }

	}
}
