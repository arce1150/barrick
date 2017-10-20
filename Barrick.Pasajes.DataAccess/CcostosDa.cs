using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.DataAccess
{
    public class CcostosDa : ICcostosDa
    {
        public List<CcostosBe> listarCostosPorArea ( SqlConnection con, int area )
        {
            List<CcostosBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_COSTOS_POR_AREA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@area" , SqlDbType.Int) {Value =  (object)area ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pIdCostos = dr.GetOrdinal("IdCostos");
                    int pDescripcion = dr.GetOrdinal("Descripcion");
                    int pArea = dr.GetOrdinal("Area");
                    int pHabilitado = dr.GetOrdinal("Habilitado"); 
                    CcostosBe objCostos = null;
                    lista = new List<CcostosBe>();
                    while (dr.Read())
                    {
                        objCostos = new CcostosBe();
                        objCostos.Idcc = dr.GetValue(pIdCostos) == DBNull.Value ? default(int) : dr.GetInt32(pIdCostos);
                        objCostos.Descripcion = dr.GetValue(pDescripcion) == DBNull.Value ? default(string) : dr.GetString(pDescripcion);
                        objCostos.Area = dr.GetValue(pArea) == DBNull.Value ? default(int) : dr.GetInt32(pArea);
                        objCostos.Habilitado = dr.GetValue(pHabilitado) == DBNull.Value ? default(int) : dr.GetInt32(pHabilitado);
                        lista.Add(objCostos);
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
