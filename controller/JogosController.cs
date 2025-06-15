using System;
using System.Collections.Generic;
using Model.Jogos;
using Repository.Jogos;

namespace Controller.Jogos;

public class JogosController
{
    private readonly JogoRepository jogoRepository;

    public JogosController()
    {
        jogoRepository = new JogoRepository();
    }

    // Cadastrar novo jogo (com validação de Id único)
    public bool CadastrarJogo(Jogo jogo)
    {
        if (jogoRepository.ObterPorId(jogo.Id) != null)
            return false; // Já existe jogo com esse Id

        jogoRepository.Adicionar(jogo);
        return true;
    }

    // Listar todos os jogos
    public IEnumerable<Jogo> ListarJogos()
    {
        return jogoRepository.ListarTodos();
    }

    // Atualizar jogo
    public bool AtualizarJogo(Jogo jogo)
    {
        var existente = jogoRepository.ObterPorId(jogo.Id);
        if (existente == null)
            return false;

        jogoRepository.Atualizar(jogo);
        return true;
    }

    // Remover jogo
    public bool RemoverJogo(Guid id)
    {
        var existente = jogoRepository.ObterPorId(id);
        if (existente == null)
            return false;

        jogoRepository.Remover(id);
        return true;
    }

    // Buscar jogo por Id
    public Jogo BuscarJogo(Guid id)
    {
        return jogoRepository.ObterPorId(id);
    }
}