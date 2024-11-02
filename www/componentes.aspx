<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="componentes.aspx.cs" Inherits="www.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Notas</title>

    <!-- Estilo css para la web -->
    <link rel="stylesheet" type="text/css" href="estilo-componentes.css" />

    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style3 {
            margin-bottom: 0px;
        }
        .auto-style4 {
            font-size: 14px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <!-- Tabla para organizar el botón de desconectar en la esquina superior derecha -->
        <asp:Table runat="server" Width="100%" Height="16px" CssClass="auto-style3">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Button ID="btnPanelControl" runat="server" Text="Panel de Control" OnClick="btnPanelControl_Click" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="btnDesconectar" runat="server" Text="Desconectar" OnClick="btnDesconectar_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <div class="auto-style1">
            <h1>Gestión de Notas</h1>
            <asp:Label ID="lblBienvenida" runat="server" ForeColor="Black" Width="100%"></asp:Label>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Height="16px" CssClass="auto-style4"></asp:Label>
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Height="16px"></asp:Label>
            <br /><br />

            <!-- Entrada para nombre y descripción de la nota -->
            <asp:TextBox ID="txtNomElemento" runat="server" Placeholder="Nombre de la Nota"></asp:TextBox>
            <asp:TextBox ID="txtDesElemento" runat="server" Placeholder="Descripción de la Nota"></asp:TextBox>
            <br /><br />

            <!-- Botones de acciones -->
            <asp:Button ID="btnCrearNota" runat="server" Text="Crear Nota" OnClick="btnCrearNota_Click" />
            <asp:Button ID="btnEditarNota" runat="server" Text="Editar Nota" OnClick="btnEditarNota_Click" />
            <asp:Button ID="btnEliminarNota" runat="server" Text="Eliminar Nota" OnClick="btnEliminarNota_Click" />
            <br /><br />
            <center>
            <!-- Lista de Notas -->
            <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvNotas_SelectedIndexChanged" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:BoundField DataField="IdElemento" HeaderText="Id" />
                    <asp:BoundField DataField="IdentificadorUsuario" HeaderText="IdUsuario" />
                    <asp:BoundField DataField="NomElemento" HeaderText="Nombre" />
                    <asp:BoundField DataField="DesElemento" HeaderText="Descripción" />
                    <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                </Columns>
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
            </center>
        </div>
    </form>
</body>
</html>
