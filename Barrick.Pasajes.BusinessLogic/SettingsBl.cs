using System.ComponentModel;
using System.Configuration;
namespace Barrick.Pasajes.BusinessLogic
{
    public class SettingsBl
    {
        public static string CadenaDeConexion
        {
            get { return ConfigurationManager.ConnectionStrings["conex"].ConnectionString; }
        }
        public enum TipoAprobacion : int
        {
            [Description("Aprobacion automatica")]
            AprobacionAutomatica = 1,
            [Description("Listado de Aprobadores")]
            AprobacionAplicacion = 2
        }
        public enum ConCorreoSinCorreo  {
            ConCorreo = 1,
            SinCorreo = 0
        }
        public enum NroPlantilla : int {
            [Description("Plantilla notificación al jefe y solicitante")]
            Plantilla1=1,
            [Description("Plantilla notificacion de contrasenia")]
            Plantilla2 = 2
        }
    }
}
