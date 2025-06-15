namespace Controller.Associados;
using System;
using System.Collections.Generic;
using Model.Jogadores;
using Repository.Associados;


public class JogadorController
{
    private readonly JogadorRepository jogadorRepository;

    public JogadorController()
    {
        jogadorRepository = new JogadorRepository();
    }

    // Cadastrar novo jogador (com validação de código único)
    public bool CadastrarJogador(Jogador jogador)
    {
        if (jogadorRepository.ObterPorCodigo(jogador.Codigo) != null)
            return false; // Já existe jogador com esse código

        jogadorRepository.Adicionar(jogador);
        return true;
    }

    // Listar todos os jogadores
    public IEnumerable<Jogador> ListarJogadores()
    {
        return jogadorRepository.ListarTodos();
    }

    // Atualizar jogador
    public bool AtualizarJogador(Jogador jogador)
    {
        var existente = jogadorRepository.ObterPorCodigo(jogador.Codigo);
        if (existente == null)
            return false;

        jogadorRepository.Atualizar(jogador);
        return true;
    }

    // Remover jogador
    public bool RemoverJogador(string codigo)
    {
        var existente = jogadorRepository.ObterPorCodigo(codigo);
        if (existente == null)
            return false;

        jogadorRepository.Remover(codigo);
        return true;
    }

    // Buscar jogador por código
    public Jogador BuscarJogador(string codigo)
    {
        return jogadorRepository.ObterPorCodigo(codigo);
    }
}