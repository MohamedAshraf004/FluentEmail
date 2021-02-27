using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FluentEmail
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("localhost")
            {
                EnableSsl = false,//test perposes only
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Port = 25//Port number for the smtp server
                //DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation = @"C:\Demos"
            });
            StringBuilder template = new();
            template.AppendLine("Dear @Model.FriendName");
            template.AppendLine("<p> My Name Is @Model.FirstName , I am .net developer </p>");
            template.AppendLine("Best Wishes");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer(); //As to render the templete as razor text

            var email = await Email.
                From("Mohamed@dev.com") //the client name not the server from which I send the mail {server name maybe gmail}
                .To("MohamedReceiver@test.com")
                .Subject("You are welcome")
                .UsingTemplate(template.ToString(), new { FriendName ="Ahmed", FirstName="Mohamed" })
                //.Body("If I don't have a templete I sue the body to write what I want.")
                .SendAsync();
        }
    }
}
