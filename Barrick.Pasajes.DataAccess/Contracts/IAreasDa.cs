using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface IAreasDa
    {
        List<AreasBe> listarTodasLasAreas (System.Data.SqlClient.SqlConnection con );
    }
}
