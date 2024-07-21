public class Mensagem
{
    public int Id { get; set; }
    public string Conteudo { get; set; }

    public Mensagem(int id, string conteudo)
    {
        Id = id;
        Conteudo = conteudo;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Mensagem: {Conteudo}";
    }
}
