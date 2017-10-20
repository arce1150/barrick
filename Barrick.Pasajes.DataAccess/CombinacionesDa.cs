using System;
using System.Collections.Generic; 
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.DataAccess
{
    public class CombinacionesDa : ICombinacionesDa
    {
        public List<CombinacionesBe> listarCombincionesPorArea ( System.Data.SqlClient.SqlConnection con, int idarea )
        {
            List<CombinacionesBe> lista = null;
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("USP_PA_LISTAR_COMBINACIONES_POR_AREA", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new System.Data.SqlClient.SqlParameter("@idArea" , System.Data.SqlDbType.Int) {Value =  (object)idarea ?? DBNull.Value ,Direction = System.Data.ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pIdCombinacion = dr.GetOrdinal("idCombinacion");
                    int pDescripcion = dr.GetOrdinal("Descripcion");
                    int pIdArea = dr.GetOrdinal("idArea"); 
                    CombinacionesBe objCostos = null;
                    lista = new List<CombinacionesBe>();
                    while (dr.Read())
                    {
                        objCostos = new CombinacionesBe();
                        objCostos.IdCombinacion = dr.GetValue(pIdCombinacion) == DBNull.Value ? default(int) : dr.GetInt32(pIdCombinacion);
                        objCostos.Descripcion = dr.GetValue(pDescripcion) == DBNull.Value ? default(string) : dr.GetString(pDescripcion);
                        objCostos.IdArea = dr.GetValue(pIdArea) == DBNull.Value ? default(int) : dr.GetInt32(pIdArea); 
                        lista.Add(objCostos);
                    }
                    dr.Close();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            return lista;
        }
    }
}
