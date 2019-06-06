<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Details and Configuration</h2>
            <p>
                <strong>System details</strong></p>
            <p>
                System memory :
                <asp:Label ID="LabelSystemMemory" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                Reserved for OS :
                <asp:Label ID="LabelOSSize" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                Reserved for Simulation : <asp:Label ID="LabelSimulationSize" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Configuration</strong></p>
            <p>
                <em style="box-sizing: border-box; font-style: italic; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">Amount of memory to reserve for the operating system :&nbsp; </em>
                <asp:TextBox ID="TextBoxSizeOS" runat="server"></asp:TextBox>
            </p>
            <p>
                &nbsp;<em style="box-sizing: border-box; font-style: italic; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">How large the page frames should be : </em>
                <asp:TextBox ID="TextBoxSizePage" runat="server"></asp:TextBox>
            </p>
            <p>
                <em>Secondary storage size : </em>
                <asp:TextBox ID="TextBoxSizeSecondary" runat="server"></asp:TextBox>
            </p>
            <p>
                Estimated pages : <asp:Label ID="LabelPageCount" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <asp:Button ID="ButtonStart" runat="server" Text="Go!" />
&nbsp;</p>
        </div>
        <div class="col-md-4">
            <h2>Paging Simulation</h2>
            <p>
                List of program in <strong>physical memory</strong>:
                <asp:DropDownList ID="DropDownListProgramsPhysical" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                List of program in <strong>secondary storage</strong>:
                <asp:DropDownList ID="DropDownListProgramsSecondary" runat="server">
                </asp:DropDownList>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Page Search</h2>
            <p>
                P<span style="box-sizing: border-box; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">age to search for :&nbsp; </span>
                <asp:DropDownList ID="DropDownListProgramsRead" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                Page status: <asp:Label ID="LabelReadStatus" runat="server" Text="..."></asp:Label>
            </p>
        </div>
    </div>
</asp:Content>
