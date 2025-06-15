namespace IInteressadosRepository;
using System;
using System.Collections.Generic;

// Lista de interessados para o próximo jogo
public interface IInteressadosRepository
    {
        void RegistrarInteresse(string codigoJogador, Guid idJogo);
        IEnumerable<Jogador> ListarInteressados(Guid idJogo);
        void RemoverInteresse(string codigoJogador, Guid idJogo);
    }