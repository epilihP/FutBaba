using System;
using System.Collections.Generic;

namespace Model.Partidas;
// Representa uma partida de um jogo específico
public class Partida
{
    public Guid Id { get; set; } // Identificador único da partida
    public Guid IdJogo { get; set; } // Referência ao jogo (Jogo.Id)
    public DateTime DataHora { get; set; } // Data e hora da partida
    public List<string> Times { get; set; } = new List<string>(); // Lista de times participantes (pode ser lista de códigos ou nomes)
    public Resultado Resultado { get; set; } // Resultado da partida
}

// Representa o resultado de uma partida
public class Resultado
{
    public string TimeVencedor { get; set; } // Nome ou código do time vencedor
    public string TimePerdedor { get; set; } // Nome ou código do time perdedor
    public int GolsVencedor { get; set; } // Gols do vencedor
    public int GolsPerdedor { get; set; } // Gols do perdedor
    // Adicione outros campos se necessário
}