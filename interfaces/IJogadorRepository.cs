using System.Collections.Generic;
using System;

namespace IJogadorRepository;

//para o crud do jogador
public interface IJogadorRepository
    {
        void Adicionar(Jogador jogador);
        Jogador ObterPorCodigo(string codigo);
        IEnumerable<Jogador> ListarTodos();
        void Atualizar(Jogador jogador);
        void Remover(string codigo);
    }