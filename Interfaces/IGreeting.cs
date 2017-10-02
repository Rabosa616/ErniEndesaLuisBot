using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndesaBot.Interfaces
{
    public interface IGreeting
    {
        string GetGreeting();
        string GetGreeting(string name);
    }
}