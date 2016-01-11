using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Configuration;
using EnetApplication.DAL;
using DevExpress.Web.ASPxGridView;

namespace EnetApplication
{
    public partial class AuditPackage : System.Web.UI.Page
    {
        #region Variables
        string constr = ConfigurationManager.ConnectionStrings["dbEnetConnectionString"].ToString();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["DistributionCenterID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                MakeAccessible(gvAudit);
                LoadGrid();
            }
        }

        #region DAL GetAuditDetails
        protected DataTable GetAuditInfo(string DistributionCenterID)
        {
            string[] ColumnNames = new string[1];
            SqlDbType[] DataType = new SqlDbType[1];
            Object[] Values = new object[1];
            Database da = new Database(constr);

            ColumnNames[0] = "DistributionCenterID";
            DataType[0] = SqlDbType.BigInt;
            Values[0] = DistributionCenterID;

            DataSet dsRateCard = new DataSet();

            dsRateCard = da.SelectRecords("usp_tblPackageSelectDistribution", ColumnNames, Values, DataType);
            return dsRateCard.Tables[0];
        }
        #endregion GetAuditDetails

        protected void gvCurrentLocItems_OnHtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "ExpiryDate")
            {
                var cellvalue = e.CellValue;
                if (Convert.ToDateTime(e.CellValue) <= DateTime.Now.AddDays(7))
                {
                    e.Cell.BackColor = Color.Orange;
                    e.Cell.ForeColor = Color.White;
                }
                if ((DateTime)e.CellValue <= DateTime.Now)
                {
                    e.Cell.BackColor = Color.Red;
                    e.Cell.ForeColor = Color.White;
                }
            }
        }


        #region PageIndex Changing Event
        protected void gvAudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAudit.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        #endregion

        #region Load Grid Data
        private void LoadGrid()
        {
            string CurrDistrID = ((int)Session["DistributionCenterID"]).ToString();
            DataTable dtAudit = GetAuditInfo(CurrDistrID);
            gvAudit.DataSource = dtAudit;
            gvAudit.DataBind();
            ViewState["gvAudit"] = dtAudit;
        }
        #endregion

        #region Row Databound
        protected void gvAudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ExpiryDate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ExpiryDate"));
                if (Convert.ToDateTime(ExpiryDate) <= DateTime.Now.AddDays(7))
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
                if (Convert.ToDateTime(ExpiryDate) <= DateTime.Now)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
        #endregion

        private void MakeAccessible(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute 
                grid.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. Remove if you don't have a footer row 
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        #region Button Start Audit Clicking
        protected void btnStartAuditing_Click(object sender, EventArgs e)
        {
            if (btnStartAuditing.Text == "Start Auditing")
            {
                btnStartAuditing.Text = "Finish Auditing";
                lblBarcodeText.Visible = true;
                txtBarcode.Visible = true;
            }
            else
            {
                RemoveCheckedEntries();
                btnStartAuditing.Text = "Start Auditing";
            }
        }
        #endregion

        #region Barcode Text Changed Event on Textbox
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text.Length == 10)
            {
                int chkCount = 0;
                foreach (GridViewRow row in gvAudit.Rows)
                {
                    string tempBarcode = ((Label)row.FindControl("lblBarcode")).Text;
                    if (txtBarcode.Text == tempBarcode)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        chkSelect.Checked = true;
                        chkCount++;
                    }
                }
                if (chkCount == 0)
                {
                    BAL.Package objPakcageBal = new BAL.Package();
                    var objPackage = objPakcageBal.Get(txtBarcode.Text);
                    BAL.PackageTransactions objBal = new BAL.PackageTransactions();
                    var objPackageTransaction = objBal.GetAll().First(x => x.BarcodeId == txtBarcode.Text);
                    if (objPackageTransaction == null)
                    {
                        pnlSuccess.Visible = true;
                        lblError.Text = "No Such Package Exists";
                        return;
                    }
                    int setStatus = SetPackageStatus(objPackage.BarcodeId, "5");
                    objPackageTransaction.ReceivedBy = Session["User"].ToString();
                    objPackageTransaction.ToLocId = int.Parse(Session["DistributionCenterId"].ToString());
                    objBal.Update(objPackageTransaction);
                    objPackage.CurrentLocationId = int.Parse(Session["DistributionCenterId"].ToString());
                    objPakcageBal.UpdatePackage(objPackage);
                    if (setStatus != 0)
                    {
                        pnlSuccess.Visible = true;
                        lblError.Text = "Error in Updating Status";
                        return;
                    }
                }

                txtBarcode.Text = "";
            }
        }
        #endregion

        #region Datatable Compare
        protected void RemoveCheckedEntries()
        {
            DataTable dtAudit;
            if (ViewState["gvAudit"] != null)
                dtAudit = (DataTable)ViewState["gvAudit"];
            else
            {
                dtAudit = null;
                return;
            }
            for (int i = gvAudit.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow row = gvAudit.Rows[i];
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    DataRow dr = dtAudit.Rows[i];
                    dr.Delete();
                }
            }
            gvAudit.DataSource = dtAudit;
            gvAudit.DataBind();

            // Keep the rows selected
            foreach (GridViewRow row in gvAudit.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }
        #endregion

        #region DAL Update Status as Lost
        protected int SetPackageStatus(string BarcodeID, string StatusID)
        {
            string[] ColumnNames = new string[2];
            SqlDbType[] DataType = new SqlDbType[2];
            Object[] Values = new object[2];
            Database da = new Database(constr);

            ColumnNames[0] = "BarcodeID";
            DataType[0] = SqlDbType.VarChar;
            Values[0] = BarcodeID;

            ColumnNames[1] = "StatusID";
            DataType[1] = SqlDbType.Int;
            Values[1] = StatusID;

            bool res = da.UpdateData("usp_setpackagestatus", ColumnNames, DataType, Values);
            if (res)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region Button Lost Click
        protected void btnLost_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvAudit.Rows)
            {
                string tempBarcode = ((Label)row.FindControl("lblBarcode")).Text;
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    int setStatus = SetPackageStatus(tempBarcode, "3");
                    if (setStatus != 0)
                    {
                        pnlSuccess.Visible = true;
                        lblError.Text = "Error in Updating Status";
                        return;
                    }
                }
            }
            Response.Redirect("AuditPackage.aspx");
        }
        #endregion
    }
}