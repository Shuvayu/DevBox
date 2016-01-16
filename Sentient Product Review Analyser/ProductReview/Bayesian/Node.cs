using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Bayesian
{
    public abstract class Node
    {
        // Constructor
        public Node()
        {
            
        }

        // Total Count for initializers
        public abstract int CountIntializers { get;}

        public abstract void AddContent(Initializer collection);

        public virtual void AddContent(params Initializer[] Collections)
        {
            for (int i = 0; i < Collections.Length; i++)
            {
                this.AddContent(Collections[i]);
            }
        }

        public static Node CreateMemNodes()
        {
            return new MemNodes();
        }

        public abstract int GetTotalTokenCount(string token); // Get total token count
    }
}