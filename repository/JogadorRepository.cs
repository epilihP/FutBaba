namespace  Repository.Associados;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Model.Jogadores;

// Implementa o repositório de jogadores, salvando e carregando os dados em um arquivo JSON
public class JogadorRepository : IJogadorRepository
{
    private readonly string caminhoArquivo = "jogadores.json"; // Caminho do arquivo JSON
    private List<Jogador> jogadores;

    public JogadorRepository()
    {
        // Ao criar o repositório, tenta carregar os jogadores do arquivo JSON
        jogadores = CarregarDoArquivo();
    }

    // Adiciona um novo jogador e salva no arquivo
    public void Adicionar(Jogador jogador)
    {
        jogadores.Add(jogador);
        SalvarNoArquivo();
    }

    // Busca um jogador pelo código único
    public Jogador ObterPorCodigo(string codigo)
    {
        return jogadores.Find(j => j.Codigo == codigo);
    }

    // Retorna todos os jogadores cadastrados
    public IEnumerable<Jogador> ListarTodos()
    {
        return jogadores;
    }

    // Atualiza os dados de um jogador existente e salva no arquivo
    public void Atualizar(Jogador jogador)
    {
        var existente = ObterPorCodigo(jogador.Codigo);
        if (existente != null)
        {
            existente.Nome = jogador.Nome;
            existente.Idade = jogador.Idade;
            existente.Posicao = jogador.Posicao;
            existente.Time = jogador.Time;
            SalvarNoArquivo();
        }
    }

    // Remove um jogador pelo código e salva no arquivo
    public void Remover(string codigo)
    {
        var jogador = ObterPorCodigo(codigo);
        if (jogador != null)
        {
            jogadores.Remove(jogador);
            SalvarNoArquivo();
        }
    }

    // Salva a lista de jogadores no arquivo JSON
    private void SalvarNoArquivo()
    {
        var json = JsonSerializer.Serialize(jogadores, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }

    // Carrega a lista de jogadores do arquivo JSON, se existir
    private List<Jogador> CarregarDoArquivo()
    {
        if (File.Exists(caminhoArquivo))
        {
            var json = File.ReadAllText(caminhoArquivo);
            return JsonSerializer.Deserialize<List<Jogador>>(json) ?? new List<Jogador>();
        }
        return new List<Jogador>();
    }
}