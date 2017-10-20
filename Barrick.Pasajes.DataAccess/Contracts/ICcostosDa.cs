using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface ICcostosDa
    {
        List<CcostosBe> listarCostosPorArea (System.Data.SqlClient.SqlConnection con, int area);
    }
}
