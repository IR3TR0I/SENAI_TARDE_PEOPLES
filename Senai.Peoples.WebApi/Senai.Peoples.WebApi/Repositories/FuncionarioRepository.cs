using System;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.InterFaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository : IFuncionariosRepository
    {
        string StringConexao = "Data source= LAPTOP-L0JSQP3E; initial catalog= T_Peoples; user Id= sa; pwd= 20nov2004";
        public void AtualizarIdCorpo(FuncionariosDomain Funcionario)
        {
            using (var con = new SqlConnection(StringConexao))
            {
                string QueryUpdateIdUrl = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @ID";

                using (var cmd = new SqlCommand(QueryUpdateIdUrl, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", Funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Funcionario.Sobrenome);
                    cmd.Parameters.AddWithValue("@ID", Funcionario.IdFuncionario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int id, FuncionariosDomain Funcionario)
        {
            using (var con = new SqlConnection(StringConexao))
            {
                string QueryUpdateIdUrl = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @ID";

                using (var cmd = new SqlCommand(QueryUpdateIdUrl, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", Funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Funcionario.Sobrenome);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FuncionariosDomain BuscarPorId(int id)
        {
            using (var con = new SqlConnection(StringConexao))
            {
                string QuerySelectById = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios WHERE IdFuncionario = @ID";

                con.Open();
                using (var cmd = new SqlCommand(QuerySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionariosDomain FunBuscado = new FuncionariosDomain()
                        {
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        return FunBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(FuncionariosDomain Funcionario)
        {
            using (var con = new SqlConnection(StringConexao))
            {
                string QueryCreate = "INSERT INTO Funcionarios(Nome,Sobrenome) VALUES(@Nome,@Sobrenome)";

                using (var cmd = new SqlCommand(QueryCreate, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", Funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Funcionario.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Deletar(int id)
        {
            using (var con = new SqlConnection(StringConexao))
            {
                string QueryDelete = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";

                using (var cmd = new SqlCommand(QueryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FuncionariosDomain> ListarTodos()
        {
            List<FuncionariosDomain> FuncionariosLista = new List<FuncionariosDomain>();

            using (var con = new SqlConnection(StringConexao))
            {
                string QueryRead = "SELECT * FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                using (var cmd = new SqlCommand(QueryRead, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        FuncionariosDomain Funcionarios = new FuncionariosDomain()
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr[1].ToString(),
                            Sobrenome = rdr[2].ToString(),
                        };

                        FuncionariosLista.Add(Funcionarios);
                    };
                }
            }

            return FuncionariosLista;
        }
    }
}

