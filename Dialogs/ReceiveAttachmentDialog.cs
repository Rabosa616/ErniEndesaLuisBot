﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.IO;
using System.Collections.Generic;

namespace EndesaBot.Dialogs
{
    [Serializable]
    public class ReceiveAttachmentDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Attachments != null && message.Attachments.Any())
            {
                var attachment = message.Attachments.First();
                using (HttpClient httpClient = new HttpClient())
                {
                    // Skype & MS Teams attachment URLs are secured by a JwtToken, so we need to pass the token from our bot.
                    if ((message.ChannelId.Equals("skype", StringComparison.InvariantCultureIgnoreCase) || message.ChannelId.Equals("msteams", StringComparison.InvariantCultureIgnoreCase))
                        && new Uri(attachment.ContentUrl).Host.EndsWith("skype.com"))
                    {
                        var token = await new MicrosoftAppCredentials().GetTokenAsync();
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    var responseMessage = await httpClient.GetAsync(attachment.ContentUrl);

                    var contentLenghtBytes = responseMessage.Content.Headers.ContentLength;

                    await context.PostAsync($"Attachment {attachment.Name} of {attachment.ContentType} type and size of {contentLenghtBytes} bytes received.");
                    var replyMessage = context.MakeMessage();

                    using (var connector = new ConnectorClient(new Uri(replyMessage.ServiceUrl)))
                    {
                        var attachments = new Attachments(connector);
                        var response = await attachments.Client.Conversations.UploadAttachmentAsync(
                            replyMessage.Conversation.Id,
                            new AttachmentData
                            {
                                Name = "contrato.pdf",
                                OriginalBase64 = File.ReadAllBytes(@"C:/Users/vaqueroo/Downloads/DNI.pdf"),
                                Type = "application/pdf"
                            });

                        var attachmentUri = attachments.GetAttachmentUri(response.Id);

                        replyMessage.Attachments = new List<Attachment> {  new Attachment
                        {
                            Name = "contrato.pdf",
                            ContentType = "application/pdf",
                            ContentUrl = attachmentUri
                        } }
                        ;
                        await context.PostAsync(replyMessage);
                    }
                }
            }
            else
            {
                await context.PostAsync("Hi there! I'm a bot created to show you how I can receive message attachments, but no attachment was sent to me. Please, try again sending a new message including an attachment.");
            }

            context.Wait(this.MessageReceivedAsync);
        }
    }
}