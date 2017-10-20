using System;
namespace Barrick.Pasajes.BusinessEntity
{
    public class MPersonalBe
    {
        public int CodigoInterno { get; set; }
        public string CodigoPersona { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string Email { get; set; }
    }
}
