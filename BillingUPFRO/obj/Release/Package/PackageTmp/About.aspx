<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BillingUPFRO.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>UPFRO.</h2>
    </hgroup>

    <article>
        <p>
            Formed, as an Association of Inspectors circa 1950 by Tony Passarelli & Dick Haupt, UPFRO remains dedicated to offering personalized, dedicated service and value.  
        </p>
        <p>
            Did you know that UPFRO was the:
        </p>
        <ul>
            <li>First to collect report data in SQL databases</li>

            <li>First to supply digital photos. </li>

            <li>First to require rear photos. </li>

            <li>First to require confirming house number photos. </li>

            <li>First to supply CAD diagrams. </li>

            <li>First to use the World Wide Web for distribution and processing. </li>

            <li>First company to be paperless. </li>

            <li>First to use GPS data for elevations and coastal water evaluation. </li>
        </ul>
        <p>
        UPFRO's emphasis is focus on our customer's needs providing quality and timely reports, technological advances and value-added capabilities.
        </p>
        <p>Click on Key Contacts to learn more</p>
        <p>UPFRO "You Pay For Results Only"  </p>
        <p>Your Success is Our Success </p>

 

    </article>

    <aside>
        <h3>Aside Title</h3>
        <p>
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a runat="server" href="~/Import">Import</a></li>
            <li><a runat="server" href="~/About">About</a></li>
            <li><a runat="server" href="~/Contact">Contact</a></li>
            <li><a runat="server" href="~/OurTeam">Our Team</a></li>
        </ul>
    </aside>
</asp:Content>
