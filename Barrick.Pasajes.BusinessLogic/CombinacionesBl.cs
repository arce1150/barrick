using System;
using System.Collections.Generic;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.BusinessLogic
{
    public class CombinacionesBl
    {
        public ICombinacionesDa _CombinacionesDa;
        public CombinacionesBl ( ICombinacionesDa CombinacionesDa )
        {
            _CombinacionesDa = CombinacionesDa;
        }
        public List<CombinacionesBe> listarCombincionesPorArea ( int idarea )
        {
            List<CombinacionesBe> lista = null;
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    lista = _CombinacionesDa.listarCombincionesPorArea(con, idarea);
                }
            }
            catch (Exception ex)
            {

            }

            return lista;
        }
    }
}
