<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cambioContrasena.aspx.cs" Inherits="www.cambioContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cambiar Contraseña</title>

    <!-- Estilo css para la web -->
    <link rel="stylesheet" type="text/css" href="estilo-contrasena.css" />

    <style type="text/css">
        .auto-style1 {
            font-size: 14px;
        }
    </style>

    </head>
<body>
    <center>
    <form id="form1" runat="server">
        <h2>Cambiar Contraseña</h2>
        
        <div class="form-field">
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </div>
        <br /><br />

        <div class="form-field">
            <asp:TextBox ID="txtContrasenaActual" runat="server" TextMode="Password" Placeholder="Contraseña Actual" Height="16px"></asp:TextBox>
        </div>
        <br />
        <div class="form-field">
            <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password" Placeholder="Nueva Contraseña"></asp:TextBox>
        </div>
        <br />

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False" Height="16px"></asp:Label>
        <br />
        <asp:Label ID="lblSeguridad" runat="server" ForeColor="Orange" Visible="false" CssClass="auto-style1"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Nueva Contraseña" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnInicio" runat="server" Text="Volver a Inicio" OnClick="btnInicio_Click" />
    </form>
        </center>
</body>
</html>
