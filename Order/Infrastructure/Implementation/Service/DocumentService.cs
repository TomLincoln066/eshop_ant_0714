using ApplicationCore.ViewModel;
using Azure.Storage.Blobs;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using Infrastructure.Contract.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.Implementation.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        public DocumentService(IConfiguration configuration, BlobServiceClient blobServiceClient) 
        {
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
        }

        public async Task GenerateInvoiceDocxAsync(List<OrderViewModel> orders)
        {
            for(int x = 0; x < orders.Count(); x++)
            {
                OrderViewModel order = orders[x];
                
                string email = string.Empty;
                var htmlcontent = new StringBuilder();
                
                htmlcontent.Append(@"
                           <!doctype html>
                           <html>
                                <head>
                                </head>
                                <body>
                                    ");

                htmlcontent.Append(@"<h3>Invoice Copy</h3>");
                if (order != null)
                {

                    email = order.CustomerEmail;
                    htmlcontent.Append(@"<h3> Invoice No: " + "AB00" + order.Id + " & Invoice Date: " + DateTime.UtcNow + "</h3>");
                    htmlcontent.Append(@"<h3> Customer: " + order.CustomerName + "</h3>");
                    htmlcontent.Append(@"<h3> Address: " + order.ShippingAddress + "</h3>");
                    htmlcontent.Append(@"<h3> Email: " + order.CustomerEmail + " </h3>");
                    htmlcontent.Append(@"<h3> Payment: " + order.PaymentName + " </h3>");
                    htmlcontent.Append(@"<h3> Shipment: " + order.Shipping + " </h3>");

                    htmlcontent.Append(@"<table>
                    <thead>
                    <tr>
                    <td> Product Code </td>
                    <td>Qty</td>
                    <td>Discount</td>
                    <td>Price</td>
                    </tr>
                    </thead >
                    <tbody>");

                    order.OrderDetail.ForEach(item =>
                    {
                            htmlcontent.Append(@"<tr>");
                            htmlcontent.Append(@"<td>" + item.ProductName + "</td>");
                            htmlcontent.Append(@"<td>" + item.Qty + "</td >");
                            htmlcontent.Append(@"<td>" + item.Discount + "</td>");
                            htmlcontent.Append(@"<td> " + item.Price + "</td >" +
                                "</tr>");
                    });

                    htmlcontent.Append(@"
                    </tbody>
                    </table>
                    </div>
                    <div>
                    <table>
                    <tr>
                    <td> Summary Total </td>
                    </tr>");
                    orders.ForEach(item =>
                    {
                        htmlcontent.Append(@"<tr>");
                        htmlcontent.Append(@"<td> " + item.BillAmount + " </td>");
                        htmlcontent.Append(@"</tr>");
                    });
                    htmlcontent.Append(@"</table>");
                    htmlcontent.Append(@"</div>");
                }
                htmlcontent.Append(@"
                                </body>
                            </html>");


                string htmlCode = htmlcontent.ToString();
                string fileName = "Invoice_" + email;
                using (MemoryStream stream = new MemoryStream())
                {
                    using (WordprocessingDocument package = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
                    {

                        MainDocumentPart mainPart = package.MainDocumentPart;
                        if (mainPart == null)
                        {
                            mainPart = package.AddMainDocumentPart();
                            new DocumentFormat.OpenXml.Wordprocessing.Document(new Body()).Save(mainPart);
                        }


                        HtmlConverter converter = new HtmlConverter(mainPart);
                        Body body = mainPart.Document.Body;
                        converter.ConsiderDivAsParagraph = false;

                        var paragraphs = converter.Parse(htmlCode);
                        for (int i = 0; i < paragraphs.Count; i++)
                        {
                            body.Append(paragraphs[i]);
                        }

                        mainPart.Document.Save();
                    }

                    // Saving loacl directory

                    string localDirectoryPath = _configuration["FileLocation:DirectoryPath"].Trim().ToString();

                    if (!Directory.Exists(localDirectoryPath))
                    {
                        Directory.CreateDirectory(localDirectoryPath);
                    }

                    string filePath = System.IO.Path.Combine(localDirectoryPath, $"{fileName}.docx");

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        fileStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
                        fileStream.Dispose();
                    }

                    // Saving Azure Blob Storage

                    BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("orderinvoice");
                    if(await blobContainerClient.ExistsAsync())
                    {
                        Console.WriteLine("Container 'orderinvoice' already exists.");
                    }
                    else
                    {
                        // Create the container
                        await blobContainerClient.CreateAsync();
                        Console.WriteLine("Container 'orderinvoice' created successfully.");
                    }

                    string blobName = "Invoice_" + email +".docx";

                    BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

                    stream.Seek(0, SeekOrigin.Begin);
                    await blobClient.UploadAsync(stream, true);

                    var fileUrl = blobClient.Uri.AbsoluteUri;

                    stream.Dispose();
                }
            }
        }
    }
}
