<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication2._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Valg app</title>
    <link rel="stylesheet" href="SkoleValgProsjekt/styles.css"/>
</head>
<body>

    <form id="form1" runat="server">
        
        <asp:Button ID ="ButtonSearchParti" runat="server" Text="søk gjennom partier" OnClick="ButtonSearchParti_Click1" />
        
        <asp:TextBox ID="TextBoxSearchParti" runat="server"></asp:TextBox>
        <br />
        
        <br />


<%--        <asp:Button ID="AddNewPerson" runat="server" Text="Legg til ny person" OnClick="AddNewPerson_Click" />

        <asp:TextBox ID="NewPersonFirstName" runat="server" ></asp:TextBox>

        <asp:TextBox ID="NewPersonLastName" runat="server" ></asp:TextBox>
    <asp:DropDownList runat="server"></asp:DropDownList>
        <asp:TextBox ID="NewPersonID" runat="server" ></asp:TextBox>
        <br />--%>

       
        <asp:DropDownList ID="DropDownListParti" runat="server">
                <asp:ListItem Selected="True" Value="0">Velg Parti</asp:ListItem>
                <asp:ListItem Value="1">H</asp:ListItem>
                <asp:ListItem Value="2">Pp</asp:ListItem>
            </asp:DropDownList>

<%--            <asp:Button ID="StemmeKnapp" runat="server" Text="Klikk her for å avgi stemme" OnClick="StemmeKnapp_Click" />--%>

        <asp:GridView ID="GridView1" runat="server"></asp:GridView>

    </form>
</body>
</html>
