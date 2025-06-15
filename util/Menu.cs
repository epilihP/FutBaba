using System;
using Controller.Associados;
using Controller.Jogos;
using Controller.Partidas;
using Controller.InteressadosService;
using Controller.PartidaService;
using Model.Jogadores;
using Model.Jogos;
using Model.Partidas;
using Model.Times;
using System.Collections.Generic;

namespace Menu;

public class Menu
{
    public void Exibir()
    {
        var jogadorController = new JogadorController();
        var jogosController = new JogosController();
        var partidaController = new PartidaController();
        var partidaServiceController = new PartidaServiceController();

        DateTime horarioJogo = ProximaQuintaFeira20h();
        var interessadosController = new InteressadosController(horarioJogo);

        int opcao = -1;
        bool jogoCriado = false;
        bool partidaCriada = false;
        Jogo jogo = null;

        while (opcao != 0)
        {
            // Geração automática de partida ao fechar a lista de interessados
            if (jogoCriado && !partidaCriada && !interessadosController.ListaEstaAberta(DateTime.Now))
            {
                var lista = interessadosController.ListarInteressados();
                var jogadores = new List<Jogador>();
                foreach (var cod in lista)
                {
                    var jog = jogadorController.BuscarJogador(cod);
                    if (jog != null) jogadores.Add(jog);
                }

                var times = partidaServiceController.GerarTimesBalanceados(jogadores, 5, 4);

                // Limita cada jogador a no máximo 2 partidas
                times = partidaServiceController.AplicarLimiteJogos(times);

                // Simula resultado
                var resultado = partidaServiceController.SimularResultado(times);

                // Cria e registra a partida
                var partida = new Partida
                {
                    Id = Guid.NewGuid(),
                    IdJogo = jogo.Id,
                    DataHora = DateTime.Now,
                    Times = new List<string>(),
                    Resultado = resultado
                };
                foreach (var t in times)
                    partida.Times.Add(t.Nome);

                partidaController.RegistrarPartida(partida);
                partidaCriada = true;

                Console.WriteLine("Partida criada automaticamente!");
                Console.WriteLine($"Vencedor: {resultado.TimeVencedor} ({resultado.GolsVencedor} x {resultado.GolsPerdedor}) Perdedor: {resultado.TimePerdedor}");
            }

            Console.WriteLine("\n--- MENU PRINCIPAL ---");
            Console.WriteLine("1 - Cadastrar Jogador");
            Console.WriteLine("2 - Listar Jogadores");
            Console.WriteLine("3 - Cadastrar Jogo");
            Console.WriteLine("4 - Listar Jogos");
            if (jogoCriado)
            {
                Console.WriteLine("5 - Adicionar Interessado");
                Console.WriteLine("6 - Listar Interessados");
            }
            Console.WriteLine("7 - Ver histórico de partidas");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1:
                    Console.Write("Código: ");
                    string codigo = Console.ReadLine();
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Posição (goleiro/defesa/ataque): ");
                    string posicao = Console.ReadLine();
                    Console.Write("Idade: ");
                    int idade = int.Parse(Console.ReadLine());
                    var jogador = new Jogador { Codigo = codigo, Nome = nome, Posicao = posicao, Idade = idade };
                    if (jogadorController.CadastrarJogador(jogador))
                        Console.WriteLine("Jogador cadastrado!");
                    else
                        Console.WriteLine("Já existe jogador com esse código.");
                    break;
                case 2:
                    foreach (var j in jogadorController.ListarJogadores())
                        Console.WriteLine($"{j.Codigo} - {j.Nome} ({j.Posicao})");
                    break;
                case 3:
                    jogo = new Jogo();
                    jogo.Id = Guid.NewGuid();
                    jogo.Data = horarioJogo; // Corrija para DateTime se necessário!
                    Console.Write("Local: ");
                    jogo.Local = Console.ReadLine();
                    Console.Write("Tipo de campo: ");
                    jogo.TipoCampo = Console.ReadLine();
                    Console.Write("Jogadores por time: ");
                    jogo.JogadoresPorTime = int.Parse(Console.ReadLine());
                    if (jogosController.CadastrarJogo(jogo))
                    {
                        Console.WriteLine("Jogo cadastrado!");
                        jogoCriado = true;
                        partidaCriada = false; // Permite nova partida para o novo jogo
                    }
                    else
                        Console.WriteLine("Já existe jogo com esse Id.");
                    break;
                case 4:
                    foreach (var jg in jogosController.ListarJogos())
                        Console.WriteLine($"{jg.Id} - {jg.Local} em {jg.Data}");
                    break;
                case 5:
                    if (!jogoCriado)
                    {
                        Console.WriteLine("Cadastre um jogo primeiro!");
                        break;
                    }
                    Console.Write("Código do jogador: ");
                    string codJog = Console.ReadLine();
                    if (interessadosController.ListaEstaAberta(DateTime.Now))
                    {
                        if (interessadosController.AdicionarInteressado(codJog, DateTime.Now))
                            Console.WriteLine("Adicionado à lista de interessados!");
                        else
                            Console.WriteLine("Jogador já está na lista.");
                    }
                    else
                    {
                        Console.WriteLine("Lista de interessados está fechada.");
                    }
                    break;
                case 6:
                    if (!jogoCriado)
                    {
                        Console.WriteLine("Cadastre um jogo primeiro!");
                        break;
                    }
                    var listaInt = interessadosController.ListarInteressados();
                    Console.WriteLine("Interessados:");
                    foreach (var cod in listaInt)
                        Console.WriteLine(cod);
                    break;
                case 7:
                    var historico = partidaController.ListarHistorico();
                    Console.WriteLine("Histórico de partidas:");
                    foreach (var p in historico)
                        Console.WriteLine($"{p.DataHora:dd/MM} - {p.Resultado.TimeVencedor} venceu {p.Resultado.TimePerdedor} ({p.Resultado.GolsVencedor}x{p.Resultado.GolsPerdedor})");
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    private DateTime ProximaQuintaFeira20h()
    {
        var hoje = DateTime.Now;
        int diasAteQuinta = ((int)DayOfWeek.Thursday - (int)hoje.DayOfWeek + 7) % 7;
        var quinta = hoje.AddDays(diasAteQuinta).Date.AddHours(20);
        return quinta;
    }
}