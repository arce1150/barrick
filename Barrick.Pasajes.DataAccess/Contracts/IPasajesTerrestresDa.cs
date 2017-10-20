using System;
using System.Collections.Generic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface IPasajesTerrestresDa
    {
        int insertarPasajesTerrestres (System.Data.SqlClient.SqlConnection con,PasajesTerrestresBe pasaje,List<DetallePasajesBe> detallePasaje );
        PasajesTerrestresBe listarPasajesTerrestres ( System.Data.SqlClient.SqlConnection con,int IdRegistro );
        List<PasajesTerrestresBe> paginarPasajesTerrestres ( System.Data.SqlClient.SqlConnection con, bool condicion, string mailsolicitante );
    }
}
