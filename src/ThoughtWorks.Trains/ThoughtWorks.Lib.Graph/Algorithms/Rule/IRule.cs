using System;

namespace ThoughtWorks.Lib.Graph.Algorithms.Rule
{
    public interface IRule<T_Node>
            where T_Node : IEquatable<T_Node>
    {
        bool Applies( ListEdge<T_Node> source, bool isInTarget );

        bool AllowsConcatenation
        {
            get;
        }
    }
}