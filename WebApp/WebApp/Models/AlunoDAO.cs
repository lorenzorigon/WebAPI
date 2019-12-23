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

        public List<Aluno> ListarAlunosDB(int? id)
        {
            try
            {
                var listaAlunos = new List<Aluno>();

                IDbCommand selectCmd = conexao.CreateCommand();

                if (id == null)
                    selectCmd.CommandText = "SELECT * FROM alunos";
                else
                    selectCmd.CommandText = $"SELECT * FROM alunos where id = {id}";

                IDataReader resultado = selectCmd.ExecuteReader();

                while (resultado.Read())
                {
                    var alu = new Aluno()
                    {
                        id = Convert.ToInt32(resultado["Id"]),
                        nome = Convert.ToString(resultado["nome"]),
                        sobrenome = Convert.ToString(resultado["sobrenome"]),
                        ra = Convert.ToInt32(resultado["ra"]),
                    };

                    listaAlunos.Add(alu);
                }

                return listaAlunos;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        public void InserirAlunoDB(Aluno aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO alunos (nome, sobrenome, ra) VALUES (@nome, @sobrenome, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                IDbDataParameter paramRA = new SqlParameter("ra", aluno.ra);

                insertCmd.Parameters.Add(paramNome);
                insertCmd.Parameters.Add(paramSobrenome);
                insertCmd.Parameters.Add(paramRA);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        public void AtualizarAlunoDB(Aluno aluno)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE alunos set nome = @nome, sobrenome = @sobrenome, ra = @ra WHERE id = @id";

                IDbDataParameter paramID = new SqlParameter("id", aluno.id);
                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                IDbDataParameter paramRA = new SqlParameter("ra", aluno.ra);
                updateCmd.Parameters.Add(paramNome);

                updateCmd.Parameters.Add(paramID);
                updateCmd.Parameters.Add(paramSobrenome);
                updateCmd.Parameters.Add(paramRA);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM alunos WHERE id = @id";

                IDbDataParameter paramID = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramID);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}