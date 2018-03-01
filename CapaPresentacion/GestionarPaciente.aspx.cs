using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaLogicaNegocios;
using System.Web.Services;

namespace CapaPresentacion
{
    public partial class frmGestionarPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                ddlSexo.Items.Insert(0,"MASCULINO");
                ddlSexo.Items.Insert(1, "FEMENINO");
            }
        }

        [WebMethod]
        public static List<Paciente> ListarPacientes()
        {
            List<Paciente> Lista = null;
            try
            {
                Lista = PacienteLN.getInstance().ListarPacientes();
            }
            catch (Exception ex)
            {

                Lista = null;
            }
            return Lista;
        }
        private Paciente GetEntity() {
            Paciente objPaciente = new Paciente();
            objPaciente.IdPaciente = 0;
            objPaciente.Nombres = txtNombres.Text;
            objPaciente.ApPaterno = txtApPaterno.Text;
            objPaciente.ApMaterno = txtApMaterno.Text;
            objPaciente.Edad = Convert.ToInt32(txtEdad.Text);
            objPaciente.Sexo = (ddlSexo.SelectedValue == "Femenino")? 'F':'M';
            objPaciente.NroDcoumento = txtNroDocumento.Text;
            objPaciente.Direccion = txtDireccion.Text;
            objPaciente.Telefono = txtTelefono.Text;
            objPaciente.Estado = true;

            return objPaciente;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                //Registro del Paciente
                Paciente objPaciente = GetEntity();
                //Enviar a la capa logica de negocio
                bool response = PacienteLN.getInstance().RegistrarPaciente(objPaciente);
                if (response == true)
                {
                    Response.Write("<script>alert('REGISTRO CORRECTO')</script>");
                }
                else
                {
                    Response.Write("<script>alert('REGISTRO INCORRECTO')</script>");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}