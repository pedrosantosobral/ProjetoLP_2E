﻿using System;
namespace Projeto_LP2e
{
    public class Agent_AI : IGameObject
    {
        public Type Type { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsPlayable { get; } = false;
        public int ID { get; }

        public Agent_AI(Type type, int row, int col, int id)
        {
            Type = type;
            Row = row;
            Col = col;
            ID = id;

        }
    }
}
