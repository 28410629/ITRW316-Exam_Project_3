<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body 
        {
         
            background-color: #040b11;
            
        }
         .mytxt
        {
            margin-top:15px;
           
            border-radius: 10px;
            border: 4px solid #337ab7;
            padding :20px;
            background-color :#ffffff;
           
        }
     </style>
    
    <h2 style="color:#ffffff";><%: Title %></h2>
     <div class ="mytxt">
     <h3>This is the third project for ITRW 316 exam projects.</h3>
    <p>This project allows users to simulate paging.</p>
    <p>Statistics and measurments are also displayed.</p>
     </div>
</asp:Content>
