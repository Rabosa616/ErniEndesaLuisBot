using EndesaBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndesaBot.Services
{
    [Serializable]
    public class GreetingService : IGreeting
    {
        public string GetGreeting()
        {
            return "Hola soy el asistente de Endesa, como te llamas?";
        }

        public string GetGreeting(string name)
        {
            return $"Hola {name}, ¿cómo puedo ayudarte?";
        }
    }
}