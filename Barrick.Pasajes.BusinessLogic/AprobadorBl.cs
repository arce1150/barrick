using System;
using System.Collections.Generic;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.BusinessLogic
{
    public class AprobadorBl
    {
        private IAprobadorDa _Aprobador;
        public AprobadorBl ( IAprobadorDa Aprobador )
        {
            _Aprobador = Aprobador;
        }
        public List<AprobadorBe> listarAprobadores ( int tipo )
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion))
                {
                    con.Open();
                    return _Aprobador.listarAprobadores(con, tipo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
