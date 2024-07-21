public class Mensagem
{
    public int Id { get; set; }
    public string Conteudo { get; set; }
    public string Autor { get; set; }
    public DateTime DataEnvio { get; set; }

    public Mensagem(int id, string conteudo, string autor, DateTime dataEnvio)
    {
        Id = id;
        Conteudo = conteudo;
        Autor = autor;
        DataEnvio = dataEnvio;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Autor: {Autor}, Mensagem: {Conteudo}, Enviada em: {DataEnvio}";
    }
}
