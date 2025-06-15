using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Model.Jogadores;

namespace Repository.Associados;

public class JogadorRepository : IJogadorRepository
{
    private readonly string caminhoArquivo = "jogadores.json";
    private List<Jogador> jogadores;

    public JogadorRepository()
    {
        jogadores = CarregarDoArquivo();
    }

    public void Adicionar(Jogador jogador)
    {
        if (jogador == null)
            throw new ArgumentNullException(nameof(jogador));

        jogadores.Add(jogador);
        SalvarNoArquivo();
    }

    public Jogador ObterPorCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("Código inválido.", nameof(codigo));

        return jogadores.Find(j => j.Codigo == codigo);
    }

    public IEnumerable<Jogador> ListarTodos()
    {
        return jogadores;
    }

    public void Atualizar(Jogador jogador)
    {
        if (jogad
