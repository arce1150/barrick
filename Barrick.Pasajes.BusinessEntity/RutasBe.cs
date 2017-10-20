using System; 
namespace Barrick.Pasajes.BusinessEntity
{
    public class RutasBe
    {
        public int IdRutas { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime Hora { get; set; }
        public int IdEmpresa { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public string Nota { get; set; }
        public string HoraString { get; set; }
        public string HoraStringValor { get; set; }
    }
}
