using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string data { get; set; }
        public string telefone { get; set; }
        public int ra { get; set; }
        public List<Aluno> ListarAlunos(int? id = null)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                return alunoDB.ListarAlunosDB(id);

            }
            catch (Exception e)
            {

                throw new Exception($"Erro: {e.Message}");
            }

        }




        public bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;
        }

        public void Inserir(Aluno aluno)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.InserirAlunoDB(aluno);

            }
            catch (Exception e)
            {

                throw new Exception($"Erro: {e.Message}");
            }

        }

        public void Atualizar(Aluno aluno)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.AtualizarAlunoDB(aluno);

            }
            catch (Exception e)
            {

                throw new Exception($"Erro: {e.Message}");
            }

        }

        public void Deletar(int id)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.DeletarAlunoDB(id);

            }
            catch (Exception e)
            {

                throw new Exception($"Erro: {e.Message}");
            }

        }

    }
}