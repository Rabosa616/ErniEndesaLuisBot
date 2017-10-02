using EndesaBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndesaBot.Services
{
    [Serializable]
    public class HelpService : IHelp
    {

        private IList<string> helpOptions = new List<string> { "“quiero contratar un servicio”",
                                                              "“quiero ver la última factura”",
                                                              "“quiero ver la factura del mes de agosto”",
                                                              "“quiero ver el consumo del mes de enero”",
                                                              "“quiero introducir el consumo del último mes”"};

        public string GetHelp()
        {
            string response = string.Join(" \n\n", helpOptions.ToArray());
            response = $"Hola, pruebe a decirme cosas como: \n\n {response}";
            return response;
        }
    }
}