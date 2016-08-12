using ApiCafe.Areas.Turma.Models;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ApiCafe.Areas.Turma.Controllers
{
    public class TurmaPontuacaoController : ApiController
    {
        // GET: api/TurmaPontuacao
        public HttpResponseMessage Alterar(int id, [ModelBinder]List<TurmaPontuacaoModel> turmaPontuacao)
        {
            IEnumerable<Core.Entidades.TurmaPontuacao> turma;

            using (var conn = new Core.Infra.DapperConnection())
            {
                var dapper = conn.Conectar();
                turma = dapper.GetAll<Core.Entidades.TurmaPontuacao>().Where(x => x.IdTurma == id).OrderBy(x => x.DataUltimaAlteracao).OrderBy(x => x.Pontuacao);
            }

            var retorno = PreencherEntidade(turma, turmaPontuacao);

            return Request.CreateResponse(HttpStatusCode.OK, retorno, Configuration.Formatters.JsonFormatter);
        }

        private IEnumerable<Core.Entidades.TurmaPontuacao> PreencherEntidade (IEnumerable<Core.Entidades.TurmaPontuacao> turmaEntidade, List<TurmaPontuacaoModel> turmaPontuacao)
        {
            List<Core.Entidades.TurmaPontuacao> retorno = new List<Core.Entidades.TurmaPontuacao>();

            for (int i = 0; i < turmaPontuacao.Count(); i++)
            {
                var turma = turmaEntidade.Where(x => x.IdTurma == turmaPontuacao[i].IdTurma && x.IdUsuario == turmaPontuacao[i].IdUsuario).FirstOrDefault();
                turma.Pontuacao = turmaPontuacao[i].Pontuacao;

                retorno.Add(turma);
            }

            return retorno;
        }

        // GET: api/TurmaPontuacao/5
        public HttpResponseMessage Get(int id)
        {

            List<Core.Entidades.TurmaPontuacao> retorno = new List<Core.Entidades.TurmaPontuacao>();

            using (var conn = new Core.Infra.DapperConnection())
            {
                var dapper = conn.Conectar();
                retorno = dapper.GetAll<Core.Entidades.TurmaPontuacao>().Where(x => x.IdTurma == id).OrderBy(x => x.DataUltimaAlteracao).OrderBy(x => x.Pontuacao).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, retorno, Configuration.Formatters.JsonFormatter);
        }

        // POST: api/TurmaPontuacao
        public void Post([FromBody]string value, int idTurma)
        {

        }

        // PUT: api/TurmaPontuacao/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TurmaPontuacao/5
        public void Delete(int id)
        {
        }
    }
}
