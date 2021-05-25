using CAMS.Module.DBAux;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.Classi
{
    class GetOidRegolaAssegnazione
    {


        public static int getOidRegolaAssegnazionexTRisorse(RdL rdl, IObjectSpace ObjectSpace)
        {
            int OidRegolaAssegnazionexTRisorse = 0;
            bool isGranted = SecuritySystem.IsGranted(new
               DevExpress.ExpressApp.Security.PermissionRequest(ObjectSpace, typeof(RegoleAutoAssegnazioneRdL),
             DevExpress.ExpressApp.Security.SecurityOperations.Read));
            /////////////////////////////////////////////////////////////////////////////////////////    x smartphone
            if (isGranted && rdl.Immobile.Contratti.AttivaAutoAssegnazioneTeam && rdl.UltimoStatoSmistamento.Oid == 2 && rdl.Servizio != null)//  solo se è appena creata
            {
                //  ASSEGNA AUTOMATICAMENTE
                //IObjectSpace xpObjectSpace = Application.CreateObjectSpace();//  -------------------------   
                //int OidEdificio = rdl.Immobile.Oid;
                //int OidImpianto = rdl.Immobile.Impianti.Oid;
             //if(rdl.UltimoStatoSmistamento.Oid == 2)
             //   TipoAssegnazione tipoAssegnazione =  new  TipoAssegnazione.SM;
                int OidEdificio = 0; int OidImpianto = 0; int OidCategoria = 0; 

                if(rdl.Immobile!=null)
                    OidEdificio = rdl.Immobile.Oid;
                if (rdl.Servizio != null)
                    OidImpianto = rdl.Servizio.Oid;
                if (rdl.Categoria != null)
                    OidCategoria = rdl.Categoria.Oid;

                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                XPQuery<RegoleAutoAssegnazioneRdL> RegoleQuery = new XPQuery<RegoleAutoAssegnazioneRdL>(Sess);
                var query = RegoleQuery.Where(w => w.Immobile.Oid == OidEdificio && w.Servizio.Oid == OidImpianto)
                                       .Where(w => w.Categoria.Oid == OidCategoria)
                                       .Where(w => w.CalendarioCadenze != null)
                                       .Where(w => w.TipoRegola == TipoRegola.RegolaAutomatismiAssegnazione) // aggiunto per regola di automazione
                                         .Where(w => w.TipoAssegnazione == TipoAssegnazione.SM)     //**************************************************************************************
                                       .Where(w => w.TipoAssegnazione == TipoAssegnazione.SO || w.TipoAssegnazione == TipoAssegnazione.SM || w.TipoAssegnazione == TipoAssegnazione.SME)
                                       .Select(s => new { OidRegoleAutoAssegnazioneRdL = s.Oid, s.TipoAssegnazione, s.CalendarioCadenze, s.RisorseTeam, s.FesteNazionali, s.RegoleAutoAssegnazioneRisorseTeams, s.AggiungiRisorsaVicina }).ToList();


                DateTime data = DateTime.Now;
                bool mesetrovato = false;
                bool giornotrovato = false;
                bool OrarioCompreso = false;
                bool createAotoregolaassegnazione = false;
                bool SpedisciMessaggioAssegnazione = false;
                TipoAssegnazione vTipoAssegnazione = Classi.TipoAssegnazione.ND;
                AggiungiRisorsaVicina vTipoStrategiaAssegnazione = Classi.AggiungiRisorsaVicina.ND;
                RisorseTeam vRisorseTeam = null;
                bool vFesteNazionali = false;
                int OidRegoleAutoAssegnazione = 0;
                List<RisorseTeam> listRisorseTeams = new List<RisorseTeam>();
                string MessaggioRegola = string.Empty;
                foreach (var dr in query)
                {
//                    listRisorseTeams = dr.RegoleAutoAssegnazioneRisorseTeams.Select(s => s.RisorseTeam).ToList();
                    OidRegoleAutoAssegnazione = dr.OidRegoleAutoAssegnazioneRdL;
                    listRisorseTeams.Add(dr.RisorseTeam); // aggiunge quella di default
                    vRisorseTeam = dr.RisorseTeam;      // assegna  quella di default per caso di so e sm
                    mesetrovato = false;
                    giornotrovato = false;
                    OrarioCompreso = false;
                    createAotoregolaassegnazione = false;
                    vTipoAssegnazione = dr.TipoAssegnazione;
                    vTipoStrategiaAssegnazione = dr.AggiungiRisorsaVicina;
                    //vAggiungiRisorsaVicina = dr.AggiungiRisorsaVicina;                    
                    vFesteNazionali = dr.FesteNazionali;

                    foreach (var cc in dr.CalendarioCadenze.CalendarioCadenzeDettaglioS)
                    {
                        if (vFesteNazionali) /// anche se è festa nazionale
                        {
                            createAotoregolaassegnazione = FestivitaNazionale();
                            if (createAotoregolaassegnazione)
                                break;
                        }
                        foreach (var mm in cc.GruppoMesi.Mesi)
                        {
                            if (mm.Mesi.Mese == data.Month)
                            {
                                mesetrovato = true;
                                if (mesetrovato)
                                    break;
                            }
                        }
                        if (mesetrovato)
                        {
                            int nowDayOfWeek = (int)DateTime.Now.DayOfWeek;	//5	= ven
                            int tipoGGSett = (int)cc.TipoGiornoSettimana;
                            if (nowDayOfWeek == tipoGGSett) //data
                            {
                                giornotrovato = true;
                                OrarioCompreso = VerificaOrario(cc.TimeDalle, cc.TimeAlle);
                                if (OrarioCompreso)
                                {
                                    OidRegolaAssegnazionexTRisorse = OidRegoleAutoAssegnazione;
                                    createAotoregolaassegnazione = true;
                                    if (createAotoregolaassegnazione)
                                        break;
                                }
                            }
                        }
                    }
                    if (createAotoregolaassegnazione)//allora assegno al tecnico
                        break;
                }



            }

            return OidRegolaAssegnazionexTRisorse;
        }

        public static bool FestivitaNazionale()  // int anno, int mese, int giorno)
        {
            DateTime adesso = DateTime.Now;
            int anno = adesso.Year;
            int mese = adesso.Month;
            int giorno = adesso.Day;
            DateTime dt;
            try
            {
                dt = new DateTime(anno, mese, giorno);
                //if (DayOfWeek.Saturday.Equals(dt.DayOfWeek))
                //    return true;

                //if (DayOfWeek.Sunday.Equals(dt.DayOfWeek))
                //    return true;

                /*capodanno*/
                if (giorno == 1 && mese == 1)
                    return true;

                /*6 gennaio epifania*/
                if (giorno == 6 && mese == 1)
                    return true;

                /*25 aprile*/
                if (giorno == 25 && mese == 4)
                    return true;

                /*1 maggio*/
                if (giorno == 1 && mese == 5)
                    return true;

                /*29 giugno s.pietro e paolo*/
                if (giorno == 29 && mese == 6)
                    return true;

                /*15 agosto*/
                if (giorno == 15 && mese == 8)
                    return true;

                /*2 giugno*/
                if (giorno == 2 && mese == 6)
                    return true;

                /*2 novembre*/
                if (giorno == 2 && mese == 11)
                    return true;

                /*8 dicembre*/
                if (giorno == 8 && mese == 12)
                    return true;

                /*natale*/
                if (giorno == 25 && mese == 12)
                    return true;

                /*s stefano*/
                if (giorno == 26 && mese == 12)
                    return true;
                // pasqua
                DateTime pasquadey = DateTime.MinValue;
                if (Module.Classi.EasterCalculation.GetEasterDate(anno) != null)
                    pasquadey = (DateTime)Module.Classi.EasterCalculation.GetEasterDate(anno);

                if (dt == pasquadey || dt == pasquadey.AddDays(1)) // pasqua
                    return true;

            }
            catch (Exception ex)
            {
                //log.Error(ex);
                return false;
            }

            return false;
        }

        private static bool VerificaOrario(TimeSpan t_left, TimeSpan t_right) //(String strleft, String strright)
        {
            DateTime d = DateTime.Now;
            TimeSpan t_center = d.TimeOfDay;
            //TimeSpan t_left = TimeSpan.Parse(strleft);
            //TimeSpan t_right = TimeSpan.Parse(strright);
            bool l = t_center >= t_left;
            bool r = t_center <= t_right;
            bool v = l && r;
            return v;
        }

    }
}
