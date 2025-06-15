namespace Service.Partida;

using System;
using System.Collections.Generic;
using Model.Times;
using Model.Jogadores;
using Model.Partidas;


public class PartidaService
{
    // Gera times balanceados considerando goleiros, defesa e ataque
    public List<Time> GerarTimesBalanceados(List<Jogador> interessados, int jogadoresPorTime, int maxTimes)
    {
        // Separar goleiros, defesa e ataque
        var goleiros = interessados.FindAll(j => j.Posicao.ToLower() == "goleiro");
        var defensores = interessados.FindAll(j => j.Posicao.ToLower() == "defesa");
        var atacantes = interessados.FindAll(j => j.Posicao.ToLower() == "ataque");

        var times = new List<Time>();
        int timesCriados = 0;

        // Garante pelo menos um goleiro por time, se possível
        while (timesCriados < maxTimes && interessados.Count >= jogadoresPorTime)
        {
            var time = new Time
            {
                Id = Guid.NewGuid(),
                Nome = $"Time {timesCriados + 1}",
                Jogadores = new List<string>(),
                NumeroDefesa = 0,
                NumeroAtaque = 0
            };

            // Adiciona goleiro
            if (goleiros.Count > 0)
            {
                var goleiro = goleiros[0];
                time.Goleiro = goleiro.Codigo;
                time.Jogadores.Add(goleiro.Codigo);
                goleiros.RemoveAt(0);
                interessados.Remove(goleiro);
            }
            else if (interessados.Count > 0) // Se não houver goleiro, pega o próximo
            {
                var jogador = interessados[0];
                time.Goleiro = jogador.Codigo;
                time.Jogadores.Add(jogador.Codigo);
                interessados.RemoveAt(0);
            }

            // Adiciona defensores
            for (int i = 0; i < jogadoresPorTime - 2 && defensores.Count > 0; i++)
            {
                var defensor = defensores[0];
                time.Jogadores.Add(defensor.Codigo);
                time.NumeroDefesa++;
                defensores.RemoveAt(0);
                interessados.Remove(defensor);
            }

            // Adiciona atacantes
            while (time.Jogadores.Count < jogadoresPorTime && atacantes.Count > 0)
            {
                var atacante = atacantes[0];
                time.Jogadores.Add(atacante.Codigo);
                time.NumeroAtaque++;
                atacantes.RemoveAt(0);
                interessados.Remove(atacante);
            }

            // Se ainda faltar jogador, preenche com qualquer um
            while (time.Jogadores.Count < jogadoresPorTime && interessados.Count > 0)
            {
                var jogador = interessados[0];
                time.Jogadores.Add(jogador.Codigo);
                interessados.RemoveAt(0);
            }

            times.Add(time);
            timesCriados++;
        }

        return times;
    }

    // Controla rotação de times (rei da quadra)
    public List<Time> RotacionarTimes(List<Time> times, int maxTimes)
    {
        // Exemplo: remove o time perdedor e coloca o próximo da fila
        if (times.Count > maxTimes)
        {
            times.RemoveAt(0); // Remove o primeiro time (perdedor)
        }
        return times;
    }

    // Simula o resultado da partida (pode ser substituído por entrada manual)
    public Resultado SimularResultado(List<Time> times)
    {
        // Simulação simples: sorteia um vencedor
        var rand = new Random();
        int vencedor = rand.Next(times.Count);
        int perdedor = (vencedor + 1) % times.Count;
        return new Resultado
        {
            TimeVencedor = times[vencedor].Nome,
            TimePerdedor = times[perdedor].Nome,
            GolsVencedor = rand.Next(1, 6),
            GolsPerdedor = rand.Next(0, 5)
        };
    }

    // Limita cada jogador a no máximo 2 partidas por rodada
    public List<Time> AplicarLimiteJogos(List<Time> times)
    {
        var contador = new Dictionary<string, int>();
        foreach (var time in times)
        {
            foreach (var jogador in time.Jogadores)
            {
                if (!contador.ContainsKey(jogador))
                    contador[jogador] = 0;
                contador[jogador]++;
            }
        }
        // Remove jogadores que já jogaram 2 vezes
        foreach (var time in times)
        {
            time.Jogadores.RemoveAll(j => contador[j] > 2);
        }
        return times;
    }
}