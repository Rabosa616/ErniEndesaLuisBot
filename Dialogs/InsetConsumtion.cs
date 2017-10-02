using System;
using Microsoft.Bot.Builder.FormFlow;

namespace EndesaBot.Dialogs
{
    [Serializable]
    public class InsetConsumtion
    {   

        public ContractType Tipo { get; set; }
        public string Consumo { get; set; }

        public static IForm<InsetConsumtion> BuildForm()
        {
            return new FormBuilder<InsetConsumtion>()
                    .Message("¿Que tipo de consumo quiere introducir?")
                    .Build();
        }
    }

    [Serializable]
    public enum ContractType
    {
        IGNORE,
        Gas,
        Luz
    }


    [Serializable]
    public enum ContactMethod
    {
        IGNORE,
        Telefono,
        SMS,
        Email
    }

    [Serializable]
    public class Contract
    {
        public ContractType Tipo { get; set; }
        public string SuNombre { get; set; }
        public string SusApellidos { get; set; }
        public string Direcion { get; set; }
        public string NumeroDeContacto { get; set; }
        public string Email { get; set; }
        public ContactMethod FormaDeContacto { get; set; }

        public static IForm<Contract> BuildForm()
        {
            return new FormBuilder<Contract>()
                    .Message("¿Que tipo de servicio quiere contratar?")
                    .Build();
        }
    }
}