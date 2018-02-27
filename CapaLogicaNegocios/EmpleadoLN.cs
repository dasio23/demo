using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaLogicaNegocios
{
    public class EmpleadoLN
    {
        #region "PATRON SINGLETON"

        private static EmpleadoLN objEmpleado = null;
        private EmpleadoLN() { }

        public static EmpleadoLN getInstance()
        {
            if (objEmpleado == null)
            {
                objEmpleado = new EmpleadoLN();
            }
            return objEmpleado;
        }

        #endregion

        public Empleado AccesoSistema(String user, String pass)
        {
            try
            {
                return EmpleadoDAO.getInstance().AccesoSistema(user, pass);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
