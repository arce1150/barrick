using System;
using System.Data.SqlClient;
using System.Transactions;
using Barrick.Pasajes.DataAccess.Contracts;
using System.Data;
using Barrick.Pasajes.BusinessEntity;
using System.Collections.Generic;

namespace Barrick.Pasajes.DataAccess
{
    public class MPersonalDa : IMPersonalDa
    {
        public int cambiarContrasenia ( SqlConnection con, string contrasenia, string usuario )
        {
            int filasAfectadas = 0;
            try
            {
                using (var tnxsInsert = new TransactionScope())
                {

                    using (var cmd = new SqlCommand("USP_PA_CAMBIAR_CONTRASENIA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var parametros = new[] {
                             new SqlParameter("@n_password", SqlDbType.NVarChar) {Value =  (object)contrasenia  ?? DBNull.Value ,Direction = ParameterDirection.Input},
                             new SqlParameter("@n_user", SqlDbType.NVarChar) {Value =  (object)usuario  ?? DBNull.Value ,Direction = ParameterDirection.Input}
                    };
                        cmd.Parameters.AddRange(parametros);
                        filasAfectadas = cmd.ExecuteNonQuery();
                    }
                    tnxsInsert.Complete();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return filasAfectadas;
        }      
        public MPersonalBe ListarPersonalPorUsuario ( SqlConnection con, string usuario )
        {
            MPersonalBe persona = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_PERSONAL_POR_USUARIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@n_user" , SqlDbType.NVarChar) {Value =  (object)usuario ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    int pCodInterno = dr.GetOrdinal("CodigoInterno");
                    int pCodPersona = dr.GetOrdinal("CodigoPersona");
                    int pUsuario = dr.GetOrdinal("usuario");
                    int pContrasenia = dr.GetOrdinal("Contrasenia");
                    int pNombre = dr.GetOrdinal("Nombre");
                    int pEstado = dr.GetOrdinal("Estado");
                    int pEmail = dr.GetOrdinal("Email");
                    if (dr.Read())
                    {
                        persona = new MPersonalBe();
                        persona.CodigoInterno = dr.GetValue(pCodInterno) == DBNull.Value ? default(int) : dr.GetInt32(pCodInterno);
                        persona.CodigoPersona = dr.GetValue(pCodPersona) == DBNull.Value ? default(string) : dr.GetString(pCodPersona);
                        persona.Usuario = dr.GetValue(pUsuario) == DBNull.Value ? default(string) : dr.GetString(pUsuario);
                        persona.Contrasenia = dr.GetValue(pContrasenia) == DBNull.Value ? default(string) : dr.GetString(pContrasenia);
                        persona.Nombre = dr.GetValue(pNombre) == DBNull.Value ? default(string) : dr.GetString(pNombre);
                        persona.Activo = dr.GetValue(pEstado) == DBNull.Value ? default(bool) : dr.GetBoolean(pEstado);
                        persona.Email = dr.GetValue(pEmail) == DBNull.Value ? default(string) : dr.GetString(pEmail);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return persona;
        }
        public MPersonalBe validarLoguin ( SqlConnection con, string usuario, string contrasenia )
        {
            MPersonalBe persona = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LOGUIN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@n_user" , SqlDbType.NVarChar) {Value =  (object)usuario ?? DBNull.Value ,Direction = ParameterDirection.Input},
                    new SqlParameter("@n_password" , SqlDbType.NVarChar) {Value =  (object)contrasenia ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pUsuario = dr.GetOrdinal("usuario");
                    int pNombre = dr.GetOrdinal("Nombre");
                    int pEstado = dr.GetOrdinal("Estado");
                    int pEmail = dr.GetOrdinal("Correo");
                    if (dr.Read())
                    {
                        persona = new MPersonalBe();
                        persona.Usuario = dr.GetValue(pUsuario) == DBNull.Value ? default(string) : dr.GetString(pUsuario);
                        persona.Nombre = dr.GetValue(pNombre) == DBNull.Value ? default(string) : dr.GetString(pNombre);
                        persona.Activo = dr.GetValue(pEstado) == DBNull.Value ? default(bool) : dr.GetBoolean(pEstado);
                        persona.Email = dr.GetValue(pEmail) == DBNull.Value ? default(string) : dr.GetString(pEmail);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return persona;
        }
        public List<MPersonalBe> listarPersonalAprobador ( SqlConnection con )
        {
            List<MPersonalBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_USUARIOS_APROBADORES", con);
                cmd.CommandType = CommandType.StoredProcedure; 
                SqlDataReader dr = cmd.ExecuteReader();
                MPersonalBe objPersonal= null;
                lista=new List<MPersonalBe>();
                if (dr != null)
                {
                    int pCodInterno = dr.GetOrdinal("CodigoInterno");
                    int pCodPersona = dr.GetOrdinal("CodigoPersona");
                    int pUsuario = dr.GetOrdinal("usuario"); 
                    int pNombre = dr.GetOrdinal("Nombre");
                    int pEstado = dr.GetOrdinal("Estado");
                    int pEmail = dr.GetOrdinal("Email");
                    while (dr.Read())
                    {
                        objPersonal = new MPersonalBe();
                        objPersonal.CodigoInterno = dr.GetValue(pCodInterno) == DBNull.Value ? default(int) : dr.GetInt32(pCodInterno);
                        objPersonal.CodigoPersona = dr.GetValue(pCodPersona) == DBNull.Value ? default(string) : dr.GetString(pCodPersona);
                        objPersonal.Usuario = dr.GetValue(pUsuario) == DBNull.Value ? default(string) : dr.GetString(pUsuario);
                        objPersonal.Nombre = dr.GetValue(pNombre) == DBNull.Value ? default(string) : dr.GetString(pNombre);
                        objPersonal.Activo = dr.GetValue(pEstado) == DBNull.Value ? default(bool) : dr.GetBoolean(pEstado);
                        objPersonal.Email = dr.GetValue(pEmail) == DBNull.Value ? default(string) : dr.GetString(pEmail);
                        lista.Add(objPersonal);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
    }
}
