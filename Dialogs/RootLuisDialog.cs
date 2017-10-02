using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using EndesaBot.Interfaces;
using EndesaBot.Services;
using Microsoft.Bot.Builder.FormFlow;

namespace EndesaBot.Dialogs
{
    [LuisModel("8d050e61-542c-4e1a-8bfc-79c3bbc546b8", "256078b147dd45cb85845d5037447464")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        //private Activity _activity;
        private IGreeting _greeting;
        private IHelp _help;
        private List<IAction> _actions;

        public RootLuisDialog()
        {
            //_activity = activity;
            _greeting = new GreetingService();
            _help = new HelpService();
            _actions = new List<IAction> { new FacturaService(), new ContratoService(), new ConsumoService() };
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Perdon, no he entendido '{result.Query}'. Escriba 'ayuda' si necessita mas información.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ACTIONS")]
        public async Task Actions(IDialogContext context, LuisResult result)
        {
            string response = _help.GetHelp();
            string action = result.Entities.FirstOrDefault(entity => entity.Type == "ACTION").Entity;
            if (_actions.Any(item => item.Parse(result)))
            {
                response = _actions.FirstOrDefault(item => item.Parse(result)).GetResponse(result.Entities);

                await context.PostAsync(response.ToString());

                context.Wait(this.MessageReceived);
                return;
            }

            if (action.ToLowerInvariant().Contains("introducir"))
            {
                var insert = new InsetConsumtion();
                var InsetConsumtionFormDialog = new FormDialog<InsetConsumtion>(insert, this.BuildInsertForm, FormOptions.PromptInStart, result.Entities);

                context.Call(InsetConsumtionFormDialog, this.ResumeAfterInsert);
                return;
            }

            if (action.ToLowerInvariant().Contains("contratar"))
            {
                var contract = new Contract();
                var ContractFormDialog = new FormDialog<Contract>(contract, this.BuildContractForm, FormOptions.PromptInStart, result.Entities);

                context.Call(ContractFormDialog, this.ResumeAfterContract);
                return;
            }
            context.Wait(this.MessageReceived);
            return;
        }

        private IForm<InsetConsumtion> BuildInsertForm()
        {
            return InsetConsumtion.BuildForm();
        }

        private IForm<Contract> BuildContractForm()
        {
            return Contract.BuildForm();
        }

        private async Task ResumeAfterInsert(IDialogContext context, IAwaitable<InsetConsumtion> result)
        {
            InsetConsumtion consumtion = await result;
            await context.PostAsync($"Gracias, hemos introducido su consumo de {consumtion.Tipo} del mes de {DateTime.Now.ToString("MMMM")}");
            context.Done<object>(null);
        }

        private async Task ResumeAfterContract(IDialogContext context, IAwaitable<Contract> result)
        {
            Contract consumtion = await result;
            //await context.PostAsync($"Para finalizar, envienos su dni escaneado, por favor");
            await context.PostAsync($"En breve nos pondremos en contacto con usted.");
            context.Done<object>(null);
        }

        internal static IDialog<InsetConsumtion> MakeRoot()
        {
            return Chain.From(() => FormDialog.FromForm(InsetConsumtion.BuildForm));
        }

        [LuisIntent("HELP")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(_help.GetHelp().ToString());
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CUSTOMER_IDENTIFICATION")]
        public async Task CustomerIdenfitication(IDialogContext context, LuisResult result)
        {
            if (result.Entities != null && result.Entities.Any())
            {
                await context.PostAsync(_greeting.GetGreeting(result.Entities.FirstOrDefault().Entity));
            }
            else
            {
                await context.PostAsync(_greeting.GetGreeting().ToString());
            }

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("GREETING")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(_greeting.GetGreeting().ToString());

            context.Wait(this.MessageReceived);
        }
    }


}