using System.Collections.Generic;
using Microsoft.Bot.Builder.Luis.Models;

namespace EndesaBot.Interfaces
{
    public interface IAction
    {
        string GetResponse(IList<EntityRecommendation> entities);
        bool Parse(LuisResult result);
    }
}