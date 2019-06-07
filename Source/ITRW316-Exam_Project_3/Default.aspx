<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>ITRW316 - Exam Project 3</h2>
        <p class="lead" style="font-size: 13pt"><em>Coenraad Human (28410629), Eon Viljoen (28807995) and Morne Venter (28634748)</em></p>
        <p><a href="https://github.com/coenraadhuman/ITRW316-Exam_Project_3" class="btn btn-primary btn-lg">Private Github Repository &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Server Details</h2>
            <p>Operating system name :
                <asp:Label ID="LabelOSName" runat="server" Text="..."></asp:Label>
            </p>
            <p>Operating system architecture :
                <asp:Label ID="LabelOSArchitecture" runat="server" Text="..."></asp:Label>
            </p>
            <p>Operating system version :
                <asp:Label ID="LabelOSVersion" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                Physical memory available :
                <asp:Label ID="LabelPhysicalMemory" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                Total physical memory available :
                <asp:Label ID="LabelPhysicalMemoryTotal" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                Virtual memory available :
                <asp:Label ID="LabelVirtualMemory" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                Total virtual memory available :
                <asp:Label ID="LabelVirtualMemoryTotal" runat="server" Text="..."></asp:Label>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Simulation Configuration</h2>
            <p>
                <em style="box-sizing: border-box; font-style: italic; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">Reserved memory for operating system (MB) :</em><em style="box-sizing: border-box; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">&nbsp; </em>
                <em>
                <asp:TextBox ID="TextBoxSizeOS" runat="server" Height="24px" style="font-style: italic" Width="90px"></asp:TextBox>
                </em>
            </p>
            <p>
                <em>Page frame size </em><em style="box-sizing: border-box; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">&nbsp;(MB) : </em>
                <em>
                <asp:TextBox ID="TextBoxSizePage" runat="server" style="font-style: italic" Width="89px"></asp:TextBox>
                </em>
            </p>
            <p>
                <em>Simulation allocation for physical memory (%) :
                <asp:TextBox ID="textboxMemoryPercentage" runat="server" style="font-style: italic" Width="86px"></asp:TextBox>
                </em>
            </p>
            <p>
                <em>Simulation allocation for secondary storage (%) : <asp:Label ID="labelStoragePercentage" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <em>Reserved for simulation : <asp:Label ID="LabelSimulationSize" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <em>For memory :
                <asp:Label ID="labelMemorySimulation" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <em>For storage :
                <asp:Label ID="labelStorageSimulation" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <em>Estimated pages in memory : <asp:Label ID="LabelPageCountMemory" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <em>Estimated pages in secondary storage:
                <asp:Label ID="labelPageCountStorage" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <asp:Button ID="ButtonCalculate" runat="server" OnClick="ButtonCalculate_Click" Text="Calculate Total Pages" />
            </p>
            <p>
                <asp:Button ID="ButtonStart" runat="server" Text=" Start Simulation" OnClick="ButtonStart_Click" />
            </p>
        </div>
        <div class="col-md-4">
            <h2>Simulation Results</h2>
            <p>
                Simulation status :
                <asp:Label ID="LabelSimulationStatus" runat="server" Text="Not Runned" ForeColor="#FF3300"></asp:Label>
            </p>
            <p>
                List of programs in <strong>physical memory</strong>:
                <asp:DropDownList ID="DropDownListProgramsPhysical" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                List of programs in <strong>secondary storage</strong>:
                <asp:DropDownList ID="DropDownListProgramsSecondary" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                &nbsp;</p>
            <h2>
                Page Search Function</h2>
            <p>
                P<span style="box-sizing: border-box; color: rgb(33, 33, 33); font-family: &quot;Open Sans&quot;, sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">age to search for :&nbsp; </span>
                <asp:DropDownList ID="DropDownListProgramsRead" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="ButtonSearchPage" runat="server" Text="Search" OnClick="ButtonSearchPage_Click" />
            </p>
            <p>
                Page status: <asp:Label ID="LabelReadStatus" runat="server" Text="..."></asp:Label>
            </p>
        </div>
    </div>
</asp:Content>
