using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface IAprobadorDa
    {
        List<AprobadorBe> listarAprobadores (System.Data.SqlClient.SqlConnection con,int tipo );
    }
}
