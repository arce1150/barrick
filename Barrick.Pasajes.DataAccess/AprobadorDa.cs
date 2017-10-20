using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
using System.Data;

namespace Barrick.Pasajes.DataAccess
{
    public class AprobadorDa : IAprobadorDa
    {
        public List<AprobadorBe> listarAprobadores ( SqlConnection con, int tipo )
        {
            List<AprobadorBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_APROBADORES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@tipo" , SqlDbType.Int) {Value =  (object)tipo ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    int pIdAprobador = dr.GetOrdinal("idaprobador");
                    int pNombre = dr.GetOrdinal("nombre");
                    int pUsuario = dr.GetOrdinal("usuario");
                    int pEmail = dr.GetOrdinal("email");
                    int pTipo = dr.GetOrdinal("tipo");
                    int pNombreCorto = dr.GetOrdinal("nombrecorto");
                    int pHabilitado = dr.GetOrdinal("habilitado");
                    AprobadorBe objAprobador = null;
                    lista = new List<AprobadorBe>();
                    while (dr.Read())
                    {
                        objAprobador = new AprobadorBe();
                        objAprobador.IdAprobador = dr.GetValue(pIdAprobador) == DBNull.Value ? default(string) : dr.GetString(pIdAprobador);
                        objAprobador.Nombre = dr.GetValue(pNombre) == DBNull.Value ? default(string) : dr.GetString(pNombre);
                        objAprobador.Login= dr.GetValue(pUsuario) == DBNull.Value ? default(string) : dr.GetString(pUsuario);
                        objAprobador.Email= dr.GetValue(pEmail) == DBNull.Value ? default(string) : dr.GetString(pEmail);
                        objAprobador.Tipo= dr.GetValue(pTipo) == DBNull.Value ? default(int) : dr.GetInt32(pTipo);
                        objAprobador.NombreCorto= dr.GetValue(pNombreCorto) == DBNull.Value ? default(string) : dr.GetString(pNombreCorto);
                        objAprobador.Habilitado = dr.GetValue(pHabilitado) == DBNull.Value ? default(int) : dr.GetInt32(pHabilitado);
                        lista.Add(objAprobador);
                    }
                    dr.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return lista;
        }
    }
}
