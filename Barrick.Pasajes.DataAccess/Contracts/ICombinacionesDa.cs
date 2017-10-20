using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface ICombinacionesDa
    {
        List<CombinacionesBe> listarCombincionesPorArea (System.Data.SqlClient.SqlConnection con,int idarea );
    }
}
