using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ApiCafe.Areas.Turma.Controllers
{
    public class TurmaController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(int id)
        {
            if (id <= 0)
            {
                
            }

            List<Core.Entidades.Turma> retorno = new List<Core.Entidades.Turma>();

            using (var conn = new Core.Infra.DapperConnection())
            {
                var dapper = conn.Conectar();
                retorno = dapper.GetAll<Core.Entidades.Turma>().Where(x => x.IdUsuarioCriador == id).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, retorno, Configuration.Formatters.JsonFormatter);
        }
    }
}
