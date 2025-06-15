using System;
using System.Collections.Generic;
using Model.Times;
using Repository.Times;
using ITimeService;

namespace Controller.Times;

public class TimeController
{
    private readonly TimeRepository timeRepository;

    public TimeController()
    {
        timeRepository = new TimeRepository();
    }

    // Cadastrar novo time
    public void CadastrarTime(Time time)
    {
        timeRepository.Adicionar(time);
    }

    // Listar todos os times
    public IEnumerable<Time> ListarTimes()
    {
        return timeRepository.ListarTodos();
    }

    // Buscar time por Id
    public Time BuscarPorId(Guid id)
    {
        return timeRepository.ObterPorId(id);
    }

    // Atualizar time existente
    public bool AtualizarTime(Time time)
    {
        var existente = timeRepository.ObterPorId(time.Id);
        if (existente == null)
            return false;

        timeRepository.Atualizar(time);
        return true;
    }

    // Remover time pelo Id
    public bool RemoverTime(Guid id)
    {
        var existente = timeRepository.ObterPorId(id);
        if (existente == null)
            return false;

        timeRepository.Remover(id);
        return true;
    }
}
