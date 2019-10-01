using System;
using MetroFramework.Controls;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public class ConfiguraoLancamentoDiario
    {
        // Qual horario deve ser o início do dia, por default 00:00 (meia-noite)
        //public TimeSpan HorarioInicioDoDia { get; set; }

        public EnumFormaDoLembrete FormaDoLembrete { get; set; }
        public TimeSpan HorarioDoLembrete { get; set; }
        
        public bool Habilitar { get; set; }
    }
}