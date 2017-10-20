using System;
namespace Barrick.Pasajes.BusinessEntity
{
    public class AprobadorBe
    {
        public string IdAprobador { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public string NombreCorto { get; set; }
        public int Habilitado { get; set; }
    }
}
