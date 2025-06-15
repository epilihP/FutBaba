namespace Repository.Partidas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Model.Partida;
using IPartidaRepository;
public class PartidaRepository : IPartidaRepository
{
    private readonly string caminhoArquivo = "partidas.json"; // Caminho do arquivo JSON
    private List<Partida> partidas;

    public PartidaRepository()
    {
        // Ao criar o repositório, tenta carregar as partidas do arquivo JSON
        partidas = CarregarDoArquivo();
    }

    // Registra uma nova partida e salva no arquivo
    public void RegistrarPartida(Partida partida)
    {
        partidas.Add(partida);
        SalvarNoArquivo();
    }

    // Lista todas as partidas de um jogo específico
    public IEnumerable<Partida> ListarPartidasPorJogo(Guid idJogo)
    {
        return partidas.FindAll(p => p.IdJogo == idJogo);
    }

    // Atualiza o resultado de uma partida e salva no arquivo
    public void AtualizarResultado(Guid idPartida, Resultado resultado)
    {
        var partida = partidas.Find(p => p.Id == idPartida);
        if (partida != null)
        {
            partida.Resultado = resultado;
            SalvarNoArquivo();
        }
    }

    // Salva a lista de partidas no arquivo JSON
    private void SalvarNoArquivo()
    {
        var json = JsonSerializer.Serialize(partidas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }

    // Carrega a lista de partidas do arquivo JSON, se existir
    private List<Partida> CarregarDoArquivo()
    {
        if (File.Exists(caminhoArquivo))
        {
            var json = File.ReadAllText(caminhoArquivo);
            return JsonSerializer.Deserialize<List<Partida>>(json) ?? new List<Partida>();
        }
        return new List<Partida>();
    }

    // Adicione este método ao PartidaRepository para buscar uma partida pelo Id
    public Partida ObterPorId(Guid idPartida)
    {
        return partidas.Find(p => p.Id == idPartida);
    }

    public IEnumerable<Partida> ListarTodos()
    {
        return partidas;
    }
}