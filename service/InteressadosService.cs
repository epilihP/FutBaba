using System;
using System.Collections.Generic;

namespace Service.Interessados;

public class InteressadosService
{
    private List<string> interessados = new List<string>();
    private DateTime horarioFechamento;

    public InteressadosService(DateTime horarioJogo)
    {
        // Fecha 1h antes do jogo
        horarioFechamento = horarioJogo.AddHours(-1);
    }

    public bool AdicionarInteressado(string codigoJogador, DateTime agora)
    {
        if (agora >= horarioFechamento)
            return false; // Lista fechada

        if (!interessados.Contains(codigoJogador))
            interessados.Add(codigoJogador);

        return true;
    }

    public List<string> ListarInteressados()
    {
        return interessados;
    }

    public bool ListaEstaAberta(DateTime agora)
    {
        return agora < horarioFechamento;
    }
}