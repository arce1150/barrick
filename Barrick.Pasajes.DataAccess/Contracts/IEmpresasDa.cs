using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface IEmpresasDa
    {
        List<EmpresasBe> listarEmpresas (System.Data.SqlClient.SqlConnection con );
    }
}
