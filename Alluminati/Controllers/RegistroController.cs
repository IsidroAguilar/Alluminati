using Alluminati.Helper;
using Alluminati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Alluminati.Controllers
{
    public class RegistroController : Controller
    {
        private AlluminatiEntities db = new AlluminatiEntities();

        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegistroUsuario(tblUsuarios usuarioAlluminati)
        {
            // Proceso de Registro
            usuarioAlluminati.Contrasena = Encripcion.SHA256Encrypt(usuarioAlluminati.Contrasena);
            if (db.tblUsuarios.FirstOrDefault(u => u.Usuario == usuarioAlluminati.Usuario && u.Contrasena == usuarioAlluminati.Contrasena && u.Estatus == 1) != null)
            {
                usuarioAlluminati.Estatus = 1;
                db.tblUsuarios.Add(usuarioAlluminati);
                db.SaveChanges();
                return Json(new { result = true, url = Url.Action("Index", "Login") });
            }
            else
            {
                return Json(new { result = false });
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}