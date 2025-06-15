using System;
using System.Collections.Generic;
using Service.Interessados;

namespace Controller.InteressadosService;


public class InteressadosController
{
    private readonly InteressadosService interessadosService;

    public InteressadosController(DateTime horarioJogo)
    {
        interessadosService = new InteressadosService(horarioJogo);
    }

    public bool AdicionarInteressado(string codigoJogador, DateTime agora)
    {
        return interessadosService.AdicionarInteressado(codigoJogador, agora);
    }

    public List<string> ListarInteressados()
    {
        return interessadosService.ListarInteressados();
    }

    public bool ListaEstaAberta(DateTime agora)
    {
        return interessadosService.ListaEstaAberta(agora);
    }
}