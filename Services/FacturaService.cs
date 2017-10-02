using EndesaBot.Extensions;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EndesaBot.Services
{
    [Serializable]
    public class FacturaService : BaseService
    {
        public FacturaService()
        {
            _type = "factura";
        }
        public override string GetResponse(IList<EntityRecommendation> entities)
        {
            Random _random = new Random();
            string response = "";
            if (entities.Any(item => item.Type == "DATE_TIME" && item.Entity.ParseMonth() !=0))
            {
                string month = entities.FirstOrDefault(item => item.Type == "DATE_TIME").Entity;
                response = $"La factura del mes de {month} es de {Math.Round(_random.NextDouble(0, 200), 2)}";
            }
            else
            {
                string month = DateTime.Now.AddMonths(-1).ToString("MMMM");
                response = $"Su última factura es del {month} y es de: {Math.Round(_random.NextDouble(0, 200), 2)}";
            };
            return response;
        }
    }
}