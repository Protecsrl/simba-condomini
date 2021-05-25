using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CAMS.Module.Classi
{
    public class Util : IDisposable
    {
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

        public FileData getFileFromViewId(string viewId)
        {
            return null;
        }

        public string AggiornaLogSchedeMPeSpedisceMail(string UserNameCorrente, int Oidpmp, CAMS.Module.DBAngrafica.ParametriNET par)
        {
            var db = new DB();
            var dv = db.AggiornaLogSchedeMP(UserNameCorrente, Oidpmp);

            var ml = new CAMS.Module.Classi.Mail(par.ServerSMTP, par.PortaSMTP, par.UserSMTP, par.PwSMTP, par.MailFrom, par.MailToCC);
            var PathNameFileSave = ml.InviaMailAvvisoSchedeMP(UserNameCorrente, dv);

            ml.Dispose();
            db.Dispose();

            return PathNameFileSave;
        }

        public string SpedisceMailEdificioControlloNormativo(string UserNameCorrente,
                                                     int OidEdificio, DateTime DataDA, DateTime DataA)
        {
            string Messaggio = string.Empty;
            using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
            {
                im.InviaReport(Classi.SetVarSessione.OracleConnString, UserNameCorrente, OidEdificio, 1, ref Messaggio, DataDA, DataA);

            }
            //CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail();
            //public void InviaReport(string Connessione, string UserNameCorrente, int OidEdificio, int TuttiZeroUnoUno, ref string Messaggio
            //                                      , DateTime DataDA, DateTime DataA)
            //im.InviaReport(Classi.SetVarSessione.OracleConnString, UserNameCorrente, OidEdificio, 1, ref Messaggio, DataDA, DataA);
            //im.Dispose();
            //var db = new DB();
            //var dv = db.GetDatiInvioMailControlloNormativo(UserNameCorrente, OidControlloNormativo, OidEdificio, Data);
            //var ml = new Mail();
            //var PathNameFileSave = ml.InviaMailAvvisoControlliPeriodici(UserNameCorrente, dv);

            //ml.Dispose();
            //db.Dispose();

            return Messaggio;
        }


        public static double CalcolaDistanzaInkiloMeteri(double Pa_lat1, double Pa_lon1, double Ar_lat2, double Ar_lon2)
        {
            //   distanza (A,B) = R * arccos(sin(latA) * sin(latB) + cos(latA) * cos(latB) * cos(lonA-lonB))
            var rad = 6371.0072d;  //6372,795477598    6371.0072d;

            var p1X = Pa_lat1 / 180 * Math.PI;
            var p1Y = Pa_lon1 / 180 * Math.PI;
            var p2X = Ar_lat2 / 180 * Math.PI;
            var p2Y = Ar_lon2 / 180 * Math.PI;

            var distanza = Math.Acos(Math.Sin(p1Y) * Math.Sin(p2Y) + Math.Cos(p1Y) * Math.Cos(p2Y) * Math.Cos(p2X - p1X)) * rad * 1000;

            var R = 6371.0072d;
            var latA = Pa_lat1 / 180 * Math.PI;
            var lonA = Pa_lon1 / 180 * Math.PI;
            var latB = Ar_lat2 / 180 * Math.PI;
            var lonB = Ar_lon2 / 180 * Math.PI;
            var distanza1 = Math.Acos(Math.Sin(latA) * Math.Sin(latB) + Math.Cos(latA) * Math.Cos(latB) * Math.Cos(lonA - lonB)) * R * 1000;
            return distanza1 / 1000D;
        }

        public static string GetContieneTesto(  string SearchProperties, string paramValue)
        {
            string add = string.Empty;
            //string add = string.Format("Contains(Upper({0}),'{1}')", SearchProperties, paramValue);
            if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                  add = string.Format("Contains({0},'{1}')", SearchProperties, paramValue);
            else
                  add = string.Format("Contains(Upper({0}),'{1}')", SearchProperties, paramValue);
            return add;
        }

     

        public string GetDescrizioneDistanza(double Distanza)
        {
            if (Distanza == 0) return "non Calcolabile";
            if (Distanza > 0 && Distanza < 100) return Math.Truncate(Distanza).ToString() + " km"; // "in Prossimità"
            if (Distanza > 100 && Distanza < 150) return "da 100 a 150 km";
            if (Distanza > 150 && Distanza < 300) return "piu di 150 km";
            return "molto Lontano";

        }

        public static string GetDescrizioni(int inx)
        {
            var Descrizione = new string[] { "Non Definito",
                "Scenario da Associare" , "Scenario Associato", "Scenario Bloccato Nella Modifica",
                "Anno Schedulazione da Assegnare", "Anno Schedulazione Asseganto", "Anno Schedulazione Blocco nella Modifica",
                "Registro Vincoli da Assegnare", "Registro Vincoli Assegnato", "Registro Vincoli Bloccato nella Modifica",
                "Schedulazione da Eseguire", "Schedulazione Eseguita", "Schedulazione Accettata e Bloccata",
            "Assegnazione Risorse da Eseguire", "Assegnazione Risorse Eseguita", "Assegnazione Risorse Bloccate nella Modifica",
            "Creazione ODL per Settimana da Eseguire", "Seleziona Settimana Per Creazione ODL", "Completata Creazione OdL per ogni Settimana, e Bloccate nella Modifica",
            "Attesa Conferma Anno precedente", "Imposta Anno Successivo", "Scenario Completato"

            };
            if (inx >= Descrizione.Length || inx <= 0)
            {
                return null;
            }
            return Descrizione[inx];
        }

        public static string GetToolTipStatoPianificazione(int inx)
        {
            var Descrizione = new string[] { "Non Definito",
                "Scenario da Associare" , "Scenario Associato", "Scenario Bloccato Nella Modifica",
                "Anno Schedulazione da Assegnare", "Anno Schedulazione Asseganto", "Anno Schedulazione Blocco nella Modifica",
                "Registro Vincoli da Assegnare", "Registro Vincoli Assegnato", "Registro Vincoli Bloccato nella Modifica",
                "Schedulazione da Eseguire", "Schedulazione Eseguita", "Schedulazione Accettata e Bloccata",
            "Assegnazione Risorse da Eseguire", "Assegnazione Risorse Eseguita", "Assegnazione Risorse Bloccate nella Modifica",
            "Creazione ODL per Settimana da Eseguire", "Seleziona Settimana Per Creazione ODL", "Completata Creazione OdL per ogni Settimana, e Bloccate nella Modifica",
            "Attesa Conferma Anno precedente", "Imposta Anno Successivo", "Scenario Completato"

            };
            if (inx >= Descrizione.Length || inx <= 0)
            {
                return null;
            }
            return Descrizione[inx];
        }

        public string GetCodiceApparato(string Impianto_CodDescrizione, string StdApparato_CodDescrizione, int Impianto_Oid, int numSequenzaApparato, DevExpress.Xpo.Session Ses)
        {
            var FormattazioneCodice = "{0:000}";
            var CodDescrizione = String.Format("{0}_{1}_{2}", Impianto_CodDescrizione, StdApparato_CodDescrizione,
                            String.Format(FormattazioneCodice,
                            Convert.ToInt32(Ses.Evaluate<CAMS.Module.DBPlant.Asset>(DevExpress.Data.Filtering.CriteriaOperator.Parse("Count"),
                            new DevExpress.Data.Filtering.BinaryOperator("Oid", Impianto_Oid))) + (1 + numSequenzaApparato)));

            return CodDescrizione;
        }

        //public void CaricaNrCopieApparato()
        //{
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}



        public void InserSegnalazione(string Connessione, string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody,
                                      string emailReceived, string emailMessageID, string CurentUser, string StringaRicerca)
        {
            try
            {
                using (var db = new DB())
                {
                    if (emailBody.Contains("Richiesta"))
                    {
                        //- Richiesta numero: [R/0148/18] 
                        //- Sito: [SP01]  
                        //- Servizio: [03a]  
                        //- Urgenza: [Giallo]
                        //- Descrizione: [Al Piano Terra nell'ufficio del personale ci sono n. 2 neon bruciati. Altezza superiore ai 3 metri]
                        string strAvviso = " ";
                        string strEdificio = " ";
                        string strAssemblaggio = " ";
                        string strDescrizione = " ";
                        // inserire l'email nella tabella AVVISI SEGNALAZIONI 
                        Classi.SetVarSessione.OracleConnString = Connessione;
                        var dv = db.InsertSegnalazioneMail(emailFrom, emailTo, emailDate, emailSubject, emailBody, emailReceived, emailMessageID,
                            strEdificio, strAvviso, strAssemblaggio, strDescrizione);
                    }
                }
            }
            catch
            { }

            // di segnito spedisce le segnalazioni di creazione
            //try
            //{
            //    DataTable TRdLSeg_Edifici = new DataTable();
            //    //  recupero tutti le rdl pronte da creare le richieste
            //    using (var db = new DB())
            //    {
            //        if (CurentUser != null)
            //        {
            //            Classi.SetVarSessione.OracleConnString = Connessione;
            //            TRdLSeg_Edifici = db.GetRdLSegnalazioneMail(CurentUser, StringaRicerca);
            //            //  qui metto il recupero della rdl e la spedizione
            //            System.Diagnostics.Debug.WriteLine("mio");
            //        }
            //    }

            //    if (TRdLSeg_Edifici.Rows.Count > 0)
            //    {
            //        string UserNameCorrente = string.Empty;
            //        string Messaggio = string.Empty;
            //        int OidEdificio = 0;
            //        try
            //        {
            //            int dtRowsCount = TRdLSeg_Edifici.Rows.Count;
            //            for (int j = 0; j < dtRowsCount; j++)
            //            {
            //                // dt1.Rows[j][colName] = TRdLSeg_Edifici.Rows[j][colName];
            //                //StringBuilder corpo = new StringBuilder("", 32000000);
            //                //string IMMOBILE = TRdLSeg_Edifici.Rows[j]["IMMOBILE"].ToString(); //rowBL["IMMOBILE"].ToString(); // oid immobile
            //                //string STREDIFICIO = TRdLSeg_Edifici.Rows[j]["STREDIFICIO"].ToString(); //rrowBL["STREDIFICIO"].ToString();  
            //                //string STRAVVISO = TRdLSeg_Edifici.Rows[j]["STRAVVISO"].ToString(); // rowBL["STRAVVISO"].ToString();
            //                //corpo.Append(TRdLSeg_Edifici.Rows[j]["CORPO"].ToString()); // rowBL["CORPO"].ToString());
            //                //string OID = TRdLSeg_Edifici.Rows[j]["OID"].ToString(); //rowBL["OID"].ToString();
            //                string REGRDL_OID = TRdLSeg_Edifici.Rows[j]["REGRDL"].ToString(); //  rowBL["RDL"].ToString();
            //                int Oid_Reg_RdL = 0;
            //                if (int.TryParse(REGRDL_OID, out Oid_Reg_RdL))
            //                {
            //                    if (Oid_Reg_RdL > 0)
            //                    {
            //                        //71353/19183/71298
            //                        Oid_Reg_RdL = 71305;
            //                        this.CreateRdLEmergenzadaMail(Oid_Reg_RdL);

            //                        //using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
            //                        //{
            //                        //    im.InviaMessaggiRdL(SetVarSessione.OracleConnString, CurentUser, Oid_Reg_RdL, ref  Messaggio);
            //                        //}
            //                    }
            //                }
            //            }
            //            //////   im.Invia_Segnalazione_RdL(IMMOBILE, STREDIFICIO, STRAVVISO, corpo, OID, RDL,
            //            //////Classi.SetVarSessione.OracleConnString, UserNameCorrente, OidEdificio, ref Messaggio);
            //            //Debug.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6}", IMMOBILE, STREDIFICIO, STRAVVISO, corpo, OID, RDL, dtRowsCount));
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new Exception(ex.Message);
            //        }

            //    }

            //}
            //catch
            //{ }

            // fine inser segnalazione
        }





        public DataTable GetRegRdLdaSpedireMail(string Connessione, string CurentUser, string StringaRicerca)
        {
            DataTable TRegRdLdaSpedireMail = new DataTable();
            try
            {
                using (CAMSInvioMailCN.GetDataDB gdb = new CAMSInvioMailCN.GetDataDB(Connessione))
                {
                    Classi.SetVarSessione.OracleConnString = Connessione;
                    TRegRdLdaSpedireMail = gdb.GetRegRdLdaSpedireMail(CurentUser, StringaRicerca);
                    //  qui metto il recupero della rdl e la spedizione
                    System.Diagnostics.Debug.WriteLine("mio");
                }
            }
            catch
            { }
            return TRegRdLdaSpedireMail;
        }

        public static string GetPercorsoFile(string subPath = "")
        {
            string path1 = CAMS.Module.Classi.SetVarSessione.PhysicalPathSito;//
            if (subPath == "")
            {
                path1 = SetVarSessione.PhysicalPathSito;//
            }
            else
            {
                path1 = System.IO.Path.Combine(SetVarSessione.PhysicalPathSito, subPath);
            }
            if (!Directory.Exists(path1))
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(path1);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path1));
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
            return path1;
        }

        //  System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
        public CAMS.Module.DBAgenda.NotificaRdL SetNotificaRdL(CAMS.Module.DBAgenda.NotificaRdL nrdl
            , CAMS.Module.DBTask.RdL rdl, string location, int addTempo,
            CAMS.Module.DBAgenda.AppuntamentiRisorse resource, ref List<string> sbMessaggio)  ///  ref StringBuilder sbMessaggio)
        {
            if (nrdl != null)
            {
                try
                {
                    //nrdl.RdL = rdl;
                    if (!string.IsNullOrEmpty(location))
                        nrdl.Location = location;// indirizzo

                    nrdl.Subject = string.Format("RdL/RegdL: {0}/{1} - Immobile: {2}",
                                                            rdl.Oid, rdl.RegistroRdL.Oid, rdl.Immobile.Descrizione);
                    nrdl.Description = string.Format("{0}", rdl.Descrizione);
                    //  persintalias
                    nrdl.LabelListView = LabelListView.in_attesa_di_dichiarazione_tecnico;// = 1
                    //nrdl.Label = 1;// SetLabelNotifica(rdl, nrdl);// in attesa della dichiarazione del tecnico #############
                    //nrdl.LabelListView = (LabelListView)nrdl.Label;

                    nrdl.StartDate = DateTime.Now.AddMinutes(2); // - TimeSpan.FromDays(1) data di inizio avviso di popup    -  obj.StartOn = DateTime.Now - TimeSpan.FromDays(1);
                    nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);// data scadenza   //--obj.EndOn = obj.StartDate.AddMinutes(2); 
                    ///nrdl.DateCompleted =   // data fine scadenza

                    nrdl.RemindIn = TimeSpan.Zero;//TimeSpan.FromMinutes(addTempo);
                    nrdl.Status = 1; //DevExpress.Persistent.Base.General.TaskStatus.NotStarted;
                    //obj.AlarmTime = DateTime.Now.Add(TimeSpan.FromMinutes(addTempo));  obj.AlarmTime = obj.StartDate - obj.RemindIn.Value;   AlarmTime    obj.StartDate - obj.RemindIn.Value;
                    ((DevExpress.Persistent.Base.General.ISupportNotifications)nrdl)
                    .AlarmTime = DateTime.Now.AddMinutes(addTempo); // tempo in cui scatta l'allarme di avviso popup

                    if (nrdl.Resources.Count == 0 && resource != null)
                    {
                        nrdl.Resources.Add(resource);
                    }
                    else if (nrdl.Resources.Count > 0 && resource != null)
                    {
                        int i = 0;
                        while (nrdl.Resources.Count > 0)
                        {
                            nrdl.Resources.Remove(nrdl.Resources[0]);
                            i++;
                        }
                        nrdl.Resources.Add(resource);
                        nrdl.UpdateResourceIds();

                    }

                    nrdl.StatusNotifica = TaskStatus.NotStarted;
                    //// dati rdl 
                    if (nrdl.StartOn < DateTime.Now.AddMinutes(1))
                        nrdl.StartOn = DateTime.Now.AddMinutes(1);/// rdl.DataPianificata;//   data pianificata di inizio lavoro
                    int minutidiintervento = 60;
                    //if (rdl.Problema != null)
                    //    minutidiintervento = Convert.ToInt32(rdl.Problema.Problemi.Valore);
                    if (rdl.Prob != null)
                        minutidiintervento = Convert.ToInt32(rdl.Prob.Valore);

                    nrdl.EndOn = nrdl.StartOn.AddMinutes(minutidiintervento);// data pianificata di fine lavoro
                    if (rdl.DataPianificata != nrdl.StartOn)
                        nrdl.MessaggioUtente = string.Format("La Data di Pianificazione ({0}) della RdL({1}) verrà Aggiornata con la nuova data ({2})", rdl.DataPianificata, rdl.Oid, nrdl.StartOn);

                    //sbMessaggio.AppendLine("Richiesta intervento Sezionata:");
                    //sbMessaggio.AppendLine("nr.:" + rdl.Oid.ToString());
                    //sbMessaggio.AppendLine(string.Format("La Data di Pianificazione ({0}) della RdL({1}) verrà Aggiornata con la nuova data ({2})", rdl.DataPianificata, rdl.Oid, nrdl.StartOn));

                    sbMessaggio.Add("Richiesta intervento Sezionata:");
                    sbMessaggio.Add("nr.:" + rdl.Oid.ToString());
                    sbMessaggio.Add(string.Format("La Data di Pianificazione ({0}) della RdL({1}) verrà Aggiornata con la nuova data ({2})", rdl.DataPianificata, rdl.Oid, nrdl.StartOn));

                }
                catch (Exception e)
                {
                    sbMessaggio.Add("Errore");
                    sbMessaggio.Add("The process failed: {0}" + e.ToString());
                    //Console.WriteLine("The process failed: {0}", e.ToString());
                }

            }

            return nrdl;
        }

        public int SetaddTempo(int OidSAtorizzativoNuovo, CAMS.Module.DBTask.RdL rdl)
        {
            string strTempo = rdl.Asset.Servizio.Immobile.Contratti.TempoLivelloAutorizzazioneGuasto;
            int addTempo = 5;
            if (strTempo != null)
            {
                string[] splitTo = strTempo.Split(new Char[] { ';' });
                try
                {
                    addTempo = Convert.ToInt32(splitTo[OidSAtorizzativoNuovo]);//   addTempo = Convert.ToInt32(splitTo[0]);
                }
                catch
                {
                    addTempo = 5;
                }
            }

            return addTempo;
        }


        //public StringBuilder SetNotificaRdL_Changes( 
        //   ref  CAMS.Module.DBAgenda.NotificaRdL nrdl, CAMS.Module.DBTask.RdL rdl, string location,
        //    int addTempo, CAMS.Module.DBTask.StatoAutorizzativo SAutorizzazione, DateTime DataDichiaratadiArrivo, string tipoCambio)//  ,      ref StringBuilder sbMessaggio)          
        //{
        //    /*//      imposta il nuovo tepo di allarme per il popap    
        //           //  statoAutorizzativo = 0 [quando si crea la RdL] 
        //            //  statoAutorizzativo = 1 [quando si crea la notifica] 
        //            //  statoAutorizzativo = 2 [quando dichiara il tecnico]         **@@
        //            //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]   
        //            //  statoAutorizzativo = 4 [quando in trasferimento il tecnico] **@@   
        //       "DA"   ASO        TR      * */
        //    System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
        //    if (nrdl != null)
        //    {
        //        try
        //        {
        //            int OidAutorizzaNuovo = 0;
        //            switch (SAutorizzazione.Oid)
        //            {
        //                case 1:   // il TECNICO HA DICHIARATO ORARIO DI ARRIVO   
        //                    nrdl.StatusNotifica = TaskStatus.InProgress;
        //                    DataDichiaratadiArrivo = DateTime.Now.AddMinutes(125);
        //                    OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1; //xpObjectSpace.GetObjectByKey<CAMS.Module.DBTask.StatoAutorizzativo>(2);                            
        //                    addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;
        //                    // persintalias
        //                    nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
        //                    //nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
        //                    //nrdl.LabelListView = (LabelListView)nrdl.Label;
        //                    //  imposto le date di avviso
        //                    nrdl.StartDate = DateTime.Now.AddMinutes(2); // DATA DI INIZIO TIMER DI ALLARME
        //                    nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);// DATA SCADENZA ALLARME 
        //                    ((ISupportNotifications)nrdl).AlarmTime = nrdl.StartDate.AddMinutes(addTempo);
        //                    nrdl.MessaggioUtente = string.Format("il Tecnico ha Dichiarato la seguente data di arrivo {0}, " +
        //                        "\r\n  Accettare per confermare e aggiornare l'attuale data pianificata({1}) .", DataDichiaratadiArrivo, rdl.DataPianificata);

        //                    //   idsa = (rdl.StatoAutorizzativo.Oid + 1) > xpObjectSpace.GetObjects<StatoAutorizzativo>().Max(a => a.Oid) ? idsa = 1 : (rdl.StatoAutorizzativo.Oid + 1);                            
        //                    break;
        //                case 2:  // la SO ha ACCETTATO L'ORARIO DEL TECNICO
        //                    nrdl.StatusNotifica = TaskStatus.InProgress;
        //                    OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1; //xpObjectSpace.GetObjectByKey<CAMS.Module.DBTask.StatoAutorizzativo>(2);                            
        //                    addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;
        //                    // persintalias
        //                    nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
        //                    //nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
        //                    //nrdl.LabelListView = (LabelListView)nrdl.Label;

        //                    nrdl.MessaggioUtente = string.Format("Autorizzando!, La data di Arrivo dichiarata dal tecnico({0}) aggiornerà la precedente data data pianificata({1})",
        //                        rdl.DataPrevistoArrivo, rdl.DataPianificata);
        //                    //  imposto le date di avviso                
        //                    nrdl.StartDate = DateTime.Now.AddMinutes(2); // DATA DI INIZIO TIMER DI ALLARME
        //                    nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);// DATA SCADENZA ALLARME 
        //                    ((ISupportNotifications)nrdl).AlarmTime = nrdl.StartDate.AddMinutes(addTempo);
        //                //DataPrevistoArrivoDichTecnico.AddMinutes(addTempo);
        //                    // la data di allarme deve essere la data di arrivo prevista dal tecnico meno il tempo di avviso su commessa  
        //                    DateTime DataPrevistoArrivoDichTecnico = rdl.DataPrevistoArrivo;
        //                    //inposto la DataArrivoDichiarata come datapianificata
        //                    if (nrdl.EndOn != null && nrdl.StartOn != null)
        //                    {
        //                        int minute = Math.Abs((nrdl.EndOn - nrdl.StartOn).Minutes);
        //                        nrdl.StartOn = DataPrevistoArrivoDichTecnico;
        //                        if (minute < 15)
        //                            minute = 15;
        //                        nrdl.EndOn = nrdl.StartOn.AddMinutes(minute);
        //                    }
        //                    break;
        //                case 3:  // IL TECNICO HA PRESO IN CARICO L'INTERVENTO (IN TRASFERIMENTO)
        //                    OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1; //xpObjectSpace.GetObjectByKey<CAMS.Module.DBTask.StatoAutorizzativo>(2);                            
        //                    addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;
        //                    // persintalias
        //                    nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
        //                    //nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
        //                    //nrdl.LabelListView = (LabelListView)nrdl.Label;

        //                    nrdl.MessaggioUtente = string.Format("Il Tecnico ha Iniziato il Trasferimento alla data ({0})", DateTime.Now);
        //                    //  imposto le date di avviso

        //                    nrdl.StatusNotifica = TaskStatus.Completed;
        //                    ((ISupportNotifications)nrdl).AlarmTime = null;

        //                    break;

        //                default:// altro non pervenuto
        //                    ((ISupportNotifications)nrdl).AlarmTime = null;
        //                    break;
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            sbMessaggio.AppendLine("Errore");
        //            sbMessaggio.AppendLine("The process failed: {0}" + e.ToString());
        //            Console.WriteLine("The process failed: {0}", e.ToString());
        //        }
        //        sbMessaggio.AppendLine("Richiesta intervento Sezionata:");
        //        sbMessaggio.AppendLine("nr.:" + rdl.Oid.ToString());
        //    }

        //    return sbMessaggio;
        //}


        public int SetLabelNotifica(CAMS.Module.DBTask.RdL RdL,
            CAMS.Module.DBAgenda.NotificaRdL nrdl,
            int oidAutorizzativoNuovo)  //  CAMS.Module.DBTask.StatoAutorizzativo sa)
        {
            /*//      imposta il nuovo tepo di allarme per il popap    
                   //  statoAutorizzativo = 0 [quando si crea la RdL] 
                    //  statoAutorizzativo = 1 [quando si crea la notifica] 
                    //  statoAutorizzativo = 2 [quando dichiara il tecnico]         **@@
                    //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]   
                    //  statoAutorizzativo = 4 [quando in trasferimento il tecnico] **@@   
               * */
            if (RdL != null) //Evaluate("RdL") != null)
            {
                int oidSmistamento = RdL.UltimoStatoSmistamento != null ? RdL.UltimoStatoSmistamento.Oid : 0;
                int oidOperativo = RdL.UltimoStatoOperativo != null ? RdL.UltimoStatoOperativo.Oid : 0;
                //int oidAutorizzativo = sa != null ? sa.Oid : 0;
                if (oidSmistamento == 1)
                    return 0;

                if (oidSmistamento == 2) // in attesa di assegnazione
                {
                    switch (oidAutorizzativoNuovo)
                    {
                        case 1: // trasferimento = 4                   
                            return 1;
                            break;
                        case 2:     //  in sito = 5
                            return 2;
                            break;
                        case 3:     //  sospeso =              
                            return 3;
                            break;
                        case 4:     //  sospeso =              
                            return 4;
                            break;
                        default:// altro non pervenuto
                            return 0;
                            break;
                    }
                }
                if (oidSmistamento == 3) // in lavorazione
                {

                    switch (oidOperativo)
                    {
                        case 1: // trasferimento = 4
                        case 3:
                        case 4:
                        case 5:
                            return 4;
                            break;
                        case 2:     //  in sito = 5
                            return 5;
                            break;
                        case 6:     //  sospeso = 
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            if (nrdl.DateCompleted == null)
                                return 6;
                            if (nrdl.DateCompleted != null)
                                return 8;
                            break;
                        default:// altro non pervenuto
                            return 0;
                            break;
                    }
                }

                if (oidSmistamento == 4) // in chiso
                {
                    return 7;
                }
                if (oidSmistamento == 10) // in chiso
                {
                    return 9;
                }
            }
            else
            {
                return 0;
            }

            return 0;
        }


    }
}
