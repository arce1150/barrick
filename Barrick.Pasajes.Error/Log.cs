using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Barrick.Pasajes.Error
{
    public class Log
    {
        public static void grabarLog (string mensaje,string detalle)
        {
            string ruta = ConfigurationManager.AppSettings.Get("rutalogs").ToString();
            LogBe log = new LogBe();
            log.FechaLog = DateTime.Now;
            log.MensajeLog = mensaje;
            log.DetalleLog = detalle;
            PropertyInfo[] propiedades = log.GetType().GetProperties();
            string archivoLog = string.Format("Barrick_Log_{0}{1}{2}.txt", System.DateTime.Now.Year.ToString(), System.DateTime.Now.Month.ToString("00"), System.DateTime.Now.Day.ToString("00"));
            string rutaArchivo = string.Concat(ruta, archivoLog);
            using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
            {
                foreach (var item in propiedades)
                {
                    sw.WriteLine("{0}={1}", item.Name, item.GetValue(log, null));
                }
                sw.WriteLine(new string('-', 50));
            }
        }
    }
}
