using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper.Contrib.Extensions;


namespace ApiCafe.Areas.Usuario.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario/Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AutenticarUsuario(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                return Json("Usuario ou Senha inválidos.", JsonRequestBehavior.AllowGet);
            }

            List<Core.Entidades.Usuario> retorno = new List<Core.Entidades.Usuario>();

            using (var conn = new Core.Infra.DapperConnection())
            {
                var dapper = conn.Conectar();
                retorno = dapper.GetAll<Core.Entidades.Usuario>().Where(x => x.nmlogin == usuario && x.nmsenha == senha).ToList();
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}