using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductReview.Classes;
using ProductReview.Bayesian;
using System.Text;
using System.Data;

namespace ProductReview
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblProductName.Text = TestSentence("I Like it so bad");
            if (!IsPostBack)
            {
                ProductDetails.Visible = false;
                CommentDetails.Visible = false;
            }
        }

        protected void tbnGenerateReport_Click(object sender, EventArgs e)
        {
            DataTable dtProductData = new DataTable();

            var url = ""; 
            
            if (ddlSelectProduct.SelectedValue == "0")
            {
                url = "https://www.kimonolabs.com/api/du33b7qw?apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Venom
            }
            else if (ddlSelectProduct.SelectedValue == "1")
            {
                url = "https://www.kimonolabs.com/api/biu6l4c8?&apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Optimum Whey
            }
            else if (ddlSelectProduct.SelectedValue == "2")
            {
                url = "https://www.kimonolabs.com/api/7qpe5n4g?&apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Pea Protein
            }
            else if (ddlSelectProduct.SelectedValue == "3")
            {
                url = "https://www.kimonolabs.com/api/87yhw5ha?&apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Nutrients Direct Whey Protein
            }
            else if (ddlSelectProduct.SelectedValue == "4")
            {
                url = "https://www.kimonolabs.com/api/adx2qlak?&apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Swisse Men’s Ultivite
            }
            else if (ddlSelectProduct.SelectedValue == "5")
            {
                url = "https://www.kimonolabs.com/api/5161uvtw?&apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Beagle Telecom
            }
            else if (ddlSelectProduct.SelectedValue == "6")
            {
                url = "https://www.kimonolabs.com/api/4t9u4z0y?&apikey=rIUTL1gnwlZf0c0S8aDdLfGpMPGblfhN"; // Bigpond
            }

            dtProductData = Review.GetDataFromURL(url);

            ProductDetails.Visible = true;
            CommentDetails.Visible = true;

            lblProductName.Text = dtProductData.Rows[0]["ProductName"].ToString();
            lblAverageRatings.Text = dtProductData.Rows[0]["AverageRating"].ToString();
            lblTotalRating.Text = dtProductData.Rows[0]["OutofRating"].ToString();

            int PositiveCount = dtProductData.Select("Interpretation = 'Positive'").Length;
            int NegativeCount = dtProductData.Select("Interpretation = 'Negative'").Length;
            int UndCount = dtProductData.Select("Interpretation = 'Undetermined'").Length;
            int TotalCount = dtProductData.Rows.Count;

            lblPositiveComments.Text = PositiveCount + "/" + TotalCount;
            lblNegativeComments.Text = NegativeCount + "/" + TotalCount;
            lblUndComments.Text = UndCount + "/" + TotalCount;
            
            StringBuilder objStringBuilder = RenderGrid(dtProductData);

            divResult.InnerHtml = objStringBuilder.ToString();
        }

        #region Test a Sample String
        private string TestSentence(string Test)
        {
            Node Positive = Node.CreateMemNodes();
            Node Negative = Node.CreateMemNodes();

            Dictionary.FillDictionary(Positive, Negative);

            Algorithm analyzer = new Algorithm(); string Interpretation = "";
            Algorithm.EnumCategory result = analyzer.Categorize(Initializer.GetStringsFrom(Dictionary.ModifyInputString(Test)), Positive, Negative);

            switch (result)
            {
                case Algorithm.EnumCategory.First:
                    Interpretation = "Positive";
                    break;
                case Algorithm.EnumCategory.Undetermined:
                    //MessageBox.Show("Can't say");
                    Interpretation = "Undetermined";
                    break;
                case Algorithm.EnumCategory.Second:
                    Interpretation = "Negative";
                    break;
            }
            return Interpretation;
        }
        #endregion

        #region Render the Data From Analyzer
        private StringBuilder RenderGrid(DataTable dtProductData)
        {
            StringBuilder objStringBuilder = new StringBuilder();
            objStringBuilder.Append("<div class=\"table-responsive\">");

            objStringBuilder.Append("<table id=\"ProductReview\" class=\"table table-responsive table-condensed table-striped table-hover\">");

            // heading row
            objStringBuilder.Append("<thead>");
            objStringBuilder.Append("<tr><th>Product Name</th><th>Average Ratings</th><th>Top Comment</th><th>Interpretation</th><th>Individual Rating</th><th>Out of Rating</th></tr>");
            objStringBuilder.Append("</thead>");
            objStringBuilder.Append("<tbody>");

            foreach (DataRow dataRow in dtProductData.Rows)
            {
                objStringBuilder.Append("<tr>");
                objStringBuilder.Append("<td>" + dataRow["ProductName"].ToString() + "</td>");
                objStringBuilder.Append("<td>" + dataRow["AverageRating"].ToString() + "</td>");
                objStringBuilder.Append("<td>" + dataRow["TopComment"].ToString() + "</td>");
                objStringBuilder.Append("<td>" + dataRow["Interpretation"].ToString() + "</td>");
                objStringBuilder.Append("<td>" + dataRow["IndividualRating"].ToString() + "</td>");
                objStringBuilder.Append("<td>" + dataRow["OutofRating"].ToString() + "</td>");
                objStringBuilder.Append("</tr>");
            }
            objStringBuilder.Append("</tbody>");
            objStringBuilder.Append("</table></div>");
            return objStringBuilder;
        }
        #endregion
    }
}