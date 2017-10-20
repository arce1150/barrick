using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.BusinessLogic
{
    public class RutasBl
    {
        private IRutasDa _RutasDa;
        public RutasBl ( IRutasDa RutasDa )
        {
            _RutasDa = RutasDa;
        }
        public List<RutasBe> listarRutasPorEmpresas ( int idempresa )
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _RutasDa.listarRutasPorEmpresas(con, idempresa);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RutasBe> listarDestinos ( string origen, int idempresa )
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _RutasDa.listarDestinos(con, origen, idempresa);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RutasBe> listarHorarioDeSalida ( string origen, string destino, int idempresa )
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _RutasDa.listarHorarioDeSalida(con, origen, destino, idempresa);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
