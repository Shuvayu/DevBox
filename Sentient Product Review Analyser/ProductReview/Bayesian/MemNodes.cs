using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Bayesian
{
    internal class MemNodes : Node
    {
        internal NodesTable<string, int> tbl = new NodesTable<string, int>();
        
        // Constructor
        public MemNodes() 
        { 

        }

        // Add Tokenized Collections with count + 1 for existing count and add new token
        public override void AddContent(Initializer collection)
        {
            foreach (string Tkn in collection)
            {
                if (tbl.ContainsKey(Tkn))
                {
                    tbl[Tkn] = tbl[Tkn] + 1;
                }
                else
                {
                    tbl.Add(Tkn, 1);
                }
            }
        }

        public override int CountIntializers
        {
            get
            {
                return tbl.Values.Sum();
            }
        }

        // Calculate the count of tokens
        public override int GetTotalTokenCount(string Tkn)
        {
            if (this.tbl.ContainsKey(Tkn))
                return this.tbl[Tkn];
            else
                return 0;
        }
    }
}