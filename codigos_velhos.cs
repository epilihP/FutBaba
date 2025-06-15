// using System;
// using System.Collections.Generic;
// using Model.Jogadores;
// using Repository.Associados;

// namespace Controller.Associados;

// public class JogadorController
// {
//     private readonly JogadorRepository jogadorRepository;

//     // Recebe a dependência via construtor (injeção de dependência)
//     public JogadorController(JogadorRepository repository)
//     {
//         jogadorRepository = repository ?? throw new ArgumentNullException(nameof(repository));
//     }

//     // Cadastrar novo jogador com validação e tratamento de exceções
//     public bool CadastrarJogador(Jogador jogador)
//     {
//         if (jogador == null) 
//             throw new ArgumentNullException(nameof(jogador));

//         if (string.IsNullOrWhiteSpace(jogador.Codigo))
//             throw new ArgumentException("Código do jogador não pode ser vazio.", nameof(jogador));

//         try
//         {
//             if (jogadorRepository.ObterPorCodigo(jogador.Codigo) != null)
//                 return false; // Já existe jogador com esse código

//             jogadorRepository.Adicionar(jogador);
//             return true;
//         }
//         catch (Exception ex)
//         {
//             // Aqui você pode logar o erro, por exemplo
//             Console.Error.WriteLine($"Erro ao cadastrar jogador: {ex.Message}");
//             return false;
//         }
//     }

//     // Listar todos os jogadores
//     public IEnumerable<Jogador> ListarJogadores()
//     {
//         try
//         {
//             return jogadorRepository.ListarTodos();
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Erro ao listar jogadores: {ex.Message}");
//             return new List<Jogador>();
//         }
//     }

//     // Atualizar jogador com validação e tratamento de exceções
//     public bool AtualizarJogador(Jogador jogador)
//     {
//         if (jogador == null) 
//             throw new ArgumentNullException(nameof(jogador));

//         if (string.IsNullOrWhiteSpace(jogador.Codigo))
//             throw new ArgumentException("Código do jogador não pode ser vazio.", nameof(jogador));

//         try
//         {
//             var existente = jogadorRepository.ObterPorCodigo(jogador.Codigo);
//             if (existente == null)
//                 return false;

//             jogadorRepository.Atualizar(jogador);
//             return true;
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Erro ao atualizar jogador: {ex.Message}");
//             return false;
//         }
//     }

//     // Remover jogador com validação e tratamento de exceções
//     public bool RemoverJogador(string codigo)
//     {
//         if (string.IsNullOrWhiteSpace(codigo))
//             throw new ArgumentException("Código não pode ser vazio.", nameof(codigo));

//         try
//         {
//             var existente = jogadorRepository.ObterPorCodigo(codigo);
//             if (existente == null)
//                 return false;

//             jogadorRepository.Remover(codigo);
//             return true;
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Erro ao remover jogador: {ex.Message}");
//             return false;
//         }
//     }

//     // Buscar jogador por código com validação e tratamento de exceções
//     public Jogador BuscarJogador(string codigo)
//     {
//         if (string.IsNullOrWhiteSpace(codigo))
//             throw new ArgumentException("Código não pode ser vazio.", nameof(codigo));

//         try
//         {
//             return jogadorRepository.ObterPorCodigo(codigo);
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Erro ao buscar jogador: {ex.Message}");
//             return null;
//         }
//     }
// }


//  repository Associados
// sing System;
// using System.Collections.Generic;
// using System.IO;
// using System.Text.Json;
// using Model.Jogadores;

// namespace Repository.Associados;

// // Implementa o repositório de jogadores, salvando e carregando os dados em um arquivo JSON
// public class JogadorRepository : IJogadorRepository
// {
//     private readonly string caminhoArquivo = "jogadores.json"; // Caminho do arquivo JSON
//     private List<Jogador> jogadores;

//     public JogadorRepository()
//     {
//         // Ao criar o repositório, tenta carregar os jogadores do arquivo JSON
//         jogadores = CarregarDoArquivo();
//     }

//     // Adiciona um novo jogador e salva no arquivo
//     public void Adicionar(Jogador jogador)
//     {
//         jogadores.Add(jogador);
//         SalvarNoArquivo();
//     }

//     // Busca um jogador pelo código único
//     public Jogador ObterPorCodigo(string codigo)
//     {
//         return jogadores.Find(j => j.Codigo == codigo);
//     }

//     // Retorna todos os jogadores cadastrados
//     public IEnumerable<Jogador> ListarTodos()
//     {
//         return jogadores;
//     }

//     // Atualiza os dados de um jogador existente e salva no arquivo
//     public void Atualizar(Jogador jogador)
//     {
//         var existente = ObterPorCodigo(jogador.Codigo);
//         if (existente != null)
//         {
//             existente.Nome = jogador.Nome;
//             existente.Idade = jogador.Idade;
//             existente.Posicao = jogador.Posicao;
//             existente.Time = jogador.Time;
//             SalvarNoArquivo();
//         }
//     }

//     // Remove um jogador pelo código e salva no arquivo
//     public void Remover(string codigo)
//     {
//         var jogador = ObterPorCodigo(codigo);
//         if (jogador != null)
//         {
//             jogadores.Remove(jogador);
//             SalvarNoArquivo();
//         }
//     }

//     // Salva a lista de jogadores no arquivo JSON
//     private void SalvarNoArquivo()
//     {
//         var json = JsonSerializer.Serialize(jogadores, new JsonSerializerOptions { WriteIndented = true });
//         File.WriteAllText(caminhoArquivo, json);
//     }

//     // Carrega a lista de jogadores do arquivo JSON, se existir
//     private List<Jogador> CarregarDoArquivo()
//     {
//         if (File.Exists(caminhoArquivo))
//         {
//             var json = File.ReadAllText(caminhoArquivo);
//             return JsonSerializer.Deserialize<List<Jogador>>(json) ?? new List<Jogador>();
//         }
//         return new List<Jogador>();
//     }
// }

