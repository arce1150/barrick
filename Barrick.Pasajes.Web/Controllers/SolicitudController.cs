using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessLogic;
using Barrick.Pasajes.BusinessEntity;
using System.Globalization;

namespace Barrick.Pasajes.Web.Controllers
{
    public class SolicitudController : DefaultController
    {
        #region Constructor
        private IAreasDa _AreasDa;
        private ICcostosDa _CcostosDa;
        private ICombinacionesDa _CombinacionesDa;
        private ICuentasDa _CuentasDa;
        private IEmpresasDa _IEmpresasDa;
        private IRutasDa _RutasDa;
        private IAprobadorDa _AprobadorDa;
        private IPasajesTerrestresDa _PasajesTerrestresDa;
        private IMPersonalDa _MPersonalDa;
        public SolicitudController ( IAreasDa AreasDa, ICcostosDa CcostosDa, ICombinacionesDa CombinacionesDa, ICuentasDa CuentasDa,
            IEmpresasDa IEmpresasDa, IRutasDa RutasDa, IAprobadorDa AprobadorDa, IPasajesTerrestresDa PasajesTerrestresDa,
            IMPersonalDa MPersonalDa )
        {
            _AreasDa = AreasDa;
            _CcostosDa = CcostosDa;
            _CombinacionesDa = CombinacionesDa;
            _CuentasDa = CuentasDa;
            _IEmpresasDa = IEmpresasDa;
            _RutasDa = RutasDa;
            _AprobadorDa = AprobadorDa;
            _PasajesTerrestresDa = PasajesTerrestresDa;
            _MPersonalDa = MPersonalDa;
        }
        #endregion
        const int registrosPorPagina =20;
        public ActionResult Index ( )
        {
            return View();
        }

        public JsonResult listarTodasLasSedes ( )
        {
            var bl = new AreasBl(_AreasDa);
            var lista = bl.listarTodasLasAreas();
            var json = Json(new { listadoSedes = lista }, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult listarTodasLasAreas ( int area )
        {
            var bl = new CcostosBl(_CcostosDa);
            var lista = bl.listarCostosPorArea(area);
            var json = Json(new { listadoAreas = lista }, JsonRequestBehavior.AllowGet);
            return json;
        }
        public JsonResult listarCostos ( int idarea )
        {
            var bl = new CombinacionesBl(_CombinacionesDa);
            var lista = bl.listarCombincionesPorArea(idarea);
            var json = Json(new { listadoCostos = lista }, JsonRequestBehavior.AllowGet);
            return json;
        }
        public JsonResult listarCuenta ( )
        {
            var bl = new CuentasBl(_CuentasDa);
            var lista = bl.listarCuentas();
            var json = Json(new { listadoCuenta = lista }, JsonRequestBehavior.AllowGet);
            return json;
        }
        public JsonResult listarEmpresas ( )
        {
            var bl = new EmpresasBl(_IEmpresasDa);
            var lista = bl.listarEmpresas();
            var json = Json(new { listadoEmpresas = lista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult listarRutasDeOrigen ( int idempresa )
        {
            var bl = new RutasBl(_RutasDa);
            var lista = bl.listarRutasPorEmpresas(idempresa);
            var json = Json(new { listaRutaOrigen = lista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult listarRutasDeDestino ( string origen, int idempresa )
        {
            var bl = new RutasBl(_RutasDa);
            var lista = bl.listarDestinos(origen, idempresa);
            var json = Json(new { listaRutaDestino = lista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult listarHorarioDeSalida ( string origen, string destino, int idempresa )
        {
            var bl = new RutasBl(_RutasDa);
            var lista = bl.listarHorarioDeSalida(origen, destino, idempresa);
            var json = Json(new { listaHoraSalida = lista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult listarAprobadores ( )
        {
            var bl = new AprobadorBl(_AprobadorDa);
            var lista = bl.listarAprobadores((int)SettingsBl.TipoAprobacion.AprobacionAplicacion);
            var lista2 = bl.listarAprobadores((int)SettingsBl.TipoAprobacion.AprobacionAutomatica);
            var json = Json(new { listaAprobadores = lista, listaAprobadoAutomatico = lista2 }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult insertarPasajes ( string pasajeString, string DetalleString )
        {
            //var pasaje = Request.Form.Get("pasaje");
            //var detallePasaje= Request.Form.Get("detallePasaje"); 
            //PasajesTerrestresBe pasaje, List< DetallePasajesBe > detallePasaje
            var bl = new PasajesTerrestresBl(_PasajesTerrestresDa);
            try
            {
                int IdRegistro = 0;
                string[] objPasaje = pasajeString.Split('|');
                string[] filaString = DetalleString.Split('#');
                var correoSolitante=HttpContext.Session["n_email"];
                PasajesTerrestresBe pasaje = new PasajesTerrestresBe();
                DetallePasajesBe objDetalle = null;
                List<DetallePasajesBe> listaDetallePasaje = new List<DetallePasajesBe>();
                pasaje.Idcc = int.Parse(objPasaje[0]);
                pasaje.Cuenta = int.Parse(objPasaje[1]);
                pasaje.Job = objPasaje[2].ToString().Trim();
                pasaje.Empresa = int.Parse(objPasaje[3].ToString().Trim());
                pasaje.Origen = objPasaje[4].ToString().Trim();
                pasaje.Destino = objPasaje[5].ToString().Trim();
                pasaje.FechaSalida = DateTime.Parse(objPasaje[6].ToString()); //DateTime date1 = new DateTime(2008, 8, 29, 19, 27, 15);CultureInfo.CreateSpecificCulture("en-US")
                pasaje.HorSalida = DateTime.Parse(objPasaje[7].ToString().Trim());
                if (bool.Parse(objPasaje[18].ToString()) == false)
                {
                    pasaje.FechaRetorno = null;
                    pasaje.HorRetorno = null;
                }
                else
                {
                    pasaje.FechaRetorno = DateTime.Parse(objPasaje[8].ToString().Trim());
                    pasaje.HorRetorno = DateTime.Parse(objPasaje[9].ToString().Trim());
                }
                pasaje.Motivo = objPasaje[10].ToString().Trim();
                pasaje.Solicitante = objPasaje[11].ToString().Trim();
                pasaje.MailSolicitante = correoSolitante!=null? correoSolitante.ToString():null ;
                pasaje.CodigoJefe = objPasaje[13].ToString().Trim();
                pasaje.CodigoContabilidad = objPasaje[14].ToString().Trim();
                pasaje.Aprobado = int.Parse(objPasaje[15].ToString().Trim());
                pasaje.Condicion = int.Parse(objPasaje[16].ToString().Trim());
                pasaje.Nota = objPasaje[17].ToString().Trim();
                for (var i = 0; i < filaString.Length; i++)
                {
                    string[] columna = filaString[i].Split('|');
                    objDetalle = new DetallePasajesBe();
                    for (var j = 0; j < columna.Length; j++)
                    {
                        objDetalle.NomPasajero = columna[0];
                        objDetalle.DocIdentidad = columna[1];
                        j++;
                    }
                    listaDetallePasaje.Add(objDetalle);
                }
                
                #region Notificar al Solicitante y Jefe
                IdRegistro = bl.insertarPasajesTerrestres(pasaje, listaDetallePasaje);
                var PasajeTerrestre = bl.listarPasajesTerrestres(IdRegistro);
                var correoSolicitante = PasajeTerrestre.MailSolicitante;
                var correoJefe = PasajeTerrestre.MailJefe;
                var asuntoSolicitante = string.Format("Solicitud de Pasajes Terrestre Nº{0}", PasajeTerrestre.Idregistro);
                var asuntoAlJefe =string.Format("Solicitud de Pasajes Terrestre Nº{0} - Evaluación", PasajeTerrestre.Idregistro);
                MailService.EmailService.EnviarCorreo(correoSolicitante, asuntoSolicitante, PasajeTerrestre );
                MailService.EmailService.EnviarCorreo(correoJefe, asuntoAlJefe, PasajeTerrestre);
                #endregion

                var json = Json(new { PasajeInsertado = IdRegistro }, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
            catch (Exception ex)
            {
                Error.Log.grabarLog(ex.Message,ex.StackTrace);
                throw ex;
            }
        }
        public ActionResult listadoSolicitud ( ) {
            var EmailAprobador = HttpContext.Session["n_email"].ToString();
            var bl = new PasajesTerrestresBl(_PasajesTerrestresDa);
            var blAprobadores = new MPersonalBl(_MPersonalDa);
            int totalRegistros = 0;
            int nroPaginas = 0;
            var aprovadores = blAprobadores.listarPersonalAprobador().Where(x => x.Email == EmailAprobador).Count();
            List<PasajesTerrestresBe> lista = new List<PasajesTerrestresBe>();
            if (aprovadores > 0)
            {
                lista = bl.paginarPasajesTerrestres(Convert.ToBoolean(SettingsBl.ConCorreoSinCorreo.ConCorreo), EmailAprobador);
            }
            else
            {
                lista = bl.paginarPasajesTerrestres(Convert.ToBoolean(SettingsBl.ConCorreoSinCorreo.SinCorreo), EmailAprobador);
            }
            totalRegistros = lista.Count();
            nroPaginas = totalRegistros / registrosPorPagina;
            TempData["lista"] = lista;
            if (nroPaginas % registrosPorPagina == 0)
            {
                nroPaginas--;
            }
            var pagina = new List<PasajesTerrestresBe>();
            for (int i = 0; i < registrosPorPagina; i++)
            {
                if (i < lista.Count()) pagina.Add(lista[i]);
                else break;
            }
            ViewBag.nroPaginas = nroPaginas;
            return View();
        }
        public ActionResult vistaPreviaSolicitud (string codigo ) {
            PasajesTerrestresBe pasaje = null;
            if (codigo != null) {
                var bl = new PasajesTerrestresBl(_PasajesTerrestresDa);
                pasaje = bl.listarPasajesTerrestres(int.Parse(codigo));
            }
            return View(pasaje);
        }
        public JsonResult paginarPasajes (int indicePagina )
        {
            indicePagina--;
            var lista = (List<PasajesTerrestresBe>)TempData["lista"];
            TempData["lista"] = lista;
            List<PasajesTerrestresBe> pagina = new List<PasajesTerrestresBe>();
            int inicio = indicePagina * registrosPorPagina;
            int fin = inicio + registrosPorPagina;
            for (int i = inicio; i < fin; i++)
            {
                if (i < lista.Count()) pagina.Add(lista[i]);
                else break;
            }

            var json = Json(new { listaPaginacion = pagina }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

    }
}
