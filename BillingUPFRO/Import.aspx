<%@ Page Title="Import" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="BillingUPFRO.Import" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
              
            </hgroup>
           
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
    <div>
        <table style="width:500px;" >
           <tr><td><b>Import:</b></td> 
               <td>
                   <asp:DropDownList ID="DDLImport" runat="server" Width="300px" OnSelectedIndexChanged="DDLImport_SelectedIndexChanged" AutoPostBack="true">
                       <asp:ListItem Text="please select" Value="" Selected="True" ></asp:ListItem>
                       <asp:ListItem Text="304 Import" Value="304" ></asp:ListItem>
                       <asp:ListItem Text="42 Import" Value="42" ></asp:ListItem>
                       <asp:ListItem Text="170 Import" Value="170" ></asp:ListItem>
                       <asp:ListItem Text="Prudential NJ Import" Value="PNJI" ></asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
        </table>
        <div align="center">
             <asp:Panel ID="pnlImport" runat="server" Visible="false">
                 <br /><br />
                <asp:Label ID="lbl" Font-Bold="true" Font-Size="Larger"  Text="Import Functionality under testing! " runat="server"></asp:Label>
             </asp:Panel>
            <asp:Panel ID="pnl340" runat="server" Visible="false">
                <table>
                    <thead title="340A Import"></thead>
                    <tr>
                        <td><asp:Label ID="lblFName" Text="File Name to Import: " runat="server"></asp:Label></td>
                        <td><%--<asp:TextBox ID="txtFileName" Width="300px" Text="C:\vijay\UPFRO\Data\4205.csv" runat="server"></asp:TextBox>--%>
                            <asp:FileUpload ID="fLoad" Width="300px"  runat="server" />
                            <asp:RequiredFieldValidator ID="RFVFLoad" ForeColor="Red" ControlToValidate="fLoad" runat="server" ErrorMessage="Please select file"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblType" Text="Type: " runat="server"></asp:Label>
                            <asp:DropDownList ID="DDLType" runat="server" Width="50px">
                                <asp:ListItem Text="H" Value="H" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="A" Value="A"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblAccNumber" Text="AccountNumber: " runat="server"></asp:Label>
                            <asp:TextBox ID="txtAccountNumber" Width="50px" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVAccountNumber" ForeColor="Red" ControlToValidate="txtAccountNumber" runat="server" ErrorMessage="Please enter Account Number"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="Label1" Text="Processing Line: " runat="server"></asp:Label>
                        </td>
                         <td>
                             <asp:Panel runat="server" HorizontalAlign="Right">
                                 <asp:Button ID="btnStart" runat="server" Text="Start" OnClick="btnStart_Click" />
                                 <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                             </asp:Panel>
                           
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

    </div>
</asp:Content>

