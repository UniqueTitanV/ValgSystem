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
        
        <asp:Button ID ="ButtonSearchParti" runat="server" Text="Søk gjennom partier" OnClick="ButtonSearchParti_Click1" />
        
        <asp:TextBox ID="TextBoxSearchParti" runat="server"></asp:TextBox>
        <br />
        <br />

        <br />
       
       <asp:DropDownList ID="DropDownListWithData" runat="server" DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true">


           <asp:ListItem Text="Velg et parti" Value="" />
        </asp:DropDownList>


            <asp:TextBox ID="kommuneboks" runat="server" Text="Skriv inn kommune"></asp:TextBox>
            <asp:Button ID="StemmeKnapp" runat="server" Text="Avgi din stemme" OnClick="StemmeKnapp_Click" />
            <asp:Button ID="ButtonKommuneSok" runat="server" OnClick="ButtonKommuneSok_Click" Text="Vis stemmer for kommune" />


        <div class="Grid-Container">
            <div class="Grid-Item">
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </div> 
            <div class="Grid-Item, Grid-Item2">
                <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            </div>

        </div>
        

        
    </form>
</body>
</html>
