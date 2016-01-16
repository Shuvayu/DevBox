using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Classes
{
    public class clsJson
    {
        public class Rootobject
        {
            public string name { get; set; }
            public int count { get; set; }
            public string frequency { get; set; }
            public int version { get; set; }
            public bool newdata { get; set; }
            public string lastrunstatus { get; set; }
            public string thisversionstatus { get; set; }
            public string thisversionrun { get; set; }
            public Results results { get; set; }
        }

        public class Results
        {
            public Overall[] Overall { get; set; }
            public Individual[] Individual { get; set; }
        }

        public class Overall
        {
            public string ProductName { get; set; }
            public string AvgRating { get; set; }
            public int index { get; set; }
            public string url { get; set; }
        }

        public class Individual
        {
            public string TopComment { get; set; }
            public string[] IndividualRating { get; set; }
            public int index { get; set; }
            public string url { get; set; }
        }
    }
}