using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Bayesian
{
    public class Algorithm
    {
        private float I = 0;
        private float invI = 0;

        public Algorithm()
        {
            this.AllowedRanange = .05f;
        }

        public float AllowedRanange { get; set; }

        public EnumCategory Categorize(Initializer item, Node first, Node second)
        {
            float forecast = GetForecast(item, first, second);

            if (forecast <= .5f - this.AllowedRanange)
                return EnumCategory.Second;

            if (forecast >= .5 + this.AllowedRanange)
                return EnumCategory.First;


            return EnumCategory.Undetermined;
        }

        public float GetForecast(Initializer item, Node first, Node second)
        {
            foreach (string token in item)
            {
                int firstCount = first.GetTotalTokenCount(token);
                int secondCount = second.GetTotalTokenCount(token);

                float probability = CalcProbability(firstCount, first.CountIntializers, secondCount, second.CountIntializers);

                Console.WriteLine("{0}: [{1}] ({2}-{3}), ({4}-{5})",
                    token,
                    probability,
                    firstCount,
                    first.CountIntializers,
                    secondCount,
                    second.CountIntializers);
            }

            float prediction = CombineProbability();
            return prediction;
        }

        #region Private Methods

        /// <summary>
        /// Calculates a probablility value based on statistics from two categories
        /// </summary>
        private float CalcProbability(float cat1count, float cat1total, float cat2count, float cat2total)
        {
            float bw = cat1count / cat1total;
            float gw = cat2count / cat2total;
            float pw = ((bw) / ((bw) + (gw)));
            float
                s = 1f,
                x = .5f,
                n = cat1count + cat2count;
            float fw = ((s * x) + (n * pw)) / (s + n);

            LogProbability(fw);

            return fw;
        }

        private void LogProbability(float prob)
        {
            if (!float.IsNaN(prob))
            {
                I = I == 0 ? prob : I * prob;
                invI = invI == 0 ? (1 - prob) : invI * (1 - prob);
            }
        }

        private float CombineProbability()
        {
            return I / (I + invI);
        }

        #endregion

        #region Enum Categories
        public enum EnumCategory
        {
            First = -1,
            Undetermined = 0,
            Second = 1
        }
        #endregion
    }
}