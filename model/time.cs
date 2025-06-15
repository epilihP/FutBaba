using System;
using System.Collections.Generic;

namespace Model.Times;
// Representa um time em uma partida
public class Time
{
    public Guid Id { get; set; } // Identificador único do time
    public string Nome { get; set; } // Nome do time (opcional)
    public List<string> Jogadores { get; set; } = new List<string>(); // Lista de códigos dos jogadores do time
    public string Goleiro { get; set; } // Código do goleiro do time
    public int NumeroDefesa { get; set; } // Quantidade de jogadores de defesa
    public int NumeroAtaque { get; set; } // Quantidade de jogadores de ataque
    // Adicione outros atributos se necessário para critérios de geração de times
}