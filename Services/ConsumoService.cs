using EndesaBot.Extensions;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EndesaBot.Services
{
    [Serializable]
    public class ConsumoService : BaseService
    {
        public ConsumoService()
        {
            _type = "consumo";
        }

        public override string GetResponse(IList<EntityRecommendation> entities)
        {
            Random _random = new Random();
            string response = "";
            if (entities.Any(item => item.Type == "DATE_TIME"))
            {
                if (entities.Any(item => item.Type == "DATE_TIME" && item.Entity.ParseMonth() != 0))
                {
                    string month = entities.FirstOrDefault(item => item.Type == "DATE_TIME").Entity;
                    response = $"Su consumo del mes de {month} es de {_random.Nextint(10000, 200000)}";
                }
                else
                {
                    string month = DateTime.Now.AddMonths(-1).ToString("MMMM");
                    response = $"Su último consumo es del {month} y es de: {_random.Nextint(10000, 200000)}";
                };
            }

            return response;
        }
    }
}