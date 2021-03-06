﻿using SisoDb.Lambdas.Operators;

namespace SisoDb.Lambdas.Nodes
{
    internal class OperatorNode : INode
    {
        public Operator Operator { get; private set; }

        public OperatorNode(Operator op)
        {
            Operator = op;
        }

        public override string ToString()
        {
            return Operator.ToString();
        }
    }
}