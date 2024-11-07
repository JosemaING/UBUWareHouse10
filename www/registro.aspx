<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="www.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro de Usuario UBUWareHouse10</title>
    <link rel="stylesheet" type="text/css" href="registro.css" />
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Registro de Usuario UBUWareHouse10</h2>
            <table>
                <tr>
                    <td>Nombre:</td>
                    <td><asp:TextBox ID="txtNombre" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Apellidos:</td>
                    <td><asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email:</td>
                    <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Contraseña:</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" AutoPostBack="False"></asp:TextBox>
                        <asp:Button ID="btnGenerarPassword" runat="server" Text="Generar Contraseña" OnClick="btnGenerarPassword_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" OnClick="btnRegistrarse_Click" />
                        <asp:Button ID="btnVolverInicio" runat="server" Text="Volver a Inicio" PostBackUrl="~/inicio.aspx" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
