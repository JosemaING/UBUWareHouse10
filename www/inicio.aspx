<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="www.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio de sesión</title>
    <link rel="stylesheet" type="text/css" href="inicio-sesion.css" />
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            width: 128px;
        }
        .auto-style4 {
            height: 26px;
            width: 128px;
        }
        .auto-style5 {
            height: 22px;
            width: 128px;
        }
        .auto-style6 {
            height: 16px;
            width: 355px;
        }
        .auto-style7 {
            height: 16px;
        }
        .auto-style8 {
            width: 128px;
            height: 16px;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <h1>
            <asp:Label ID="lblMensaje" runat="server" Text="UBU Warehouse10 Login"></asp:Label>
        </h1>
        <table class="auto-style6" width="100%">
            <tr>
                <td class="auto-style13">&nbsp;</td>
                <td class="auto-style22">&nbsp;</td>
                <td class="auto-style11"></td>
                <td class="auto-style3"></td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style14">&nbsp;</td>
                <td class="auto-style23">&nbsp;</td>
                <td class="auto-style5" >
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style20">&nbsp;</td>
                <td class="auto-style20">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style2"></td>
                <td class="auto-style2">
                    <asp:Label ID="lblPass" runat="server" Text="Contraseña:"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style2"></td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style5">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" Width="121px" />
                </td>
                <td class="auto-style21">&nbsp;</td>
                <td class="auto-style21">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style6"></td>
                <td class="auto-style8"></td>
                <td class="auto-style7"></td>
            </tr>
        </table>
        <center>¿No estás registrado? <asp:Button ID="btnRegistro" runat="server" OnClick="btnRegistro_Click" Text="Registrarse" BackColor="White" CssClass="auto-style5" />
            </center>
    </form>
</body>
</html>
