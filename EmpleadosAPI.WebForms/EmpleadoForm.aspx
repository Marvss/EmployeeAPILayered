<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpleadoForm.aspx.cs" Inherits="EmpleadosAPI.WebForms.EmpleadoForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Empleado</title>
    <style>
        body { font-family: Arial, sans-serif; }
        .form-group { margin-bottom: 15px; }
        label { display: inline-block; width: 150px; }
        .button { padding: 5px 10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Registro de Empleado</h2>
            <div class="form-group">
                <label for="txtNombres">Nombres:</label>
                <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombres" runat="server" 
                    ControlToValidate="txtNombres" ErrorMessage="El nombre es requerido." 
                    ForeColor="Red"></asp:RequiredFieldValidator ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="txtDPI">DPI:</label>
                <asp:TextBox ID="txtDPI" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDPI" runat="server" 
                    ControlToValidate="txtDPI" ErrorMessage="El DPI es requerido." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="txtFechaNacimiento">Fecha de Nacimiento:</label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" 
                    ControlToValidate="txtFechaNacimiento" ErrorMessage="La fecha de nacimiento es requerida." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="ddlSexo">Sexo:</label>
                <asp:DropDownList ID="ddlSexo" runat="server">
                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtFechaIngreso">Fecha de Ingreso:</label>
                <asp:TextBox ID="txtFechaIngreso" runat="server" TextMode="Date"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFechaIngreso" runat="server" 
                    ControlToValidate="txtFechaIngreso" ErrorMessage="La fecha de ingreso es requerida." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="txtDireccion">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtNIT">NIT:</label>
                <asp:TextBox ID="txtNIT" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ddlDepartamento">Departamento:</label>
                <asp:DropDownList ID="ddlDepartamento" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" 
                    ControlToValidate="ddlDepartamento" ErrorMessage="El departamento es requerido." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" CausesValidation="false" />
            </div>
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
    <script type="text/javascript">
        function calcularEdad() {
            var fechaNacimiento = new Date(document.getElementById('<%= txtFechaNacimiento.ClientID %>').value);
            var hoy = new Date();
            var edad = hoy.getFullYear() - fechaNacimiento.getFullYear();
            var m = hoy.getMonth() - fechaNacimiento.getMonth();
            if (m < 0 || (m === 0 && hoy.getDate() < fechaNacimiento.getDate())) {
                edad--;
            }
            document.getElementById('lblEdad').innerText = 'Edad: ' + edad + ' años';
        }

        document.getElementById('<%= txtFechaNacimiento.ClientID %>').addEventListener('change', calcularEdad);
    </script>
</body>
</html>