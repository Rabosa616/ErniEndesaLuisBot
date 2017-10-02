using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using EndesaBot.Interfaces;
using EndesaBot.Services;
using System.Linq;

namespace EndesaBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog<object>
    {
        IGreeting _greeting;

        public GreetingDialog()
        {
            _greeting = new GreetingService();
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.GetType() == typeof(Activity))
            {
                Activity activity = (Activity)message;
                if (activity.MembersAdded != null && activity.MembersAdded.Any())
                {
                    await context.PostAsync(_greeting.GetGreeting(activity.MembersAdded.FirstOrDefault().Name).ToString());
                }
            }

            context.Wait(this.MessageReceivedAsync);
        }
    }
}