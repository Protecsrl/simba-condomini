using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask.DC
{
    [DomainComponent]
    [VisibleInReportsAttribute]
    public class DCRdLListReport
    {
        [Browsable(false)]  // Hide the entity identifier from UI.
        [DevExpress.ExpressApp.Data.Key]
        public int ID { get; set; }

        //[XafDisplayName("Codice")]     // non server
        //public string codice { get; set; }

        [XafDisplayName("Codice RdL")]
        public int codiceRdL { get; set; }

        [XafDisplayName("Codice RegRdL")]
        public int CodRegRdL { get; set; }

        [XafDisplayName("Descrizione")]
        public string Descrizione { get; set; }

        [XafDisplayName("RegRdL Descrizione")]
        public string RegRdLDescrizione { get; set; }

        [XafDisplayName("Immobile")]
        public string Immobile { get; set; }

        [XafDisplayName("Codedificio")]
        public string Codedificio { get; set; }

        //[XafDisplayName("AreadiPolo")]
        //public string AreaDiPolo { get; set; }

        [XafDisplayName("Centro di Costo")]
        public string CentroCosto { get; set; }

        [XafDisplayName("Impianto")]
        public string Impianto { get; set; }

        [XafDisplayName("Apparato")]
        public string Apparato { get; set; }

        [XafDisplayName("Tipo Apparato")]
        public string TipoApparato { get; set; }

        [XafDisplayName("Problema")]
        public string Problema { get; set; }

        [XafDisplayName("Causa")]
        public string Causa { get; set; }

        [XafDisplayName("Rimedio")]
        public string Rimedio { get; set; }

        [XafDisplayName("Categoria Manutenzione")]
        public string CategoriaManutenzione { get; set; }

        [XafDisplayName("Utente")]
        public string Utente { get; set; }

        [XafDisplayName("Data Creazione")]
        public string DataCreazione { get; set; } //DateTime

        [XafDisplayName("Data Richiesta")]
        public string DataRichiesta { get; set; }

        [XafDisplayName("Data Aggiornamento")]
        public string DataAggiornamento { get; set; }

        [XafDisplayName("Data Pianificata")]
        public string DataPianificata { get; set; }

        [XafDisplayName("Data Assegnazione")]
        public string DataAssegnazione { get; set; }

        [XafDisplayName("Data Completamento")]
        public string DataCompletamento { get; set; }

        [XafDisplayName("Settimana")]
        public int Settimana { get; set; }

        [XafDisplayName("Mese")]
        public int Mese { get; set; }

        [XafDisplayName("Anno")]
        public int Anno { get; set; }

        [XafDisplayName("Team")]
        public string Team { get; set; }

        [XafDisplayName("Mansione Team")]
        public string TeamMansione { get; set; }

        [XafDisplayName("Stato Smistamento")]
        public string StatoSmistamento { get; set; }

        [XafDisplayName("Stato Operativo")]
        public string StatoOperativo { get; set; }

        [XafDisplayName("Note Completamento")]
        public string NoteCompletamento { get; set; }

        [XafDisplayName("Richiedente")]
        public string Richiedente { get; set; }

        [XafDisplayName("Priorita")]
        public string Priorita { get; set; }

        [XafDisplayName("Priorita Intervento")]
        public string PrioritaIntervento { get; set; }

        [XafDisplayName("Referente Amministrativo")]
        public string RefAmministrativo { get; set; }

        [XafDisplayName("SchedaMp")]
        public string CodSchedeMp { get; set; }

        [XafDisplayName("SchedaMp Uni")]
        public string CodSchedaMpUni { get; set; }

        [XafDisplayName("Descrizione Manutenzione")]
        public string DescrizioneManutenzione { get; set; }

        [XafDisplayName("Frequenza")]
        public string FrequenzaDescrizione { get; set; }

        [XafDisplayName("Codice Frequenza")]
        public string FrequenzaCod_Descrizione { get; set; }

        [XafDisplayName("Passo Procedura")]
        public string PassoSchedaMp { get; set; }

        [XafDisplayName("Nr Ordine")]
        public int NrOrdine { get; set; }

        [XafDisplayName("Apparato Padre")]
        public string ApparatoPadre { get; set; }

        [XafDisplayName("Apparato Sostegno")]
        public string ApparatoSostegno { get; set; }

        [XafDisplayName("Componenti Manutenzione")]
        [Size(SizeAttribute.Unlimited)]
        [DbType("CLOB")]
        public string ComponentiManutenzione { get; set; }

        [XafDisplayName("Annotazioni")]
        [Size(SizeAttribute.Unlimited)]
        [DbType("CLOB")]
        public string Annotazioni { get; set; }

        [XafDisplayName("Corpo MP")]
        public string CorpoMP { get; set; }

        [XafDisplayName("Ordine Passo")]
        public string OrdinePasso { get; set; }

        //[XafDisplayName("Componenti Sostegno")]
        //public string ComponentiSostegno { get; set; }

        [XafDisplayName("OidCategoria")]
        public int OidCategoria { get; set; }

        public int ApparatoId { get; set; }
        public int ApparatoSostegnoId { get; set; }
        public int Categoria { get; set; }
        public DateTime DatePian { get; set; }
        public int schedaMpOid { get; set; }

        //[XafDisplayName("OidCategoria")]
        //public int OidCategoria { get; set; }


        //DATACREAZIONE	13/11/2017 10:52:10	13/11/2017 10:50:08
        //DATARICHIESTA	13/11/2017 10:52:10	13/11/2017 10:50:08
        //DATAAGGIORNAMENTO	13/11/2017 11:30:34	13/11/2017 11:29:49
        //DATAPIANIFICATA	13/11/2017 15:00:00	13/11/2017 12:40:00
        //DATAASSEGNAZIONE	13/11/2017 11:30:29	13/11/2017 11:29:39
        //DATACOMPLETAMENTO		
        //SETTIMANA	46	46
        //MESE	11	11
        //ANNO	2017	2017

        //TEAM	Bari Manutentore(2017)	Bari Manutentore(2017)
        //TEAMMANSIONE	Manutentore	Manutentore
        //STATOSMISTAMENTO	Assegnata	Assegnata
        //STATOOPERATIVO	Assegnata-Da prendere in carico	Assegnata-Da prendere in carico
        //NOTECOMPLETAMENTO		
        //RICHIEDENTE	Manutentore(Manutentore)	Manutentore(Manutentore)
        //PRIORITA	Bassa	Bassa
        //PRIORITATIPOINTERVENTO	Programmato	Programmato
        //REFAMMINISTRATIVO	RUP Contratto	RUP Contratto
        //CODICEODL	18580	18579
        //CODREGRDL	25813	25812
        //OIDEDIFICIO	3980	3980
        //OIDREFERENTECOFELY	262	262
        //OIDCATEGORIA	2	5
        //OIDSMISTAMENTO	2	2
        //CODSCHEDEMP		
        //CODSCHEDAMPUNI		IL.01.01.01.01.02
        //DESCRIZIONEMANUTENZIONE		 Verifica funzionale ed eventuale sostituzione  
        //FREQUENZADESCRIZIONE		Annuale
        //FREQUENZACOD_DESCRIZIONE		A
        //PASSOSCHEDAMP		Lampada: Verifica funzionale ed eventuale sostituzione  
        //NORDINE	0	0
        //INSOURCING	0	

        //APPARATOPADRE	Quadro di distribuzione(PAL_QE004) Piazza Aldo Moro, 1(PAL_QE004)	Quadro di distribuzione(PAL_QE004) Piazza Aldo Moro, 1(PAL_QE004)
        //APPARATOSOSTEGNO	Palo (PAL_S01337) Strada Provinciale 31, 109(PAL_S01337)	Palo (PAL_S01335) Strada Provinciale 31, 91(PAL_S01335)


        public DCRdLListReport Clone(DCRdLListReport obj)
        {
            return new DCRdLListReport()
            {
                RefAmministrativo = obj.RefAmministrativo,
                Anno = obj.Anno,
                Annotazioni = obj.Annotazioni,
                Apparato = obj.Apparato,
                ApparatoId = obj.ApparatoId,
                ApparatoPadre = obj.ApparatoPadre,
                ApparatoSostegno = obj.RefAmministrativo,
                ApparatoSostegnoId = obj.ApparatoSostegnoId,
                Categoria = obj.Categoria,
                CategoriaManutenzione = obj.CategoriaManutenzione,
                Causa = obj.Causa,
                CentroCosto = obj.CentroCosto,
                Codedificio = obj.Codedificio,
                codiceRdL = obj.codiceRdL,
                CodRegRdL = obj.CodRegRdL,
                CodSchedaMpUni = obj.CodSchedaMpUni,
                CodSchedeMp = obj.CodSchedeMp,
                ComponentiManutenzione = obj.ComponentiManutenzione,
                CorpoMP = obj.CorpoMP,
                DataAggiornamento = obj.DataAggiornamento,
                DataAssegnazione = obj.DataAssegnazione,
                DataCompletamento = obj.DataCompletamento,
                DataCreazione = obj.DataCreazione,
                DataPianificata = obj.DataPianificata,
                DataRichiesta = obj.DataRichiesta,
                DatePian = obj.DatePian,
                Descrizione = obj.Descrizione,
                DescrizioneManutenzione = obj.DescrizioneManutenzione,
                Immobile = obj.Immobile,
                FrequenzaCod_Descrizione = obj.FrequenzaCod_Descrizione,
                FrequenzaDescrizione = obj.FrequenzaDescrizione,
                ID = obj.ID,
                Impianto = obj.Impianto,
                Mese = obj.Mese,
                NoteCompletamento = obj.NoteCompletamento,
                NrOrdine = obj.NrOrdine,
                OidCategoria = obj.OidCategoria,
                OrdinePasso = obj.OrdinePasso,
                PassoSchedaMp = obj.PassoSchedaMp,
                Priorita = obj.Priorita,
                PrioritaIntervento = obj.PrioritaIntervento,
                Problema = obj.Problema,
                RegRdLDescrizione = obj.RegRdLDescrizione,
                Richiedente = obj.Richiedente,
                Rimedio = obj.Rimedio,
                schedaMpOid = obj.schedaMpOid,
                Settimana = obj.Settimana,
                StatoOperativo = obj.StatoOperativo,
                StatoSmistamento = obj.StatoSmistamento,
                Team = obj.Team,
                TeamMansione = obj.TeamMansione,
                TipoApparato = obj.TipoApparato,
                Utente = obj.Utente


            };
        }
        public DCRdLListReport Clone(int ID, int codiceRdL, int CodRegRdL, string Descrizione, string RegRdLDescrizione
            , string Immobile, string Codedificio, string CentroCosto, string Impianto, string Apparato, string TipoApparato
            , string Problema, string Causa, string Rimedio, string CategoriaManutenzione, string Utente, string DataCreazione
            , string DataRichiesta, string DataAggiornamento, string DataPianificata, string DataAssegnazione, string DataCompletamento
            , int Settimana, int Mese, int Anno, string Team, string TeamMansione, string StatoSmistamento, string StatoOperativo
            , string NoteCompletamento, string Richiedente, string Priorita, string PrioritaIntervento, string RefAmministrativo
            , string CodSchedeMp, string CodSchedaMpUni, string DescrizioneManutenzione, string FrequenzaDescrizione, string FrequenzaCod_Descrizione
            , string PassoSchedaMp, int NrOrdine, string ApparatoPadre, string ApparatoSostegno, string ComponentiManutenzione
            , string Annotazioni, string CorpoMP, string OrdinePasso, int OidCategoria, int ApparatoId, int ApparatoSostegnoId
            , int Categoria, DateTime DatePian, int schedaMpOid
           )
        {
            return new DCRdLListReport()
            {
                RefAmministrativo = RefAmministrativo,
                Anno = Anno,
                Annotazioni = Annotazioni,
                Apparato = Apparato,
                ApparatoId = ApparatoId,
                ApparatoPadre = ApparatoPadre,
                ApparatoSostegno = RefAmministrativo,
                ApparatoSostegnoId = ApparatoSostegnoId,
                Categoria = Categoria,
                CategoriaManutenzione = CategoriaManutenzione,
                Causa = Causa,
                CentroCosto = CentroCosto,
                Codedificio = Codedificio,
                codiceRdL = codiceRdL,
                CodRegRdL = CodRegRdL,
                CodSchedaMpUni = CodSchedaMpUni,
                CodSchedeMp = CodSchedeMp,
                ComponentiManutenzione = ComponentiManutenzione,
                CorpoMP = CorpoMP,
                DataAggiornamento = DataAggiornamento,
                DataAssegnazione = DataAssegnazione,
                DataCompletamento = DataCompletamento,
                DataCreazione = DataCreazione,
                DataPianificata = DataPianificata,
                DataRichiesta = DataRichiesta,
                DatePian = DatePian,
                Descrizione = Descrizione,
                DescrizioneManutenzione = DescrizioneManutenzione,
                Immobile = Immobile,
                FrequenzaCod_Descrizione = FrequenzaCod_Descrizione,
                FrequenzaDescrizione = FrequenzaDescrizione,
                ID = ID,
                Impianto = Impianto,
                Mese = Mese,
                NoteCompletamento = NoteCompletamento,
                NrOrdine = NrOrdine,
                OidCategoria = OidCategoria,
                OrdinePasso = OrdinePasso,
                PassoSchedaMp = PassoSchedaMp,
                Priorita = Priorita,
                PrioritaIntervento = PrioritaIntervento,
                Problema = Problema,
                RegRdLDescrizione = RegRdLDescrizione,
                Richiedente = Richiedente,
                Rimedio = Rimedio,
                schedaMpOid = schedaMpOid,
                Settimana = Settimana,
                StatoOperativo = StatoOperativo,
                StatoSmistamento = StatoSmistamento,
                Team = Team,
                TeamMansione = TeamMansione,
                TipoApparato = TipoApparato,
                Utente = Utente


            };

        }



    }
}