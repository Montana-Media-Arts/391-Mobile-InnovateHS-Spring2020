using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Email
/// </summary>
public static class Email
{
    //Generates a SmtpClient connection
    private static SmtpClient getClient()
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.Credentials = new System.Net.NetworkCredential("InnovateHighSchool@gmail.com", "innovatehs!1");
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        return client;
    }

    //Sends the email based on the provided values.
    public static bool sendEmail(string sendAddress, string subject, string body, string sender = "InnovateHS")
    {
        MailMessage message = new MailMessage("InnovateHighSchool@gmail.com", sendAddress);
        message.From = new MailAddress("InnovateHighSchool@gmail.com", sender);
        message.Subject = subject;
        message.Body = body;

        try
        {
            getClient().Send(message);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}