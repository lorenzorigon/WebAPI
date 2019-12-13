﻿using System;
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

        // GET: api/Aluno/5
        [HttpGet]
        [Route("Recuperar/{id:int}/{nome}/{sobrenome=cardoso}")]
        public Aluno Get(int id, string nome, string sobrenome)
        {
            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().Where(x => x.id == id).FirstOrDefault();
        }

        // GET: api/Aluno/5
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
            catch(Exception ex)
            {
                return InternalServerError(ex); 
            }

           
        }

        // POST: api/Aluno
        public List<Aluno> Post([FromBody]Aluno aluno)
        {
            Aluno _aluno = new Aluno();
            _aluno.Inserir(aluno);

            return _aluno.ListarAlunos();
        }

        // PUT: api/Aluno/5
        public void Put(int id, [FromBody]Aluno aluno)
        {
            Aluno _aluno = new Aluno();
            _aluno.Atualizar(id, aluno);
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Aluno _aluno = new Aluno();
            _aluno.Deletar(id);
        }
    }
}