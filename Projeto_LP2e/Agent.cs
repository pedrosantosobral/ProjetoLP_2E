using System;
namespace Projeto_LP2e
{
    public abstract class Agent : IGameObject
    {
        public int Id { get; protected set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Type Type { get; set; }

        protected readonly Random rand = new Random();

        public abstract void Move();
    }
}
