using System;
using System.Collections.Generic;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.BusinessLogic
{
    public class CuentasBl
    {
        private ICuentasDa _CuentasDa;
        public CuentasBl ( ICuentasDa CuentasDa )
        {
            _CuentasDa = CuentasDa;
        }
        public List<CuentasBe> listarCuentas ( )
        {
            List<CuentasBe> lista = null;
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    lista = _CuentasDa.listarCuentas(con);
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
