//  Emailer.cs
//  http://www.mrventures.net/all-tutorials/sending-emails
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Emailer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txtData;
    [SerializeField] UnityEngine.UI.Button btnSubmit;
    [SerializeField] bool sendDirect;

    const string kSenderEmailAddress = "berserksurvivors@gmail.com";
    const string kSenderPassword = "Berserk.1383";
    const string kReceiverEmailAddress = "yosefabadisadra@gmail.com";


    void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(txtData);
        UnityEngine.Assertions.Assert.IsNotNull(btnSubmit);
        btnSubmit.onClick.AddListener(delegate {
             SendAnEmail(txtData.text);
            
            
        });
    }

    // Method 1: Direct message
    private static void SendAnEmail(string message)
    {
        // Create mail
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(kSenderEmailAddress);
        mail.To.Add(kReceiverEmailAddress);
        mail.Subject = "Email Title";
        mail.Body = message;

        // Setup server 
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential(
            kSenderEmailAddress, kSenderPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        // Send mail to server, print results
        try
        {
            smtpServer.Send(mail);
        }
        catch (System.Exception e)
        {
            Debug.Log("Email error: " + e.Message);
        }
        finally
        {
            Debug.Log("Email sent!");
        }
    }

    // Method 2: Server request
   

    // Method 2: Server request
    
}
