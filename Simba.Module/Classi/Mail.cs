using System.Data;

using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CAMS.Module.Classi
{
    internal class Mail : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private string _SmtpServer;
        private int _SmtpSrvPorta;
        private string _UserSmtp;
        private string _pwdsmtp;

        private string _MailFrom;
        private string _MailToCc;

        public Mail(string SmtpServer, int SmtpSrvPorta, string UserSmtp, string pwdsmtp)
        {
            this._SmtpServer = SmtpServer;
            this._SmtpSrvPorta = SmtpSrvPorta;
            this._UserSmtp = UserSmtp;
            this._pwdsmtp = pwdsmtp;
        }

        public Mail(string SmtpServer, int SmtpSrvPorta, string UserSmtp, string pwdsmtp, string MailFrom, string MailToCc)
        {
            this._SmtpServer = SmtpServer;
            this._SmtpSrvPorta = SmtpSrvPorta;
            this._UserSmtp = UserSmtp;
            this._pwdsmtp = pwdsmtp;
            this._MailFrom = MailFrom;
            this._MailToCc = MailToCc;
        }

        public string InviaMailAvvisoSchedeMP(string UserNameCorrente, DataView dv)
        {
            if (!string.IsNullOrEmpty(dv[0]["DESEMAIL"].ToString()))
            {
                var MailMessage = new MailMessage();
                //  var SmtpServer = dv[0]["SERVERSMTP"].ToString();    var UserSmtp = dv[0]["USERSSMTP"].ToString();      var pwdsmtp = dv[0]["PWSSMTP"].ToString();   var SmtpSrvPorta = dv[0]["PORTASMTP"].ToString();
                //  var MailFrom = dv[0]["MAILFROM"].ToString();      var MailToCc = dv[0]["MAILTOCC"].ToString();
                //  var OggettoMail = dv[0]["OBJECTMAIL"].ToString();
                //    var sbCorpoMail = new StringBuilder(dv[0]["EMAILBODY"].ToString(), 32000000);
                var OggettoMail = dv[0]["OBJECTMAIL"].ToString();
                var sbCorpoMail = new StringBuilder(dv[0]["EMAILBODY"].ToString(), 32000);
                var OidDettaglioLog = dv[0]["OIDDETTLOG"].ToString();
                var NomeFileMail = dv[0]["NomeFileMail"].ToString();

                var ClientPosta = new SmtpClient(this._SmtpServer, this._SmtpSrvPorta) { Credentials = new System.Net.NetworkCredential(this._UserSmtp, this._pwdsmtp) };
                var From = new MailAddress(this._MailFrom, "EAMS ENGIE Italia Spa", System.Text.Encoding.UTF8);
                MailMessage.From = From;
                foreach (DataRowView dvrow in dv)
                {
                    string nomecognomedestinatario = dvrow["DESNOMECOGNOME"].ToString() != null ? dvrow["DESNOMECOGNOME"].ToString().Trim() : dvrow["DESEMAIL"].ToString().Trim();
                    var to = new MailAddress(dvrow["DESEMAIL"].ToString().Trim(), dvrow["DESNOMECOGNOME"].ToString().Trim());
                    MailMessage.To.Add(to);
                }

                MailMessage.CC.Add(new MailAddress(this._MailToCc.Trim()));
                MailMessage.Subject = OggettoMail;
                MailMessage.Body = sbCorpoMail.ToString();

                try
                {
                    ClientPosta.Send(MailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mail Avviso Mofifiche Schede MP non Spedito" + ex.ToString());
                }
                var PathNameFileSave = GetFileName(NomeFileMail);
                try
                {
                    ClientPosta.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    ClientPosta.PickupDirectoryLocation = System.IO.Path.GetDirectoryName(PathNameFileSave);
                    ClientPosta.Send(MailMessage);
                    MailMessage.Dispose();

                    if (!string.IsNullOrEmpty(RiNominaFile(PathNameFileSave, ".eml")))
                    {
                        return String.Format("{0}.eml;{1}", PathNameFileSave, OidDettaglioLog);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mail Avviso Mofifiche Schede MP non Spedito" + ex.ToString());
                }

                return String.Format("{0};{1}", string.Empty, string.Empty);
            }
            else
            {
                var OidDettaglioLog = dv[0]["OIDDETTLOG"].ToString();

                return String.Format("{0};{1}", "Mail Avviso Non Spedita, non sono previsti destinatari.", OidDettaglioLog);
            }
        }

        public string ReInvioMailLogContrPeriodici(List<CAMS.Module.DBAngrafica.Destinatari> dvDestinatari, int LogOid,string OggettoMail, StringBuilder sbCorpoMail,out string NomeFileMail)
        {
            NomeFileMail = "";
            var MailMessage = new MailMessage();
            if (dvDestinatari.Count() > 0)  // oidEdificio   IN  number,
            {
                StringBuilder CorpoMail = new StringBuilder(sbCorpoMail.ToString(), 32000);
                 NomeFileMail = String.Format("CP_{0}_{1}_Reinvio.eml", SetVarSessione.CodSessioneWeb, LogOid);

                var ClientPosta = new SmtpClient(this._SmtpServer, this._SmtpSrvPorta) { Credentials = new System.Net.NetworkCredential(this._UserSmtp, this._pwdsmtp) };
                var From = new MailAddress(this._MailFrom, "EAMS ENGIE Italia Spa", System.Text.Encoding.UTF8);
                MailMessage.From = From;
                foreach (var dvrow in dvDestinatari)
                {
                    var to = new MailAddress(dvrow.Email.ToString().Trim(), string.Format("{0}, {1}", dvrow.Nome.ToString().Trim(), dvrow.Cognome.ToString().Trim()));
                    MailMessage.To.Add(to);
                }

                MailMessage.CC.Add(new MailAddress(this._MailToCc.Trim()));
                MailMessage.Subject = OggettoMail;
                MailMessage.Body = sbCorpoMail.ToString();
                try
                {
                    ClientPosta.Send(MailMessage);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Mail Avviso Controlli Normativi non Spedito" + ex.ToString());
                }


                var PathNameFileSave = GetFileName(NomeFileMail);
                try
                {
                    ClientPosta.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    ClientPosta.PickupDirectoryLocation = System.IO.Path.GetDirectoryName(PathNameFileSave);
                    ClientPosta.Send(MailMessage);
                    MailMessage.Dispose();

                    NomeFileMail = RiNominaFile(PathNameFileSave, ".eml");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mail Avviso Controlli Normativi non Spedito" + ex.ToString());
                }

                return String.Format("Inviata Mail:{0}", OggettoMail);
            }
            else
            {               
                return String.Format("{0}", "Mail Avviso Non Spedita, non sono previsti destinatari.");
            }
        }

        private string GetFileName(string NomefileTemp)
        {
            var Revisione = 0;
            var FileTemp = string.Empty;
            var FileName = System.IO.Path.Combine(SetVarSessione.PhysicalPathSito, "Temp", NomefileTemp, NomefileTemp);/// creo directori se necessario
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
