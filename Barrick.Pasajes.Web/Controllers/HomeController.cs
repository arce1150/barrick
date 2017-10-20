using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barrick.Pasajes.DataAccess.Contracts;
using Barrick.Pasajes.BusinessLogic;
using Barrick.Pasajes.BusinessEntity;
namespace Barrick.Pasajes.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor
        private IDaUsuario _daUsuario;
        private IMPersonalDa _MPersonalDa;
        public HomeController ( IDaUsuario daUsuario, IMPersonalDa MPersonalDa )
        {
            _daUsuario = daUsuario;
            _MPersonalDa = MPersonalDa;
        }
        #endregion

        public ActionResult Index ( )
        {
            return View();
        }
        public PartialViewResult Superior ( string pagina )
        {
            ViewBag.Pagina = pagina;
            return PartialView();
        }
        public ActionResult Login ( string txtUser, string txtPassword )
        {
            var Vali = string.Empty;
            if (txtUser == null && txtPassword == null)
            {
                ViewBag.Vali = Vali;
            }
            else
            {
                var bl = new MPersonalBl(_MPersonalDa);
                var persona = bl.validarLoguin(txtUser, txtPassword);
                if (persona != null)
                {
                    if (persona.Usuario.Trim() == txtUser.Trim() && persona.Activo == true)
                    {
                        Session["LoginUsuario"] = persona.Usuario;
                        Session["n_nombre"] = persona.Nombre;
                        Session["n_email"] = persona.Email;
                        return RedirectToAction("Index", "Solicitud");

                    }
                    else Vali = "1";
                }
                else Vali = "2";
            }
            ViewBag.Vali = Vali;
            return View();
        }

        public ActionResult CambiarPassword ( string user )
        {
            ViewBag.n_nombre = string.Empty;
            if (Request.HttpMethod == "GET")
            {
                var bl = new MPersonalBl(_MPersonalDa);
                var persona = bl.ListarPersonalPorUsuario(user);
                if (persona != null)
                    ViewBag.n_nombre = persona.Nombre;
            }

            return View();
        }
        public JsonResult CambiarContrasenia ( )
        {
            string contrasenia = Request.Form.Get("contrasenia");
            string usuario = Request.Form.Get("usuario");
            var bl = new MPersonalBl(_MPersonalDa);
            var filasAfectadas = bl.cambiarContrasenia(contrasenia, usuario);
            var json = Json(new { filasAfectadas = filasAfectadas }, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public ActionResult CerrarSession ( )
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult RecuperarContrasenia (string user )
        {
            MPersonalBe personal = null;
            if (!string.IsNullOrEmpty(user)) {
                personal=new MPersonalBe();
                var bl = new MPersonalBl(_MPersonalDa);
                personal = bl.ListarPersonalPorUsuario(user);
                if (personal != null) {
                    string correoSolicitante = personal.Email.Trim();
                    string asuntoSolicitante = "Password de Acceso al Sistema de Pasajes";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    System.Text.RegularExpressions.Match match = regex.Match(correoSolicitante);
                    if (match.Success)
                    {
                        MailService.EmailService.EnviarCorreoRecuperacion(correoSolicitante, asuntoSolicitante, personal);
                    }
                }   
            }
            return View(personal);
        }
    }
}
