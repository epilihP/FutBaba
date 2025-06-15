using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Model.Jogos;
using IJogoRepository;

namespace Repository.Jogos;
public class JogoRepository : IJogoRepository
{
    
    private readonly string caminhoArquivo = "jogos.json"; // Caminho do arquivo JSON
    private List<Jogo> jogos;

    public JogoRepository()
    {
        // Ao criar o repositório, tenta carregar os jogos do arquivo JSON
        jogos = CarregarDoArquivo();
    }

    // Adiciona um novo jogo e salva no arquivo
    public void Adicionar(Jogo jogo)
    {
        jogos.Add(jogo);
        SalvarNoArquivo();
    }

    // Busca um jogo pelo Id único
    public Jogo ObterPorId(Guid id)
    {
        return jogos.Find(j => j.Id == id);
    }

    // Retorna todos os jogos cadastrados
    public IEnumerable<Jogo> ListarTodos()
    {
        return jogos;
    }

    // Atualiza os dados de um jogo existente e salva no arquivo
    public void Atualizar(Jogo jogo)
    {
        var existente = ObterPorId(jogo.Id);
        if (existente != null)
        {
            existente.Data = jogo.Data;
            existente.Local = jogo.Local;
            existente.TipoCampo = jogo.TipoCampo;
            existente.JogadoresPorTime = jogo.JogadoresPorTime;
            existente.LimiteTimes = jogo.LimiteTimes;
            existente.LimiteJogadores = jogo.LimiteJogadores;
            existente.Interessados = jogo.Interessados;
            SalvarNoArquivo();
        }
    }

    // Remove um jogo pelo Id e salva no arquivo
    public void Remover(Guid id)
    {
        var jogo = ObterPorId(id);
        if (jogo != null)
        {
            jogos.Remove(jogo);
            SalvarNoArquivo();
        }
    }

    // Salva a lista de jogos no arquivo JSON
    private void SalvarNoArquivo()
    {
        var json = JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }

    // Carrega a lista de jogos do arquivo JSON, se existir
    private List<Jogo> CarregarDoArquivo()
    {
        if (File.Exists(caminhoArquivo))
        {
            var json = File.ReadAllText(caminhoArquivo);
            return JsonSerializer.Deserialize<List<Jogo>>(json) ?? new List<Jogo>();
        }
        return new List<Jogo>();
    }
}