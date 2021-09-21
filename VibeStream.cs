using System;

namespace DIO.Entreterimento
{
    class Program
    {
        static SerieRepositorio repositorioSerie = new SerieRepositorio();
        static FilmeRepositorio repositorioFilme = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string tipoOpcaoUsuario = ObterTipoOpcaoUsuario();
            string tipoOpcaoUsuarioNome = "";
            while (tipoOpcaoUsuario.ToUpper() != "X")
            {
                
                switch (tipoOpcaoUsuario)
                {
                    case "1":
                        tipoOpcaoUsuarioNome = "Filmes";
                        break;
                    case "2":
                        tipoOpcaoUsuarioNome = "Series";
                        break;
                    case "C":
                        Console.Clear();
                        break;
                }
                
                string opcaoUsuario = ObterOpcaoUsuario(tipoOpcaoUsuarioNome);

                while (opcaoUsuario.ToUpper() != "R")
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            Listar(tipoOpcaoUsuario);
                            break;
                        case "2":
                            Inserir(tipoOpcaoUsuario);
                            break;
                        case "3":
                            Atualizar(tipoOpcaoUsuario);
                            break;
                        case "4":
                            Excluir(tipoOpcaoUsuario);
                            break;
                        case "5":
                            Visualizar(tipoOpcaoUsuario);
                            break;
                        case "C":
                            Console.Clear();
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    opcaoUsuario = ObterOpcaoUsuario(tipoOpcaoUsuarioNome);

                }
                tipoOpcaoUsuario = ObterTipoOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void Excluir(string tipo)
        {
            if (tipo == "1")
            {
                Console.Write("Digite o id do Filme: ");
                int indiceFilme = int.Parse(Console.ReadLine());

                repositorioFilme.Exclui(indiceFilme);
            }
            else if (tipo == "2")
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                repositorioSerie.Exclui(indiceSerie);
            }
         }


        private static void Visualizar(string tipo)
        {
            if (tipo == "1")
            {
                Console.Write("Digite o id do filme: ");
                int indiceFilme = int.Parse(Console.ReadLine());
                var Filme = repositorioFilme.RetornaPorId(indiceFilme);
                Console.WriteLine(Filme);
            }
            else if (tipo == "2")
            {
                Console.Write("Digite o id do série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                var Serie = repositorioSerie.RetornaPorId(indiceSerie);
                Console.WriteLine(Serie);
            }
        }

        private static void Atualizar(string tipo)
        {
            if (tipo == "1")
            {
                Console.Write("Digite o id do filme: ");
                int indiceFilme = int.Parse(Console.ReadLine());

                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título do filme: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de lançamento do filme: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição do filme: ");
                string entradaDescricao = Console.ReadLine();

                Entreterimento atualizaFilme = new Entreterimento(id: indiceFilme,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao,
                                        tipo: "Filme");

                repositorioFilme.Atualiza(indiceFilme, atualizaFilme);
            }
            else if (tipo == "2")
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Entreterimento atualizaSerie = new Entreterimento(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao,
                                        tipo: "Serie");

            repositorioSerie.Atualiza(indiceSerie, atualizaSerie);
            }
        }
        private static void Listar(string tipo)
        {
            if (tipo == "1")
            {

                Console.WriteLine("Listar Filmes");

                var lista = repositorioFilme.Lista();

                if (lista.Count == 0)
                {
                    Console.WriteLine("Nenhum filme cadastrado.");
                    return;
                }

                foreach (var filme in lista)
                {
                    var excluido = filme.retornaExcluido();

                    Console.WriteLine("#ID {0}: - {1} {2}", filme.retornaId(), filme.retornaTitulo(), (excluido ? "*Excluído*" : ""));
                }
            }
            else if (tipo == "2")
            {
                Console.WriteLine("Listar séries");

                var lista = repositorioSerie.Lista();

                if (lista.Count == 0)
                {
                    Console.WriteLine("Nenhuma série cadastrada.");
                    return;
                }

                foreach (var serie in lista)
                {
                    var excluido = serie.retornaExcluido();

                    Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
                }
            }
        }

        private static void Inserir(string tipo)
        {
            if (tipo == "1")
            {
                Console.WriteLine("Inserir novo filme");

                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título do filme: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de lançamento do filme: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição do filme: ");
                string entradaDescricao = Console.ReadLine();

                Entreterimento novoFilme = new Entreterimento(id: repositorioFilme.ProximoId(),
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao,
                                            tipo: "filme");

                repositorioFilme.Insere(novoFilme);
            }
            else if (tipo == "2")
            {
                Console.WriteLine("Inserir nova série");

                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Entreterimento novaSerie = new Entreterimento(id: repositorioSerie.ProximoId(),
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao,
                                            tipo: "serie");

                repositorioSerie.Insere(novaSerie);
            }
        }
          
        private static string ObterOpcaoUsuario(string tipo)
        {
            Console.WriteLine();
            Console.WriteLine("Vibe Stream - " + tipo);
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar " + tipo);
            Console.WriteLine("2- Inserir " + tipo);
            Console.WriteLine("3- Atualizar " + tipo);
            Console.WriteLine("4- Excluir " + tipo);
            Console.WriteLine("5- Visualizar " + tipo);
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("R- Retorne ao menu anterior");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static string ObterTipoOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Vibe Stream!");
            Console.WriteLine("Qual sua vibe?");

            Console.WriteLine("1- Filmes");
            Console.WriteLine("2- Séries");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}

