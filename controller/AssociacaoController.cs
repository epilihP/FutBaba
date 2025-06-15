using System;
using System.Collections.Generic;
using Model.Jogadores;
using Repository.Associados;

namespace Controller.Associados;

public class JogadorController
{
    private readonly JogadorRepository jogadorRepository;

    // Recebe injeção de dependência (pra separar melhor o sitema)
    public JogadorController(JogadorRepository repository)
    {
        jogadorRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    // Cadastrar novo jogador com com as exceções 
    public bool CadastrarJogador(Jogador jogador)
    {
        if (jogador == null) 
            throw new ArgumentNullException(nameof(jogador));

        if (string.IsNullOrWhiteSpace(jogador.Codigo))
            throw new ArgumentException("Código do jogador não pode ser vazio.", nameof(jogador));

        try
        {
            if (jogadorRepository.ObterPorCodigo(jogador.Codigo) != null)
                return false; // Já existe jogador com esse código

            jogadorRepository.Adicionar(jogador);
            return true;
        }
        catch (Exception ex)
        {
            // Aqui é por conta do erro
            Console.Error.WriteLine($"Erro ao cadastrar jogador: {ex.Message}");
            return false;
        }
    }

    // Listar todos os jogadores
    public IEnumerable<Jogador> ListarJogadores()
    {
        try
        {
            return jogadorRepository.ListarTodos();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao listar jogadores: {ex.Message}");
            return new List<Jogador>();
        }
    }

    // Atualizar jogador com validação e tratamento de exceções
    public bool AtualizarJogador(Jogador jogador)
    {
        if (jogador == null) 
            throw new ArgumentNullException(nameof(jogador));

        if (string.IsNullOrWhiteSpace(jogador.Codigo))
            throw new ArgumentException("Código do jogador não pode ser vazio.", nameof(jogador));

        try
        {
            var existente = jogadorRepository.ObterPorCodigo(jogador.Codigo);
            if (existente == null)
                return false;

            jogadorRepository.Atualizar(jogador);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao atualizar jogador: {ex.Message}");
            return false;
        }
    }

    // Remover jogador com validação e tratamento de exceções
    public bool RemoverJogador(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("Código não pode ser vazio.", nameof(codigo));

        try
        {
            var existente = jogadorRepository.ObterPorCodigo(codigo);
            if (existente == null)
                return false;

            jogadorRepository.Remover(codigo);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao remover jogador: {ex.Message}");
            return false;
        }
    }

    // Buscar jogador por código com validação e tratamento de exceções
    public Jogador BuscarJogador(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("Código não pode ser vazio.", nameof(codigo));

        try
        {
            return jogadorRepository.ObterPorCodigo(codigo);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao buscar jogador: {ex.Message}");
            return null;
        }
    }
}
