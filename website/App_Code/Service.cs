using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Net.Mime;
using iTextSharp.tool.xml;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class Service
{
	// To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
	// To create an operation that returns XML,
	//     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
	//     and include the following line in the operation body:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
    public string SendEmail(string to, string subject, string note, string charts, string patient_info, string patient)
	{
        //string lpath = "C:\\Users\\mc142\\Source\\Repos\\JourneyCompass\\website\\tmp\\";
        string lpath = "C:\\inetpub\\wwwroot\\tmp\\";

		// Add your operation implementation here
        string[] files = Regex.Split(charts, "!#!");

        // Generate PDF file.

        // First create a table of charts.
        string chart_tbl = "<table border=\"0\"><tr>";
        int i = 0;
        int count = 0;
        int tot_count = files.Length;
        foreach (string fn in files)
        {
            string img_path = "<td><img width=\"350\" src=\""+lpath + fn + "\"/></td>";
            //string img_path = "<td><img border=\"0\" src=\""+lpath + fn + "\"/></td>";
            chart_tbl += img_path;
            i++;
            count++;
            if (i % 2 == 0)
            {
                if (count == tot_count)
                {
                    chart_tbl += "</tr>";
                }
                else
                {
                    chart_tbl += "</tr><tr>";
                }
            }
        }
        if (i % 2 == 1)
        {
            chart_tbl += "<td></td></tr></table>";
        }
        else
        {
            chart_tbl += "</table>";
        }

        string html = "<p align=\"center\"><b>JourneyCompass Symptom Report</b></p><br/><br/><p>"+patient_info+"</p><br/><br/>"+chart_tbl;
        Document document = new Document(PageSize.LETTER, 30, 30, 30, 30);
        MemoryStream msOutput = new MemoryStream();
        TextReader reader = new StringReader(html);

        //PdfWriter pdfwriter = PdfWriter.GetInstance(document, msOutput);
        string unique_name = Guid.NewGuid().ToString() + ".pdf";
        string pdfn = lpath+unique_name;
        PdfWriter pdfwriter = PdfWriter.GetInstance(document, new FileStream(pdfn, FileMode.Create));
        //HTMLWorker worker = new HTMLWorker(document);
        document.Open();
        XMLWorkerHelper worker = XMLWorkerHelper.GetInstance();
        worker.ParseXHtml(pdfwriter, document, reader);
        //worker.Parse(reader);
        //worker.Close();
        document.Close();
        pdfwriter.Close();
        
        // Send an Direct email.
        // string from = "JourneyCompass Symptom Tracker <#>";
        string p_to = to;

        // For testing..
        bool testing = false;
        if (p_to == "#")
        {
            testing = true;
        }

        MailAddress m_from, m_to;
        string host;
        if (testing == true)
        {
            m_from = new MailAddress("#", "# (Direct)");
            m_to = new MailAddress("#", "# (HISP)");
            p_to = "#";
            host = "#";
        }
        else
        {
            m_from = new MailAddress("#", "JourneyCompass Symptom Tracker");
            m_to = new MailAddress(p_to);
            host = "#";
        }
        int port = 25;

		MailMessage message = new MailMessage(m_from, m_to);
        //MailMessage message = new MailMessage(from, p_to);

        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = @"Dear <b>Harbin Clinic</b><br/><br/>This message contains a symptom report.<br/><br/>======== Below is a note from patient =========";
        if (note != null) {
            message.Body += "<pre>" + note + "</pre>";
        }
        //Attachment data = new Attachment(msOutput, "SymptomReport.pdf", "application/pdf");
        Attachment data = new Attachment(pdfn, MediaTypeNames.Application.Pdf);
        message.Attachments.Add(data);
        ContentDisposition disposition = data.ContentDisposition;
        disposition.FileName = patient.Replace(" ", "_") + "_" + DateTime.Now.ToString("MMddHHmmssyyyy");

        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
        SmtpClient client = new SmtpClient(host, port);
        if (testing == true)
        {
            client.Credentials = new System.Net.NetworkCredential("#", "#");
        }

        client.EnableSsl = true;
        client.Send(message);

        data.Dispose();
        //msOutput.Close();
        System.IO.File.Delete(pdfn);

        foreach (string fn in files)
        {
            string fn_path = lpath+fn;
            System.IO.File.Delete(fn_path);
        }

		return "Direct Message Sent Out to <b>"+p_to+"</b>";
	}

    private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
        //throw new NotImplementedException();
    }

	// Add more operations here and mark them with [OperationContract]
}
