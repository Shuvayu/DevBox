<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductReview._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" integrity="sha512-dTfge/zgoMYpP7QbHy4gWMEGsbsdZeCXz7irItjcC3sPUFtf0kuFbDz/ixG7ArTxmDjLXDmezHubeNikyKGVyQ==" crossorigin="anonymous">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" integrity="sha512-K1qjQ+NcF2TYO/eI3M6v8EiNYZfA95pQumfvcVrTHtwQVDG+aHRqLi/ETn2uB+1JqwYqVG3LIvdm9lj6imS/pQ==" crossorigin="anonymous"></script>
    
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.9/css/dataTables.bootstrap.min.css">
    <script src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js" ></script>
    <script src="https://cdn.datatables.net/1.10.9/js/dataTables.bootstrap.min.js"></script>

   <section>
  <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
  <!-- Indicators -->
  <ol class="carousel-indicators">
    <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
  </ol>

  <!-- Wrapper for slides -->
  <div class="carousel-inner" role="listbox">
    <div class="item active">
    <img src="Images/ImageSlider/Product_review1.jpg" />
    </div>
    <div class="item">
    <img src="Images/ImageSlider/Product%20review.jpg" />
    </div>
  </div>


  <!-- Controls -->
  <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
    </section>


</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <section id="aboutSPR">
        <div class="container-fluid">
            <div class="row" id="aboutDiv">
                <div>
                    <%--<h1><span class="label label-primary">The Sentiment Product Reviewer</span></h1>--%>
                    <p id="about">This web application crawls through the data on <a href="http://www.productreview.com.au"><u>www.productreview.com.au</u></a>, downloads all the top comments and analyse these comemts to determine if they are postive or negative in nature.</p>
                </div>
            </div>
        </div>
    </section>

    <script>
        // DataTable Operations.
        // For Machine_Listing
        $(document).ready(function () {
            var tableProduct = $('#ProductReview').DataTable({
                "iDisplayLength": 15,
                "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "scrollY": 250,
                "scrollX": true
            });
        });
    </script>

    <style>
        .row {
            padding: 3px 3px 3px 3px;
        }
        #divResult {
            padding: 5px 5px 5px 5px;
        }
        /* STARTS HERE*/
        .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 1px;
        }

        .table > thead > tr > th {
            padding: 5px;
        }

        .table {
            width: 100%;
        }

            .table > tbody {
                font-family: Calibri;
                font-weight: 500;
                font-size: 0.9em;
                font-style: normal;
            }

                .table > tbody > tr:hover {
                    cursor: pointer;
                }

            .table > thead > tr {
                background-color: #0B74AC;
                color: #fff;
                font-family: Calibri;
                font-weight: 600;
                font-size: 1em;
                font-style: normal;
            }

        .innertable > thead > tr {
            background-color: #F0901A;
            color: #fff;
            font-family: Calibri;
            font-weight: 600;
            font-size: 0.9em;
            font-style: normal;
        }
        #aboutDiv{
            padding-top:2em;
            padding-bottom:2em;
            color:#417afc;
        }
        /*ENDS HERE*/
    </style>

    <div class="row" id="analyseDiv">
        <div class="form-inline">
            <div class="form-group">
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSelectProduct">
                    <asp:ListItem Value="0">Venom Protein</asp:ListItem>
                    <asp:ListItem Value="1">Whey Gold Standard</asp:ListItem>
                    <asp:ListItem Value="2">Vital Protein Pea Protein Isolate</asp:ListItem>
                    <asp:ListItem Value="3">Whey Protein Isolate</asp:ListItem>
                    <asp:ListItem Value="4">Swisse Men’s Ultivite Formula 1</asp:ListItem>
                    <asp:ListItem Value="5">Beagle Telecom</asp:ListItem>
                    <asp:ListItem Value="6">Bigpond</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="tbnGenerateReport" CssClass="btn btn-primary active" runat="server" Text="Analyze" OnClick="tbnGenerateReport_Click" />
            </div>
        </div>
    </div>

    <div id="ProductDetails" runat="server" class="row form-inline">
        <div class="form-group">
            <label for="lblProductName">Product Name:</label>
            <asp:TextBox ID="lblProductName" CssClass="form-control" runat="server" Text="Label" ReadOnly="false"></asp:TextBox>
        </div>
        &nbsp;
        <div class="form-group">
            <label for="lblAverageRatings">Avg Rating</label>
            <asp:TextBox ID="lblAverageRatings" CssClass="form-control" runat="server" Text="Label" ReadOnly="false"></asp:TextBox>
        </div>
        &nbsp;
        <div class="form-group">
            <label for="pwd">Total Rating</label>
            <asp:TextBox ID="lblTotalRating" runat="server" CssClass="form-control" Text="Label" ReadOnly="false"></asp:TextBox>
        </div>
    </div>

    <div id="CommentDetails" runat="server" class="row form-inline">
        <div class="form-group">
            <label for="lblPositiveComments">Positive</label>
            <asp:TextBox ID="lblPositiveComments" CssClass="form-control" runat="server" Text="Label" ReadOnly="false"></asp:TextBox>
        </div>
        &nbsp;
        <div class="form-group">
            <label for="lblNegativeComments">Negative</label>
            <asp:TextBox ID="lblNegativeComments" CssClass="form-control" runat="server" Text="Label" ReadOnly="false"></asp:TextBox>
        </div>
        &nbsp;
        <div class="form-group">
            <label for="lblUndComments">Undetermined</label>
            <asp:TextBox ID="lblUndComments" runat="server" CssClass="form-control" Text="Label" ReadOnly="false"></asp:TextBox>
        </div>
    </div>

    <div class="row" id="divResult" runat="server">
    </div>

</asp:Content>
