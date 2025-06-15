namespace Controller.JogoService;
using System;
using Service.Jogo;

public class JogoServiceController
{
    private readonly JogoService jogoService = new JogoService();

    public bool EhDiaDeJogo(DateTime agora)
    {
        return jogoService.EhDiaDeJogo(agora);
    }

    public bool PodeAbrirListaInteressados(DateTime agora)
    {
        return jogoService.PodeAbrirListaInteressados(agora);
    }
}