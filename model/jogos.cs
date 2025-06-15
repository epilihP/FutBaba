using System;
using System.Collections.Generic;

namespace Model.Jogos;
public class Jogo
{
    public Guid Id { get; set; } // Identificador único
    public DataTime Data { get; set; } // Data do jogo
    public string Local { get; set; } // Local do jogo
    public string TipoCampo { get; set; } // Tipo de campo
    public int JogadoresPorTime { get; set; } // Jogadores por time (incluindo goleiro)
    public int? LimiteTimes { get; set; } // Limite opcional de times
    public int? LimiteJogadores { get; set; } // Limite opcional de jogadores
    public List<string> Interessados { get; set; } = new List<string>(); // Códigos dos jogadores interessados
}