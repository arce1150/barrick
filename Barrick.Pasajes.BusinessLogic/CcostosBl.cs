using System;
using System.Data.SqlClient;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
using System.Collections.Generic;
namespace Barrick.Pasajes.BusinessLogic
{
    public class CcostosBl
    {
        public ICcostosDa _CcostosDa;
        public CcostosBl ( ICcostosDa CcostosDa )
        {
            _CcostosDa = CcostosDa;
        }
        public List<CcostosBe> listarCostosPorArea ( int area )
        {
            List<CcostosBe> lista = null;
            try
            {
                using (SqlConnection con = new SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    lista = _CcostosDa.listarCostosPorArea(con, area);
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
