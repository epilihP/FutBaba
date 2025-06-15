namespace Controller.Partidas;
using System;
using System.Collections.Generic;
using Model.Partidas;
using Repository.Partidas;
using Model.Times;

public class PartidaController
{
    private readonly PartidaRepository partidaRepository;

    public PartidaController()
    {
        partidaRepository = new PartidaRepository();
    }

    // Registrar nova partida
    public void RegistrarPartida(Partida partida)
    {
        partidaRepository.RegistrarPartida(partida);
    }

    // Listar todas as partidas de um jogo específico
    public IEnumerable<Partida> ListarPartidasPorJogo(Guid idJogo)
    {
        return partidaRepository.ListarPartidasPorJogo(idJogo);
    }

    // Listar histórico de todas as partidas
    public IEnumerable<Partida> ListarHistorico()
    {
        // Supondo que o repositório tenha um método para listar todas as partidas
        return partidaRepository.ListarTodos();
    }

    // Buscar última partida
    public Partida BuscarUltimaPartida()
    {
        var todas = new List<Partida>(partidaRepository.ListarTodos());
        if (todas.Count == 0) return null;
        todas.Sort((a, b) => b.DataHora.CompareTo(a.DataHora));
        return todas[0];
    }

    // Atualizar resultado de uma partida
    public bool AtualizarResultado(Guid idPartida, Resultado resultado)
    {
        var partida = partidaRepository.ObterPorId(idPartida);
        if (partida == null)
            return false;

        partidaRepository.AtualizarResultado(idPartida, resultado);
        return true;
    }
}