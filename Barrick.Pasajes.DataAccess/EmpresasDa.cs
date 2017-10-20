using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.DataAccess
{
    public class EmpresasDa : IEmpresasDa
    {
        public List<EmpresasBe> listarEmpresas ( SqlConnection con )
        {
            List<EmpresasBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_EMPRESAS", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pIdEmpresa = dr.GetOrdinal("IdEmpresa");
                    int pDescripcion = dr.GetOrdinal("descripcion");
                    EmpresasBe objCostos = null;
                    lista = new List<EmpresasBe>();
                    while (dr.Read())
                    {
                        objCostos = new EmpresasBe();
                        objCostos.IdEmpresa = dr.GetValue(pIdEmpresa) == DBNull.Value ? default(int) : dr.GetInt32(pIdEmpresa);
                        objCostos.Descripcion = dr.GetValue(pDescripcion) == DBNull.Value ? default(string) : dr.GetString(pDescripcion); 
                        lista.Add(objCostos);
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
