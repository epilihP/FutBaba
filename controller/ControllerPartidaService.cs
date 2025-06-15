using System;
using System.Collections.Generic;
using Service.Partida;
using Model.Times;
using Model.Jogadores;

namespace Controller.PartidaService;

public class PartidaServiceController
{
    private readonly PartidaService partidaService = new PartidaService();

    public List<Time> GerarTimesBalanceados(List<Jogador> interessados, int jogadoresPorTime, int maxTimes)
    {
        return partidaService.GerarTimesBalanceados(interessados, jogadoresPorTime, maxTimes);
    }

    public List<Time> RotacionarTimes(List<Time> times, int maxTimes)
    {
        return partidaService.RotacionarTimes(times, maxTimes);
    }
}