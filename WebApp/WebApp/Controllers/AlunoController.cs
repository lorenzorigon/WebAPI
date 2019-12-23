using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/aluno")]
    public class AlunoController : ApiController
    {

        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAlunos());

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("Recuperar/{id:int}/{nome?}/{sobrenome?}")]
        public Aluno Get(int id, string nome = null, string sobrenome = null)
        {
            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().FirstOrDefault();
        }

        [HttpGet]
        [Route(@"RecuperarPorDataNome/data:regex([0-9]{4}\-[0-9]{2})/{nome:minlength(5)}")]
        public IHttpActionResult Recuperar(string nome, string data)
        {
            try
            {
                Aluno aluno = new Aluno();
                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.data == data || x.nome == nome);

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Inserir(aluno);

                return Ok(_aluno.ListarAlunos());
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }

        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.id = id;
                _aluno.Atualizar(aluno);

                return Ok(_aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Deletar(id);

                return Ok("Deletado com Sucesso!");
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }
    }
}
