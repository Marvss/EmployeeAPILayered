using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmpleadosAPI.Core.Entities;
namespace EmpleadosAPI.WebForms
{
    public partial class EmpleadoForm : Page
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoForm(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar departamentos en el DropDownList
                CargarDepartamentos();
            }
        }

        private void CargarDepartamentos()
        {
            // Aquí deberías cargar los departamentos desde la base de datos
            // Por ahora, usaremos datos de ejemplo
            ddlDepartamento.Items.Clear();
            ddlDepartamento.Items.Add(new ListItem("Seleccione un departamento", ""));
            ddlDepartamento.Items.Add(new ListItem("Recursos Humanos", "1"));
            ddlDepartamento.Items.Add(new ListItem("Contabilidad", "2"));
            ddlDepartamento.Items.Add(new ListItem("Ventas", "3"));
        }

        protected async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var empleado = new Empleado
                    {
                        Nombres = txtNombres.Text,
                        DPI = txtDPI.Text,
                        FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                        Sexo = ddlSexo.SelectedValue,
                        FechaIngreso = DateTime.Parse(txtFechaIngreso.Text),
                        Direccion = txtDireccion.Text,
                        NIT = txtNIT.Text,
                        DepartamentoId = int.Parse(ddlDepartamento.SelectedValue)
                    };

                    await _empleadoService.AddEmpleadoAsync(empleado);
                    lblMensaje.Text = "Empleado guardado exitosamente.";
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al guardar el empleado: " + ex.Message;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombres.Text = string.Empty;
            txtDPI.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            ddlSexo.SelectedIndex = 0;
            txtFechaIngreso.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtNIT.Text = string.Empty;
            ddlDepartamento.SelectedIndex = 0;
        }
    }
}