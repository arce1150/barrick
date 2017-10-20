using System;
using System.Collections.Generic;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.BusinessLogic
{
    public class EmpresasBl
    {
        private IEmpresasDa _IEmpresasDa;
        public EmpresasBl ( IEmpresasDa IEmpresasDa ) {
            _IEmpresasDa = IEmpresasDa;
        }
        public List<EmpresasBe> listarEmpresas ( ) {
            try
            {
                using (var con=new System.Data.SqlClient.SqlConnection(SettingsBl.CadenaDeConexion)) {
                    con.Open();
                    return _IEmpresasDa.listarEmpresas(con);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
