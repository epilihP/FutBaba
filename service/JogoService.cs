using System.Collections.Generic;
using System;

namespace Service.Jogo;

public class JogoService
{
    // Verifica se hoje é quinta-feira e se está no horário do jogo
    public bool EhDiaDeJogo(DateTime agora)
    {
        return agora.DayOfWeek == DayOfWeek.Thursday && agora.Hour >= 19 && agora.Hour < 21;
    }

    // Verifica se pode abrir lista de interessados (após o jogo anterior e até 1h antes do próximo)
    public bool PodeAbrirListaInteressados(DateTime agora)
    {
        // Lista abre após quinta 21h e fecha quinta 19h (1h antes do jogo)
        if (agora.DayOfWeek == DayOfWeek.Thursday && agora.Hour >= 21)
            return true;
        if (agora.DayOfWeek == DayOfWeek.Friday || agora.DayOfWeek == DayOfWeek.Saturday ||
            agora.DayOfWeek == DayOfWeek.Sunday || agora.DayOfWeek == DayOfWeek.Monday ||
            agora.DayOfWeek == DayOfWeek.Tuesday || agora.DayOfWeek == DayOfWeek.Wednesday)
            return true;
        if (agora.DayOfWeek == DayOfWeek.Thursday && agora.Hour < 19)
            return true;
        return false;
    }
}