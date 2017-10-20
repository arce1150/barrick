using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface IRutasDa
    {
        List<RutasBe> listarRutasPorEmpresas (System.Data.SqlClient.SqlConnection con ,int idempresa);
        List<RutasBe> listarDestinos( System.Data.SqlClient.SqlConnection con, string origen,int idempresa );
        List<RutasBe> listarHorarioDeSalida (System.Data.SqlClient.SqlConnection con, string origen, string destino,int idempresa );
    }
}
