<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

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
   
        <h2 style="color:#ffffff";><%: Title %>.</h2>

     <div class ="mytxt">
        <h3>ITRW316</h3>
        <p>Morne Venter 28634748</p>
        <p>Eon Viljoen 28807995</p>
        <p>Coenraad Human 28410629</p>
    </div>
</asp:Content>
