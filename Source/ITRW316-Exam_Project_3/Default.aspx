<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .block{
             
              border-radius: 10px;
              border: 1px solid #3368bf;
              padding: 12px;
              margin : 10px;
              width: 100%;
              
              padding-left: 30px;
              background-color: #eeeeee;
              box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .mycontainer
        {
            margin: auto;
            width: 85%;
        }

        .jumbotron
        {
             box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
              border-radius: 10px;
              border: 2px solid #3368bf;
              width:100%;
        }
    </style>

   
    <div class="jumbotron">
        <h2>ITRW316 - Exam Project 3</h2>
        <p class="lead" style="font-size: 13pt"><em>Coenraad Human (28410629), Eon Viljoen (28807995) and Morne Venter (28634748)</em></p>
        <p><a href="https://github.com/coenraadhuman/ITRW316-Exam_Project_3" class="btn btn-primary btn-lg">Private Github Repository &raquo;</a></p>
    </div>
    <div class ="mycontainer">
    <div class="block">
            <h2>Server Details</h2>
            <p>
                <asp:Label ID="LabelServerDetailsDateTime" runat="server" Text="..." style="font-weight: 700"></asp:Label>
            </p>
            <p><strong>OS Name:</strong>
                <asp:Label ID="LabelOSName" runat="server" Text="..."></asp:Label>
            </p>
            <p><strong>OS Architecture:</strong>
                <asp:Label ID="LabelOSArchitecture" runat="server" Text="..."></asp:Label>
            </p>
            <p><strong>OS Version: </strong>
                <asp:Label ID="LabelOSVersion" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                <strong>Free Memory:</strong>
                <asp:Label ID="LabelPhysicalMemory" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                <strong>Total Memory: </strong>
                <asp:Label ID="LabelPhysicalMemoryTotal" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                <strong>Virtual Memory Free:</strong>
                <asp:Label ID="LabelVirtualMemory" runat="server" Text="..."></asp:Label>
            </p>
            <p style="font-size: small">
                <strong>Total Virtual Memory:</strong>
                <asp:Label ID="LabelVirtualMemoryTotal" runat="server" Text="..."></asp:Label>
            </p>
        </div>


        <div class="block">
            <h2>Simulation Configuration</h2>
            <p>
                <strong>Reserve For OS (MB):&nbsp;&nbsp; </strong>
                <em>
                &nbsp;<asp:TextBox ID="TextBoxSizeOS" runat="server" Height="20px" style="font-style: italic" Width="90px" CssClass="input-lg"></asp:TextBox>
                </em>
            </p>
            <p>
                <strong>Page Frame Size (MB):&nbsp; </strong>
                <em>&nbsp;
                <asp:TextBox ID="TextBoxSizePage" runat="server" style="font-style: italic" Width="89px" CssClass="input-lg" Height="20px"></asp:TextBox>
                </em>
            </p>
            <p>
                <strong>Percentage Physical Memory (%):</strong><em>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="textboxMemoryPercentage" runat="server" style="font-style: italic" Width="86px" CssClass="input-lg" Height="21px"></asp:TextBox>
                </em>
            </p>
            <p>
                <strong>Percentage Secondary Storage (%):</strong><em> <asp:Label ID="labelStoragePercentage" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <strong>Available Memory</strong><em>: <asp:Label ID="LabelSimulationSize" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <strong>Allocated To Memory:</strong><em>
                <asp:Label ID="labelMemorySimulation" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <strong>Allocated To Storage:</strong><em>
                <asp:Label ID="labelStorageSimulation" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <strong>Pages frames in memory:</strong><em> <asp:Label ID="LabelPageCountMemory" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                <strong>Pages frames in storage:</strong><em>
                <asp:Label ID="labelPageCountStorage" runat="server" Text="..."></asp:Label>
                </em>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:Button ID="ButtonCalculate" runat="server" OnClick="ButtonCalculate_Click" Text="Calculate Total Pages" BorderColor="#0066FF" BorderStyle="Groove" BorderWidth="2px" CssClass="btn active" Height="44px" ToolTip="Calculate Total Pages" Width="188px" />

                &nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Button ID="ButtonStart" runat="server" Text=" Start Simulation" OnClick="ButtonStart_Click" BorderColor="#0066FF" BorderStyle="Groove" BorderWidth="2px" CssClass="btn active" Height="43px" ToolTip=" Start Simulation" Width="187px" />
            </p>
        </div>



        <div class="block">
            <h2>Simulation Results</h2>
            <p>
                Simulation status :
                <asp:Label ID="LabelSimulationStatus" runat="server" Text="Not Runned" ForeColor="#FF3300"></asp:Label>
            </p>
            <p>
                List of programs 
                <asp:Label ID="LabelListInPhysical" runat="server" Text="... "></asp:Label>
                in <strong>physical memory</strong>:
                <asp:DropDownList ID="DropDownListProgramsPhysical" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                List of programs 
                <asp:Label ID="LabelListInSecondary" runat="server" Text="... "></asp:Label>
                in <strong>secondary storage</strong>:
                <asp:DropDownList ID="DropDownListProgramsSecondary" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                Total programs :
                <asp:Label ID="LabelTotalPrograms" runat="server" Text="LabelTotalPrograms"></asp:Label>
            </p>
            <p>
                Total page reads :
                <asp:Label ID="LabelTotalPageReads" runat="server" Text="LabelTotalPageReads"></asp:Label>
            </p>
            <p>
                Total successful page reads :
                <asp:Label ID="LabelTotalSuccesfulPageReads" runat="server" Text="LabelTotalSuccesfulPageReads"></asp:Label>
            </p>
            <p>
                Total failed page reads :
                <asp:Label ID="LabelTotalFailedPageReads" runat="server" Text="LabelTotalFailedPageReads"></asp:Label>
            </p>
            <p>
                Total page faults : <asp:Label ID="LabelTotalPageFaults" runat="server" Text="LabelTotalPageFaults"></asp:Label>
            </p>
            <p>
                Total page faults resolved : <asp:Label ID="LabelTotalPageFaultsResolved" runat="server" Text="LabelTotalPageFaultsResolved"></asp:Label>
            </p>
            <p>
                Total unswapped pages :
                <asp:Label ID="LabelTotalUnswappedPages" runat="server" Text="LabelTotalUnswappedPages"></asp:Label>
            </p>
            <p>
                Total swapped pages :
                <asp:Label ID="LabelTotalSwappedPages" runat="server" Text="LabelTotalSwappedPages"></asp:Label>
            </p>
            <p>
                Total dropped pages :
                <asp:Label ID="LabelTotalDroppedPages" runat="server" Text="LabelTotalDroppedPages"></asp:Label>
            </p>
            </div>

    <div class="block">
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
