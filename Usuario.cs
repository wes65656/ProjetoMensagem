public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
    public bool IsAdmin { get; set; }

    public Usuario(int id, string nome, string senha, bool isAdmin)
    {
        Id = id;
        Nome = nome;
        Senha = senha;
        IsAdmin = isAdmin;
    }
}
