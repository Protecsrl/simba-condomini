using CAMS.Module.Classi;
using RS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
//using System.Data.datas;

namespace CAMS.Module.CAMSInvioMailCN
{
    public class SetMail : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region trasmetti mail per controlli normativo alias avvisi normativi
        public void InviaReport(string Connessione, string UserNameCorrente, int OidEdificio, int TuttiZeroUnoUno, ref string Messaggio
                                                  , DateTime DataDA, DateTime DataA)
        {// invia i report per ogni immobile
            string TxtLog = "";
            int Anno = DateTime.Now.Year;
            PInv.InvAnno = 0;
            PInv.InvMese = 0;
            PInv.IDSessione = Convert.ToString(DateTime.Now); ////PInv.IDSessione =   "05/04/2010 0.19.46";
            //string TemPath = Path.Combine(Environment.CurrentDirectory, "FileLog");
            string TemPath = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "FileLog");
            if (!Directory.Exists(TemPath))
                Directory.CreateDirectory(TemPath);
            ////  creao path per email
            //string TemPathMail = Path.Combine(Environment.CurrentDirectory, "Mail");
            string TemPathMail = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "Mail");
            if (!Directory.Exists(TemPathMail))
                Directory.CreateDirectory(TemPathMail);
            PInv.FullPathMail = TemPathMail;

            string NomeFIleSessione = PInv.IDSessione.Replace("/", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(".", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(" ", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(":", "-");
            PInv.NomeFileSessione = NomeFIleSessione;
            PInv.FileLogText = Path.Combine(TemPath, Path.GetFileName(NomeFIleSessione + ".txt"));
            ///////////èè/////////////////////////////////////////////

            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
            ////////////
            SmtpClient ClientPosta = GetParametriMail(gdb);
            if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
            {
                TxtLog = "Parametri Assenti";
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            else
            {
                TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                cl.TxtLogSpedizioni(TxtLog, true);
            }
            ///  istanza del messaggio  imposto i parametrifissi          
            MailMessage MailMessaggio = new MailMessage();
            MailAddress FORM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailMessaggio.From = FORM;

            if (PInv.MAILTOCC.Length != 0)
            {
                string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                foreach (string Codice in splitToCC)
                {
                    MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                    MailMessaggio.CC.Add(CC);
                }
            }
            cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);

            ///  recupero edificiDateTime DataDA, DateTime DataA)
            DataTable TabellaEdifici = gdb.GetEdifici(UserNameCorrente, TuttiZeroUnoUno, OidEdificio, DataDA, DataA);
            if (TabellaEdifici.Rows.Count == 0)
            {
                TxtLog = "Non ci sono edifici";
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            foreach (DataRow rowBL in TabellaEdifici.Rows)//  giro su edifici         @@@@@@@@@@@@@@@@  giro edifici
            {
                PInv.FullPathFileSaveMail = "";
                PInv.ErroriFileAllegatoNoTrovati = "";
                //----------------------------------------------
                string IMMOBILE = rowBL["IMMOBILE"].ToString(); // oid immobile
                string DESC_EDIFICIO = rowBL["DESC_EDIFICIO"].ToString();  // bl_id
                PInv.TotEdifici += 1;// totale edifici processati
                ///---------------------------------------------
                cl.TxtLogSpedizioni("Report Spedizione CN per immobile: " + DESC_EDIFICIO, true);
                ///// Carico Tabella Destinatari
                DataTable TDestinatari = gdb.GetDestinatari(UserNameCorrente, int.Parse(IMMOBILE), DataDA, DataA);
                if (TDestinatari.Rows.Count == 0)  // Se non ci sono destinatari conto gli edifici senza
                {
                    TxtLog = "Non ci sono destinatari per questo immobile: " + DESC_EDIFICIO;
                    cl.TxtLogSpedizioni(TxtLog, true);

                    if (PInv.ErroriEdificiNoInvio.Length == 0)
                    {
                        PInv.ErroriEdificiNoInvio = DESC_EDIFICIO;
                    }
                    else
                    {
                        PInv.ErroriEdificiNoInvio += ", " + DESC_EDIFICIO;
                    }
                }
                else           ///////////  se i destinatari sono maggiori di zero ##############################################
                {
                    string DestinatariUniti = "";
                    MailMessaggio.To.Clear();
                    foreach (DataRow RowDestinatariUniti in TDestinatari.Rows) // row4 rowdestinatari
                    {
                        string NomeDestinatario = RowDestinatariUniti["NOME"].ToString();
                        System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti["DESEMAIL"].ToString().Trim(), NomeDestinatario);
                        MailMessaggio.To.Add(TO);
                        DestinatariUniti += RowDestinatariUniti["DESEMAIL"].ToString() + "; ";
                        PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("CP_{0}_{1}_{2}_DestUni.txt", PInv.NomeFileSessione, DESC_EDIFICIO, (Anno - 2000)));

                    }
                    cl.TxtLogSpedizioni("Destinatari Mail: " + DestinatariUniti, true);
                    // imposta corpo
                    StringBuilder corpo = new StringBuilder("", 32000);

                    DataTable TMessaggioMail = gdb.GetMessaggioEMailCN(UserNameCorrente, int.Parse(IMMOBILE), DataDA, DataA);
                    foreach (DataRow rowt_MsgMail in TMessaggioMail.Rows)
                    {
                        string Tipo = rowt_MsgMail["ORDINE"].ToString();
                        string Stringa = rowt_MsgMail["TESTO"].ToString();
                        if (rowt_MsgMail["ORDINE"].ToString().Contains("oggetto"))
                        {
                            MailMessaggio.Subject = Stringa;
                        }
                        else
                        {
                            corpo.Append(Stringa + "\n");
                        }
                    }
                    MailMessaggio.Body = corpo.ToString();
                    cl.TxtLogSpedizioni("Corpo Mail: " + corpo.ToString(), true);
                    cl.TxtLogSpedizioni("Fine Immobile -------------------------- ", true);
                    cl.TxtLogSpedizioni("  ", true);

                    //  MailMessaggio.Attachments.Clear();  //ANNULLO GLI ALLEGATI DA ELIMINARE  
                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
                    using (var inv = new Invio())
                    {
                        int Esito = 0;
                        Messaggio = "Email di Avviso Inviata!";
                        string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                        if (!string.IsNullOrWhiteSpace(Ritorno))
                        {
                            Messaggio = "Email di Avviso Inviata, con Avvertimenti!";
                            Esito = 1;
                        }

                        gdb.LogMessaggioEMailCN(UserNameCorrente, OidEdificio, MailMessaggio, DateTime.Now, Esito, DataDA, DataA);
                    }

                    ///********************************************************************************************************




                } ////  chide if destinatari nulli su else
            }  // chiude ciclo for per edifici
            /// invio log invio amministratore di sistema 
            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("CP_{0}_{1}_{2}_DestUni.txt", PInv.NomeFileSessione, "Admin", (Anno - 2000)));
            //Invio Log Spedizione - Area di Polo Energia Centrale: Centrale di COLLIO, Avviso Scadenze Controlli Periodici Normativi Data invio: 20/08/2015
            //string oggetto = MailMessaggio.Subject.ToString();
            string oggetto = string.Format("Invio Log Spedizione -  Avviso Scadenze Avvisi Periodici Data invio: ", DateTime.Now.ToShortDateString());
            SmtpClient ClientPostaAdmin = new SmtpClient(PInv.SMTPSERVER, PInv.SMTPSERVERPOSTA) { Credentials = new System.Net.NetworkCredential(PInv.USERSMTP, PInv.PWDSMTP) };

            MailMessage MailM = new MailMessage();
            FORM = new MailAddress(PInv.MAILFROM, "EAMS ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailM.From = FORM;
            MailM.To.Clear();
            MailM.Attachments.Clear();  //ANNULLO GLI ALLEGATI DA ELIMINARE  
            if (PInv.MAILTOAdmin.Length != 0)
            {
                string[] splitTo = PInv.MAILTOAdmin.Split(new Char[] { ';' });
                foreach (string Codice in splitTo)
                {
                    MailAddress to = new MailAddress(Codice.Trim(), "Amministratore CAMS");  //Dest
                    MailM.To.Add(to);
                }
            }

            MailM.Body = cl.LeggiTxtLogSpedizioni();
            MailM.CC.Clear();
            MailM.Subject = oggetto;
            using (var inv = new Invio())
            {
                inv.SendMailNew(ClientPostaAdmin, MailM, PInv.FullPathFileSaveMail);
            }


        }

        private SmtpClient GetParametriMail(CAMSInvioMailCN.GetDataDB gdb)
        {

            DataTable TabellaParametriEmail = gdb.GetParametriMail();
            //GetParametriEmail(PInv.USERNAME, "CP");
            if (TabellaParametriEmail.Rows.Count > 0)
            {
                foreach (DataRow rowParEmail in TabellaParametriEmail.Rows)//  giro su edifici
                {
                    PInv.MAILFROM = rowParEmail["MAILFROM"].ToString();
                    PInv.SMTPSERVER = rowParEmail["SERVERSMTP"].ToString();
                    PInv.SMTPSERVERPOSTA = int.Parse(rowParEmail["PORTASMTP"].ToString());
                    PInv.USERSMTP = rowParEmail["USERSSMTP"].ToString();
                    PInv.PWDSMTP = rowParEmail["PWSSMTP"].ToString();
                    PInv.MAILTOCC = rowParEmail["MAILTOCC"].ToString();
                    PInv.MAILTOAdmin = rowParEmail["MAILTOADMIN"].ToString();
                    //PInv.PATHFILEPMP = rowParEmail["PATHFILEPMP"].ToString();
                    //PInv.PATHFILESTORE = rowParEmail["PATHFILESTORE"].ToString();
                    //PInv.USERNAME = rowParEmail["USERNAME"].ToString();
                }
                //   SmtpClient ClientPosta = new SmtpClient(SmtpServer, SmtpSrvPorta) { Credentials = new System.Net.NetworkCredential(UserSmtp, pwdsmtp) };
                SmtpClient ClientPosta = new SmtpClient(PInv.SMTPSERVER, PInv.SMTPSERVERPOSTA) { Credentials = new System.Net.NetworkCredential(PInv.USERSMTP, PInv.PWDSMTP) };

                return ClientPosta;
            }
            else
            {
                SmtpClient ClientPosta = new SmtpClient();
                return ClientPosta;
            }
        }
        #endregion
        #region trasmetti mail da rdl - COPIA DI CONTROLLI NORMATIVI DA VERIFICARE??????????????????????????????? @@@@@@@@@@@@@@@@@@@@@@@@@

        class MyObject
        {
            public string O_TIPOAZIONEMAIL { get; set; }
            public string O_INDIRIZZO_MAIL { get; set; }
            public string O_NOME_COGNOME { get; set; }
            public string O_SUBJECT { get; set; }
            public string O_BODY { get; set; }
            public string O_INDIRIZZO_SMS { get; set; }
            public int O_AZIONESPEDIZIONE { get; set; }

        }

        public void InviaMessaggiRdLCittadino(string Connessione, string UserNameCorrente, int OidRegRdL, ref string Messaggio)
        {
            ///// Carico Tabella Destinatari  
            DataTable TDestinatariMail = new DataTable();
            DataView DvDestinatari = new DataView();
            //   connessione che vale per tutto
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);

            BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail = new BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            objDCDatiSMSMail.Clear();

            if (OidRegRdL != 0)
            {
                Classi.SetVarSessione.OracleConnString = Connessione;
                objDCDatiSMSMail = gdb.GetDestinatariSMSMail_byTIPO(Connessione, UserNameCorrente, OidRegRdL, "Cittadino");
            }

            var DestMail = objDCDatiSMSMail.Where(w => new[] { 0, 2 }.Contains(w.AzioneSpedizione) && (w.IndirizzoMail != null || w.IndirizzoMail != ""))
           .Select(s => new
           {
               s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
               s.IndirizzoMail, //    O_INDIRIZZO_MAIL,
               s.NomeCognome,//   O_NOME_COGNOME,
               s.Subject,  //O_SUBJECT,
               s.Body    //O_BODY
           }).Distinct();

            var DestSMS = objDCDatiSMSMail.Where(w => new[] { 1, 2 }.Contains(w.AzioneSpedizione) && (w.IndirizzoSMS != null || w.IndirizzoSMS != ""))
          .Select(s => new
          {
              s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
              s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
              s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
              s.Body
          }).Distinct();

            /////////////////////
            if (objDCDatiSMSMail.Count > 0)  // Se non ci sono destinatari e informazioni mail esco
            {
                //qui invio le mail
                string returnMessaggioMail = string.Empty;
                string returnMessaggioSmS = string.Empty;
                int ContaMail = DestMail.Count();
                if (ContaMail > 0)
                {
                    returnMessaggioMail = InviaMessaggiRdLMail(Connessione, UserNameCorrente, OidRegRdL, objDCDatiSMSMail);
                }
                //  qui invio gli SMS
                int ContaSMS = DestSMS.Count();
                if (ContaSMS > 0)
                {
                    returnMessaggioSmS = InviaMessaggiRdLSMS(Connessione, UserNameCorrente, OidRegRdL, objDCDatiSMSMail); //, TDestinatariMail);
                }

                string TemgMail = string.Empty;
                string TemgSMS = string.Empty;

                if (returnMessaggioMail.ToUpper().Contains("Nessun Destinatario") && returnMessaggioSmS.ToUpper().Contains("Nessun Destinatario"))
                    Messaggio = string.Empty;
                else
                {  ///  ha spedito qualcosa
                    if (returnMessaggioMail.ToUpper().Contains("Nessun Destinatario"))
                        TemgMail = string.Empty;
                    else if (returnMessaggioMail.ToUpper().Contains("ERRORE"))
                        TemgMail = "Trasmissione Mail con Errori";
                    else
                        TemgMail = returnMessaggioMail;

                    if (returnMessaggioSmS.ToUpper().Contains("Nessun Destinatario"))
                        TemgSMS = string.Empty;
                    else if (returnMessaggioSmS.ToUpper().Contains("ERRORE"))
                        TemgSMS = "Trasmissione SMS con Errori"; //Trasmissione Mail 
                    else
                        TemgSMS = returnMessaggioSmS;

                    Messaggio = string.Format("{0}   {1}", TemgSMS, TemgMail);
                }




            }
            else
            {
                Messaggio = string.Empty;
                const string TxtLog = "Destinatari Assenti";
                setLogSpedizioni();
                CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            gdb.Dispose();
        }


        public void InviaMessaggiRdL(string Connessione, string UserNameCorrente, int OidRegRdL, ref string Messaggio)
        {
            ///// Carico Tabella Destinatari  
            DataTable TDestinatariMail = new DataTable();
            DataView DvDestinatari = new DataView();
            //   connessione che vale per tutto
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);

            BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail = new BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            objDCDatiSMSMail.Clear();

            if (OidRegRdL != 0)
            {
                Classi.SetVarSessione.OracleConnString = Connessione;
                objDCDatiSMSMail = gdb.GetDestinatariSMSMail_byRdL(Connessione, UserNameCorrente, OidRegRdL);
            }

            //[XafDisplayName("Mail")] 0
            //MAIL,
            //[XafDisplayName("SMS")] 1
            //SMS,
            //[XafDisplayName("Mail + SMS")] 2
            //MAILSMS,
            //[XafDisplayName("SMS Breve")] 3
            //SMSBREVE,
            //[XafDisplayName("Mail + SMS Breve")] 4
            //MAILSMSBREVE

            var DestMail = objDCDatiSMSMail.Where(w => new[] { 0, 2, 4 }.Contains(w.AzioneSpedizione) && w.IndirizzoMail != null && w.IndirizzoMail != "" && w.IndirizzoMail != "ND" && !string.IsNullOrEmpty(w.IndirizzoMail))
           .Select(s => new
           {
               s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
               s.IndirizzoMail, //    O_INDIRIZZO_MAIL,
               s.NomeCognome,//   O_NOME_COGNOME,
               s.Subject,  //O_SUBJECT,
               s.Body    //O_BODY
           }).Distinct();

            var DestSMS = objDCDatiSMSMail.Where(w => new[] { 1, 2, 3, 4 }.Contains(w.AzioneSpedizione) && w.IndirizzoSMS != null && w.IndirizzoSMS != "" && w.IndirizzoSMS != "ND")
          .Select(s => new
          {
              s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
              s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
              s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
              s.Body
          }).Distinct();

            /////////////////////
            if (objDCDatiSMSMail.Count > 0)  // Se non ci sono destinatari e informazioni mail esco
            {
                //qui invio le mail
                string returnMessaggioMail = string.Empty;
                string returnMessaggioSmS = string.Empty;
                int ContaMail = DestMail.Count();
                if (ContaMail > 0)
                {
                    returnMessaggioMail = InviaMessaggiRdLMail(Connessione, UserNameCorrente, OidRegRdL, objDCDatiSMSMail);
                }
                //  qui invio gli SMS
                int ContaSMS = DestSMS.Count();
                if (ContaSMS > 0)
                {
                    returnMessaggioSmS = InviaMessaggiRdLSMS(Connessione, UserNameCorrente, OidRegRdL, objDCDatiSMSMail); //, TDestinatariMail);
                }

                string TemgMail = string.Empty;
                string TemgSMS = string.Empty;

                if (returnMessaggioMail.ToUpper().Contains("Nessun Destinatario") && returnMessaggioSmS.ToUpper().Contains("Nessun Destinatario"))
                    Messaggio = string.Empty;
                else
                {  ///  ha spedito qualcosa
                    if (returnMessaggioMail.ToUpper().Contains("Nessun Destinatario"))
                        TemgMail = string.Empty;
                    else if (returnMessaggioMail.ToUpper().Contains("ERRORE"))
                        TemgMail = "Trasmissione Mail con Errori";
                    else
                        TemgMail = returnMessaggioMail;

                    if (returnMessaggioSmS.ToUpper().Contains("Nessun Destinatario"))
                        TemgSMS = string.Empty;
                    else if (returnMessaggioSmS.ToUpper().Contains("ERRORE"))
                        TemgSMS = "Trasmissione SMS con Errori"; //Trasmissione Mail 
                    else
                        TemgSMS = returnMessaggioSmS;

                    Messaggio = string.Format("{0}   {1}", TemgSMS, TemgMail);
                }

            }
            else
            {
                Messaggio = string.Empty;
                const string TxtLog = "Destinatari Assenti";
                setLogSpedizioni();
                CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            gdb.Dispose();
        }

        public void InviaMessaggiRdLSolleciti(string Connessione, string UserNameCorrente, int OidRegRdL, ref string Messaggio)
        {
            ///// Carico Tabella Destinatari  
            var iConnesssione = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            DataTable TDestinatariMail = new DataTable();
            DataView DvDestinatari = new DataView();
            //   connessione che vale per tutto
            //CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(iConnesssione);

            BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail = new BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            objDCDatiSMSMail.Clear();

            if (OidRegRdL != 0)
            {
                Classi.SetVarSessione.OracleConnString = Connessione;
                //objDCDatiSMSMail = gdb.GetDestinatariSMSMail_byRdLSoll(Connessione, UserNameCorrente, OidRegRdL);
                objDCDatiSMSMail = gdb.GetDestinatariSMSMail_byRdLSoll(iConnesssione, UserNameCorrente, OidRegRdL);
            }

            var DestMail = objDCDatiSMSMail.Where(w => new[] { 0, 2, 4 }.Contains(w.AzioneSpedizione) && (w.IndirizzoMail != null || w.IndirizzoMail != ""))
           .Select(s => new
           {
               s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
               s.IndirizzoMail, //    O_INDIRIZZO_MAIL,
               s.NomeCognome,//   O_NOME_COGNOME,
               s.Subject,  //O_SUBJECT,
               s.Body    //O_BODY
           }).Distinct();

            var DestSMS = objDCDatiSMSMail.Where(w => new[] { 1, 2, 3, 4 }.Contains(w.AzioneSpedizione) && (w.IndirizzoSMS != null || w.IndirizzoSMS != ""))
          .Select(s => new
          {
              s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
              s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
              s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
              s.Body
          }).Distinct();

            /////////////////////
            if (objDCDatiSMSMail.Count > 0)  // Se non ci sono destinatari e informazioni mail esco
            {
                //qui invio le mail
                string returnMessaggioMail = string.Empty;
                string returnMessaggioSmS = string.Empty;
                int ContaMail = DestMail.Count();
                if (ContaMail > 0)
                {
                    returnMessaggioMail = InviaMessaggiRdLMail(Connessione, UserNameCorrente, OidRegRdL, objDCDatiSMSMail);
                }
                //  qui invio gli SMS
                int ContaSMS = DestSMS.Count();
                if (ContaSMS > 0)
                {
                    returnMessaggioSmS = InviaMessaggiRdLSMS(Connessione, UserNameCorrente, OidRegRdL, objDCDatiSMSMail); //, TDestinatariMail);
                }

                string TemgMail = string.Empty;
                string TemgSMS = string.Empty;

                if (returnMessaggioMail.ToUpper().Contains("Nessun Destinatario") && returnMessaggioSmS.ToUpper().Contains("Nessun Destinatario"))
                    Messaggio = string.Empty;
                else
                {  ///  ha spedito qualcosa
                    if (returnMessaggioMail.ToUpper().Contains("Nessun Destinatario"))
                        TemgMail = string.Empty;
                    else if (returnMessaggioMail.ToUpper().Contains("ERRORE"))
                        TemgMail = "Trasmissione Mail con Errori";
                    else
                        TemgMail = returnMessaggioMail;

                    if (returnMessaggioSmS.ToUpper().Contains("Nessun Destinatario"))
                        TemgSMS = string.Empty;
                    else if (returnMessaggioSmS.ToUpper().Contains("ERRORE"))
                        TemgSMS = "Trasmissione SMS con Errori"; //Trasmissione Mail 
                    else
                        TemgSMS = returnMessaggioSmS;

                    Messaggio = string.Format("{0}   {1}", TemgSMS, TemgMail);
                }




            }
            else
            {
                Messaggio = string.Empty;
                const string TxtLog = "Destinatari Assenti";
                setLogSpedizioni();
                CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            gdb.Dispose();
        }



        //  InviaMessaggiRdLMail(string Connessione, string UserNameCorrente, int OidRegRdL,  DataTable TDestinatariMail)
        //  BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail

        //[XafDisplayName("Mail")]
        //MAIL,
        //[XafDisplayName("SMS")]
        //SMS,
        //[XafDisplayName("Mail + SMS")]
        //MAILSMS,
        //[XafDisplayName("SMS Breve")]
        //SMSBREVE,
        //[XafDisplayName("Mail + SMS Breve")]
        //MAILSMSBREVE

        public string InviaMessaggiRdLMail(string Connessione, string UserNameCorrente, int OidRegRdL,
            BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        {
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            var iConnesssione = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(iConnesssione);

            var objDCDatiMail = objDCDatiSMSMail.Where(w => new[] { 0, 2, 4 }.Contains(w.AzioneSpedizione) && (w.IndirizzoMail != null || w.IndirizzoMail != ""))
                                .Select(s => new
                                {
                                    s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                                    s.IndirizzoMail, //    O_INDIRIZZO_MAIL,
                                    s.NomeCognome,//   O_NOME_COGNOME,
                                    s.Subject,  //O_SUBJECT,
                                    s.Body    //O_BODY
                                }).Distinct();

            List<string> ListTipoAzioni = new List<string>();
            //int ContaMail = DestinatariMail.Count();
            int ContaMail = objDCDatiMail.Count();
            if (ContaMail > 0)
            {
                ListTipoAzioni = objDCDatiMail.Select(S => S.TipoAzioneMail).Distinct().ToList(); //.Select(S => S.O_TIPOAZIONEMAIL).Distinct().ToList();
                foreach (var item in ListTipoAzioni)
                {
                    string TxtLog = "";
                    int Anno = DateTime.Now.Year;
                    setLogSpedizioni();
                    Class1 cl = new CAMSInvioMailCN.Class1();
                    SmtpClient ClientPosta = GetParametriMailATM(gdb);// parametri di spedizione ATM
                    if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
                    {
                        TxtLog = "Parametri Assenti";
                        cl.TxtLogSpedizioni(TxtLog, true);
                        ReturnMessaggio.Append("Errore: Parametri Assenti");  // return "Errore: Parametri Assenti";
                    }
                    else
                    {
                        TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                        cl.TxtLogSpedizioni(TxtLog, true);
                    }
                    ///  istanza del messaggio  imposto i parametrifissi          
                    MailMessage MailMessaggio = new MailMessage();

                    MailAddress FROM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Italia Spa", System.Text.Encoding.UTF8);
                    MailMessaggio.From = FROM;

                    MailMessaggio.CC.Clear();
                    if (PInv.MAILTOCC.Length != 0)
                    {
                        string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                        foreach (string Codice in splitToCC)
                        {
                            MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                            MailMessaggio.CC.Add(CC);
                        }
                    }
                    cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);

                    ///  recupero rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!  
                    PInv.FullPathFileSaveMail = "";
                    StringBuilder corpo = new StringBuilder(" ", 320000);
                    PInv.TotEdifici += 1;// totale edifici processati
                    string DestinatariUniti = "";
                    MailMessaggio.To.Clear();
                    MailMessaggio.Subject = "";
                    MailMessaggio.Body = "";
                    int ContaGiri = 0;
                    ///---------------------------------------------
                    cl.TxtLogSpedizioni("Spedizione Avviso per Registro RdL: " + OidRegRdL, true);
                    //-------------------------------------------
                    var DestMAILxTipo = objDCDatiMail.Where(w => w.TipoAzioneMail == item && !w.IndirizzoMail.Equals("ND") && !string.IsNullOrEmpty(w.IndirizzoMail)); //  var DestMAILxTipo = DestinatariMail.Where(w => w.O_TIPOAZIONEMAIL == item);
                    int CountDestxTipo = DestMAILxTipo.Count();
                    if (CountDestxTipo > 0)
                    {
                        foreach (var RowDestinatariUniti in DestMAILxTipo) // row4 rowdestinatari   DestinatariMail
                        {
                            if (ContaGiri == 0)
                            {
                                MailMessaggio.Subject = RowDestinatariUniti.Subject.ToString();//    .O_SUBJECT.ToString();// 
                                corpo.Append(RowDestinatariUniti.Body.ToString() + "\n"); //         .O_BODY.ToString() + "\n"); //   
                                MailMessaggio.Body = corpo.ToString();
                            }
                            string NomeDestinatario = RowDestinatariUniti.NomeCognome.ToString();//  .O_NOME_COGNOME.ToString();// 
                            //System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti.IndirizzoMail.ToString().Trim(), NomeDestinatario); //  .O_INDIRIZZO_MAIL.ToString().Trim(), NomeDestinatario); // 
                            System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti.IndirizzoMail.ToString().Trim()); //  MODIFICA A.N. // 
                            MailMessaggio.To.Add(TO);
                            DestinatariUniti += RowDestinatariUniti.IndirizzoMail.ToString() + "; ";// .O_INDIRIZZO_MAIL.ToString() + "; ";// 
                            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_Mail.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));
                            ContaGiri = 1;
                        }
                        cl.TxtLogSpedizioni("Destinatari Mail: " + DestinatariUniti, true);
                        cl.TxtLogSpedizioni("Subject: " + MailMessaggio.Subject, true);
                        cl.TxtLogSpedizioni("Corpo Mail: " + corpo.ToString(), true);
                        cl.TxtLogSpedizioni("Fine MAIL -------------------------- ", true);
                        cl.TxtLogSpedizioni("  ", true);
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
                        using (var inv = new Invio())
                        {
                            string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                            EsitoInvioMailSMS Esito = EsitoInvioMailSMS.Inviata;
                            if (Ritorno != "Invio Eseguito")
                            {
                                Esito = EsitoInvioMailSMS.ErrorediInvio;
                                ReturnMessaggio.Append("Errore Trasmissione");
                            }
                            else
                            {

                                ReturnMessaggio.Append("Destinatari Mail: " + DestinatariUniti);
                            }
                            var dv = gdb.InsertLog_DestinatariMail_byRdL(UserNameCorrente, OidRegRdL, MailMessaggio.From.ToString(), MailMessaggio.To.ToString(),
                                                                         DateTime.Now, MailMessaggio.Subject, MailMessaggio.Body, Ritorno, Esito, 0, "0", "");
                        }
                    }
                    else
                    {
                        TxtLog = "Destinatari Assenti";
                        setLogSpedizioni();       // CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                        cl.TxtLogSpedizioni(TxtLog, true);
                        ReturnMessaggio.Append("Destinatari Assenti");
                    }
                }
            }
            else
            {
                const string TxtLog = "Destinatari Assenti";
                setLogSpedizioni();
                CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                cl.TxtLogSpedizioni(TxtLog, true);
                ReturnMessaggio.Append("Destinatari Assenti");  //return "Destinatari Assenti";
            }

            gdb.Dispose();
            return ReturnMessaggio.ToString();
        }

        // , BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiMail)    --      public string InviaMessaggiRdLSMS(string Connessione, string UserNameCorrente, int OidRegRdL, DataTable TDestinatariMail)

        //[XafDisplayName("Mail")]
        //MAIL,
        //[XafDisplayName("SMS")]
        //SMS,
        //[XafDisplayName("Mail + SMS")]
        //MAILSMS,
        //[XafDisplayName("SMS Breve")]
        //SMSBREVE,
        //[XafDisplayName("Mail + SMS Breve")]
        //MAILSMSBREVE

        public string InviaMessaggiRdLSMS(string Connessione, string UserNameCorrente, int OidRegRdL,
            BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        {
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);

            // objDCDatiSMS
            var objDCDatiSMS = objDCDatiSMSMail.Where(w => new[] { 1, 2, 3, 4 }.Contains(w.AzioneSpedizione) && (w.IndirizzoSMS != null || w.IndirizzoSMS != ""))
            .Select(s => new
            {
                s.Subject,
                s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
                s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
                s.Body
            }).Distinct();

            string sms_SMSSender = "00393441741584";
            sms_SMSSender = "00393441741584";
            List<string> ListTipoAzioni = new List<string>();
            // int ContaSMS = DestinatariSMS.Count();

            ListTipoAzioni = objDCDatiSMS.Select(S => S.TipoAzioneMail).Distinct().ToList(); //.Select(S => S.O_TIPOAZIONEMAIL).Distinct().ToList();
            foreach (var item in ListTipoAzioni)
            {
                ///  recupero dati rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!                      
                string DestinatariUniti = "";
                int ContaGiri = 0;
                string MessaggioSMS = "";
                string SubObjectSMS = "";
                List<string> TelefonoSMS = new List<string>();

                TelefonoSMS.Clear();
                ///---------------------------------------------
                string TxtLog = "";
                int Anno = DateTime.Now.Year;
                setLogSpedizioni();
                Class1 cl = new CAMSInvioMailCN.Class1();
                PInv.FullPathFileSaveMail = "";
                StringBuilder corpo = new StringBuilder("", 32000); //    _______________
                PInv.TotEdifici += 1;// totale edifici processati     ------ || --  -  --  |-|     ------  
                cl.TxtLogSpedizioni("Spedizione Avviso per Registro RdL: " + OidRegRdL, true); //  -------->>>>>>  ------->>>>>>>
                //------------------------------------------- ||||||| ----------- ùùùùùù  **************  ######## \\\\\\  -->>>   hhh   <<< - - - >>>
                var DestSMSxTipo = objDCDatiSMS.Where(w => w.TipoAzioneMail == item); // var DestSMSxTipo = DestinatariSMS.Where(w => w.O_TIPOAZIONEMAIL == item);
                int CountDestxTipo = DestSMSxTipo.Count();
                //------------------------------------------- ||||||| -----------
                if (CountDestxTipo > 0)
                {
                    foreach (var RowDestinatariUniti in DestSMSxTipo) // row4 rowdestinatari   DestinatariMail
                    {
                        //if (ContaGiri == 0)
                        //{
                        //    MessaggioSMS = RowDestinatariUniti.Body.ToString(); // .O_BODY.ToString();
                        //    SubObjectSMS = RowDestinatariUniti.Subject.ToString();
                        //}
                        MessaggioSMS = RowDestinatariUniti.Body.ToString(); // .O_BODY.ToString();
                        SubObjectSMS = RowDestinatariUniti.Subject.ToString();

                        string telefono = string.Empty;
                        telefono = RowDestinatariUniti.IndirizzoSMS.ToString().Trim().Replace("(", ""); // i.O_INDIRIZZO_SMS.ToString().Trim().Replace("(", "");
                        telefono = telefono.Replace(")", "");
                        telefono = telefono.Replace("-", "");
                        if (!string.IsNullOrEmpty(telefono) && !telefono.Equals("ND", StringComparison.InvariantCultureIgnoreCase))
                        {
                            TelefonoSMS.Add(telefono);
                        }
                        DestinatariUniti = RowDestinatariUniti.IndirizzoSMS.ToString().Trim() + "; "; // RowDestinatariUniti.O_INDIRIZZO_SMS.ToString().Trim() + "; ";
                        PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_SMS.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));
                        ContaGiri = 1;

                        cl.TxtLogSpedizioni("Destinatari SMS: " + DestinatariUniti, true);
                        cl.TxtLogSpedizioni("Corpo SMS: " + corpo.ToString(), true);
                        cl.TxtLogSpedizioni("Fine SMS -------------------------- ", true);
                        cl.TxtLogSpedizioni("  ", true);
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email

                        SMSCConnection smsc_connection = null;
                        try
                        {
                            smsc_connection = new SMSCHTTPConnection("Sms52333", "Prt_040502");//  ("1913233@aruba.it", "1973ardea");
                        }
                        catch (SMSCException smsc_ex)
                        {
                            Debug.WriteLine("Errore in connessione a Aruba:" + smsc_ex.Message);
                            ReturnMessaggio.Append("Errore");
                        }
                        // operazioni sulla connessione
                        try
                        {
                            SMS sms = new SMS();
                            sms.TypeOfSMS = SMSType.ALTA;
                            //sms.addSMSRecipient("+393463228369");                   //sms.addSMSRecipient("+393499876543");
                            foreach (var tel in TelefonoSMS) // row4 rowdestinatari   DestinatariMail
                            {
                                sms.addSMSRecipient(tel);
                            }

                            sms.Message = MessaggioSMS;  // "hello world!";
                            sms.SMSSender = sms_SMSSender; // "00393463228369";
                            sms.setImmediate(); // oppure sms.setScheduled_delivery(java.util.Date)
                                                //   sms.OrderId = "12345";
                            SendResult result = smsc_connection.sendSMS(sms);
                            EsitoInvioMailSMS eisms = EsitoInvioMailSMS.CONSEGNATO_SMSGen;
                            string dv = "Nessuna mail inviata";
                            if (result != null)
                            {
                                dv = gdb.InsertLog_DestinatariMail_byRdL(UserNameCorrente, OidRegRdL, sms_SMSSender, "",
                                                               DateTime.Now, SubObjectSMS, MessaggioSMS, result.ToString(), eisms, 1, result.OrderId, DestinatariUniti);
                            }
                            else
                            {

                            }

                        }
                        catch (SMSCRemoteException smscre)
                        {
                            System.Console.WriteLine("Errore: from Remote Provider SMS: " + smscre.Message);
                            ReturnMessaggio.Append("Errore: from Remote Provider SMS: " + smscre.Message);
                        }
                        catch (SMSCException smsce)
                        {
                            System.Console.WriteLine("Errore: from Provider SMS: " + smsce.Message);
                            ReturnMessaggio.Append("Errore: from Provider SMS: " + smsce.Message);
                        }

                        smsc_connection.logout();  // nel caso di HTTP il metodo non lancia mai eccezioni
                        ReturnMessaggio.Append("Destinatari SMS: " + DestinatariUniti);
                        TelefonoSMS.Clear();
                    }
                }
                else
                {
                    TxtLog = "Destinatari SMS Assenti";
                    setLogSpedizioni();
                    cl.TxtLogSpedizioni(TxtLog, true);
                    ReturnMessaggio.Append("Destinatari Assenti");
                }
            }
            gdb.Dispose();
            return ReturnMessaggio.ToString();
        }

        //public string InviaMessaggiRdLSMSNew(string Connessione, string UserNameCorrente, int OidRegRdL,
        //   BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        //{
        //    StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
        //    CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);

        //    // objDCDatiSMS
        //    var objDCDatiSMS = objDCDatiSMSMail.Where(w => new[] { 1, 2, 3, 4 }.Contains(w.AzioneSpedizione) && (w.IndirizzoSMS != null || w.IndirizzoSMS != ""))
        //    .Select(s => new
        //    {
        //      s.Subject,
        //      s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
        //      s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
        //      s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
        //      s.Body
        //    }).Distinct();

        //    string sms_SMSSender = "00393441741584";
        //    sms_SMSSender = "00393441741584";
        //    List<string> ListTipoAzioni = new List<string>();
        //    // int ContaSMS = DestinatariSMS.Count();

        //    ListTipoAzioni = objDCDatiSMS.Select(S => S.TipoAzioneMail).Distinct().ToList(); //.Select(S => S.O_TIPOAZIONEMAIL).Distinct().ToList();
        //    foreach (var item in ListTipoAzioni)
        //    {
        //        ///  recupero dati rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!                      
        //        string DestinatariUniti = "";
        //        int ContaGiri = 0;
        //        string MessaggioSMS = "";
        //        string SubObjectSMS = "";
        //        List<string> TelefonoSMS = new List<string>();

        //        TelefonoSMS.Clear();
        //        ///---------------------------------------------
        //        string TxtLog = "";
        //        int Anno = DateTime.Now.Year;
        //        setLogSpedizioni();
        //        Class1 cl = new CAMSInvioMailCN.Class1();
        //        PInv.FullPathFileSaveMail = "";
        //        StringBuilder corpo = new StringBuilder("", 32000); //    _______________
        //        PInv.TotEdifici += 1;// totale edifici processati     ------ || --  -  --  |-|     ------  
        //        cl.TxtLogSpedizioni("Spedizione Avviso per Registro RdL: " + OidRegRdL, true); //  -------->>>>>>  ------->>>>>>>
        //        //------------------------------------------- ||||||| ----------- ùùùùùù  **************  ######## \\\\\\  -->>>   hhh   <<< - - - >>>
        //        var DestSMSxTipo = objDCDatiSMS.Where(w => w.TipoAzioneMail == item); // var DestSMSxTipo = DestinatariSMS.Where(w => w.O_TIPOAZIONEMAIL == item);
        //        int CountDestxTipo = DestSMSxTipo.Count();
        //        //------------------------------------------- ||||||| -----------
        //        if (CountDestxTipo > 0)
        //        {
        //            foreach (var RowDestinatariUniti in DestSMSxTipo) // row4 rowdestinatari   DestinatariMail
        //            {
        //                //if (ContaGiri == 0)
        //                //{
        //                //    MessaggioSMS = RowDestinatariUniti.Body.ToString(); // .O_BODY.ToString();
        //                //    SubObjectSMS = RowDestinatariUniti.Subject.ToString();
        //                //}
        //                MessaggioSMS = RowDestinatariUniti.Body.ToString(); // .O_BODY.ToString();
        //                SubObjectSMS = RowDestinatariUniti.Subject.ToString();

        //                string telefono = string.Empty;
        //                telefono = RowDestinatariUniti.IndirizzoSMS.ToString().Trim().Replace("(", ""); // i.O_INDIRIZZO_SMS.ToString().Trim().Replace("(", "");
        //                telefono = telefono.Replace(")", "");
        //                telefono = telefono.Replace("-", "");
        //                TelefonoSMS.Add(telefono);
        //                DestinatariUniti += RowDestinatariUniti.IndirizzoSMS.ToString().Trim() + "; "; // RowDestinatariUniti.O_INDIRIZZO_SMS.ToString().Trim() + "; ";
        //                PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_SMS.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));
        //                ContaGiri = 1;
        //            }
        //            cl.TxtLogSpedizioni("Destinatari SMS: " + DestinatariUniti, true);
        //            cl.TxtLogSpedizioni("Corpo SMS: " + corpo.ToString(), true);
        //            cl.TxtLogSpedizioni("Fine SMS -------------------------- ", true);
        //            cl.TxtLogSpedizioni("  ", true);
        //            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email

        //            SMSCConnection smsc_connection = null;
        //            try
        //            {
        //                smsc_connection = new SMSCHTTPConnection("Sms52333", "Prt_040502");//  ("1913233@aruba.it", "1973ardea");
        //            }
        //            catch (SMSCException smsc_ex)
        //            {
        //                Debug.WriteLine("Errore in connessione a Aruba:" + smsc_ex.Message);
        //                ReturnMessaggio.Append("Errore");
        //            }
        //            // operazioni sulla connessione
        //            try
        //            {
        //                SMS sms = new SMS();
        //                sms.TypeOfSMS = SMSType.ALTA;
        //                //sms.addSMSRecipient("+393463228369");                   //sms.addSMSRecipient("+393499876543");
        //                foreach (var tel in TelefonoSMS) // row4 rowdestinatari   DestinatariMail
        //                {
        //                    sms.addSMSRecipient(tel);
        //                }

        //                sms.Message = MessaggioSMS;  // "hello world!";
        //                sms.SMSSender = sms_SMSSender; // "00393463228369";
        //                sms.setImmediate(); // oppure sms.setScheduled_delivery(java.util.Date)
        //                //   sms.OrderId = "12345";
        //                SendResult result = smsc_connection.sendSMS(sms);
        //                EsitoInvioMailSMS eisms = EsitoInvioMailSMS.CONSEGNATO_smsGen;

        //                var dv = gdb.InsertLog_DestinatariMail_byRdL(UserNameCorrente, OidRegRdL, sms_SMSSender, "",
        //                                                   DateTime.Now, SubObjectSMS, MessaggioSMS, result.ToString(), eisms, 1, result.OrderId, DestinatariUniti);

        //            }
        //            catch (SMSCRemoteException smscre)
        //            {
        //                System.Console.WriteLine("Errore: from Remote Provider SMS: " + smscre.Message);
        //                ReturnMessaggio.Append("Errore: from Remote Provider SMS: " + smscre.Message);
        //            }
        //            catch (SMSCException smsce)
        //            {
        //                System.Console.WriteLine("Errore: from Provider SMS: " + smsce.Message);
        //                ReturnMessaggio.Append("Errore: from Provider SMS: " + smsce.Message);
        //            }

        //            smsc_connection.logout();  // nel caso di HTTP il metodo non lancia mai eccezioni
        //            ReturnMessaggio.Append("Destinatari SMS: " + DestinatariUniti);

        //        }
        //        else
        //        {
        //            TxtLog = "Destinatari SMS Assenti";
        //            setLogSpedizioni();
        //            cl.TxtLogSpedizioni(TxtLog, true);
        //            ReturnMessaggio.Append("Destinatari Assenti");
        //        }
        //    }
        //    gdb.Dispose();
        //    return ReturnMessaggio.ToString();
        //}


        public void mio(IEnumerable pippo)
        {
            int ContaGiri = 0;
            foreach (var RowDestinatariUniti in pippo) // row4 rowdestinatari   DestinatariMail
            {

                ContaGiri = 1;
            }

        }



        void setLogSpedizioni()
        {
            string TxtLog = "";
            int Anno = DateTime.Now.Year;
            PInv.InvAnno = 0;
            PInv.InvMese = 0;
            PInv.IDSessione = Convert.ToString(DateTime.Now) + Guid.NewGuid().ToString(); ////PInv.IDSessione =   "05/04/2010 0.19.46";  @@@@@@  non andava

            //string TemPath = Path.Combine(Environment.CurrentDirectory, "SegnalazioneFileLog");
            string TemPath = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "SegnalazioneFileLog");

            if (!Directory.Exists(TemPath))
                Directory.CreateDirectory(TemPath);
            ////  creao path per email
            //string TemPathMail = Path.Combine(Environment.CurrentDirectory, "SegMail");
            string TemPathMail = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "SegMail");
            if (!Directory.Exists(TemPathMail))
                Directory.CreateDirectory(TemPathMail);
            PInv.FullPathMail = TemPathMail;

            string NomeFIleSessione = PInv.IDSessione.Replace("/", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(".", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(" ", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(":", "-");
            PInv.NomeFileSessione = NomeFIleSessione;
            PInv.FileLogText = Path.Combine(TemPath, Path.GetFileName(NomeFIleSessione + ".txt"));
            ///////////èè/////////////////////////////////////////////
        }
        #endregion


        #region invio mail atm - SIA SEGNALAZIONI DOPO LA LETTURA DELLE MAIL DA REGISTRARE CHE DOPO PER AVVESARE CHE è STATA CREATA LA RDL

        public void Invia_Segnalazione_RdL(//DataTable TRdLSeg_Edifici,
              string IMMOBILE, string STREDIFICIO, string STRAVVISO, StringBuilder corpo, string OID, string RDL,
            string Connessione, string UserNameCorrente, int OidEdificio, ref string Messaggio)
        {// invia i report per ogni immobile
            string TxtLog = "";
            int Anno = DateTime.Now.Year;
            PInv.InvAnno = 0;
            PInv.InvMese = 0;
            PInv.IDSessione = Convert.ToString(DateTime.Now); ////PInv.IDSessione =   "05/04/2010 0.19.46";
            //string TemPath = Path.Combine(Environment.CurrentDirectory, "SegnalazioneFileLog");
            string TemPath = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "SegnalazioneFileLog");
            if (!Directory.Exists(TemPath))
                Directory.CreateDirectory(TemPath);
            ////  creao path per email
            //string TemPathMail = Path.Combine(Environment.CurrentDirectory, "SegMail");
            string TemPathMail = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "SegMail");
            if (!Directory.Exists(TemPathMail))
                Directory.CreateDirectory(TemPathMail);
            PInv.FullPathMail = TemPathMail;

            string NomeFIleSessione = PInv.IDSessione.Replace("/", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(".", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(" ", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(":", "-");
            PInv.NomeFileSessione = NomeFIleSessione;
            PInv.FileLogText = Path.Combine(TemPath, Path.GetFileName(NomeFIleSessione + ".txt"));
            ///////////èè/////////////////////////////////////////////

            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
            ////////////
            SmtpClient ClientPosta = GetParametriMailATM(gdb);// parametri di spedizione ATM
            if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
            {
                TxtLog = "Parametri Assenti";
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            else
            {
                TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                cl.TxtLogSpedizioni(TxtLog, true);
            }
            ///  istanza del messaggio  imposto i parametrifissi          
            MailMessage MailMessaggio = new MailMessage();
            MailAddress FORM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailMessaggio.From = FORM;

            if (PInv.MAILTOCC.Length != 0)
            {
                string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                foreach (string Codice in splitToCC)
                {
                    MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                    MailMessaggio.CC.Add(CC);
                }
            }
            cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);

            ///  recupero rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!

            for (int i = 0; i < 1; i++)
            {
                PInv.FullPathFileSaveMail = "";
                PInv.ErroriFileAllegatoNoTrovati = "";
                //StringBuilder corpo = new StringBuilder("", 32000000);
                //----------------------------------------------
                //string IMMOBILE = rowBL["IMMOBILE"].ToString(); // oid immobile
                //string STREDIFICIO = rowBL["STREDIFICIO"].ToString();  //  string CORPO = rowBL["CORPO"].ToString();  // bl_id
                //string STRAVVISO = rowBL["STRAVVISO"].ToString();
                //corpo.Append(rowBL["CORPO"].ToString());
                //string OID = rowBL["OID"].ToString();
                //string RDL = rowBL["RDL"].ToString();

                PInv.TotEdifici += 1;// totale edifici processati
                ///---------------------------------------------
                cl.TxtLogSpedizioni("Spedizione Segnalazione per immobile: " + STREDIFICIO, true);
                ///// Carico Tabella Destinatari           GetDestinatariMail_RdLSegnalazione
                DataTable TDestinatari = new DataTable();
                using (var db = new DB())
                {
                    if (IMMOBILE != null)
                    {
                        Classi.SetVarSessione.OracleConnString = Connessione;
                        TDestinatari = db.GetDestinatariMail_RdLSegnalazione("Admin", int.Parse(IMMOBILE));
                    }
                }

                if (TDestinatari.Rows.Count == 0)  // Se non ci sono destinatari conto gli edifici senza
                {
                    TxtLog = "Non ci sono destinatari per questo immobile: " + STREDIFICIO;
                    cl.TxtLogSpedizioni(TxtLog, true);

                    if (PInv.ErroriEdificiNoInvio.Length == 0)
                    {
                        PInv.ErroriEdificiNoInvio = STREDIFICIO;
                    }
                    else
                    {
                        PInv.ErroriEdificiNoInvio += ", " + STREDIFICIO;
                    }
                }
                else           ///////////  se i destinatari sono maggiori di zero ##############################################
                {
                    string DestinatariUniti = "";
                    MailMessaggio.To.Clear();
                    foreach (DataRow RowDestinatariUniti in TDestinatari.Rows) // row4 rowdestinatari
                    {
                        string NomeDestinatario = RowDestinatariUniti["NOME"].ToString();
                        System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti["DESEMAIL"].ToString().Trim(), NomeDestinatario);
                        MailMessaggio.To.Add(TO);
                        DestinatariUniti += RowDestinatariUniti["DESEMAIL"].ToString() + "; ";
                        PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("CP_{0}_{1}_{2}_DestUni.txt", PInv.NomeFileSessione, STREDIFICIO, (Anno - 2000)));

                    }
                    cl.TxtLogSpedizioni("Destinatari Mail: " + DestinatariUniti, true);
                    // imposta   OGGETTO + CORPO @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@      
                    MailMessaggio.Subject = string.Format("RdL/Avviso :{0} / {1}; Immobile {2}", RDL, STRAVVISO, STREDIFICIO);
                    MailMessaggio.Body = corpo.ToString();

                    cl.TxtLogSpedizioni("Corpo Mail: " + corpo.ToString(), true);
                    cl.TxtLogSpedizioni("Fine Immobile -------------------------- ", true);
                    cl.TxtLogSpedizioni("  ", true);
                    //  MailMessaggio.Attachments.Clear();  //ANNULLO GLI ALLEGATI DA ELIMINARE  
                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
                    using (var inv = new Invio())
                    {
                        int Esito = 0;
                        Messaggio = "Email di Avviso Inviata!";
                        string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                        if (!string.IsNullOrWhiteSpace(Ritorno))
                        {
                            Messaggio = "Email di Avviso Inviata, con Avvertimenti!";
                            Esito = 1;
                        }
                        string strMessaggio = string.Empty;
                        string Spedita = string.Empty;
                        if (Esito == 0)
                        {
                            //  chiama procedura di cambio stato in SPEDITO                          
                            using (var db = new DB())
                            {
                                if (RDL != null)
                                {
                                    int rdl = 0;
                                    int.TryParse(RDL, out rdl);
                                    if (rdl > 0)
                                    {
                                        Classi.SetVarSessione.OracleConnString = Connessione;
                                        strMessaggio = db.SetStatoTrasmesso("Admin", rdl, ref Spedita);
                                    }
                                }
                            }
                        }
                        gdb.LogMessaggioEMailCN(UserNameCorrente, OidEdificio, MailMessaggio, DateTime.Now, Esito, DateTime.Now, DateTime.Now);
                    }
                    ///********************************************************************************************************
                } ////  chide if destinatari nulli su else
            }  // chiude ciclo for per edifici
            //#######################################################################
            //######################################################################
            /// invio log invio amministratore di sistema 
            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("CP_{0}_{1}_{2}_DestUni.txt", PInv.NomeFileSessione, "Admin", (Anno - 2000)));
            //Invio Log Spedizione - Area di Polo Energia Centrale: Centrale di COLLIO, Avviso Scadenze Controlli Periodici Normativi Data invio: 20/08/2015
            //string oggetto = MailMessaggio.Subject.ToString();
            string oggetto = string.Format("Invio Log Spedizione -  Avviso Creazione RdL da Segnalazione Mail - Data invio: ", DateTime.Now.ToShortDateString());
            SmtpClient ClientPostaAdmin = new SmtpClient(PInv.SMTPSERVER, PInv.SMTPSERVERPOSTA) { Credentials = new System.Net.NetworkCredential(PInv.USERSMTP, PInv.PWDSMTP) };

            MailMessage MailM = new MailMessage();
            FORM = new MailAddress(PInv.MAILFROM, "EAMS ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailM.From = FORM;
            MailM.To.Clear();
            MailM.Attachments.Clear();  //ANNULLO GLI ALLEGATI DA ELIMINARE  
            if (PInv.MAILTOAdmin.Length != 0)
            {
                string[] splitTo = PInv.MAILTOAdmin.Split(new Char[] { ';' });
                foreach (string Codice in splitTo)
                {
                    MailAddress to = new MailAddress(Codice.Trim(), "Amministratore EAMS");  //Dest
                    MailM.To.Add(to);
                }
            }

            MailM.Body = cl.LeggiTxtLogSpedizioni();
            MailM.CC.Clear();
            MailM.Subject = oggetto;
            using (var inv = new Invio())
            {
                inv.SendMailNew(ClientPostaAdmin, MailM, PInv.FullPathFileSaveMail);
            }


        }

        private SmtpClient GetParametriMailATM(CAMSInvioMailCN.GetDataDB gdb)
        {

            DataTable TabellaParametriEmail = gdb.GetParametriMail();
            //GetParametriEmail(PInv.USERNAME, "CP");
            if (TabellaParametriEmail.Rows.Count > 0)
            {
                foreach (DataRow rowParEmail in TabellaParametriEmail.Rows)//  giro su edifici
                {
                    PInv.MAILFROM = rowParEmail["MAILFROM"].ToString();
                    PInv.SMTPSERVER = rowParEmail["SERVERSMTP"].ToString();
                    PInv.SMTPSERVERPOSTA = int.Parse(rowParEmail["PORTASMTP"].ToString());
                    PInv.USERSMTP = rowParEmail["USERSSMTP"].ToString();
                    PInv.PWDSMTP = rowParEmail["PWSSMTP"].ToString();
                    PInv.MAILTOCC = rowParEmail["MAILTOCC"].ToString();
                    PInv.MAILTOAdmin = rowParEmail["MAILTOADMIN"].ToString();
                    //PInv.PATHFILEPMP = rowParEmail["PATHFILEPMP"].ToString();
                    //PInv.PATHFILESTORE = rowParEmail["PATHFILESTORE"].ToString();
                    //PInv.USERNAME = rowParEmail["USERNAME"].ToString();
                }
                //   SmtpClient ClientPosta = new SmtpClient(SmtpServer, SmtpSrvPorta) { Credentials = new System.Net.NetworkCredential(UserSmtp, pwdsmtp) };
                SmtpClient ClientPosta = new SmtpClient(PInv.SMTPSERVER, PInv.SMTPSERVERPOSTA) { Credentials = new System.Net.NetworkCredential(PInv.USERSMTP, PInv.PWDSMTP) };

                return ClientPosta;
            }
            else
            {
                SmtpClient ClientPosta = new SmtpClient();
                return ClientPosta;
            }
        }
        #endregion

        #region invio mail TICHET ASSISTENZA
        public void Invia_TicketAssistenza(int OidTicketAssistenza, string RuoloCorrente,
            //string IMMOBILE, string STREDIFICIO, string STRAVVISO, StringBuilder corpo, string OID, string RDL,
            string Connessione, string UserNameCorrente, ref string Messaggio)
        {// invia i report per ogni immobile
            string TxtLog = "";
            int Anno = DateTime.Now.Year;
            PInv.InvAnno = 0;
            PInv.InvMese = 0;
            PInv.IDSessione = Convert.ToString(DateTime.Now); ////PInv.IDSessione =   "05/04/2010 0.19.46";
            //string TemPath = Path.Combine(Environment.CurrentDirectory, "TicketFileLog");
            string TemPath = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "TicketFileLog");
            if (!Directory.Exists(TemPath))
                Directory.CreateDirectory(TemPath);
            ////  creao path per email
            ////string TemPathMail = Path.Combine(Environment.CurrentDirectory, "TicketMail");
            string TemPathMail = Path.Combine(CAMS.Module.Classi.SetVarSessione.PhysicalPathSito, "TicketMail");
            if (!Directory.Exists(TemPathMail))
                Directory.CreateDirectory(TemPathMail);
            PInv.FullPathMail = TemPathMail;

            string NomeFIleSessione = PInv.IDSessione.Replace("/", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(".", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(" ", "-");
            NomeFIleSessione = NomeFIleSessione.Replace(":", "-");
            PInv.NomeFileSessione = NomeFIleSessione;
            PInv.FileLogText = Path.Combine(TemPath, Path.GetFileName(NomeFIleSessione + ".txt"));
            ///////////èè/////////////////////////////////////////////

            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
            ////////////
            StringBuilder corpo = new StringBuilder("", 32000);
            //////////////
            SmtpClient ClientPosta = GetParametriMailTicketAssistenza(gdb);// parametri di spedizione ATM
            if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
            {
                TxtLog = "Parametri Assenti";
                cl.TxtLogSpedizioni(TxtLog, true);
                return;
            }
            else
            {
                TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                cl.TxtLogSpedizioni(TxtLog, true);
            }
            ///  istanza del messaggio  imposto i parametrifissi          
            MailMessage MailMessaggio = new MailMessage();
            MailAddress FORM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailMessaggio.From = FORM;

            if (PInv.MAILTOCC.Length != 0)
            {
                string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                foreach (string Codice in splitToCC)
                {
                    MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                    MailMessaggio.CC.Add(CC);
                }
            }
            cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);
            // foreach (DataRow rowBL in TRdLSeg_Edifici.Rows)//        @@@@@@@@   @@@@@@@@   giro edifici
            for (int i = 0; i < 1; i++)
            {
                PInv.FullPathFileSaveMail = "";
                PInv.ErroriFileAllegatoNoTrovati = "";

                PInv.TotEdifici += 1;// totale edifici processati
                ///---------------------------------------------
                cl.TxtLogSpedizioni("Spedizione Ticket Assistenza EAMS: " + OidTicketAssistenza.ToString(), true);
                ///// Carico Tabella Destinatari           GetDestinatariMail_RdLSegnalazione
                DataTable TDestinatari = new DataTable();
                using (var db = new DB())
                {
                    if (UserNameCorrente != null)
                    {
                        Classi.SetVarSessione.OracleConnString = Connessione;
                        TDestinatari = db.GetDestinatariMail_TicketAssistenza(UserNameCorrente, OidTicketAssistenza);
                    }
                }

                if (TDestinatari.Rows.Count == 0)  // Se non ci sono destinatari conto gli edifici senza
                {
                    TxtLog = "Non ci sono destinatari per questo Ticket Assistenza: " + OidTicketAssistenza.ToString();
                    cl.TxtLogSpedizioni(TxtLog, true);

                    if (PInv.ErroriEdificiNoInvio.Length == 0)
                    {
                        PInv.ErroriEdificiNoInvio = OidTicketAssistenza.ToString();
                    }
                    else
                    {
                        PInv.ErroriEdificiNoInvio += ", " + OidTicketAssistenza.ToString();
                    }
                }
                else           ///////////  se i destinatari sono maggiori di zero ##############################################
                {
                    string DestinatariUniti = "";
                    MailMessaggio.To.Clear();
                    foreach (DataRow RowDestinatariUniti in TDestinatari.Rows) // row4 rowdestinatari
                    {
                        string NomeDestinatario = RowDestinatariUniti["NOME"].ToString();
                        System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti["DESEMAIL"].ToString().Trim(), NomeDestinatario);
                        MailMessaggio.To.Add(TO);
                        DestinatariUniti += RowDestinatariUniti["DESEMAIL"].ToString() + "; ";
                        PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("Ticket_{0}_{1}_{2}_DestUni.txt", PInv.NomeFileSessione, OidTicketAssistenza.ToString() + "-" + UserNameCorrente, (Anno - 2000)));

                    }
                    cl.TxtLogSpedizioni("Destinatari Mail: " + DestinatariUniti, true);
                    // imposta   OGGETTO + CORPO @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@      
                    MailMessaggio.Subject = string.Format("Ticket Assistenza Eams:{0}; Utente {1}", OidTicketAssistenza.ToString(), UserNameCorrente);
                    MailMessaggio.Body = corpo.ToString();

                    cl.TxtLogSpedizioni("Corpo Mail: " + corpo.ToString(), true);
                    cl.TxtLogSpedizioni("Fine Immobile -------------------------- ", true);
                    cl.TxtLogSpedizioni("  ", true);
                    //  MailMessaggio.Attachments.Clear();  //ANNULLO GLI ALLEGATI DA ELIMINARE  
                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
                    using (var inv = new Invio())
                    {
                        int Esito = 0;
                        Messaggio = "Email di Avviso Inviata!";
                        string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                        if (!string.IsNullOrWhiteSpace(Ritorno))
                        {
                            Messaggio = "Email di Avviso Inviata, con Avvertimenti!";
                            Esito = 1;
                        }
                        string strMessaggio = string.Empty;
                        string Spedita = string.Empty;
                        if (Esito == 0)
                        {
                            //  chiama procedura di cambio stato in SPEDITO                          
                            using (var db = new DB())
                            {
                                if (OidTicketAssistenza != 0)
                                {

                                    if (OidTicketAssistenza > 0)
                                    {
                                        Classi.SetVarSessione.OracleConnString = Connessione;
                                        strMessaggio = db.SetStatoTrasmesso("Admin", OidTicketAssistenza, ref Spedita);
                                    }
                                }
                            }
                        }
                        gdb.LogMessaggioEMailCN(UserNameCorrente, OidTicketAssistenza, MailMessaggio, DateTime.Now, Esito, DateTime.Now, DateTime.Now);
                    }
                    ///********************************************************************************************************
                } ////  chide if destinatari nulli su else
            }  // chiude ciclo for per edifici
            //#######################################################################
            //######################################################################
            /// invio log invio amministratore di sistema 
            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("CP_{0}_{1}_{2}_DestUni.txt", PInv.NomeFileSessione, "Admin", (Anno - 2000)));
            //Invio Log Spedizione - Area di Polo Energia Centrale: Centrale di COLLIO, Avviso Scadenze Controlli Periodici Normativi Data invio: 20/08/2015
            //string oggetto = MailMessaggio.Subject.ToString();
            string oggetto = string.Format("Invio Log Spedizione -  Ticket Assistenza EAMS - Data invio: ", DateTime.Now.ToShortDateString());
            SmtpClient ClientPostaAdmin = new SmtpClient(PInv.SMTPSERVER, PInv.SMTPSERVERPOSTA) { Credentials = new System.Net.NetworkCredential(PInv.USERSMTP, PInv.PWDSMTP) };

            MailMessage MailM = new MailMessage();
            FORM = new MailAddress(PInv.MAILFROM, "EAMS ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailM.From = FORM;
            MailM.To.Clear();
            MailM.Attachments.Clear();  //ANNULLO GLI ALLEGATI DA ELIMINARE  
            if (PInv.MAILTOAdmin.Length != 0)
            {
                string[] splitTo = PInv.MAILTOAdmin.Split(new Char[] { ';' });
                foreach (string Codice in splitTo)
                {
                    MailAddress to = new MailAddress(Codice.Trim(), "Amministratore EAMS");  //Dest
                    MailM.To.Add(to);
                }
            }

            MailM.Body = cl.LeggiTxtLogSpedizioni();
            MailM.CC.Clear();
            MailM.Subject = oggetto;
            using (var inv = new Invio())
            {
                inv.SendMailNew(ClientPostaAdmin, MailM, PInv.FullPathFileSaveMail);
            }


        }

        private SmtpClient GetParametriMailTicketAssistenza(CAMSInvioMailCN.GetDataDB gdb)
        {

            DataTable TabellaParametriEmail = gdb.GetParametriMail();
            //GetParametriEmail(PInv.USERNAME, "CP");
            if (TabellaParametriEmail.Rows.Count > 0)
            {
                foreach (DataRow rowParEmail in TabellaParametriEmail.Rows)//  giro su edifici
                {
                    PInv.MAILFROM = rowParEmail["MAILFROM"].ToString();
                    PInv.SMTPSERVER = rowParEmail["SERVERSMTP"].ToString();
                    PInv.SMTPSERVERPOSTA = int.Parse(rowParEmail["PORTASMTP"].ToString());
                    PInv.USERSMTP = rowParEmail["USERSSMTP"].ToString();
                    PInv.PWDSMTP = rowParEmail["PWSSMTP"].ToString();
                    PInv.MAILTOCC = rowParEmail["MAILTOCC"].ToString();
                    PInv.MAILTOAdmin = rowParEmail["MAILTOADMIN"].ToString();
                    //PInv.PATHFILEPMP = rowParEmail["PATHFILEPMP"].ToString();
                    //PInv.PATHFILESTORE = rowParEmail["PATHFILESTORE"].ToString();
                    //PInv.USERNAME = rowParEmail["USERNAME"].ToString();
                }
                //   SmtpClient ClientPosta = new SmtpClient(SmtpServer, SmtpSrvPorta) { Credentials = new System.Net.NetworkCredential(UserSmtp, pwdsmtp) };
                SmtpClient ClientPosta = new SmtpClient(PInv.SMTPSERVER, PInv.SMTPSERVERPOSTA) { Credentials = new System.Net.NetworkCredential(PInv.USERSMTP, PInv.PWDSMTP) };

                return ClientPosta;
            }
            else
            {
                SmtpClient ClientPosta = new SmtpClient();
                return ClientPosta;
            }
        }
        #endregion


        //               Connessione           Admin                0
        public string InviaAvvisiMAILRdLNonPreseinCarico(string Connessione, string UserNameCorrente, int OidRegRdL,
                                                         BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        {
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            // AzioneSpedizione = 0=mail, 1=sms; 2 = sms+mail

            string linkEAMS = linkEAMS = "http://pa-eams.engie.it/Default.aspx#ViewID=RdLListView_ListView_Variant&ObjectClassName=CAMS.Module.DBTask.Vista.RdLListView&ViewVariant_ViewId=RdLListView_ListView_CO"; ;
            if (Connessione.Contains("EAMS_OL_SL"))
            {
                linkEAMS = "http://sl3-eams.engie.it/Default.aspx#ViewID=RdL_DetailView_Gestione&ObjectKey=";
                linkEAMS = "http://sl3-eams.engie.it/Default.aspx#ViewID=RdLListView_ListView_Variant&ObjectClassName=CAMS.Module.DBTask.Vista.RdLListView&ViewVariant_ViewId=RdLListView_ListView_CO";
            }

            if (Connessione.Contains("EAMS_OL_PA"))
            {
                linkEAMS = "http://pa-eams.engie.it/Default.aspx#ViewID=RdL_DetailView_Gestione&ObjectKey=";
                linkEAMS = "http://pa-eams.engie.it/Default.aspx#ViewID=RdLListView_ListView_Variant&ObjectClassName=CAMS.Module.DBTask.Vista.RdLListView&ViewVariant_ViewId=RdLListView_ListView_CO";
            }


            var objDCDatiMail = objDCDatiSMSMail.Where(w => new[] { 0, 2 }.Contains(w.AzioneSpedizione) && w.IndirizzoMail != null && w.IndirizzoMail != "")
                                .Select(s => new
                                {
                                    s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                                    s.IndirizzoMail, //    O_INDIRIZZO_MAIL,
                                    s.NomeCognome,//   O_NOME_COGNOME,
                                    s.Subject,  //O_SUBJECT,
                                    s.Body    //O_BODY
                                }).Distinct();

            List<string> ListTipoAzioni = new List<string>();
            //int ContaMail = DestinatariMail.Count();
            int ContaMail = objDCDatiMail.Count();
            if (ContaMail > 0)
            {
                ListTipoAzioni = objDCDatiMail.Select(S => S.TipoAzioneMail).Distinct().ToList(); //.Select(S => S.O_TIPOAZIONEMAIL).Distinct().ToList();
                foreach (var item in ListTipoAzioni)
                {
                    string TxtLog = "";
                    int Anno = DateTime.Now.Year;
                    setLogSpedizioni();
                    Class1 cl = new CAMSInvioMailCN.Class1();
                    SmtpClient ClientPosta = GetParametriMailATM(gdb);// parametri di spedizione ATM
                    if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
                    {
                        TxtLog = "Parametri Assenti";
                        cl.TxtLogSpedizioni(TxtLog, true);
                        ReturnMessaggio.Append("Errore: Parametri Assenti");  // return "Errore: Parametri Assenti";
                    }
                    else
                    {
                        TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                        cl.TxtLogSpedizioni(TxtLog, true);
                    }
                    ///  istanza del messaggio  imposto i parametrifissi          
                    MailMessage MailMessaggio = new MailMessage();

                    MailAddress FROM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Servizi Spa", System.Text.Encoding.UTF8);
                    MailMessaggio.From = FROM;

                    MailMessaggio.CC.Clear();
                    if (PInv.MAILTOCC.Length != 0)
                    {
                        string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                        foreach (string Codice in splitToCC)
                        {
                            MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                            MailMessaggio.CC.Add(CC);
                        }
                    }
                    cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);

                    ///  recupero rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!  
                    PInv.FullPathFileSaveMail = "";
                    StringBuilder corpo = new StringBuilder(" ", 320000);
                    PInv.TotEdifici += 1;// totale edifici processati
                    string DestinatariUniti = "";
                    MailMessaggio.To.Clear();
                    MailMessaggio.Subject = "";
                    MailMessaggio.Body = "";
                    MailMessaggio.IsBodyHtml = true;
                    int ContaGiri = 0;
                    ///---------------------------------------------
                    cl.TxtLogSpedizioni("Spedizione Avviso per Registro RdL: " + OidRegRdL, true);
                    //-------------------------------------------
                    var DestMAILxTipo = objDCDatiMail.Where(w => w.TipoAzioneMail == item); // item= sms, poi va su MAIL, poi va su sms+mail
                    int CountDestxTipo = DestMAILxTipo.Count();
                    if (CountDestxTipo > 0)
                    {
                        //http://eams.cofely.it/PA/Default.aspx#ViewID=RdL_DetailView_Gestione&ObjectKey=71442&ObjectClassName=CAMS.Module.DBTask.RdL&mode=View
                        
                        corpo.Append("di seguito le RdL non ancora prese in Carico:" + "\n");
                        corpo.Append(string.Format(" <a href='{0}'>link Elenco</a> ",
                            linkEAMS) + "\n <br />");
                        //  elaboro il corpo della mail 
                        foreach (var RowDestinatariUniti in DestMAILxTipo) // row4 rowdestinatari   DestinatariMail
                        {
                            corpo.Append(RowDestinatariUniti.Body.ToString() + "\n <br />"); //         .O_BODY.ToString() + "\n"); //   

                            MailMessaggio.Body = corpo.ToString();

                            if (ContaGiri == 0)
                            {
                                MailMessaggio.Subject = RowDestinatariUniti.Subject.ToString();//   è sempre lo stesso

                                string NomeDestinatario = RowDestinatariUniti.NomeCognome.ToString();//  .O_NOME_COGNOME.ToString();// 
                                System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti.IndirizzoMail.ToString().Trim(), NomeDestinatario); //  .O_INDIRIZZO_MAIL.ToString().Trim(), NomeDestinatario); // 

                                MailMessaggio.To.Add(TO);
                                DestinatariUniti += RowDestinatariUniti.IndirizzoMail.ToString() + "; ";// .O_INDIRIZZO_MAIL.ToString() + "; ";// 
                                PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_Mail.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));
                            }
                            ContaGiri = 1;
                        }


                        cl.TxtLogSpedizioni("Destinatari Mail: " + DestinatariUniti, true);
                        cl.TxtLogSpedizioni("Subject: " + MailMessaggio.Subject, true);
                        cl.TxtLogSpedizioni("Corpo Mail: " + corpo.ToString(), true);
                        cl.TxtLogSpedizioni("Fine MAIL -------------------------- ", true);
                        cl.TxtLogSpedizioni("  ", true);
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
                        using (var inv = new Invio())
                        {
                            string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                            EsitoInvioMailSMS Esito = EsitoInvioMailSMS.Inviata;
                            if (Ritorno != "Invio Eseguito")
                            {
                                Esito = EsitoInvioMailSMS.ErrorediInvio;
                                ReturnMessaggio.Append("Errore Trasmissione");
                            }
                            else
                            {

                                ReturnMessaggio.Append("Destinatari Mail: " + DestinatariUniti);
                            }
                            var dv = gdb.InsertLog_DestinatariMail_byRdL(UserNameCorrente, OidRegRdL, MailMessaggio.From.ToString(), MailMessaggio.To.ToString(),
                                                                         DateTime.Now, MailMessaggio.Subject, MailMessaggio.Body, Ritorno, Esito, 0, "0", "");
                        }
                    }
                    else
                    {
                        TxtLog = "Destinatari Assenti";
                        setLogSpedizioni();       // CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                        cl.TxtLogSpedizioni(TxtLog, true);
                        ReturnMessaggio.Append("Destinatari Assenti");
                    }
                }
            }
            else
            {
                const string TxtLog = "Destinatari Assenti";
                setLogSpedizioni();
                CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                cl.TxtLogSpedizioni(TxtLog, true);
                ReturnMessaggio.Append("Destinatari Assenti");  //return "Destinatari Assenti";
            }

            gdb.Dispose();
            return ReturnMessaggio.ToString();
        }

        public EsitoInvioMailSMS EsitoInvio { get; private set; }
        //               Connessione           Admin                0
        public string InviaAvvisiMAILReprtPresenze(string Connessione, string UserNameCorrente, int OidRegRdL,
                                                         BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail, string PFileAttachment = "")
        {
            EsitoInvio = EsitoInvioMailSMS.NonInviata;
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            // AzioneSpedizione = 0=mail, 1=sms; 2 = sms+mail
            var objDCDatiMail = objDCDatiSMSMail.Where(w => new[] { 0, 2 }.Contains(w.AzioneSpedizione) && w.IndirizzoMail != null && w.IndirizzoMail != "")
                                .Select(s => new
                                {
                                    s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                                    s.IndirizzoMail, //    O_INDIRIZZO_MAIL,
                                    s.NomeCognome,//   O_NOME_COGNOME,
                                    s.Subject,  //O_SUBJECT,
                                    s.Body    //O_BODY
                                }).Distinct();

            List<string> ListTipoAzioni = new List<string>();
            //int ContaMail = DestinatariMail.Count();
            int ContaMail = objDCDatiMail.Count();
            if (ContaMail > 0)
            {
                ListTipoAzioni = objDCDatiMail.Select(S => S.TipoAzioneMail).Distinct().ToList(); //.Select(S => S.O_TIPOAZIONEMAIL).Distinct().ToList();
                foreach (var item in ListTipoAzioni)
                {
                    string TxtLog = "";
                    int Anno = DateTime.Now.Year;
                    setLogSpedizioni();
                    Class1 cl = new CAMSInvioMailCN.Class1();
                    SmtpClient ClientPosta = GetParametriMailATM(gdb);// parametri di spedizione ATM
                    if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
                    {
                        TxtLog = "Parametri Assenti";
                        cl.TxtLogSpedizioni(TxtLog, true);
                        ReturnMessaggio.Append("Errore: Parametri Assenti");  // return "Errore: Parametri Assenti";
                    }
                    else
                    {
                        TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                        cl.TxtLogSpedizioni(TxtLog, true);
                    }
                    ///  istanza del messaggio  imposto i parametrifissi          
                    MailMessage MailMessaggio = new MailMessage();

                    MailAddress FORM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Italia Spa", System.Text.Encoding.UTF8);
                    MailMessaggio.From = FORM;

                    MailMessaggio.CC.Clear();
                    if (PInv.MAILTOCC.Length != 0)
                    {
                        string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                        foreach (string Codice in splitToCC)
                        {
                            MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                            MailMessaggio.CC.Add(CC);
                        }
                    }
                    cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);

                    ///  recupero rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!  
                    PInv.FullPathFileSaveMail = "";
                    StringBuilder corpo = new StringBuilder(" ", 320000);
                    PInv.TotEdifici += 1;// totale edifici processati
                    string DestinatariUniti = "";
                    MailMessaggio.To.Clear();
                    MailMessaggio.Subject = "";
                    MailMessaggio.Body = "";
                    MailMessaggio.IsBodyHtml = true;
                    int ContaGiri = 0;
                    ///---------------------------------------------
                    cl.TxtLogSpedizioni("Spedizione Avviso per Registro RdL: " + OidRegRdL, true);
                    //-------------------------------------------
                    var DestMAILxTipo = objDCDatiMail.Where(w => w.TipoAzioneMail == item); // item= sms, poi va su MAIL, poi va su sms+mail
                    int CountDestxTipo = DestMAILxTipo.Count();
                    if (CountDestxTipo > 0)
                    {
                        //  elaboro il corpo della mail 
                        foreach (var RowDestinatariUniti in DestMAILxTipo) // row4 rowdestinatari   DestinatariMail
                        {
                            corpo.Append(RowDestinatariUniti.Body.ToString() + "\n <br />"); //         .O_BODY.ToString() + "\n"); //   

                            MailMessaggio.Body = corpo.ToString();

                            if (ContaGiri == 0)
                            {
                                MailMessaggio.Subject = RowDestinatariUniti.Subject.ToString();//   è sempre lo stesso

                                string NomeDestinatario = RowDestinatariUniti.NomeCognome.ToString();//  .O_NOME_COGNOME.ToString();// 
                                System.Net.Mail.MailAddress TO = new MailAddress(RowDestinatariUniti.IndirizzoMail.ToString().Trim(), NomeDestinatario); //  .O_INDIRIZZO_MAIL.ToString().Trim(), NomeDestinatario); // 

                                MailMessaggio.To.Add(TO);
                                DestinatariUniti += RowDestinatariUniti.IndirizzoMail.ToString() + "; ";// .O_INDIRIZZO_MAIL.ToString() + "; ";// 
                                PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_Mail.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));

                                MailMessaggio.Attachments.Clear();
                                if (PFileAttachment != "")
                                {
                                    if (File.Exists(PFileAttachment))
                                    {
                                        System.Net.Mail.Attachment attachment;
                                        attachment = new System.Net.Mail.Attachment(PFileAttachment);
                                        MailMessaggio.Attachments.Add(attachment);
                                    }
                                }
                            }
                            ContaGiri = 1;
                        }


                        cl.TxtLogSpedizioni("Destinatari Mail: " + DestinatariUniti, true);
                        cl.TxtLogSpedizioni("Subject: " + MailMessaggio.Subject, true);
                        cl.TxtLogSpedizioni("Corpo Mail: " + corpo.ToString(), true);
                        cl.TxtLogSpedizioni("Fine MAIL -------------------------- ", true);
                        cl.TxtLogSpedizioni("  ", true);
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
                        using (var inv = new Invio())
                        {
                            string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                            EsitoInvioMailSMS Esito = EsitoInvioMailSMS.Inviata;
                            EsitoInvio = Esito;
                            if (Ritorno != "Invio Eseguito")
                            {
                                Esito = EsitoInvioMailSMS.ErrorediInvio;
                                ReturnMessaggio.Append("Errore Trasmissione");
                            }
                            else
                            {

                                ReturnMessaggio.Append("Destinatari Mail: " + DestinatariUniti);
                            }
                            var dv = gdb.InsertLog_DestinatariMail_byRdL(UserNameCorrente, OidRegRdL, MailMessaggio.From.ToString(), MailMessaggio.To.ToString(),
                                                                         DateTime.Now, MailMessaggio.Subject, MailMessaggio.Body, Ritorno, Esito, 0, "0", "");
                        }
                    }
                    else
                    {
                        TxtLog = "Destinatari Assenti";
                        setLogSpedizioni();       // CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                        cl.TxtLogSpedizioni(TxtLog, true);
                        ReturnMessaggio.Append("Destinatari Assenti");
                    }
                }
            }
            else
            {
                const string TxtLog = "Destinatari Assenti";
                setLogSpedizioni();
                CAMSInvioMailCN.Class1 cl = new CAMSInvioMailCN.Class1();
                cl.TxtLogSpedizioni(TxtLog, true);
                ReturnMessaggio.Append("Destinatari Assenti");  //return "Destinatari Assenti";
            }

            gdb.Dispose();

            return ReturnMessaggio.ToString();
        }


        public string InviaAvvisiMAILRdL(string Connessione, string UserNameCorrente, int OidRegRdL,
                                                         BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        {
            EsitoInvio = EsitoInvioMailSMS.NonInviata;
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);
            // AzioneSpedizione = 0=mail, 1=sms; 2 = sms+mail
            var objDCDatiMail = objDCDatiSMSMail
                                //.Where(w => new[] { 0, 2 }.Contains(w.AzioneSpedizione) && w.IndirizzoMail != null && w.IndirizzoMail != "")
                                .Select(s => new
                                {
                                    s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                                    s.IndirizzoMail, //    O_INDIRIZZO_MAIL
                                    s.Subject,  //O_SUBJECT,
                                    s.Body    //O_BODY
                                }).Distinct();

            var item = objDCDatiMail.First();

            // List<string> ListTipoAzioni = new List<string>();
            //int ContaMail = DestinatariMail.Count();
            // int ContaMail = objDCDatiMail.Count();

            string TxtLog = "";
            int Anno = DateTime.Now.Year;
            setLogSpedizioni();
            Class1 cl = new CAMSInvioMailCN.Class1();
            SmtpClient ClientPosta = GetParametriMailATM(gdb);// parametri di spedizione ATM
            if (ClientPosta.Host == "" || string.IsNullOrEmpty(ClientPosta.Host))
            {
                TxtLog = "Parametri Assenti";
                cl.TxtLogSpedizioni(TxtLog, true);
                ReturnMessaggio.Append("Errore: Parametri Assenti");  // return "Errore: Parametri Assenti";
                EsitoInvio = EsitoInvioMailSMS.NonInviata;
            }
            else
            {
                TxtLog = String.Format("Parametri: {0},{1},{2},", PInv.SMTPSERVER, PInv.USERSMTP, PInv.MAILFROM);
                cl.TxtLogSpedizioni(TxtLog, true);
            }
            ///  istanza del messaggio  imposto i parametrifissi          
            MailMessage MailMessaggio = new MailMessage();

            MailAddress FROM = new MailAddress(PInv.MAILFROM, "EAMS" + " ENGIE Italia Spa", System.Text.Encoding.UTF8);
            MailMessaggio.From = FROM;

            MailMessaggio.CC.Clear();
            if (PInv.MAILTOCC.Length != 0)
            {
                string[] splitToCC = PInv.MAILTOCC.Split(new Char[] { ';' });
                foreach (string Codice in splitToCC)
                {
                    MailAddress CC = new MailAddress(Codice.Trim());  //Dest
                    MailMessaggio.CC.Add(CC);
                }
            }
            cl.TxtLogSpedizioni("Destinatari CC: " + PInv.MAILTOCC, true);

            ///  recupero rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!  
            PInv.FullPathFileSaveMail = "";
            PInv.TotEdifici += 1;// totale edifici processati
            string DestinatariUniti = "";
            MailMessaggio.To.Clear();
            MailMessaggio.Subject = "";
            MailMessaggio.Body = "";
            MailMessaggio.IsBodyHtml = true;
            ///---------------------------------------------
            cl.TxtLogSpedizioni("Spedizione Avviso per Registro RdL: " + OidRegRdL, true);
            //-------------------------------------------
            var DestMAILxTipo = objDCDatiMail; // item= sms, poi va su MAIL, poi va su sms+mail
            int CountDestxTipo = DestMAILxTipo.Count();

            MailMessaggio.Subject = item.Subject;//   è sempre lo stesso

            string NomeDestinatario = string.Empty; // RowDestinatariUniti.NomeCognome.ToString();//  .O_NOME_COGNOME.ToString();// 
            System.Net.Mail.MailAddress TO = new MailAddress(item.IndirizzoMail); //  .O_INDIRIZZO_MAIL.ToString().Trim(), NomeDestinatario); // 

            MailMessaggio.To.Add(TO);
            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_Mail.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));
            MailMessaggio.Body = new StringBuilder(item.Body).Append("\n <br />").ToString(); //         .O_BODY.ToString() + "\n"); //   


            cl.TxtLogSpedizioni("Destinatari Mail: " + item.IndirizzoMail, true);
            cl.TxtLogSpedizioni("Subject: " + item.Subject, true);
            cl.TxtLogSpedizioni("Corpo Mail: " + item.Body, true);
            cl.TxtLogSpedizioni("Fine MAIL -------------------------- ", true);
            cl.TxtLogSpedizioni("  ", true);
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email
            using (var inv = new Invio())
            {
                string Ritorno = inv.SendMailNew(ClientPosta, MailMessaggio, PInv.FullPathFileSaveMail);
                EsitoInvioMailSMS Esito = EsitoInvioMailSMS.Inviata;
                EsitoInvio = Esito;
                if (Ritorno != "Invio Eseguito")
                {
                    Esito = EsitoInvioMailSMS.ErrorediInvio;
                    ReturnMessaggio.Append("Errore Trasmissione");
                }
                else
                {

                    ReturnMessaggio.Append("Destinatari Mail: " + DestinatariUniti);
                }

            }


            gdb.Dispose();

            return ReturnMessaggio.ToString();
        }

        public SendResult InviaAvvisiSMSRdL(string Connessione, string UserNameCorrente, int OidRegRdL,
            BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        {
            EsitoInvio = EsitoInvioMailSMS.NonInviata;
            SendResult result = null;
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);

            // objDCDatiSMS
            var objDCDatiSMS = objDCDatiSMSMail
                .Where(w => w.IndirizzoSMS != null && w.IndirizzoSMS.Length > 6) // aggiunto ag il 09 7 2020
            .Select(s => new
            {
                s.Subject,
                s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
                                //  s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
                s.Body
            }).Distinct();

            string sms_SMSSender = "00393441741584";
            sms_SMSSender = "00393441741584";
            List<string> ListTipoAzioni = new List<string>();
            // int ContaSMS = DestinatariSMS.Count();

            var item = objDCDatiSMSMail.First();

            ///  recupero dati rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!                      
            string DestinatariUniti = "";
            string MessaggioSMS = "";
            string SubObjectSMS = "";
            List<string> TelefonoSMS = new List<string>();

            TelefonoSMS.Clear();

            int Anno = DateTime.Now.Year;
            setLogSpedizioni();
            Class1 cl = new CAMSInvioMailCN.Class1();
            PInv.FullPathFileSaveMail = "";
            StringBuilder corpo = new StringBuilder("", 32000); //    _______________
            PInv.TotEdifici += 1;// totale edifici processati     ------ || --  -  --  |-|     ------  
            cl.TxtLogSpedizioni("Spedizione Sms per Registro RdL: " + OidRegRdL, true); //  -------->>>>>>  ------->>>>>>>
                                                                                        //------------------------------------------- ||||||| ----------- ùùùùùù  **************  ######## \\\\\\  -->>>   hhh   <<< - - - >>>
            var DestSMSxTipo = objDCDatiSMS.Where(w => w.TipoAzioneMail == item.TipoAzioneMail); // var DestSMSxTipo = DestinatariSMS.Where(w => w.O_TIPOAZIONEMAIL == item);
            int CountDestxTipo = DestSMSxTipo.Count();
            //MessaggioSMS = new StringBuilder(item.Body).Append("\n <br />").ToString();//  "/n" item.Body.ToString(); // .O_BODY.ToString();
            MessaggioSMS = new StringBuilder(item.Body).Append("/n").ToString();//  "/n" item.Body.ToString(); // .O_BODY.ToString();
            SubObjectSMS = item.Subject.ToString();

            string telefono = string.Empty;
            telefono = item.IndirizzoSMS.ToString().Trim().Replace("(", ""); // i.O_INDIRIZZO_SMS.ToString().Trim().Replace("(", "");
            telefono = telefono.Replace(")", "");
            telefono = telefono.Replace("-", "");
            if (!string.IsNullOrEmpty(telefono) && !telefono.Equals("ND", StringComparison.InvariantCultureIgnoreCase))
            {
                TelefonoSMS.Add(telefono);
            }
            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_SMS.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));

            cl.TxtLogSpedizioni("Destinatari SMS: " + telefono, true);
            cl.TxtLogSpedizioni("Corpo SMS: " + MessaggioSMS.ToString(), true);
            cl.TxtLogSpedizioni("Fine SMS -------------------------- ", true);
            cl.TxtLogSpedizioni("  ", true);
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  send email

            SMSCConnection smsc_connection = null;
            try
            {
                smsc_connection = new SMSCHTTPConnection("Sms52333", "Prt_040502");//  ("1913233@aruba.it", "1973ardea");
            }
            catch (SMSCException smsc_ex)
            {
                Debug.WriteLine("Errore in connessione a Aruba:" + smsc_ex.Message);
                ReturnMessaggio.Append("Errore");
            }
            // operazioni sulla connessione
            try
            {
                SMS sms = new SMS();
                sms.TypeOfSMS = SMSType.ALTA;
                //sms.addSMSRecipient("+393463228369");                   //sms.addSMSRecipient("+393499876543");
                foreach (var tel in TelefonoSMS) // row4 rowdestinatari   DestinatariMail
                {
                    sms.addSMSRecipient(tel);
                }

                sms.Message = MessaggioSMS;  // "hello world!";
                sms.SMSSender = sms_SMSSender; // "00393463228369";
                sms.setImmediate(); // oppure sms.setScheduled_delivery(java.util.Date)
                                    //   sms.OrderId = "12345";
                result = smsc_connection.sendSMS(sms);
                // result.CountSentSMS
                EsitoInvio = EsitoInvioMailSMS.CONSEGNATO_SMSGen;
                //string dv = "Nessuna mail inviata";
            }
            catch (SMSCRemoteException smscre)
            {
                System.Console.WriteLine("Errore: from Remote Provider SMS: " + smscre.Message);
                ReturnMessaggio.Append("Errore: from Remote Provider SMS: " + smscre.Message);
            }
            catch (SMSCException smsce)
            {
                System.Console.WriteLine("Errore: from Provider SMS: " + smsce.Message);
                ReturnMessaggio.Append("Errore: from Provider SMS: " + smsce.Message);
            }

            smsc_connection.logout();  // nel caso di HTTP il metodo non lancia mai eccezioni
            ReturnMessaggio.Append("Destinatari SMS: " + DestinatariUniti);
            TelefonoSMS.Clear();


            gdb.Dispose();
            return result;
        }


        public SendResult GetStatusInvioSMS(string Connessione, string UserNameCorrente, int OidRegRdL,
    BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objDCDatiSMSMail)
        {
            EsitoInvio = EsitoInvioMailSMS.NonInviata;
            SendResult result = null;
            StringBuilder ReturnMessaggio = new StringBuilder("", 32000);
            CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione);

            // objDCDatiSMS
            var objDCDatiSMS = objDCDatiSMSMail
            .Select(s => new
            {
                s.Subject,
                s.TipoAzioneMail,//    O_TIPOAZIONEMAIL,
                s.IndirizzoSMS, //    O_INDIRIZZO_MAIL,
                                //  s.NomeCognome,//   O_NOME_COGNOME,          // s.Subject   ,  //O_SUBJECT,
                s.Body
            }).Distinct();

            string sms_SMSSender = "00393441741584";
            sms_SMSSender = "00393441741584";
            List<string> ListTipoAzioni = new List<string>();
            var item = objDCDatiSMSMail.First();
            ///  recupero dati rdl da spedire !!!!!!!!!!!!!!!!!!!!!!!!                      
            string DestinatariUniti = "";
            string MessaggioSMS = "";
            string SubObjectSMS = "";
            List<string> TelefonoSMS = new List<string>();
            TelefonoSMS.Clear();
            int Anno = DateTime.Now.Year;
            setLogSpedizioni();
            Class1 cl = new CAMSInvioMailCN.Class1();
            PInv.FullPathFileSaveMail = "";
            StringBuilder corpo = new StringBuilder("", 32000); //    _______________
            PInv.TotEdifici += 1;// totale edifici processati     ------ || --  -  --  |-|     ------  
            cl.TxtLogSpedizioni("Spedizione Sms per Registro RdL: " + OidRegRdL, true); //  -------->>>>>>  ------->>>>>>>
                                                                                        //------------------------------------------- ||||||| ----------- ùùùùùù  **************  ######## \\\\\\  -->>>   hhh   <<< - - - >>>
            var DestSMSxTipo = objDCDatiSMS.Where(w => w.TipoAzioneMail == item.TipoAzioneMail); // var DestSMSxTipo = DestinatariSMS.Where(w => w.O_TIPOAZIONEMAIL == item);
            int CountDestxTipo = DestSMSxTipo.Count();
            //MessaggioSMS = new StringBuilder(item.Body).Append("\n <br />").ToString();//item.Body.ToString(); // .O_BODY.ToString();
            MessaggioSMS = new StringBuilder(item.Body).Append("/n").ToString();//item.Body.ToString(); // .O_BODY.ToString();
            SubObjectSMS = item.Subject.ToString();

            string telefono = string.Empty;
            telefono = item.IndirizzoSMS.ToString().Trim().Replace("(", ""); // i.O_INDIRIZZO_SMS.ToString().Trim().Replace("(", "");
            telefono = telefono.Replace(")", "");
            telefono = telefono.Replace("-", "");
            if (!string.IsNullOrEmpty(telefono) && !telefono.Equals("ND", StringComparison.InvariantCultureIgnoreCase))
            {
                TelefonoSMS.Add(telefono);
            }
            PInv.FullPathFileSaveMail = Path.Combine(PInv.FullPathMail, String.Format("RdL_{0}_{1}_{2}_SMS.txt", PInv.NomeFileSessione, OidRegRdL, (Anno - 2000)));

            cl.TxtLogSpedizioni("Destinatari SMS: " + telefono, true);
            cl.TxtLogSpedizioni("Corpo SMS: " + MessaggioSMS.ToString(), true);
            cl.TxtLogSpedizioni("Fine SMS -------------------------- ", true);
            cl.TxtLogSpedizioni("  ", true);
            //@@@@   stabilisce connessione SMS @@@@@
            SMSCConnection smsc_connection = null;
            try
            {
                smsc_connection = new SMSCHTTPConnection("Sms52333", "Prt_040502");//  ("1913233@aruba.it", "1973ardea");
            }
            catch (SMSCException smsc_ex)
            {
                Debug.WriteLine("Errore in connessione a Aruba:" + smsc_ex.Message);
                ReturnMessaggio.Append("Errore");
            }
            // operazioni sulla connessione
            try
            {
                #region invio mail codice vecchio
                //   SMS sms = new SMS();
                //   sms.TypeOfSMS = SMSType.ALTA;          //sms.addSMSRecipient("+393463228369");                   //sms.addSMSRecipient("+393499876543");
                //   foreach (var tel in TelefonoSMS) // row4 rowdestinatari   DestinatariMail
                //   {
                //       sms.addSMSRecipient(tel);
                //   }
                //   sms.Message = MessaggioSMS;  // "hello world!";
                //   sms.SMSSender = sms_SMSSender; // "00393463228369";
                //   sms.setImmediate(); // oppure sms.setScheduled_delivery(java.util.Date)
                ////   sms.OrderId = "12345";
                //   result = smsc_connection.sendSMS(sms);
                //   EsitoInvio = EsitoInvioMailSMS.CONSEGNATO_smsGen;
                #endregion
                ///////
                //SCHEDULED	// posticipato, non ancora inviato
                //SENT	// inviato, non attende delivery
                //DLVRD	// l'SMS è stato correttamente ricevuto
                //ERROR	// errore in invio dell'SMS
                //TIMEOUT	// l'operatore non ha fornito informazioni sull'SMS entro le 48 ore
                //TOOM4NUM	// troppi SMS per lo stesso destinatario nelle ultime 24 ore
                //TOOM4USER	// troppi SMS inviati dall'utente nelle ultime 24 ore
                //UNKNPFX	// prefisso SMS non valido o sconosciuto
                //UNKNRCPT	// numero di telefono del destinatario non valido o sconosciuto
                //WAIT4DLVR	// messaggio inviato, in attesa di delivery
                //WAITING	// in attesa, non ancora inviato
                //UNKNOWN	// stato sconosciuto

                string OrderId = "1234";
                try
                {
                    List<MessageStatus> smsstatus = smsc_connection.getMessageStatus(OrderId); //  List<MessageStatus> smsstatus = smsc_connection.getMessageStatus("1234");
                    foreach (MessageStatus message_status in smsstatus)
                    {
                        System.Console.WriteLine("destinatario :" + message_status.Recipient + ": ");
                        if (message_status.IsError)
                        {
                            System.Console.WriteLine("errore!");
                        }
                        else
                        {
                            if (message_status.Status == MessageStatus.SMSStatus.DLVRD)
                            {
                                System.Console.WriteLine("consegnato.");
                            }
                            else
                            {
                                System.Console.WriteLine("in attesa...");
                            }
                        }
                    }
                }
                catch (SMSCRemoteException smscre)
                {
                    System.Console.WriteLine("Exception from Aruba: " + smscre.Message);
                }


            }
            catch (SMSCRemoteException smscre)
            {
                System.Console.WriteLine("Errore: from Remote Provider SMS: " + smscre.Message);
                ReturnMessaggio.Append("Errore: from Remote Provider SMS: " + smscre.Message);
            }
            catch (SMSCException smsce)
            {
                System.Console.WriteLine("Errore: from Provider SMS: " + smsce.Message);
                ReturnMessaggio.Append("Errore: from Provider SMS: " + smsce.Message);
            }

            smsc_connection.logout();  // nel caso di HTTP il metodo non lancia mai eccezioni
            ReturnMessaggio.Append("Destinatari SMS: " + DestinatariUniti);
            TelefonoSMS.Clear();


            gdb.Dispose();
            return result;
        }




    }
}
