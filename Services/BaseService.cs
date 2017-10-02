using EndesaBot.Interfaces;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EndesaBot.Services
{
    [Serializable]
    public abstract class BaseService : IAction
    {
        internal List<string> _actions = new List<string> { "ver", "consultar" };
        internal string _type;
        public abstract string GetResponse(IList<EntityRecommendation> entities);

        public virtual bool Parse(LuisResult result)
        {
            return result.Entities.Any(item => ParseAction(item)) &&
                result.Entities.Any(item => ParseServiceType(item));
        }

        internal virtual bool ParseAction(EntityRecommendation action)
        {
            return action.Type == "ACTION" && _actions.Any(item => item.ToLowerInvariant().Contains(action.Entity.ToLowerInvariant()));
        }

        internal virtual bool ParseServiceType(EntityRecommendation serviceType)
        {
            return serviceType.Type == "SERVICE_TYPE" && serviceType.Entity.ToLowerInvariant().Contains(_type);
        }

    }
}