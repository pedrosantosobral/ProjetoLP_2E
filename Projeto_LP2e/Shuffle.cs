using System;
namespace Projeto_LP2e
{
    /// <summary>
    /// Fisher Yates shuffle.
    /// </summary>
    public class Shuffle
    {
        private Random rand = new Random();

        public void ShuffleAgents(Agent[] agents)
        {
            int n = agents.Length;
            for (int i = 0; i < agents.Length; i++)
            {
                int r = i + rand.Next(n - i);
                Agent t = agents[r];
                agents[r] = agents[i];
                agents[i] = t;
            }
        }
    }
}
