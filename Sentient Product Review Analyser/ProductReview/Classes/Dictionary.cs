using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProductReview.Classes;
using ProductReview.Bayesian;

namespace ProductReview.Classes
{
    public class Dictionary
    {

        public Dictionary()
        {

        }

        public DataTable ConvertJsontoDatatableRequired(string[] ColumnsRequired)
        {
            return null;
        }

        public static string ModifyInputString(string Input)
        {
            Input = Input.ToLower();
            string[] ArrInput = Input.Split(' ');
            string[] Output = new string[ArrInput.Length];
            bool NegationFound = false;
            int i = 0;
            foreach (string s in ArrInput)
            {
                string sTemp = "";
                if (NegationFound)
                {
                    sTemp = "NOT_" + s;
                    sTemp = sTemp.Replace("!", "");
                    sTemp = sTemp.Replace("/", "");
                    sTemp = sTemp.Replace("?", "");
                    sTemp = sTemp.Replace(",", "");
                    Output[i] = ReturnStemmer(new Stemmer(), sTemp);
                    i++;
                    continue;
                }
                else
                {
                    if (s.Contains("n't") || s.Equals("not") || s.Equals("no") || s.Equals("never"))
                    {
                        NegationFound = true;
                    }
                }

                sTemp = s.Replace("!", "");
                sTemp = sTemp.Replace("/", "");
                sTemp = sTemp.Replace("?", "");
                sTemp = sTemp.Replace(",", "");
                Output[i] = ReturnStemmer(new Stemmer(), sTemp);
                i++;
            }

            return string.Join(" ", Output);
        }

        private static string ReturnStemmer(IStemmer stemmer, params string[] words)
        {
            string CleanOutput = "";
            foreach (string word in words)
            {
                CleanOutput = stemmer.Stem(word);
            }
            return CleanOutput;
        }

        public static void FillDictionary(ProductReview.Bayesian.Node Positive, ProductReview.Bayesian.Node Negative)
        {
            // train the indexes
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Like")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Love")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Love")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Awesome")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_hate")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("want")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("great")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("best")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Simple")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Simple")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Informative")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Informative")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Well Priced")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Priced")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("fine")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("finest")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_finest")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Perfect")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("good")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EASY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EASY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Never Let Down")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_good")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_like")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Hated")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_want")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_great")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_best")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AFFORDABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AFFORDABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AWESOME")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AWESOME")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AMAZING")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AMAZING")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AWSOME")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AWSOME")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AMAZE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AMAZE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Abnormal")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Convenient")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Convenient")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Abnormal")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Abandoned")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Abandoned")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Accurate")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Accurate")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Acknowledge")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Acknowledge")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Achievement")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Achievement")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ADAPTABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ADAPTABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ADMIRE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ADMIRE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ADVICE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ADVISABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ ADVICE ")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ ADVISABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AFRAID")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AGAINST")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AFRAID")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AGAINST")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ALARMING")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ALARMING")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AMBIGUITY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ AMBIGUITY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ANGRY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ANGRY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ANNOY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ANNOY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("APPRECIATE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_APPRECIATE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("APPROPRIATE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_APPROPRIATE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("AWFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_AWFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BEAUTIFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BEAUTY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BEAUTIFUL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BEAUTY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BENEFICENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BENEFICIAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BENEFICENT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BENEFICIAL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BENEFIT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BENEFIT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BRAVE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BRAVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BRILLIANT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BRILLIANT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BAD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BAD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CAPABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CAPABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CHAMP")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CHAMP")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CONSISTENT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CONSISTENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("USEFUL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_USEFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CLASSIC")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CLASSIC")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("COMFORT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_COMFORT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("COMPLETE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_COMPLETE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CONFIDENCE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CONFIDENCE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CORRECT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CORRECT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("CORRUPT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_CORRUPT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("COURAGE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_COURAGE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DANGER")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DANGER")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DEADLY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DEADLY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DEFFECT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DELICIOUS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DELICIOUS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DELIGHTFUL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DELIGHTFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DEFFECT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DEPRESS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DEPRESS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DISAPPOINT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DISAPPOINT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DISASTER")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DISASTER")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DISASTROUS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DISASTROUS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("DOUBT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_DOUBT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EFFECTIVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EFFICIENT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EFFECTIVE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EFFICIENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ENCOURAGE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ENCOURAGE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EXCITE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EXCITE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EXPENSIVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EXPENSIVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ENERGETIC")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ENERGETIC")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EXCELLENT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EXCELLENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("EXTRAORDINARY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_EXTRAORDINARY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FAIL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FAIL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FALSE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FALSE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FANTASTIC")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FANTASTIC")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FATAL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FATAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FLAWLESS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FLAWLESS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FORWARD")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FORWARD")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FRAUD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FRAUD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FRIENDLY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FRIENDLY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FUSSY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_FUSSY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("GAIN")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_GAIN")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("GREAT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_GREAT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("HAPPY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_HAPPY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("HARMFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_HARMFUL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("BOTHER")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_BOTHER")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("HATE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_HATE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("HEALTHY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_HEALTHY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("HONEST")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_HONEST")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("IDIOT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INEXPERIENCED")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INCOMPITENT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("FRUSTRATING")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INCOMPETENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_IDIOT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("IGNORE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("ILLNESS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_IGNORE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_ILLNESS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INCREDIBLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_INCREDIBLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INJURIOUS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_INJURIOUS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INSUFFICIENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_INSUFFICIENT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("INTERESTED")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_INTERESTED")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("KILL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_KILL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("LIAR")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_LIAR")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("MAD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_MAD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("MARVELOUS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_MARVELOUS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("MISLED")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_MISLED")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("MISTAKE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_MISTAKE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("MOTIVATE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_MOTIVATE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NATURAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_NATURAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NEGATIVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_NEGATIVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NICE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_NICE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PAIN")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PAIN")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PATIENCE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PATIENCE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PERFECT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PERFECT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PLEASE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PLEASE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("POOR")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("STAY AWAY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_POOR")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("POPULAR")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_POPULAR")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PROFIT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PROFIT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PROFESSIONAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("UNPROFESSIONAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("PUNISH")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PUNISH")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("QUIT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_QUIT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("REAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_REAL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("REASONABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_REASONABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("REFINE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_REFINE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("REGRET")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_REGRET")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RELAXATION")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RELAXATION")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RELIABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RELIABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("REMARKABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_REMARKABLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("REPAIR")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_REPAIR")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RESOLVE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RESPECT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RESOLVE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RESPECT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RESTRICT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RESTRICT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RETURN")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RETURN")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RELIABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RELIABLE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("RIDICULOUS")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_RIDICULOUS")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SAD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SAD")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SAFE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SAFE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SATISFY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SATISFY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SATISFACTION")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SAVE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SAVE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SCARE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SCARE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SCREW")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SCREW")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SENSATIONAL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SENSATIONAL")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SENSE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SENSE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SHAME")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SHAME")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SHOCK")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SHOCK")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SHORT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SHORT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SICK")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SICK")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SIGNIFICANT")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SIGNIFICANT")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SMART")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SMART")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SMILE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SMILE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SORRY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("SORRY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_WRONG")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("WRONG")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("WORTH")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_WORTH")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_WORRY")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("WORRY")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_WORSE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("WORSE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("WONDER")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_WONDER")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_VAGUE")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("VAGUE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("USEFUL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_USEFUL")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("UPSET")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_UPSET")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Crap")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Disgusting")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Slow")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Congested")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Terrible")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("Dislike")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_Dislike")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_SLOW")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_PROBLEM")));
            Negative.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("HORRIBLE")));
            Positive.AddContent(Initializer.GetStringsFrom(Dictionary.ModifyInputString("NOT_HORRIBLE")));
        }
    }
}