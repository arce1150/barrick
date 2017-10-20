using System;
using System.Collections.Generic;

namespace Barrick.Pasajes.BusinessEntity
{
    public class PasajesTerrestresBe
    {
        public int Idregistro { get; set; }
        public DateTime Fecha { get; set; }
        public int Idcc { get; set; }
        public int Cuenta { get; set; }
        public string Job { get; set; }
        public int Empresa { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime HorSalida { get; set; }
        public DateTime? FechaRetorno { get; set; }
        public DateTime? HorRetorno { get; set; }
        public string Motivo { get; set; }
        public string Solicitante { get; set; }
        public string MailSolicitante { get; set; }
        public string CodigoJefe { get; set; }
        public string CodigoContabilidad { get; set; }
        public string SolicitanteLogin { get; set; }
        public string SolicitanteNombre { get; set; }
        public string SolicitanteEmail { get; set; }
        public string AprobadorLogin { get; set; }
        public string AprobadorNombre { get; set; }
        public string AprobadorEmail { get; set; }
        public int Aprobado { get; set; }
        public int Condicion { get; set; }
        public string Nota { get; set; }
        public string FechaSalidaString { get; set; }
        public string HoraSalidaString { get; set; }
        public string FechaRetornoString { get; set; }
        public string HoraRetornoString { get; set; }
        public List<DetallePasajesBe> detallePasaje { get; set; }
        public string Seccion { get; set; }
        public string Area { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string LoginJefe { get; set; }
        public string Jefe { get; set; }
        public string MailJefe { get; set; }
        public string LoginConta { get; set; }
        public string NombreConta { get; set; }
        public string MailConta { get; set; }
        public string AprobacionConta { get; set; }
        public int Costo { get; set; }
    }
}
