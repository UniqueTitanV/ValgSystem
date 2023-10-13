<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication2._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Valg app</title>
    <link rel="stylesheet" href="Style.css" />

</head>

<body>

    <form id="form1" runat="server">

        <h1>Valg</h1>
        
        <asp:Button ID ="ButtonSearchParti" runat="server" Text="søk gjennom partier" OnClick="ButtonSearchParti_Click1" />
        
        <asp:TextBox ID="TextBoxSearchParti" runat="server"></asp:TextBox>
        <br />
        <br />

        <br />


       
       <asp:DropDownList ID="DropDownListWithData" runat="server" DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true">


           <asp:ListItem Text="Velg et parti" Value="" />
        </asp:DropDownList>


            <asp:TextBox ID="kommuneboks" runat="server" Text="Skriv inn kommune"></asp:TextBox>
            <asp:Button ID="StemmeKnapp" runat="server" Text="Klikk her for å avgi stemme" OnClick="StemmeKnapp_Click" />
            <asp:Button ID="ButtonKommuneSok" runat="server" OnClick="ButtonKommuneSok_Click" Text="vis stemmer for kommune" />



        <asp:GridView class="Ivory" ID="GridView1" runat="server"></asp:GridView>
<%--       <asp:GridView ID="GridView2" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"></asp:GridView>--%>
    </form>
</body>
</html>
