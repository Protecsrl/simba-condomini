using System.Data;

using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace CAMS.Module.CAMSInvioMailCN
{
    class Invio : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string SendMailNew(SmtpClient ClientPosta, MailMessage MessaggioMail, string NomeFileMail)
        {
            CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1(); ///   spedizione della email     vin
            string Error = "Invio Eseguito";       // impostazione di accesso al server di posta       vin 
            try
            {
                ClientPosta.Send(MessaggioMail); 
            }
            catch (SmtpException ex)
            {
                Error = String.Format("ClientPosta.Send (ex.ToString): {0} \n StatusCode: {1}", ex, ex.StatusCode);
                Console.WriteLine(Error);
                cl.TxtLogSpedizioni(Error, true);
            }            // salvataggio della email
            var PathNameFileSave = GetFileName(NomeFileMail);
            try
            {
                ClientPosta.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                ClientPosta.PickupDirectoryLocation = System.IO.Path.GetDirectoryName(PathNameFileSave);
                ClientPosta.Send(MessaggioMail);     //  MessaggioMail.Dispose();
            }
            catch (SmtpException ex)
            {
                Error = String.Format("ClientPosta.Send(ex.ToString): {0} \n StatusCode: {1}", ex, ex.StatusCode);
                Console.WriteLine(Error);
                cl.TxtLogSpedizioni(Error, true);
            }     // registrazione della email spedita    
       
            return Error;
        }

        //public void SendMailReport(StringBuilder Corpo, string Subject, string MailTo, string MailToCC)
        //{
        //    // Command line argument must the the SMTP host.
        //    MailMessage MailMessage = new MailMessage();
        //    string SmtpServer = SPMP.PInv.SMTPSERVER;
        //    string UserSmtp = SPMP.PInv.USERSMTP;
        //    string pwdsmtp = SPMP.PInv.PWDSMTP;
        //    int SmtpSrvPorta = SPMP.PInv.SMTPSERVERPOSTA;
        //    string Error = "";
        //    // impostazione di accesso al server di posta
        //    SmtpClient ClientPosta = new SmtpClient(SmtpServer, SmtpSrvPorta);
        //    ClientPosta.Credentials = new System.Net.NetworkCredential(UserSmtp, pwdsmtp);
        //    // imposta gli indirizzi
        //    System.Net.Mail.MailAddress FORM = new MailAddress(SPMP.PInv.MAILFROM, "SIR" + " Cofely Italia Spa", System.Text.Encoding.UTF8);
        //    MailMessage.From = FORM;

        //    string[] splitTo = MailTo.Split(new Char[] { ';' });
        //    foreach (string Codice in splitTo)
        //    {
        //        MailAddress to = new MailAddress(Codice.Trim());  //Dest
        //        MailMessage.To.Add(to);
        //    }
        //    if (MailToCC.Length != 0)
        //    {
        //        string[] splitToCC = MailToCC.Split(new Char[] { ';' });
        //        foreach (string Codice in splitToCC)
        //        {
        //            MailAddress tocc = new MailAddress(Codice.Trim());  //Dest
        //            MailMessage.CC.Add(tocc);
        //        }
        //    }

        //    //  message.IsBodyHtml = true;
        //    MailMessage.Subject = Subject;
        //    //// Create  the file attachment for this e-mail message.
        //    MailMessage.Body = Corpo.ToString();
        //    try
        //    {
        //        //  Console.WriteLine(message.Attachments.Count);     
        //        ClientPosta.Send(MailMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Error += ex.ToString();
        //        SPMP.SPMP.TxtLogSpedizioni("Report Finale non Spedito", true);
        //        Console.WriteLine("Report Finale non Spedito" + Error);
        //    }


        //    try
        //    {
        //        MailMessageExtension.MailMessageExt.Save(MailMessage, SPMP.PInv.FullPathFileSaveMail);

        //    }
        //    catch (Exception ex)
        //    {
        //        Error += ex.ToString();
        //        Console.WriteLine(ex.ToString());
        //        SPMP.SPMP.TxtLogSpedizioni("Report Finale non Salvato", true);
        //    }

        //    MailMessage.Dispose();
        //    Console.WriteLine("Report Finale  Salvato");
        //}



        private string GetFileName(string NomefileTemp)
        {
            var Revisione = 0;
            var FileTemp = string.Empty;

            //var FileName = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp", NomefileTemp, NomefileTemp);/// creo directori se necessario
            var FileName = System.IO.Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "Temp", NomefileTemp, NomefileTemp);/// creo directori se necessario
          
            if (!Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));
            }
            if (File.Exists(FileName))
            {
                while (true)
                {
                    FileTemp = FileName;
                    FileTemp = FileTemp.Replace(".eml", String.Format("_R{0}.eml", Revisione.ToString()));
                    if (!File.Exists(FileTemp))
                    {
                        break;
                    }
                    Revisione++;
                }
                FileName = FileTemp;
            }

            return FileName;
        }

        private string RiNominaFile(string PathNameFileSave, string estenzionefile)
        {
            var Risultato = string.Empty;
            var NFilepiuRecente = string.Empty;
            var indxFilepiuRecente = -1;
            var dir = System.IO.Path.GetDirectoryName(PathNameFileSave);
            var retval = DateTime.MinValue;

            var dirInfo = new DirectoryInfo(dir);

            var files = dirInfo.GetFiles("*.eml", SearchOption.TopDirectoryOnly);
            for (var i = 0; i < files.Length; i++)
            {
                if (files[i].LastWriteTime > retval)
                {
                    retval = files[i].LastWriteTime;
                    NFilepiuRecente = files[i].FullName;
                    indxFilepiuRecente = i;
                }
            }
            if (indxFilepiuRecente >= 0)
            {
                Risultato = files[indxFilepiuRecente].CopyTo(PathNameFileSave + estenzionefile, true).ToString();
            }
            return Risultato;
        }


    }
}
