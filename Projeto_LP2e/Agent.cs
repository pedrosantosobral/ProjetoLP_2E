using System;
namespace Projeto_LP2e
{
    public abstract class Agent : IGameObject
    {
        public int Id { get; protected set; }

        protected readonly Random rand = new Random();

        public abstract void Move();
    }
}
