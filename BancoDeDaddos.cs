using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class BancoDeDados
{
    private string _connectionString;

    public BancoDeDados(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AdicionarMensagem(int autorId, string conteudo)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Mensagens (AutorId, Conteudo) VALUES (@autorId, @conteudo)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@autorId", autorId);
            command.Parameters.AddWithValue("@conteudo", conteudo);
            command.ExecuteNonQuery();
        }
    }

    public List<Mensagem> ObterMensagens()
    {
        List<Mensagem> mensagens = new List<Mensagem>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT m.Id, m.Conteudo, u.Nome AS Autor, m.DataEnvio FROM Mensagens m JOIN Usuarios u ON m.AutorId = u.Id ORDER BY m.DataEnvio DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string conteudo = reader.GetString(1);
                    string autor = reader.GetString(2);
                    DateTime dataEnvio = reader.GetDateTime(3);
                    mensagens.Add(new Mensagem(id, conteudo, autor, dataEnvio));
                }
            }
        }

        return mensagens;
    }

    public void RegistrarUsuario(string nome, string senha, bool isAdmin)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Usuarios (Nome, Senha, IsAdmin) VALUES (@nome, @senha, @isAdmin)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@senha", senha);
            command.Parameters.AddWithValue("@isAdmin", isAdmin ? 1 : 0);
            command.ExecuteNonQuery();
        }
    }

    public Usuario AutenticarUsuario(string nome, string senha)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM Usuarios WHERE Nome = @nome AND Senha = @senha";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@senha", senha);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    bool isAdmin = reader.GetBoolean(3);
                    return new Usuario(id, nome, senha, isAdmin);
                }
            }
        }

        return null;
    }
}
