namespace IJogoRepository;
using System;
using System.Collections.Generic;
//crud para jogos
public interface IJogoRepository
    {
        void Adicionar(Jogo jogo);
        Jogo ObterPorId(Guid id);
        IEnumerable<Jogo> ListarTodos();
        void Atualizar(Jogo jogo);
        void Remover(Guid id);
    }