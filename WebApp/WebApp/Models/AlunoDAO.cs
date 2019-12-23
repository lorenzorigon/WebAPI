using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Models
{
    public class AlunoDAO
    {
        private string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
        private IDbConnection conexao;
        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Aluno> ListarAlunosDB()
        {



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

        public void InserirAlunoDB(Aluno aluno)
        {
            IDbCommand insertCmd = conexao.CreateCommand();
            insertCmd.CommandText = "INSERT INTO alunos (nome, sobrenome, ra) VALUES (@nome, @sobrenome, @ra)";

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
            insertCmd.Parameters.Add(paramNome);

            IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
            insertCmd.Parameters.Add(paramSobrenome);

            IDbDataParameter paramRA = new SqlParameter("ra", aluno.ra);
            insertCmd.Parameters.Add(paramRA);

            insertCmd.ExecuteNonQuery();
        }
    }
}