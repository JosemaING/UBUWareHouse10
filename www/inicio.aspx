<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="www.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        body{
            color: black;
        }
        #form1 {
            text-align: center;
            font-weight: 700;
        }
        .auto-style3 {
            height: 21px;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style5 {
            height: 26px;
            width: 141px;
        }
        .auto-style6 {
            width: 141px;
        }
        .auto-style8 {
            height: 26px;
            width: 170px;
        }
        .auto-style9 {
            width: 170px;
        }
        .auto-style11 {
            height: 21px;
            width: 141px;
        }
        .auto-style12 {
            height: 21px;
            width: 170px;
        }
        .auto-style13 {
            height: 21px;
            width: 381px;
        }
        .auto-style14 {
            height: 26px;
            width: 381px;
        }
        .auto-style15 {
            width: 381px;
        }
        .auto-style19 {
            height: 21px;
            width: 313px;
        }
        .auto-style20 {
            height: 26px;
            width: 313px;
        }
        .auto-style21 {
            width: 313px;
        }
        .auto-style22 {
            height: 21px;
            width: 265px;
        }
        .auto-style23 {
            height: 26px;
            width: 265px;
        }
        .auto-style24 {
            width: 265px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            <asp:Label ID="lblMensaje" runat="server" Text="UBU Warehouse10 Login"></asp:Label>
        </h1>
        <table class="auto-style4">
            <tr>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style22">&nbsp;</td>
                <td class="auto-style11"></td>
                <td class="auto-style12"></td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style14">&nbsp;</td>
                <td class="auto-style23">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style20">&nbsp;</td>
                <td class="auto-style20">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Label ID="lblPass" runat="server" Text="Contraseña:"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style21">&nbsp;</td>
                <td class="auto-style21">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style9">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </td>
                <td class="auto-style21">&nbsp;</td>
                <td class="auto-style21">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style9">
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" Width="121px" />
                </td>
                <td class="auto-style21">&nbsp;</td>
                <td class="auto-style21">&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
