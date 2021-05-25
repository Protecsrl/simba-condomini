using System.Data;
using RS;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CAMS.Module.Classi
{
    public class SMSEAMS : IDisposable
    {

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        //string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
        public string SendSMSNew(string UserNameCorrente)
        {
            SMSCConnection smsc_connection = null;
            try
            {
                smsc_connection = new SMSCHTTPConnection("Sms52333", "Prt_040502");//  ("1913233@aruba.it", "1973ardea");
            }
            catch (SMSCException smsc_ex)
            {
                Debug.WriteLine("errore in connessione a Aruba:" + smsc_ex.Message);
                return "errore";
            }
            // operazioni sulla connessione
            try
            {
                SMS sms = new SMS();
                sms.TypeOfSMS = SMSType.ALTA;
                sms.addSMSRecipient("+393463228369");
                sms.addSMSRecipient("+393499876543");
                sms.Message = "hello world!";
                sms.SMSSender = "00393463228369";
                sms.setImmediate(); // oppure sms.setScheduled_delivery(java.util.Date)
                sms.OrderId = "12345";
                SendResult result = smsc_connection.sendSMS(sms);
            }
            catch (SMSCRemoteException smscre)
            {
                System.Console.WriteLine("Exception from Aruba: " + smscre.Message);
            }
            catch (SMSCException smsce)
            {
                System.Console.WriteLine("Exception creating message: " + smsce.Message);
            }

            

            smsc_connection.logout();  // nel caso di HTTP il metodo non lancia mai eccezioni
            return "OK";
        }

        //}


    }
}
