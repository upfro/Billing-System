using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace BillingUPFRO
{
    public partial class Import : System.Web.UI.Page
    {
        public class Import304
        {
            public string policyno { get; set; }
            public string m1 { get; set; }
            public string m2 { get; set; }
            public string m3 { get; set; }
            public string insuredlname { get; set; }
            public string insuredfname { get; set; }
            public string address1 { get; set; }
            public string city1 { get; set; }
            public string state1 { get; set; }
            public string zip1 { get; set; }
            public string m4 { get; set; }
            public string m5 { get; set; }
            public string coverageA { get; set; }
            public string address2 { get; set; }
            public string city2 { get; set; }
            public string state2 { get; set; }
            public string zip2 { get; set; }
            public string agency { get; set; }
            public string m6 { get; set; }
            public string agentsphone { get; set; }
            public bool duplicatePolicyNumber = false;
        }
       // policyno, m1, m2, m3, insuredlname, insuredfname, address1, city1, state1, zip1, m4, m5, coverageA, address2, city2, state2, zip2, agency, m6, agentsphone

        public class DuplicatePolicyNumber
        {
            public string policyno { get; set; }
        }

        public List<Import304> globalImport304 = null;
        public string AccountNumber = string.Empty;
        public List<DuplicatePolicyNumber> GlobalDupPolicy = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountNumber = txtAccountNumber.Text.ToString();
        }

        protected void DDLImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLImport.SelectedIndex != -1 && DDLImport.SelectedIndex != 0)
            {
                string selectedVal = DDLImport.SelectedItem.Value.ToString();
                if (selectedVal == "304")
                {
                    pnl340.Visible = true;
                    pnlImport.Visible = false;
                    return;
                }
                else {
                    pnl340.Visible = false;
                    pnlImport.Visible = true;
                    return;
                }
                               
            }
            pnl340.Visible = false;

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            //Upload and save the file
           
            //string csvPath = Server.MapPath("~/Files/") + txtFileName.Text.ToString();
            //string csvPath = txtFileName.Text.ToString();
            //string csvPath = fLoad.PostedFile.FileName.ToString();
            string csvPath = "//UPFROSQL01/Data2/Imports/" + fLoad.FileName;
            string csvData = File.ReadAllText(csvPath);

            List<Import304> lstImport304 = new List<Import304>();

            foreach (string row in csvData.Split('\n'))
            {
                Import304 currImportRow = null;
                if (!string.IsNullOrEmpty(row))
                {
                 
                    string[] split = row.Split(',');

                    #region  Import304
                    //check if policy number is NOT NULL
                    if (!string.IsNullOrEmpty(split[0].ToString()))
                    {
                        currImportRow = new Import304();
                        currImportRow.policyno = split[0];
                        currImportRow.m1 = split[1];
                        currImportRow.m2 = split[2];
                        currImportRow.m3 = split[3];
                        currImportRow.insuredlname = split[4];
                        currImportRow.insuredfname = split[5];
                        currImportRow.address1 = split[6];
                        currImportRow.city1 = split[7];
                        currImportRow.state1 = split[8];

                        string chkZip1 = split[9];
                        if (chkZip1 != string.Empty)
                        {
                            currImportRow.zip1 = chkZip1.Substring(0, 5);
                        }
                        else
                        {
                            currImportRow.zip1 = split[9];
                        }

                        currImportRow.m4 = split[10];
                        currImportRow.m5 = split[11];
                        currImportRow.coverageA = split[12];
                        currImportRow.address2 = split[13];
                        currImportRow.city2 = split[14];
                        currImportRow.state2 = split[15];

                        //currImportRow.zip2 = split[16];
                        string chkZip2 = split[16];
                        if (chkZip2 != string.Empty)
                        {
                            currImportRow.zip2 = chkZip2.Substring(0, 5);
                        }
                        else
                        {
                            currImportRow.zip2 = split[16];
                        }

                        //currImportRow.agency = split[17];
                        string chkAgency = split[17];
                        if (chkAgency != string.Empty)
                        {
                            currImportRow.agency = chkAgency.Substring(0, 20);
                        }
                        else
                        {
                            currImportRow.agency = split[17];
                        }

                        currImportRow.m6 = split[18];
                        currImportRow.agentsphone = split[19];

                    #endregion Import304

                        lstImport304.Add(currImportRow);
                    }
                }
            }
            globalImport304 = lstImport304;

            //check duplicate ploicy number
            bool chkBoolDup = false;
            chkBoolDup = CheckForDuplicate();
            //if (chkBoolDup)
            //{
            //    return;
            //}

            if (GlobalDupPolicy.Count > 0)
            {
                string strDup = string.Empty;

                foreach (DuplicatePolicyNumber lst in GlobalDupPolicy)
                {
                    strDup += lst.policyno + ",";
                    
                }
               // Response.Write("<script>alert('Record Exists For Policy Number : " + strDup + "');</script>");
               
            }

            //check if any rows exists to insert in Database
            int RowsCount = (from n in globalImport304
                                    where n.duplicatePolicyNumber == false
                                    select n).Count();

            if (RowsCount <= 0)
            {
                Response.Write("<script>alert('No Data to Import');</script>");
                return;
            }

            DataTable dtTblHeader = getTblHeader();
            DataTable dtTblHome = getTblHome();
            DataTable dtTblAssignedReports = getTblAssignedReports();

            bool checkBulkStatus = true;

            checkBulkStatus = BulkCopy(dtTblHeader, dtTblHome, dtTblAssignedReports);

            if (checkBulkStatus == false)
            {
                Response.Write("<script>alert('Failed Transaction');</script>");
            }
            else
            {
                Response.Write("<script>alert('Successfull Transaction');</script>");
                txtAccountNumber.Text = string.Empty;
               // txtFileName.Text = string.Empty;
               // fLoad.FileName. = string.Empty;
            }
           
        }

        //check if policy number exists in tbl_header table
        public bool CheckForDuplicate()
        {
           string policyNumber = string.Empty;
           var connString = ConfigurationManager.ConnectionStrings["BillingConnectionString"].ConnectionString;
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                GlobalDupPolicy = new List<DuplicatePolicyNumber>();
                DuplicatePolicyNumber dpnumber = null;
                foreach (Import304 list in globalImport304)
                {
                   policyNumber= list.policyno;
                  

                    var cmd = new SqlCommand("select COUNT(*)  from tbl_header where policynumber = '" + policyNumber + "'", conn);
                    Int32 count = (Int32)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        //update the global list where the policy number is duplicate
                        globalImport304.First(d => d.policyno == policyNumber).duplicatePolicyNumber = true;

                        //Response.Write("<script>alert('Duplicate Found: " + policyNumber + "');</script>");
                        //return true; 
                        dpnumber = new DuplicatePolicyNumber();
                        dpnumber.policyno = policyNumber;
                        GlobalDupPolicy.Add(dpnumber);
                    } 
                    cmd.Dispose();
                    
                    policyNumber = string.Empty;
                }
                conn.Close();
                return false;
            }
        }

        //check if policy number exists in tbl_header table
        public string  GetInspectorID(string zip1)
        {
            string strInspectorID = string.Empty;
            var connString = ConfigurationManager.ConnectionStrings["BillingConnectionString"].ConnectionString;
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var cmd = new SqlCommand("select * from tbl_zipassigned where zip ='" + zip1 + "'", conn);
                var reader = cmd.ExecuteReader();
                    
                while (reader.Read())
                {
                    strInspectorID = reader["inspectorid"].ToString();
                }
                reader.Close();
                cmd.Dispose();      
                conn.Close();
            }
            return strInspectorID;
        }

        public DataTable getTblHeader()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable("tbl_header");
           
            // Add three column objects to the table. 
            DataColumn policynumber = new DataColumn();
            policynumber.DataType = System.Type.GetType("System.String");
            policynumber.ColumnName = "policynumber";            
            table.Columns.Add(policynumber);

            DataColumn agency = new DataColumn();
            agency.DataType = System.Type.GetType("System.String");
            agency.ColumnName = "agency";
            table.Columns.Add(agency);

            DataColumn lastname = new DataColumn();
            lastname.DataType = System.Type.GetType("System.String");
            lastname.ColumnName = "lastname";
            table.Columns.Add(lastname);

            DataColumn firstname = new DataColumn();
            firstname.DataType = System.Type.GetType("System.String");
            firstname.ColumnName = "firstname";
            table.Columns.Add(firstname);

            DataColumn street = new DataColumn();
            street.DataType = System.Type.GetType("System.String");
            street.ColumnName = "street";
            table.Columns.Add(street);

            DataColumn city = new DataColumn();
            city.DataType = System.Type.GetType("System.String");
            city.ColumnName = "city";
            table.Columns.Add(city);

            DataColumn state = new DataColumn();
            state.DataType = System.Type.GetType("System.String");
            state.ColumnName = "state";
            table.Columns.Add(state);

            DataColumn zip = new DataColumn();
            zip.DataType = System.Type.GetType("System.String");
            zip.ColumnName = "zip";
            table.Columns.Add(zip);

            DataColumn datereceived = new DataColumn();
            datereceived.DataType = System.Type.GetType("System.DateTime");
            datereceived.ColumnName = "datereceived";
            table.Columns.Add(datereceived);

            DataColumn reporttype = new DataColumn();
            reporttype.DataType = System.Type.GetType("System.String");
            reporttype.ColumnName = "reporttype";
            table.Columns.Add(reporttype);

            DataColumn account = new DataColumn();
            account.DataType = System.Type.GetType("System.String");
            account.ColumnName = "account";
            table.Columns.Add(account);

            DataColumn inspectorid = new DataColumn();
            inspectorid.DataType = System.Type.GetType("System.String");
            inspectorid.ColumnName = "inspectorid";
            table.Columns.Add(inspectorid);

            DataColumn status = new DataColumn();
            status.DataType = System.Type.GetType("System.String");
            status.ColumnName = "status";
            table.Columns.Add(status);

                       

            //table.Columns.Add("policynumber", typeof(string));
            //table.Columns.Add("agency", typeof(string));
            //table.Columns.Add("lastname", typeof(string));
            //table.Columns.Add("firstname", typeof(string));
            //table.Columns.Add("street", typeof(string));
            //table.Columns.Add("city", typeof(string));
            //table.Columns.Add("state", typeof(string));
            //table.Columns.Add("zip", typeof(string));            
            //table.Columns.Add("datereceived", typeof(DateTime));
            //table.Columns.Add("reporttype", typeof(string));
            //table.Columns.Add("account", typeof(string));
            //table.Columns.Add("inspectorid", typeof(string));
            //table.Columns.Add("status", typeof(string));
                          

            DateTime dt = DateTime.Now;
          //  DateTime date = DateTime.ParseExact("06/15/2008", "d", CultureInfo.InvariantCulture);

            // Populate one row with values.

            DataRow myNewRow;

            foreach(Import304 lst in globalImport304)
            {
                if(lst.duplicatePolicyNumber != true)
                {
                    //getInspectorID
                    string strInspectorID = GetInspectorID(lst.zip1);
                   // table.Rows.Add(lst.policyno, lst.agency, lst.insuredlname, lst.insuredfname, lst.address1, lst.city1, lst.state1, lst.zip1, date, "H", AccountNumber, strInspectorID,"H");
                    myNewRow = table.NewRow();
                    myNewRow["policynumber"] = lst.policyno;
                    myNewRow["agency"] = lst.agency;
                    myNewRow["lastname"] = lst.insuredlname;
                    myNewRow["firstname"] = lst.insuredfname;
                    myNewRow["street"] = lst.address1;
                    myNewRow["city"] = lst.city1;
                    myNewRow["state"] = lst.state1;
                    myNewRow["zip"] = lst.zip1;
                    myNewRow["datereceived"] = dt;
                    myNewRow["reporttype"] = "H";
                    myNewRow["account"] = AccountNumber;
                    myNewRow["inspectorid"] = strInspectorID;
                    myNewRow["status"] = "H";
                    
                    table.Rows.Add(myNewRow);                    
                }
            }
            table.AcceptChanges();
	        return table;
        }
        public DataTable getTblHome()
        {
            //// Here we create a DataTable with four columns.
            //DataTable table = new DataTable();

            //table.Columns.Add("policynumber", typeof(string));
            //table.Columns.Add("account", typeof(string));
            //table.Columns.Add("coverageA", typeof(double));
            //table.Columns.Add("phone", typeof(string));
            //table.Columns.Add("street", typeof(string));
            //table.Columns.Add("city", typeof(string));            
            //table.Columns.Add("state", typeof(string));
            //table.Columns.Add("zip", typeof(string));
            //table.Columns.Add("memo", typeof(string));

            // Here we create a DataTable with four columns.
            DataTable table = new DataTable("tbl_home");

            // Add three column objects to the table. 
            DataColumn policynumber = new DataColumn();
            policynumber.DataType = System.Type.GetType("System.String");
            policynumber.ColumnName = "policynumber";
            table.Columns.Add(policynumber);

            DataColumn account = new DataColumn();
            account.DataType = System.Type.GetType("System.String");
            account.ColumnName = "account";
            table.Columns.Add(account);

            DataColumn coverageA = new DataColumn();
            coverageA.DataType = System.Type.GetType("System.Double");
            coverageA.ColumnName = "coverageA";
            table.Columns.Add(coverageA);

            DataColumn phone = new DataColumn();
            phone.DataType = System.Type.GetType("System.String");
            phone.ColumnName = "phone";
            table.Columns.Add(phone);

            DataColumn street = new DataColumn();
            street.DataType = System.Type.GetType("System.String");
            street.ColumnName = "street";
            table.Columns.Add(street);           

            DataColumn city = new DataColumn();
            city.DataType = System.Type.GetType("System.String");
            city.ColumnName = "city";
            table.Columns.Add(city);

            DataColumn state = new DataColumn();
            state.DataType = System.Type.GetType("System.String");
            state.ColumnName = "state";
            table.Columns.Add(state);

            DataColumn zip = new DataColumn();
            zip.DataType = System.Type.GetType("System.String");
            zip.ColumnName = "zip";
            table.Columns.Add(zip);

            DataColumn memo = new DataColumn();
            memo.DataType = System.Type.GetType("System.String");
            memo.ColumnName = "memo";
            table.Columns.Add(memo);

          
            //foreach (Import304 lst in globalImport304)
            //{
            //    if (lst.duplicatePolicyNumber == false)
            //    { 
            //        //memo
            //        string strMemo = lst.m1 + lst.m2+ lst.m3 + lst.m4 + lst.m5 + lst.m6;
            //        table.Rows.Add(lst.policyno, AccountNumber, lst.coverageA, lst.agentsphone, lst.address2, lst.city2, lst.state2, lst.zip2,strMemo);
            //    }
            //}
            //return table;

            DataRow myNewRow;

            foreach (Import304 lst in globalImport304)
            {
                if (lst.duplicatePolicyNumber != true)
                {
                    //memo
                    string strMemo = lst.m1 + lst.m2 + lst.m3 + lst.m4 + lst.m5 + lst.m6;

                    myNewRow = table.NewRow();
                    myNewRow["policynumber"] = lst.policyno;
                    myNewRow["account"] = AccountNumber;
                    if(!string.IsNullOrEmpty(lst.coverageA))
                    {
                        myNewRow["coverageA"] = double.Parse(lst.coverageA);
                    }
                    else
                    {  
                        myNewRow["coverageA"] = DBNull.Value;
                    }
                   
                    myNewRow["phone"] = lst.agentsphone;
                    myNewRow["street"] = lst.address2;
                    myNewRow["city"] = lst.city2;
                    myNewRow["state"] = lst.state2;
                    myNewRow["zip"] = lst.zip2;
                    myNewRow["memo"] = strMemo;

                    table.Rows.Add(myNewRow);
                }
            }
            table.AcceptChanges();
            return table;
        }


        public DataTable getTblAssignedReports()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();

            table.Columns.Add("account", typeof(string));
            table.Columns.Add("policynumber", typeof(string));
            table.Columns.Add("reportname", typeof(string));

            foreach (Import304 lst in globalImport304)
            {
                if (lst.duplicatePolicyNumber == false)
                {
                    table.Rows.Add(AccountNumber, lst.policyno, AccountNumber);
                }
            }
            return table;
        }

        public bool BulkCopy(DataTable dtTblHeader, DataTable dtTblHome, DataTable dtTblAssignedReports)
        {
            //bool checkFailedTran = false;

            var connString = ConfigurationManager.ConnectionStrings["BillingConnectionString"].ConnectionString;
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                   //header
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BatchSize = 10;
                        bulkCopy.DestinationTableName = "tbl_header";

                        try
                        {

                            // Set up the column mappings by name.
                            var po = new SqlBulkCopyColumnMapping("policynumber", "policynumber");
                            var ag = new SqlBulkCopyColumnMapping("agency", "agency");
                            var ln = new SqlBulkCopyColumnMapping("lastname", "lastname");
                            var fn = new SqlBulkCopyColumnMapping("firstname", "firstname");
                            var st = new SqlBulkCopyColumnMapping("street", "street");
                            var ct = new SqlBulkCopyColumnMapping("city", "city");
                            var ste = new SqlBulkCopyColumnMapping("state", "state");
                            var zp = new SqlBulkCopyColumnMapping("zip", "zip");
                            var rt = new SqlBulkCopyColumnMapping("reporttype", "reporttype");
                            var acc = new SqlBulkCopyColumnMapping("account", "account");
                            var ii = new SqlBulkCopyColumnMapping("inspectorid", "inspectorid");
                            var sta = new SqlBulkCopyColumnMapping("status", "status");
                            var dr = new SqlBulkCopyColumnMapping("datereceived", "datereceived");

                            bulkCopy.ColumnMappings.Add(po);
                            bulkCopy.ColumnMappings.Add(ag);
                            bulkCopy.ColumnMappings.Add(ln);
                            bulkCopy.ColumnMappings.Add(fn);
                            bulkCopy.ColumnMappings.Add(st);
                            bulkCopy.ColumnMappings.Add(ct);
                            bulkCopy.ColumnMappings.Add(ste);
                            bulkCopy.ColumnMappings.Add(zp);
                            bulkCopy.ColumnMappings.Add(rt);
                            bulkCopy.ColumnMappings.Add(acc);
                            bulkCopy.ColumnMappings.Add(ii);
                            bulkCopy.ColumnMappings.Add(sta);
                            bulkCopy.ColumnMappings.Add(dr);

                            bulkCopy.WriteToServer(dtTblHeader);                           
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                            //checkFailedTran = true;
                            return false;
                        }
                        finally
                        {
                            //reader.Close();
                        }
                    }

                    //tblHome
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                    {
                        bulkCopy.BatchSize = 10;
                        bulkCopy.DestinationTableName = "dbo.TBL_HOME";

                        try
                        {
                                                       // Set up the column mappings by name.
                            var po = new SqlBulkCopyColumnMapping("policynumber", "policynumber");
                            var acc = new SqlBulkCopyColumnMapping("account", "account");
                            var ca = new SqlBulkCopyColumnMapping("coverageA", "coverageA");
                            var fn = new SqlBulkCopyColumnMapping("phone", "phone");
                            var st = new SqlBulkCopyColumnMapping("street", "street");
                            var ct = new SqlBulkCopyColumnMapping("city", "city");
                            var ste = new SqlBulkCopyColumnMapping("state", "state");
                            var zp = new SqlBulkCopyColumnMapping("zip", "zip");
                            var mem = new SqlBulkCopyColumnMapping("memo", "memo");

                            bulkCopy.ColumnMappings.Add(po);
                            bulkCopy.ColumnMappings.Add(acc);
                            bulkCopy.ColumnMappings.Add(ca);
                            bulkCopy.ColumnMappings.Add(fn);
                            bulkCopy.ColumnMappings.Add(st);
                            bulkCopy.ColumnMappings.Add(ct);
                            bulkCopy.ColumnMappings.Add(ste);
                            bulkCopy.ColumnMappings.Add(zp);
                            bulkCopy.ColumnMappings.Add(mem);

                            bulkCopy.WriteToServer(dtTblHome);
                           // transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                           // checkFailedTran = true;
                            return false;
                        }
                        finally
                        {
                            
                        }
                    }

                    //TblAssignedReports
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                    {
                        bulkCopy.BatchSize = 10;
                        bulkCopy.DestinationTableName = "dbo.tbl_assignedreports";

                        try
                        {
                            var po = new SqlBulkCopyColumnMapping("policynumber", "policynumber");
                            var acc = new SqlBulkCopyColumnMapping("account", "account");
                            var rn = new SqlBulkCopyColumnMapping("reportname", "reportname");
                           

                            bulkCopy.ColumnMappings.Add(po);
                            bulkCopy.ColumnMappings.Add(acc);
                            bulkCopy.ColumnMappings.Add(rn);

                            bulkCopy.WriteToServer(dtTblAssignedReports);
                           // transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                            //checkFailedTran = true;
                            return false;
                        }
                        finally
                        {
                            
                        }
                    }

                    transaction.Commit();
                    conn.Close();
                    return true;
                }
            }
                   
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
           // Response.Redirect("Default.aspx", false);
        }

    }
}