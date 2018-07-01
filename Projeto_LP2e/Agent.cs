using System;
namespace Projeto_LP2e
{
    /// <summary>
    /// Agent.
    /// Class that contains diferent Agent properties. 
    /// </summary>
    [Serializable]
    public abstract class Agent : IGameObject
    {
        /// <summary>
        /// Gets or sets the Agent ID.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; protected set; }
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row { get; set; }
        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The col.</value>
        public int Col { get; set; }
        /// <summary>
        /// Gets or sets the Agent type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// The Random generator.
        /// </summary>
        protected readonly Random rand = new Random();

        /// <summary>
        /// Method that is going to be used by the Agent_Play and Agent_AI classes .
        /// </summary>
        /// <returns>The move.</returns>
        /// <param name="move">Move.</param>
        public abstract void Move(string move);
    }
}
