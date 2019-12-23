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
        public List<Aluno> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = File.ReadAllText(caminhoArquivo);
            List<Aluno> listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);
            return listaAlunos;
        }


        public List<Aluno> ListarAlunosDB()
        {

            string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
            IDbConnection conexao;
            conexao = new SqlConnection(stringConexao);
            conexao.Open();     

            var listaAlunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM alunos";

            IDataReader resultado = selectCmd.ExecuteReader();

            while (resultado.Read())
            {
                var alu = new Aluno();
                alu.id = Convert.ToInt32(resultado["Id"]);
                alu.nome = Convert.ToString(resultado["nome"]);
                alu.sobrenome = Convert.ToString(resultado["sobrenome"]);
                alu.ra = Convert.ToInt32(resultado["ra"]);

                listaAlunos.Add(alu);

            }

            conexao.Close();


            return listaAlunos;
        }

        public bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;
        }

        public Aluno Inserir(Aluno Aluno)
        {
            List<Aluno> listaAlunos = this.ListarAlunos();

            int maxId = listaAlunos.Max(x => x.id);
            Aluno.id = maxId + 1;
            listaAlunos.Add(Aluno);
            ReescreverArquivo(listaAlunos);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            List<Aluno> listaAlunos = this.ListarAlunos();

            int itemIndex = listaAlunos.FindIndex(x => x.id == id);
            if (itemIndex >= 0)
            {
                Aluno.id = id;
                listaAlunos[itemIndex] = Aluno;
            }
            else
            {
                return null;
            }
            ReescreverArquivo(listaAlunos);
            return Aluno;
        }

        public bool Deletar(int id)
        {
            List<Aluno> listaAlunos = this.ListarAlunos();

            int itemIndex = listaAlunos.FindIndex(x => x.id == id);
            if (itemIndex >= 0)
            {

                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }
            ReescreverArquivo(listaAlunos);
            return true;
        }

    }
}