using System;
namespace Projeto_LP2e
{
    /// <summary>
    /// Agent Playable.
    /// Class that overrides the Move method (from Agent class) to pick a move
    /// option from the Player and contains method with the unicode to be printed
    /// accordingly to the different agent types.
    /// </summary>
    [Serializable]
    public class Agent_Play : Agent 
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:Projeto_LP2e.Agent_Play"/> class.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="id">Identifier.</param>
        public Agent_Play(Type type, int row, int col, int id)
        {
            // Agent type.
            Type = type;
            //Agent row position.
            Row = row;
            //Agent column position.
            Col = col;
            //Agent ID.
            Id = id;

        }

        /// <summary>
        /// Method that overrides the Move method (from Agent class) to pick
        /// a move option from the AI. 
        /// </summary>
        /// <returns>The move.</returns>
        /// <param name="move">Move.</param>
        public override void Move(string move)
        { 
            // swithc to choose one of the four directions.
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
        /// <summary>
        /// Returns a <see cref="T:System.String"/>
        /// that represents the current <see cref="T:Projeto_LP2e.Agent_Play"/>.
        /// Method that contains the
        /// unicode to be printed accordingly to the different agent types 
        /// controlled by the Player.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/>
        /// that represents the current <see cref="T:Projeto_LP2e.Agent_Play"/>.
        /// </returns>
        public override string ToString()
        {
            //variable that initializes the agent state to null. 
            string state = null;

            //see if its a human or a zombie.
            if (Type == Type.Human)
            {
                //Change the console text color to green.
                Console.ForegroundColor = ConsoleColor.Green;
                //variable with human state.
                state = "\u0429";
            }
            else if (Type == Type.Zombie)
            {
                //Change the console text color to red.
                Console.ForegroundColor = ConsoleColor.Red;
                //variable with zombie state.
                state = "\u04C1";
            }
            // return the state and the ID in hexadecimal with 
            //two decimal cases.
            return $"{state}{Id:x2}";
        }
    }
} 