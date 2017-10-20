using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
using System.Data;

namespace Barrick.Pasajes.DataAccess
{
    public class AreasDa : IAreasDa
    {
        public List<AreasBe> listarTodasLasAreas ( SqlConnection con )
        {
            List<AreasBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_TODAS_LAS_AREAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pIdArea = dr.GetOrdinal("idarea");
                    int pDescripcion = dr.GetOrdinal("descripcion");
                    AreasBe objArea = null;
                    lista = new List<AreasBe>();
                    while (dr.Read())
                    {
                        objArea = new AreasBe();
                        objArea.Idarea = dr.GetValue(pIdArea) == DBNull.Value ? default(int) : dr.GetInt32(pIdArea);
                        objArea.Descripcion = dr.GetValue(pDescripcion) == DBNull.Value ? default(string) : dr.GetString(pDescripcion);
                        lista.Add(objArea);
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
