<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BillingUPFRO._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <%-- <h2>Modify this template to jump-start your ASP.NET application.</h2>--%>
            </hgroup>
            <%--           <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>--%>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%-- <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>--%>
       <p>
          <table>
              <tr>
                  <td><label id="lblAccountID" style="width:100px">Account </label></td>
                  <td> <label id="lblName"  style="width:100px">Name</label></td>
              </tr>
          </table>
<%--        <label id="lblAccountID" style="width:100px">Account </label>
         <label id="lblName"  style="width:100px">Name</label>--%>
          
        <asp:TextBox ID="txtAccountID" runat="server" width="100px"  CssClass="textEntry"></asp:TextBox>
        
        <asp:TextBox ID="txtName" runat="server" CssClass="textEntry"></asp:TextBox>

        <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="btnSearch_Click" OnClientClick="btnSearch_Click"/>
        <asp:DropDownList ID ="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatusFilter" AutoPostBack="true" >
            <asp:ListItem Selected="true" value="Select Status">Select Status</asp:ListItem>
            <asp:ListItem Value="D">Demand</asp:ListItem>
            <asp:ListItem Value="M">Monthly</asp:ListItem>
            <asp:ListItem Value="S">Semi-Monthly</asp:ListItem>
            <asp:ListItem Value="T">Terminated</asp:ListItem>
       </asp:DropDownList>
        <asp:DropDownList ID ="ddlState" runat="server" OnSelectedIndexChanged="ddlStateFilter" AutoPostBack="true" >
            <asp:ListItem Selected="true" value="Select State">Select State All</asp:ListItem>
            <asp:ListItem Value="CT">CT</asp:ListItem>
            <asp:ListItem Value="NJ">NJ</asp:ListItem>
            <asp:ListItem Value="NY">NY</asp:ListItem>
            <asp:ListItem Value="PA">PA</asp:ListItem>
            <asp:ListItem Value="XX">Others</asp:ListItem>
       </asp:DropDownList>
    </p>

    <p>
        <asp:DetailsView ID="dvBilling" runat="server" Width="125px" Height="50px" DataKeyNames="accountid" GridLines="None"
            DataSourceID="sql_accountinfo2" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateRows="false" OnItemInserted="RefreshGridReview" OnItemUpdated="RefreshGridReview">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <EditRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <Fields>

                <asp:BoundField DataField="accountid" HeaderText="ID" InsertVisible="true" ReadOnly="true"></asp:BoundField>
                <asp:BoundField DataField="name" HeaderText="Name"></asp:BoundField>
                <asp:BoundField DataField="street" HeaderText="Street"></asp:BoundField>
                <asp:BoundField DataField="city" HeaderText="City"></asp:BoundField>
                <asp:BoundField DataField="state" HeaderText="State"></asp:BoundField>
                <asp:BoundField DataField="zip" HeaderText="Zip Code"></asp:BoundField>
                <asp:BoundField DataField="contact" HeaderText="Contact"></asp:BoundField>
                <asp:BoundField DataField="faxphone" HeaderText="Fax"></asp:BoundField>
                <asp:BoundField DataField="email" HeaderText="E-Mail"></asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="Status"></asp:BoundField>
                <asp:BoundField DataField="sendmethod" HeaderText="Send Method"></asp:BoundField>
                <asp:BoundField DataField="phone" HeaderText="Phone"></asp:BoundField>
                <asp:BoundField DataField="zipassigned" HeaderText="Zip Assigned" Visible="True"></asp:BoundField>
                <asp:BoundField DataField="daysdue" HeaderText="Days Due" Visible="True"></asp:BoundField>
                <asp:BoundField DataField="duedate" HeaderText="Due Date" Visible="True"></asp:BoundField>
                <asp:CommandField ShowEditButton="True" ItemStyle-CssClass="bold" />

            </Fields>
        </asp:DetailsView>
    </p>
   <p>
        <asp:Button id="btnCreate" runat="server" OnClick="btnCreate_Click" CssClass="submitButton" Text="Create New"/>
    </p>

    <p>
        <asp:GridView ID="gvAccountInfo" runat="server" AutoGenerateColumns="False" ClientIDMode="Static" DataSourceID="sql_accountinfo" DataKeyNames="accountid" AllowSorting="True"
            OnRowCommand="GvOnRowCommand" OnRowDataBound="gvAccountInfo_RowDataBound" onselectedindexchanged="gv_select">
            <Columns>
                <asp:CommandField ShowSelectButton="true" HeaderText="Select" />
                <asp:BoundField DataField="accountid" HeaderText="ID" SortExpression="accountid"></asp:BoundField>
                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name"></asp:BoundField>
                <asp:BoundField DataField="street" HeaderText="Street" SortExpression="street"></asp:BoundField>
                <asp:BoundField DataField="city" HeaderText="City" SortExpression="city"></asp:BoundField>
                <asp:BoundField DataField="state" HeaderText="State" SortExpression="state"></asp:BoundField>
                <asp:BoundField DataField="zip" HeaderText="Zip" SortExpression="zip"></asp:BoundField>
                <asp:BoundField DataField="contact" HeaderText="Contact" SortExpression="contact"></asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status"></asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" HeaderText="Delete" />
            </Columns>
        </asp:GridView>
    </p>

    <asp:SqlDataSource ID="sql_accountinfo"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:BillingConnectionString %>"
        SelectCommand="SELECT [accountid]
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
                FROM [dbo].[tbl_accountInfo]"
        DeleteCommand="DELETE FROM [dbo].[tbl_accountInfo] WHERE [accountid] = @AccountID"></asp:SqlDataSource>

        <asp:SqlDataSource ID="sql_accountinfo2" runat="server" 
		ConnectionString="<%$ ConnectionStrings:BillingConnectionString %>"
		SelectCommand="select * from  [dbo].[tbl_accountInfo] WHERE  [accountid] = @AccountID" 
        insertcommand= "insert into  [dbo].[tbl_accountInfo] ([accountid],[name],[street],[city],[state],[zip],[contact],[faxphone],[email],[status],[sendmethod],[phone],[zipassigned],[daysdue],[duedate])  VALUES (@accountid,@name,@street,@city,@state,@zip,@contact,@faxphone,@email,@status,@sendmethod,@phone,@zipassigned,@daysdue,@duedate) "
        updatecommand=" update [dbo].[tbl_accountInfo] SET [accountid] = @accountid,[name] = @name,[street] = @street,[city] = @city,[state] = @state,[zip] = @zip,[contact] = @contact,[faxphone] = @faxphone,[email] = @email,[status] = @status,[sendmethod] = @sendmethod,[phone] = @phone,[zipassigned] = @zipassigned,[daysdue] = @daysdue,[duedate] = @duedate  WHERE [accountid] = @AccountID "
        DeleteCommand= "DELETE FROM [dbo].[tbl_accountInfo] WHERE [accountid] = @AccountID">
		<SelectParameters>
		   <asp:ControlParameter Name="accountid" ControlID="gvAccountInfo" />
		</SelectParameters>
		</asp:SqlDataSource>


</asp:Content>
