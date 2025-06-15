using System;
using System.Collections.Generic;

namespace ITimeService;


// Gestão de times
public interface ITimeService
    {
        IEnumerable<Time> GerarTimesPorOrdemChegada(List<Jogador> jogadores, int jogadoresPorTime);
        IEnumerable<Time> GerarTimesPorPosicao(List<Jogador> jogadores, int jogadoresPorTime);
        IEnumerable<Time> GerarTimesOutroCriterio(List<Jogador> jogadores, int jogadoresPorTime);
    }