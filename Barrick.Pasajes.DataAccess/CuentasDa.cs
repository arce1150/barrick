using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.DataAccess
{
    public class CuentasDa : ICuentasDa
    {
        public List<CuentasBe> listarCuentas ( SqlConnection con )
        {
            List<CuentasBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_CUENTAS", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pIdCuenta = dr.GetOrdinal("IdCuenta");
                    int pDescripcion = dr.GetOrdinal("descripcion");
                    CuentasBe objCostos = null;
                    lista = new List<CuentasBe>();
                    while (dr.Read())
                    {
                        objCostos = new CuentasBe();
                        objCostos.IdCuenta = dr.GetValue(pIdCuenta) == DBNull.Value ? default(int) : dr.GetInt32(pIdCuenta);
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
