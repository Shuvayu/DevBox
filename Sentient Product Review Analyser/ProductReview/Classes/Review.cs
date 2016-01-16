using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProductReview.Bayesian;

namespace ProductReview.Classes
{
    public class Review
    {
        public Review()
        {
            
        }

        public static DataTable GetDataFromURL(string URL)
        {
            // Train Dictionary with positive and negative words

            Node Positive = Node.CreateMemNodes();
            Node Negative = Node.CreateMemNodes();
            Dictionary.FillDictionary(Positive, Negative);

            // Get Data From Webclient
            var url = URL;
            clsSerializer objSerializer = new clsSerializer();
            var objVenomReviewOverall = objSerializer._download_serialized_json_data<clsJson.Rootobject>(url);
            clsJson.Results objRes = new clsJson.Results();
            objRes = objVenomReviewOverall.results;
            clsJson.Overall[] objOverall; clsJson.Individual[] objIndividual;
            objOverall = objRes.Overall; objIndividual = objRes.Individual;

            DataTable dtProductData = new DataTable();
            dtProductData.Columns.Add("ProductName");
            dtProductData.Columns.Add("AverageRating");
            dtProductData.Columns.Add("TopComment");
            dtProductData.Columns.Add("Interpretation");
            dtProductData.Columns.Add("IndividualRating");
            dtProductData.Columns.Add("OutofRating");

            for (int i = 0; i < objIndividual.Length; i++)
            {
                string ProductName = objOverall[0].ProductName;
                string AvgRating = objOverall[0].AvgRating;
                string TopComment = objIndividual[i].TopComment;
                string IndividualRating = objIndividual[i].IndividualRating[0];
                string TopCommentModified = Dictionary.ModifyInputString(TopComment);
                string OutofRating = objIndividual[i].IndividualRating[1];

                Algorithm analyzer = new Algorithm(); string Interpretation = "";
                Algorithm.EnumCategory result = analyzer.Categorize(Initializer.GetStringsFrom(TopCommentModified), Positive, Negative);

                switch (result)
                {
                    case Algorithm.EnumCategory.First:
                        Interpretation = "Positive";
                        break;
                    case Algorithm.EnumCategory.Undetermined:
                        Interpretation = "Undetermined";
                        break;
                    case Algorithm.EnumCategory.Second:
                        Interpretation = "Negative";
                        break;
                }

                dtProductData.Rows.Add(new string[] { ProductName, AvgRating, TopComment, Interpretation, IndividualRating, OutofRating });
            }
            return dtProductData;
        }
    }
}