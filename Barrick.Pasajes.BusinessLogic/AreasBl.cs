using System;
using System.Data.SqlClient;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
using System.Collections.Generic;

namespace Barrick.Pasajes.BusinessLogic
{
    public class AreasBl
    {
        public IAreasDa _AreasDa;
        public AreasBl ( IAreasDa AreasDa )
        {
            _AreasDa = AreasDa;
        }
        public List<AreasBe> listarTodasLasAreas ( )
        {
            List<AreasBe> lista = null;
            try
            {
                using (SqlConnection con = new SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    lista = _AreasDa.listarTodasLasAreas(con);
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
