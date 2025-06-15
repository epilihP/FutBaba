using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Model.Times;

namespace Repository.Times;
// Repositório para gerenciar os times, salvando e carregando em arquivo JSON
public class TimeRepository
{
    private readonly string caminhoArquivo = "times.json"; // Caminho do arquivo JSON
    private List<Time> times;

    public TimeRepository()
    {
        // Ao criar o repositório, tenta carregar os times do arquivo JSON
        times = CarregarDoArquivo();
    }

    // Adiciona um novo time e salva no arquivo
    public void Adicionar(Time time)
    {
        times.Add(time);
        SalvarNoArquivo();
    }

    // Busca um time pelo Id único
    public Time ObterPorId(Guid id)
    {
        return times.Find(t => t.Id == id);
    }

    // Retorna todos os times cadastrados
    public IEnumerable<Time> ListarTodos()
    {
        return times;
    }

    // Atualiza os dados de um time existente e salva no arquivo
    public void Atualizar(Time time)
    {
        var existente = ObterPorId(time.Id);
        if (existente != null)
        {
            existente.Nome = time.Nome;
            existente.Jogadores = time.Jogadores;
            existente.Goleiro = time.Goleiro;
            existente.NumeroDefesa = time.NumeroDefesa;
            existente.NumeroAtaque = time.NumeroAtaque;
            SalvarNoArquivo();
        }
    }

    // Remove um time pelo Id e salva no arquivo
    public void Remover(Guid id)
    {
        var time = ObterPorId(id);
        if (time != null)
        {
            times.Remove(time);
            SalvarNoArquivo();
        }
    }

    // Salva a lista de times no arquivo JSON
    private void SalvarNoArquivo()
    {
        var json = JsonSerializer.Serialize(times, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }

    // Carrega a lista de times do arquivo JSON, se ela realmente existir
    private List<Time> CarregarDoArquivo()
    {
        if (File.Exists(caminhoArquivo))
        {
            var json = File.ReadAllText(caminhoArquivo);
            return JsonSerializer.Deserialize<List<Time>>(json) ?? new List<Time>();
        }
        return new List<Time>();
    }
}