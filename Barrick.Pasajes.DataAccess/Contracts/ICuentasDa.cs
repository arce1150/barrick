using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface ICuentasDa
    {
        List<CuentasBe> listarCuentas (System.Data.SqlClient.SqlConnection con );
    }
}
