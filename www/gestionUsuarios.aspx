<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gestionUsuarios.aspx.cs" Inherits="www.gestionUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Usuarios</title>

    <!-- Estilo css para la web -->
    <link rel="stylesheet" type="text/css" href="estilo-gestion.css" />

     <style type="text/css">
     .auto-style1 {
         text-align: center;
     }
     .auto-style3 {
         margin-bottom: 0px;
     }
     </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Tabla para organizar el botón de desconectar en la esquina superior derecha -->
        <asp:Table runat="server" Width="100%" Height="16px" CssClass="auto-style3">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Button ID="btnArticulos" runat="server" Text="Panel de Articulos" OnClick="btnArticulos_Click" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="btnDesconectar" runat="server" Text="Desconectar" OnClick="btnDesconectar_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    <div class="auto-style1">
        <h1>Gestión de Usuarios</h1>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <br /><br />

        <!-- Tabla de usuarios -->
        <asp:GridView ID="gvUsuarios" runat="server" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged" DataKeyNames="IdUsuario" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
            <Columns>
                <asp:BoundField DataField="IdUsuario" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Password" HeaderText="Contraseña" />
                <asp:BoundField DataField="EsGestor" HeaderText="Gestor" />
                <asp:BoundField DataField="Activo" HeaderText="Activo" />
                <asp:BoundField DataField="CaducidadPassword" HeaderText="Días para Caducidad" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>

        <!-- Formulario de edición de usuario -->
        <asp:TextBox ID="txtNombre" runat="server" Placeholder="Nombre"></asp:TextBox>
        <asp:TextBox ID="txtApellidos" runat="server" Placeholder="Apellidos"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" Placeholder="Contraseña"></asp:TextBox>
        <asp:HiddenField ID="hdnPassword" runat="server" />
        <asp:CheckBox ID="chkEsGestor" runat="server" Text="Es Gestor" />
        <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" />
        <asp:CheckBox ID="chkCuentaGratuita" runat="server" Text="Cuenta gratuita" />
        <br />
        <br />
        <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear Usuario" OnClick="btnCrearUsuario_Click" />
        <asp:Button ID="btnEditarUsuario" runat="server" Text="Editar Usuario" OnClick="btnEditarUsuario_Click" />
        <asp:Button ID="btnEliminarUsuario" runat="server" Text="Eliminar Usuario" OnClick="btnEliminarUsuario_Click" />

        <!-- Botón para cambiar la contraseña -->
        <asp:Button ID="btnCambiarContrasena" runat="server" Text="Cambiar Mi Contraseña" OnClick="btnCambiarContrasena_Click" />
    </form>
</body>
</html>
