namespace IPartidaRepository;
using System;   
using System.Collections.Generic;
// Gestão de partidas
public interface IPartidaRepository
    {
        void RegistrarPartida(Partida partida);
        IEnumerable<Partida> ListarPartidasPorJogo(Guid idJogo);
        void AtualizarResultado(Guid idPartida, Resultado resultado);
    }