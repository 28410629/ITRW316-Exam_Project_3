<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        body 
        {
         
            background-color: #040b11;
       
        }
     </style>

    <style>
        .block{
             
              border-radius: 10px;
              border: 4px solid #337ab7;
              padding: 12px;
              margin : 10px;
              width: 100%;
              
              padding-left: 30px;
              background-color: #ffffff;
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
              border: 2px solid #ffffff;
              width:100%;
        }

        .mybtn
        {
              
              border-radius: 2px;
             
        }


    </style>

   <div class ="mycontainer">
    <div class="block">
        <h2>ITRW316 - Exam Project 3</h2>
        <p class="lead" style="font-size: 13pt">Coenraad Human (28410629), Eon Viljoen (28807995) and Morne Venter (28634748).</p>
        <p><a href="https://github.com/coenraadhuman/ITRW316-Exam_Project_3" class="btn btn-primary btn-lg">Github Repository &raquo;</a></p>
    </div>
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
                &nbsp;<asp:TextBox ID="TextBoxSizeOS" runat="server" Height="27px" style="font-style: italic; top: 0px; left: 0px;" Width="160px" CssClass="mybtn"></asp:TextBox>
                </em>
                <asp:Label ID="labelOSValidation" runat="server" style="font-style: italic"></asp:Label>
                <asp:RangeValidator ID="osRange" runat="server" ControlToValidate="TextBoxSizeOS" ErrorMessage="Value out of range." Font-Bold="True" Font-Italic="True" ForeColor="#CC0000" MaximumValue="250" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxSizeOS" ErrorMessage=" Please enter a digit." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </p>
            <p>
                <strong>Page Frame Size (MB):&nbsp; </strong>
                <em>&nbsp;
                <asp:TextBox ID="TextBoxSizePage" runat="server" style="font-style: italic" Width="160px" Height="27px" CssClass="mybtn"></asp:TextBox>
                <asp:Label ID="labelPageFrameValidation" runat="server"></asp:Label>
                <asp:RangeValidator ID="pagesize" runat="server" ControlToValidate="TextBoxSizePage" ErrorMessage="Value out of range." Font-Bold="True" ForeColor="#CC0000" MaximumValue="1000" MinimumValue="50" Type="Integer"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxSizePage" ErrorMessage=" Please enter a digit." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </em>
            </p>
            <p>
                <strong>Percentage Physical Memory (%):</strong><em>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="textboxMemoryPercentage" runat="server" style="font-style: italic" Width="160px" Height="27px" CssClass="mybtn"></asp:TextBox>
                <asp:Label ID="labelMemoryValidation" runat="server"></asp:Label>
                <asp:RangeValidator ID="physMem" runat="server" ControlToValidate="textboxMemoryPercentage" ErrorMessage="Value out of range." Font-Bold="True" ForeColor="#CC0000" MaximumValue="80" MinimumValue="20" Type="Integer"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="textboxMemoryPercentage" ErrorMessage=" Please enter a digit." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
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
                <asp:Button ID="ButtonCalculate" runat="server" OnClick="ButtonCalculate_Click" Text="Calculate Total Pages" BorderStyle="Groove" BorderWidth="2px" CssClass="btn btn-primary btn-lg" Height="44px" ToolTip="Calculate Total Pages" Width="240px" />

            </p>
            <p>

                <asp:Button ID="ButtonStart" runat="server" Text=" Start Simulation" OnClick="ButtonStart_Click" BorderStyle="Groove" BorderWidth="2px" CssClass="btn btn-primary btn-lg" Height="44px" ToolTip=" Start Simulation" Width="240px" Enabled="False" />
            </p>
        </div>



        <div class="block">
            <h2>Simulation Results</h2>
            <p>
                <strong>Simulation Status:</strong>
                <asp:Label ID="LabelSimulationStatus" runat="server" Text="Inactive" ForeColor="#FF3300"></asp:Label>
            </p>
            <p>
                <strong>Pages</strong> <strong> 
                <asp:Label ID="LabelListInPhysical" runat="server" Text="... "></asp:Label>
                in memory</strong>:
                <asp:DropDownList ID="DropDownListProgramsPhysical" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                <strong>Pages</strong> <strong> 
                <asp:Label ID="LabelListInSecondary" runat="server" Text="... "></asp:Label>
                </strong>i<strong>n</strong> <strong>storage</strong>:
                <asp:DropDownList ID="DropDownListProgramsSecondary" runat="server">
                </asp:DropDownList>
            </p>
            <p>
                <strong>Total Pages:</strong>
                <asp:Label ID="LabelTotalPrograms" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Page Reads </strong>:
                <asp:Label ID="LabelTotalPageReads" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Successful Page Reads:</strong>
                <asp:Label ID="LabelTotalSuccesfulPageReads" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Failed Page Reads:</strong>
                <asp:Label ID="LabelTotalFailedPageReads" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Page Faults:</strong>
                <asp:Label ID="LabelTotalPageFaults" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Page Faults Resolved:</strong>
                <asp:Label ID="LabelTotalPageFaultsResolved" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Unswapped Pages:</strong>
                <asp:Label ID="LabelTotalUnswappedPages" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Swapped Pages:</strong>
                <asp:Label ID="LabelTotalSwappedPages" runat="server" Text="..."></asp:Label>
            </p>
            <p>
                <strong>Total Dropped Pages:</strong>
                <asp:Label ID="LabelTotalDroppedPages" runat="server" Text="..."></asp:Label>
            </p>
            </div>

    <div class="block">
            <h2>
                Page Search Function</h2>
            <p>
                <strong>Page Search:</strong>
                <asp:DropDownList ID="DropDownListProgramsRead" runat="server" Height="40px" Width="153px">
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="ButtonSearchPage" runat="server" Text="Search" OnClick="ButtonSearchPage_Click" BorderStyle="Groove" BorderWidth="2px" CssClass="btn btn-primary btn-lg" Enabled="False" Height="44px" Width="240px" />
            &nbsp;<asp:Label ID="LabelSearchValidation" runat="server"></asp:Label>
            </p>
            <p>
                <strong>Page Status:</strong> <asp:Label ID="LabelReadStatus" runat="server" Text="..."></asp:Label>
            </p>
        </div>
        </div>
 
</asp:Content>
