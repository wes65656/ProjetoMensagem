using System;
using System.Collections.Generic;

class Program
{
    static BancoDeDados bancoDeDados;
    static Usuario usuarioLogado;

    static void Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=MensagensDB;User Id=sa;Password=MinhaSenhaForte$;";

        bancoDeDados = new BancoDeDados(connectionString);

        while (true)
        {
            if (usuarioLogado == null)
            {
                Console.Clear();
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Registrar");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Registrar();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Bem-vindo, {usuarioLogado.Nome}.");
                Console.WriteLine("1. Enviar mensagem anônima");
                if (usuarioLogado.IsAdmin)
                {
                    Console.WriteLine("2. Ver todas as mensagens (Admin)");
                }
                Console.WriteLine("3. Logout");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        EnviarMensagem();
                        break;
                    case "2":
                        if (usuarioLogado.IsAdmin)
                        {
                            VerMensagensAdmin();
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. Tente novamente.");
                        }
                        break;
                    case "3":
                        usuarioLogado = null;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }

    static void Login()
    {
        Console.Clear();
        Console.Write("Nome de usuário: ");
        string nome = Console.ReadLine();
        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        Usuario usuario = bancoDeDados.AutenticarUsuario(nome, senha);

        if (usuario != null)
        {
            usuarioLogado = usuario;
            Console.WriteLine("Login bem-sucedido! Pressione qualquer tecla para continuar.");
        }
        else
        {
            Console.WriteLine("Nome de usuário ou senha incorretos. Pressione qualquer tecla para tentar novamente.");
        }
        Console.ReadKey();
    }

    static void Registrar()
    {
        Console.Clear();
        Console.Write("Nome de usuário: ");
        string nome = Console.ReadLine();
        Console.Write("Senha: ");
        string senha = Console.ReadLine();
        Console.Write("É administrador (s/n)? ");
        bool isAdmin = Console.ReadLine().ToLower() == "s";

        bancoDeDados.RegistrarUsuario(nome, senha, isAdmin);

        Console.WriteLine("Usuário registrado com sucesso! Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }

    static void EnviarMensagem()
    {
        Console.Clear();
        Console.WriteLine("Envie sua mensagem anônima:");
        string mensagem = Console.ReadLine();
        bancoDeDados.AdicionarMensagem(usuarioLogado.Id, mensagem);
        Console.WriteLine("Mensagem enviada com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    static void VerMensagensAdmin()
    {
        Console.Clear();
        List<Mensagem> mensagens = bancoDeDados.ObterMensagens();
        Console.WriteLine("Mensagens enviadas:");

        foreach (var mensagem in mensagens)
        {
            Console.WriteLine(mensagem);
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }
}
