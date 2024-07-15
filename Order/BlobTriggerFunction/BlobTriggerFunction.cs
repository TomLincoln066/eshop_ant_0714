using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Azure.Functions.Worker;
using MimeKit;
using MimeKit.Text;
using MimeKit.IO;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace BlobTriggerFunction
{
    public class BlobTriggerFunction
    {

        [Function("ProcessBlobAndSendEmail")]
        public static void Run([BlobTrigger("orderinvoice/{name}", Connection = "AzureWebJobsStorage")] byte[] blobBytes, string name, ILogger log)
        {
            try
            {
                string to = name.Replace("Invoice_", "");
                string To = to.Replace(".docx", "");
                string fileName = name.Substring(0, 7);
                // Create an email message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("eShop", "raj.kiran@antra.com"));
                message.To.Add(new MailboxAddress(To, To));
                message.Subject = "Order is Placed Successfully";

                // Create the message body
                var textPart = new TextPart(TextFormat.Plain)
                {
                    Text = "Thank you for ordering the products. Please check your invoice attached to this email. Thank You."
                };

                using (var blobStream = new MemoryStream(blobBytes))
                {
                    
                    // Attach the blob as an attachment
                    var attachment = new MimePart("application/octet-stream")
                    {
                        Content = new MimeContent(blobStream),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = fileName // Use the blob name as the attachment filename
                    };

                    // Add the attachment to the email
                    message.Body = new Multipart("mixed")
                    {
                         textPart,
                         attachment
                    };

                    // Send the email using SMTP
                    using (var client = new SmtpClient())
                    {
                        // Connect to the SMTP server (provide the server details)
                        client.Connect("smtp.gmail.com", 587, false);
                        
                        // Authenticate with your SMTP credentials
                        client.Authenticate("raj.kiran@antra.com", "yourpassword");

                        // Send the email
                        client.Send(message);

                        // Disconnect from the SMTP server
                        client.Disconnect(true);
                    }

                    Console.WriteLine($"Email sent with attachment: Success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing blob: {0}", ex.Message);
            }
        }
    }
}
