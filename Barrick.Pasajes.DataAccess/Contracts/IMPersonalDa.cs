using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.DataAccess.Contracts
{
    public interface IMPersonalDa
    {
        int cambiarContrasenia ( SqlConnection con, string contrasenia,string usuario );
        MPersonalBe ListarPersonalPorUsuario ( SqlConnection con,string usuario);
        MPersonalBe validarLoguin ( SqlConnection con, string usuario,string contrasenia );
        List<MPersonalBe> listarPersonalAprobador ( SqlConnection con );
    }
}
