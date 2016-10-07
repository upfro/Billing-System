<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BillingUPFRO.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your contact page.</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Phone:</h3>
        </header>
        <p>
            <span class="label">Main:</span>
            <span>800.423.7099</span>
        </p>
        <p>
            <span class="label">After Hours:</span>
            <span>800.423.7099</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:Support@upfro.com">Support@upfro.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:Marketing@upfro.com">Marketing@upfro.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:General@upfro.com">General@upfro.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <p>
            1821 Highway 71 <br />
            Wall, NJ 07719
        </p>
    </section>
</asp:Content>