using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.DBAux;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using CAMS.Module.DBTask.ParametriPopUp;
namespace CAMS.Module.Classi
{
    class PopUpAvavanzamentoSmistamentoRdLExecute
    {
        public static bool SetpAvSmistamentoRdLExecute( int OidRdL, ModificaRdL ModificaRdL, IObjectSpace xpObjectSpace, ref string Messaggio)
        {
            int OidRegolaAssegnazionexTRisorse = 0;          
            bool SpediscieMail = false;
            if (xpObjectSpace is XPObjectSpace && xpObjectSpace != null)
            {    
                RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
                if (rdl != null)
                {
                    try
                    {
                        if (rdl.UltimoStatoSmistamento.Oid != ModificaRdL.UltimoStatoSmistamento.Oid)
                        {
                            rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(ModificaRdL.UltimoStatoSmistamento.Oid);
                            rdl.UltimoStatoOperativo = xpObjectSpace.GetObject<StatoOperativo>(ModificaRdL.UltimoStatoOperativo);
                            SpediscieMail = true;
                        }

                        if (rdl.DataPianificata != ModificaRdL.DataPianificata)
                        {
                            rdl.DataPianificata = ModificaRdL.DataPianificata;
                            DateTime fine = ModificaRdL.DataPianificata.AddMinutes(ModificaRdL.StimatempoLavoro);
                            rdl.DataPianificataEnd = fine;
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid == 2 || ModificaRdL.UltimoStatoSmistamento.Oid == 11)
                        {
                            rdl.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(ModificaRdL.RisorseTeam);
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid == 4)
                        {
                            rdl.DataCompletamento = ModificaRdL.DataCompletamento;
                            rdl.NoteCompletamento = ModificaRdL.NoteCompletamento;
                            SpediscieMail = true;
                        }

                        rdl.DataAggiornamento = DateTime.Now;

                        //   RegistroRdL
                        if (rdl.RegistroRdL != null)
                        {
                            if (rdl.RegistroRdL.DataPianificata != rdl.DataPianificata)
                                rdl.RegistroRdL.DataPianificata = rdl.DataPianificata;

                            if (rdl.RegistroRdL.RisorseTeam != rdl.RisorseTeam)
                                rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;

                            if (rdl.RegistroRdL.UltimoStatoSmistamento != rdl.UltimoStatoSmistamento)
                            {
                                if (rdl.RegistroRdL.RdLes.Count == 1)
                                {
                                    rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                    if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                        rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;

                                    rdl.RegistroRdL.DataCompletamento = rdl.DataCompletamento;
                                    rdl.RegistroRdL.NoteCompletamento = rdl.NoteCompletamento;

                                }
                                else
                                {
                                    int nrRdL = rdl.RegistroRdL.RdLes.Count;
                                    int tutte_chiuse = rdl.RegistroRdL.RdLes.Where(w => w.UltimoStatoSmistamento == rdl.UltimoStatoSmistamento).Count();
                                    if (nrRdL == tutte_chiuse)
                                    {
                                        rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento; // chiudi intero registro


                                        if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                            rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                                    }
                                }

                            }
                            if (rdl.RegistroRdL.DataPrevistoArrivo != rdl.DataPrevistoArrivo)
                                rdl.RegistroRdL.DataPrevistoArrivo = rdl.DataPrevistoArrivo;

                            rdl.RegistroRdL.DataAggiornamento = rdl.DataAggiornamento;
                        }

                        rdl.Save();
                        xpObjectSpace.CommitChanges();
                    }
                    catch (Exception ex)
                    {
                        string Titolo = "Aggiornamento non Eseguito!!";
                        Messaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                        //SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Warning);
                    }

                   
                }
            }

            return SpediscieMail;
        }

    }
}
