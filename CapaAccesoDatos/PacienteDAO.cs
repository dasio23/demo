﻿using CapaEntidades;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class PacienteDAO
    {
        #region "PATRON SINGLETON"
        private static PacienteDAO daoEmpleado = null;

        private PacienteDAO() { }

        public static PacienteDAO getInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new PacienteDAO();
            }
            return daoEmpleado;
        }
        #endregion
        public bool RegistrarPaciente(Paciente objPaciente)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;                        
            bool response = false;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spRegistrarPaciente", con);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@prmNombres", objPaciente.Nombres);
                cmd.Parameters.AddWithValue("@prmApPaterno", objPaciente.ApPaterno);
                cmd.Parameters.AddWithValue("@prmApMaterno", objPaciente.ApMaterno);
                cmd.Parameters.AddWithValue("@prmEdad", objPaciente.Edad);
                cmd.Parameters.AddWithValue("@prmSexo", objPaciente.Sexo);
                cmd.Parameters.AddWithValue("@prmNroDoc", objPaciente.NroDcoumento);
                cmd.Parameters.AddWithValue("@prmDireccion", objPaciente.Direccion);
                cmd.Parameters.AddWithValue("@prmTelefono", objPaciente.Telefono);
                cmd.Parameters.AddWithValue("@prmEstado", objPaciente.Estado);
                con.Open();
                int filas = cmd.ExecuteNonQuery();
                if (filas > 0) {
                    response = true;
                }                                                
            }
            catch (Exception ex)
            {
                response = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return response;
        }

        public List<Paciente> ListarPacientes()
        {
            List<Paciente> Lista = new List<Paciente>();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            try
            {
                con = Conexion.getInstance().ConexionBD();
                cmd = new SqlCommand("spListarPacientes",con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                dr = cmd.ExecuteReader();

                //otra forma
                //using (SqlDataReader dr = cmd.ExecuteReader()) {

                //}
                while (dr.Read())
                {
                    //crear objetos de tipo Paciente
                    Paciente objPaciente = new Paciente();
                    objPaciente.IdPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                    objPaciente.Nombres = dr["nombres"].ToString();
                    objPaciente.ApPaterno = dr["apPaterno"].ToString();
                    objPaciente.ApMaterno = dr["apMaterno"].ToString();
                    objPaciente.Edad = Convert.ToInt32(dr["edad"].ToString());
                    objPaciente.Sexo = Convert.ToChar(dr["sexo"].ToString());
                    objPaciente.NroDcoumento = dr["nroDocumento"].ToString();
                    objPaciente.Direccion = dr["direccion"].ToString();
                    objPaciente.Estado = true;
                    //añadir a la lista de objetos
                    Lista.Add(objPaciente);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {
                con.Close();
            }
            return Lista;
        }
    }
}
