using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.BusinessLogic
{
    public class PasajesTerrestresBl
    {
        private IPasajesTerrestresDa _PasajesTerrestresDa;
        public PasajesTerrestresBl ( IPasajesTerrestresDa PasajesTerrestresDa )
        {
            _PasajesTerrestresDa = PasajesTerrestresDa;
        }
        public int insertarPasajesTerrestres ( PasajesTerrestresBe pasaje, List<DetallePasajesBe> detallePasaje )
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _PasajesTerrestresDa.insertarPasajesTerrestres(con, pasaje, detallePasaje);
                }
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message,ex.StackTrace);
                throw ex;
            }
        }
        public PasajesTerrestresBe listarPasajesTerrestres (int IdRegistro )
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion)) {
                    con.Open();
                    return _PasajesTerrestresDa.listarPasajesTerrestres(con,IdRegistro);
                }
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message,ex.StackTrace);
                throw ex;
            }
        } 
        public List<PasajesTerrestresBe> paginarPasajesTerrestres ( bool condicion, string mailsolicitante ) {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _PasajesTerrestresDa.paginarPasajesTerrestres(con, condicion, mailsolicitante);
                }
            }
            catch (Exception ex) {
                Error.Log.grabarLog(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}
