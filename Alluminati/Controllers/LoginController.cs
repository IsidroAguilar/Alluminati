using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alluminati.Models;
using Alluminati.Helper;
using System.Web.Security;

namespace Alluminati.Controllers
{
    public class LoginController : Controller
    {
        private AlluminatiEntities db = new AlluminatiEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ingresar(tblUsuarios usuarioAlluminati)
        {
            // Proceso de login.
            usuarioAlluminati.Contrasena = Encripcion.SHA256Encrypt(usuarioAlluminati.Contrasena);
            if (db.tblUsuarios.FirstOrDefault(u => u.Usuario == usuarioAlluminati.Usuario && u.Contrasena == usuarioAlluminati.Contrasena && u.Estatus == 1) != null)
            {
                // Los datos de acceso son correctos, ingresa una entrada en el log.
                tblLogin login = new tblLogin();
                usuarioAlluminati = db.tblUsuarios.FirstOrDefault(u => u.Usuario == usuarioAlluminati.Usuario);
                login.IdUsuario = usuarioAlluminati.IdUsuario;
                login.FechaHora = DateTime.Now;
                db.tblLogin.Add(login);
                db.SaveChanges();
                // Autentica al usuario y lo almacena en sesión.
                FormsAuthentication.SetAuthCookie(usuarioAlluminati.Usuario, false);
                Session["Usuario"] = usuarioAlluminati;
                return Json(new { result = true, url = Url.Action("Index", "Home") });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        public ActionResult Logout()
        {
            // Proceso de logout.
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}