using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.Hosting;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.MailService
{
    public class EmailService
    {
        private static readonly string correoAmbiente = ConfigurationManager.AppSettings.Get("ambiente.mail");
        private static readonly string claveAmbiente = ConfigurationManager.AppSettings.Get("ambiente.pass");
        private static readonly string smtpAmbiente = ConfigurationManager.AppSettings.Get("smtp.host");
        private static readonly int puertoSmtp = int.Parse(ConfigurationManager.AppSettings.Get("smtp.port"));
        public static void EnviarCorreo ( string correoDestino, string asunto, PasajesTerrestresBe pasaje, bool esHtml = true )
        {
             
            var msg = new MailMessage();
            msg.To.Add(correoDestino);
            msg.From = new MailAddress(correoAmbiente, "Barrick Notificaciones", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8; 
            msg.Body = CuerpoDelCorreo(pasaje);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = esHtml;
            var client = new SmtpClient(smtpAmbiente, puertoSmtp)
            {
                Credentials = new NetworkCredential(correoAmbiente, claveAmbiente),
                EnableSsl = true
            };
            try
            {
                client.Send(msg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Error.Log.grabarLog(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        public static void EnviarCorreoRecuperacion ( string correoDestino, string asunto, MPersonalBe personal, bool esHtml = true )
        {
            var msg = new MailMessage();
            msg.To.Add(correoDestino);
            msg.From = new MailAddress(correoAmbiente, "Password de Acceso al Sistema de Pasajes", System.Text.Encoding.UTF8);
            msg.Subject = asunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = CuerpoPlantilla2(personal);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = esHtml;
            var client = new SmtpClient(smtpAmbiente, puertoSmtp)
            {
                Credentials = new NetworkCredential(correoAmbiente, claveAmbiente),
                EnableSsl = true
            };
            try
            {
                client.Send(msg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Error.Log.grabarLog(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        private static string CuerpoDelCorreo ( PasajesTerrestresBe pasaje )
        {
            //var fileName = Path.Combine(HostingEnvironment.MapPath(@"~/Plantillas"), "plantilla.html");
            var fileName = ConfigurationManager.AppSettings.Get("correo.plantilla");
            string cuerpoCorreo;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                cuerpoCorreo = reader.ReadToEnd();
                reader.Close();
            }
            cuerpoCorreo = cuerpoCorreo.Replace("{fondo_cabecera}", ConfigurationManager.AppSettings.Get("barrick.fondo"));
            cuerpoCorreo = cuerpoCorreo.Replace("{barrick_blanco}", ConfigurationManager.AppSettings.Get("barrick.logo")); 
            cuerpoCorreo = cuerpoCorreo.Replace("{NRO_REGISTRO}", pasaje.Idregistro.ToString());
            cuerpoCorreo = cuerpoCorreo.Replace("{DIA_HOY}", DateTime.Now.Day.ToString().PadLeft(2, '0'));
            cuerpoCorreo = cuerpoCorreo.Replace("{MES_HOY}", DateTime.Now.ToString("MMMM"));
            cuerpoCorreo = cuerpoCorreo.Replace("{ANIO_HOY}", DateTime.Now.Year.ToString());
            cuerpoCorreo = cuerpoCorreo.Replace("{AREA}", pasaje.Area);
            cuerpoCorreo = cuerpoCorreo.Replace("{SECCION}", pasaje.Seccion);
            cuerpoCorreo = cuerpoCorreo.Replace("{JOB}", pasaje.Job);
            cuerpoCorreo = cuerpoCorreo.Replace("{CUENTA}", pasaje.Cuenta.ToString());
            cuerpoCorreo = cuerpoCorreo.Replace("{CANTIDAD_PASAJEROS}", pasaje.detallePasaje.Count.ToString());
            cuerpoCorreo = cuerpoCorreo.Replace("{NOMBRE_EMPRESA}", pasaje.NombreEmpresa.ToString());
            var trRutaSihayFechaRetorno = string.Empty;
            if (pasaje.FechaRetorno == null && pasaje.HorRetorno != null)
            {
                trRutaSihayFechaRetorno =string.Format("<tr><td colspan=2 Width='50%' align='left' bgcolor='white'><font color='#4b0082'>{0} - {1} </font></td>", pasaje.Origen, pasaje.Destino);
            }
            cuerpoCorreo = cuerpoCorreo.Replace("{RUTA_ORIGEN_SI_HAY_FECHA_RETORNO}", trRutaSihayFechaRetorno);
            cuerpoCorreo = cuerpoCorreo.Replace("{ORIGEN}", pasaje.Origen);
            cuerpoCorreo = cuerpoCorreo.Replace("{DESTINO}", pasaje.Destino);
            cuerpoCorreo = cuerpoCorreo.Replace("{ORIGEN}", pasaje.Origen);
            cuerpoCorreo = cuerpoCorreo.Replace("{DIA_SALIDA}", pasaje.FechaSalida.Day.ToString().PadLeft(2,'0'));
            cuerpoCorreo = cuerpoCorreo.Replace("{MES_SALIDA}", pasaje.FechaSalida.Month.ToString().PadLeft(2, '0'));
            cuerpoCorreo = cuerpoCorreo.Replace("{ANIO_SALIDA}", pasaje.FechaSalida.Year.ToString());
            cuerpoCorreo = cuerpoCorreo.Replace("{HORA_SALIDA}", pasaje.FechaSalida.Hour.ToString());
            if (pasaje.FechaRetorno.Value != null)
            {
                cuerpoCorreo = cuerpoCorreo.Replace("{DIA_RETORNO1}", pasaje.FechaRetorno.Value.Day.ToString().PadLeft(2, '0'));
                cuerpoCorreo = cuerpoCorreo.Replace("{MES_RETORNO1}", pasaje.FechaRetorno.Value.Month.ToString().PadLeft(2, '0'));
                cuerpoCorreo = cuerpoCorreo.Replace("{ANIO_RETORNO1}", pasaje.FechaRetorno.Value.Year.ToString());
                cuerpoCorreo = cuerpoCorreo.Replace("{HORA_RETORNO1}", pasaje.FechaRetorno.Value.Hour.ToString());
            }
            else
            {
                cuerpoCorreo = cuerpoCorreo.Replace("{DIA_RETORNO1}", string.Empty);
                cuerpoCorreo = cuerpoCorreo.Replace("{MES_RETORNO1}", string.Empty);
                cuerpoCorreo = cuerpoCorreo.Replace("{ANIO_RETORNO1}", string.Empty);
                cuerpoCorreo = cuerpoCorreo.Replace("{HORA_RETORNO1}", string.Empty);
            }
            cuerpoCorreo = cuerpoCorreo.Replace("{MOTIVO}", pasaje.Motivo);
            string trPasajero = string.Empty;
            foreach (var item in pasaje.detallePasaje)
            {
                trPasajero += "<tr>";
                trPasajero += string.Format("<td bgcolor='white'><font color='#4b0082'>{0}</font></td>", item.NomPasajero);
                trPasajero += string.Format("<td bgcolor='white'><font color='#4b0082'>{0}</font></td>", item.DocIdentidad);
                trPasajero += "</tr>";
            }
            cuerpoCorreo = cuerpoCorreo.Replace("{FOREACH}", trPasajero);
            cuerpoCorreo = cuerpoCorreo.Replace("{SOLICITADO_POR}", pasaje.Solicitante);
            cuerpoCorreo = cuerpoCorreo.Replace("{APROBADO_POR}", pasaje.Jefe);
            cuerpoCorreo = cuerpoCorreo.Replace("{APROBACION_AUTOMATICA}", pasaje.NombreConta);
            cuerpoCorreo = cuerpoCorreo.Replace("{NOTA}", pasaje.Nota);
            return cuerpoCorreo;
        }
        private static string CuerpoPlantilla2 ( MPersonalBe personal ) {
            var fileName = ConfigurationManager.AppSettings.Get("correo.plantilla2");
            string cuerpoCorreo=string.Empty;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                cuerpoCorreo = reader.ReadToEnd();
                reader.Close();
            }
            cuerpoCorreo = cuerpoCorreo.Replace("{NOMBRE_USUARIO}", personal.Nombre);
            cuerpoCorreo = cuerpoCorreo.Replace("{USUARIO}", personal.Usuario);
            cuerpoCorreo = cuerpoCorreo.Replace("{CONTRASENIA}", personal.Contrasenia);
            cuerpoCorreo = cuerpoCorreo.Replace("{CORREO}", personal.Email); 
            cuerpoCorreo = cuerpoCorreo.Replace("{URL_HOME_LOGIN}", System.Web.HttpContext.Current.Request.Url.Host+ "/Home/login");
            return cuerpoCorreo;
        }
    }
}
