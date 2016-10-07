using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingUPFRO
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UPFRO Billing";
            // dvBilling.Visible = false;
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // dvBilling.Visible = true;
            dvBilling.ChangeMode(DetailsViewMode.Insert);
            dvBilling.AutoGenerateInsertButton = true;
            dvBilling.DataBind();
        }
        protected void gv_select(object sender, EventArgs e)
        {
            // dvBilling.Visible = true;            
            dvBilling.AutoGenerateInsertButton = false;
            dvBilling.ChangeMode(DetailsViewMode.ReadOnly);
        }

        protected void RefreshGridReview(Object sender, EventArgs e)
        {
            gvAccountInfo.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string accountid = txtAccountID.Text;
            string name = txtName.Text;
            string wherename = "";
            bool firstWhere = false;
           
            if (accountid.Length > 0)
            {
                wherename = "accountid like  '%" + accountid + "%'";
                firstWhere = true;
            }

            if (name.Length > 0)
                if (firstWhere)
                {
                    wherename = wherename +  " and name like  '%" + name + "%'";
                }
                else
                {
                    wherename = "name like  '%" + name + "%'";
                    firstWhere = true;
                }

            if (ddlStatus.SelectedValue != "Select Status")

                if (firstWhere)
                {
                    wherename = wherename + " and status = '" + ddlStatus.SelectedValue + "' ";
                }
                else
                {
                    wherename = " status = '" + ddlStatus.SelectedValue + "' ";
                    firstWhere = true;
                }

            if (ddlState.SelectedValue != "Select State")

                if (firstWhere)
                {
                    if (ddlState.SelectedValue == "XX")
                    {
                        wherename = wherename + " and accountid not in (select accountid  FROM [BILLING].[dbo].[tbl_accountInfo] where state  in ('NJ','NY','CT','PA') ) ";
                    }
                    else
                    {
                        wherename = wherename + " and state = '" + ddlState.SelectedValue + "' ";
                    }
                }
                else
                {
                    if (ddlState.SelectedValue == "XX")
                    {
                        wherename = " accountid not in (select accountid  FROM [BILLING].[dbo].[tbl_accountInfo] where state  in ('NJ','NY','CT','PA') ) ";
                        firstWhere = true;
                    }
                    else
                    {
                        wherename = " state = '" + ddlState.SelectedValue + "' ";
                        firstWhere = true;
                    }
                }
           

            string sql = @"SELECT [accountid]
            ,[name]
            ,[street]
            ,[city]
            ,[state]
            ,[zip]
            ,[contact]
            ,[faxphone]
            ,[email]
            ,[status]
            ,[sendmethod]
            ,[phone]
            ,[zipassigned]
            ,[daysdue]
            ,[duedate]
                FROM [dbo].[tbl_accountInfo] ";

            if (wherename.Length > 0)
            {
                sql = sql + " where " + wherename;
            }
            sql_accountinfo.SelectCommand = sql;
            gvAccountInfo.DataBind();

            //var selectedRow = from q in DB.Menssana_Files_Metadatas
            //                  join p in DB.Menssana_Project_Details on q.ProjectID equals p.ProjectID
            //                  where q.Filename == filename
            //                  select new { q.Filename, q.FileType, q.SiteDevice, q.SiteCollection, q.PID, q.SampleType, q.InUse, p.ProjectName, q.Comments, q.AcetoneRetTime, q.IsopreneRetTime };
            //gvMetadata.DataSource = selectedRow.ToList();

            //gvMetadata.DataBind();

        }
        protected void ddlStatusFilter(object sender, EventArgs e)
        {

        }
        protected void ddlStateFilter(object sender, EventArgs e)
        {

        }

        protected void GvOnRowCommand(Object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gvAccountInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}