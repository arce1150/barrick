using System;
using System.Data.SqlClient;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
using System.Collections.Generic;

namespace Barrick.Pasajes.BusinessLogic
{
    public class MPersonalBl
    {
        public IMPersonalDa _PersonalDa;
        public MPersonalBl ( IMPersonalDa PersonalDa )
        {
            _PersonalDa = PersonalDa;
        }
        public int cambiarContrasenia ( string contrasenia, string usuario )
        {
            int filasAfectadas = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    filasAfectadas = _PersonalDa.cambiarContrasenia(con, contrasenia, usuario);
                }
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message, ex.StackTrace);
                throw ex;
            }
            return filasAfectadas;
        }
        public MPersonalBe ListarPersonalPorUsuario ( string usuario )
        {
            MPersonalBe persona = null;
            try
            {
                using (SqlConnection con = new SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    persona = _PersonalDa.ListarPersonalPorUsuario(con, usuario);
                }
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message, ex.StackTrace);
                throw ex;
            }

            return persona;
        }

        public MPersonalBe validarLoguin ( string usuario, string contrasenia )
        {
            MPersonalBe persona = null;
            try
            {
                using (SqlConnection con = new SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    persona = _PersonalDa.validarLoguin(con, usuario, contrasenia);
                }
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message, ex.StackTrace);
                throw ex;
            }
            return persona;
        }
        public List<MPersonalBe> listarPersonalAprobador ( )
        {
            try
            {
                using (var con = new SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _PersonalDa.listarPersonalAprobador(con);
                }
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message,ex.StackTrace);
                throw ex;
            }
        }
    }
}
