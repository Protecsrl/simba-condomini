using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
#pragma warning disable CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using System.Threading.Tasks;
#pragma warning restore CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi

//namespace CAMS.Module.DBMisure
//{
//    class vw_EAMS_Anagrafica_IP
//    {
//    }
//}
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

//namespace CAMS.Module.DBMisure
//{
//    class vw_EAMS_DettaglioFatture_IP
//    {
//    }
//}
namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions,
    Persistent("MIS_IST_DETTAGLIO")]
    [System.ComponentModel.DefaultProperty("Istrice Fatture Dettaglio")]
    [NavigationItem(false)]
    public class vw_EAMS_DettaglioFatture_IP : XPObject
    {
        public vw_EAMS_DettaglioFatture_IP()            : base()
        {
        }

        public vw_EAMS_DettaglioFatture_IP(Session session)            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(40),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione Valore")]
        [DbType("varchar(40)")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }
        ///   da qui
          int fID;
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fPOD;
        [Size(25)]
        public string POD
        {
            get { return fPOD; }
            set { SetPropertyValue<string>(nameof(POD), ref fPOD, value); }
        }
        string fINTESTATARIO;
        [Size(50)]
        public string INTESTATARIO
        {
            get { return fINTESTATARIO; }
            set { SetPropertyValue<string>(nameof(INTESTATARIO), ref fINTESTATARIO, value); }
        }
        string fAREA;
        [Size(25)]
        public string AREA
        {
            get { return fAREA; }
            set { SetPropertyValue<string>(nameof(AREA), ref fAREA, value); }
        }
        string fCONTRATTO;
        [Size(50)]
        public string CONTRATTO
        {
            get { return fCONTRATTO; }
            set { SetPropertyValue<string>(nameof(CONTRATTO), ref fCONTRATTO, value); }
        }
        string fSEDE_TECNICA;
        [Size(3)]
        public string SEDE_TECNICA
        {
            get { return fSEDE_TECNICA; }
            set { SetPropertyValue<string>(nameof(SEDE_TECNICA), ref fSEDE_TECNICA, value); }
        }
        string fTOPONIMO;
        [Size(50)]
        public string TOPONIMO
        {
            get { return fTOPONIMO; }
            set { SetPropertyValue<string>(nameof(TOPONIMO), ref fTOPONIMO, value); }
        }
        string fINDIRIZZO;
        [Size(50)]
        public string INDIRIZZO
        {
            get { return fINDIRIZZO; }
            set { SetPropertyValue<string>(nameof(INDIRIZZO), ref fINDIRIZZO, value); }
        }
        string fCIVICO;
        [Size(25)]
        public string CIVICO
        {
            get { return fCIVICO; }
            set { SetPropertyValue<string>(nameof(CIVICO), ref fCIVICO, value); }
        }
        string fCOMUNE;
        [Size(50)]
        public string COMUNE
        {
            get { return fCOMUNE; }
            set { SetPropertyValue<string>(nameof(COMUNE), ref fCOMUNE, value); }
        }
        string fPR;
        [Size(2)]
        public string PR
        {
            get { return fPR; }
            set { SetPropertyValue<string>(nameof(PR), ref fPR, value); }
        }
        string fREGIONE;
        [Size(30)]
        public string REGIONE
        {
            get { return fREGIONE; }
            set { SetPropertyValue<string>(nameof(REGIONE), ref fREGIONE, value); }
        }
        string fZONA;
        [Size(5)]
        public string ZONA
        {
            get { return fZONA; }
            set { SetPropertyValue<string>(nameof(ZONA), ref fZONA, value); }
        }
        string fCAP;
        [Size(5)]
        public string CAP
        {
            get { return fCAP; }
            set { SetPropertyValue<string>(nameof(CAP), ref fCAP, value); }
        }
        string fCLIENTE;
        [Size(125)]
        public string CLIENTE
        {
            get { return fCLIENTE; }
            set { SetPropertyValue<string>(nameof(CLIENTE), ref fCLIENTE, value); }
        }
        string fTIPO_UTILIZZO;
        [Size(25)]
        public string TIPO_UTILIZZO
        {
            get { return fTIPO_UTILIZZO; }
            set { SetPropertyValue<string>(nameof(TIPO_UTILIZZO), ref fTIPO_UTILIZZO, value); }
        }
        string fTIPO_TENSIONE_SM;
        [Size(10)]
        public string TIPO_TENSIONE_SM
        {
            get { return fTIPO_TENSIONE_SM; }
            set { SetPropertyValue<string>(nameof(TIPO_TENSIONE_SM), ref fTIPO_TENSIONE_SM, value); }
        }
        string fTIPO_TENSIONE;
        [Size(50)]
        public string TIPO_TENSIONE
        {
            get { return fTIPO_TENSIONE; }
            set { SetPropertyValue<string>(nameof(TIPO_TENSIONE), ref fTIPO_TENSIONE, value); }
        }
        double fLIVELLO_TENSIONE;
        public double LIVELLO_TENSIONE
        {
            get { return fLIVELLO_TENSIONE; }
            set { SetPropertyValue<double>(nameof(LIVELLO_TENSIONE), ref fLIVELLO_TENSIONE, value); }
        }
        double fCONSUMO_ANNUO;
        public double CONSUMO_ANNUO
        {
            get { return fCONSUMO_ANNUO; }
            set { SetPropertyValue<double>(nameof(CONSUMO_ANNUO), ref fCONSUMO_ANNUO, value); }
        }
        double fCONSUMO_ANNUO_F1;
        public double CONSUMO_ANNUO_F1
        {
            get { return fCONSUMO_ANNUO_F1; }
            set { SetPropertyValue<double>(nameof(CONSUMO_ANNUO_F1), ref fCONSUMO_ANNUO_F1, value); }
        }
        double fCONSUMO_ANNUO_F2;
        public double CONSUMO_ANNUO_F2
        {
            get { return fCONSUMO_ANNUO_F2; }
            set { SetPropertyValue<double>(nameof(CONSUMO_ANNUO_F2), ref fCONSUMO_ANNUO_F2, value); }
        }
        double fCONSUMO_ANNUO_F3;
        public double CONSUMO_ANNUO_F3
        {
            get { return fCONSUMO_ANNUO_F3; }
            set { SetPropertyValue<double>(nameof(CONSUMO_ANNUO_F3), ref fCONSUMO_ANNUO_F3, value); }
        }
        double fPOTENZA_IMPEGNATA;
        public double POTENZA_IMPEGNATA
        {
            get { return fPOTENZA_IMPEGNATA; }
            set { SetPropertyValue<double>(nameof(POTENZA_IMPEGNATA), ref fPOTENZA_IMPEGNATA, value); }
        }
        double fPOTENZA_DISPONIBILE;
        public double POTENZA_DISPONIBILE
        {
            get { return fPOTENZA_DISPONIBILE; }
            set { SetPropertyValue<double>(nameof(POTENZA_DISPONIBILE), ref fPOTENZA_DISPONIBILE, value); }
        }
        string fDISTRIBUTORE;
        [Size(255)]
        public string DISTRIBUTORE
        {
            get { return fDISTRIBUTORE; }
            set { SetPropertyValue<string>(nameof(DISTRIBUTORE), ref fDISTRIBUTORE, value); }
        }
        string fWBS;
        [Size(255)]
        public string WBS
        {
            get { return fWBS; }
            set { SetPropertyValue<string>(nameof(WBS), ref fWBS, value); }
        }
        DateTime fDATA_RICHIESTA_CLIENTE_INTERNO;
        public DateTime DATA_RICHIESTA_CLIENTE_INTERNO
        {
            get { return fDATA_RICHIESTA_CLIENTE_INTERNO; }
            set { SetPropertyValue<DateTime>(nameof(DATA_RICHIESTA_CLIENTE_INTERNO), ref fDATA_RICHIESTA_CLIENTE_INTERNO, value); }
        }
        string fTIPO_RICHIESTA;
        [Size(255)]
        public string TIPO_RICHIESTA
        {
            get { return fTIPO_RICHIESTA; }
            set { SetPropertyValue<string>(nameof(TIPO_RICHIESTA), ref fTIPO_RICHIESTA, value); }
        }
        string fDATA_ATTIVAZIONE_PREVISTA;
        [Size(SizeAttribute.Unlimited)]
        public string DATA_ATTIVAZIONE_PREVISTA
        {
            get { return fDATA_ATTIVAZIONE_PREVISTA; }
            set { SetPropertyValue<string>(nameof(DATA_ATTIVAZIONE_PREVISTA), ref fDATA_ATTIVAZIONE_PREVISTA, value); }
        }
        DateTime fDATA_PRESA_IN_CARICO;
        public DateTime DATA_PRESA_IN_CARICO
        {
            get { return fDATA_PRESA_IN_CARICO; }
            set { SetPropertyValue<DateTime>(nameof(DATA_PRESA_IN_CARICO), ref fDATA_PRESA_IN_CARICO, value); }
        }
        DateTime fDATA_CESSAZIONE;
        public DateTime DATA_CESSAZIONE
        {
            get { return fDATA_CESSAZIONE; }
            set { SetPropertyValue<DateTime>(nameof(DATA_CESSAZIONE), ref fDATA_CESSAZIONE, value); }
        }
        DateTime fDATA_SCADENZA_CONTRATTO_VS_CLIENTE;
        public DateTime DATA_SCADENZA_CONTRATTO_VS_CLIENTE
        {
            get { return fDATA_SCADENZA_CONTRATTO_VS_CLIENTE; }
            set { SetPropertyValue<DateTime>(nameof(DATA_SCADENZA_CONTRATTO_VS_CLIENTE), ref fDATA_SCADENZA_CONTRATTO_VS_CLIENTE, value); }
        }
        string fSTATO;
        [Size(255)]
        public string STATO
        {
            get { return fSTATO; }
            set { SetPropertyValue<string>(nameof(STATO), ref fSTATO, value); }
        }
        string fFORNITORE_EE_2016;
        [Size(50)]
        public string FORNITORE_EE_2016
        {
            get { return fFORNITORE_EE_2016; }
            set { SetPropertyValue<string>(nameof(FORNITORE_EE_2016), ref fFORNITORE_EE_2016, value); }
        }
        string fLOTTO;
        [Size(50)]
        public string LOTTO
        {
            get { return fLOTTO; }
            set { SetPropertyValue<string>(nameof(LOTTO), ref fLOTTO, value); }
        }
        string fFORNITORE_EE_2017;
        [Size(50)]
        public string FORNITORE_EE_2017
        {
            get { return fFORNITORE_EE_2017; }
            set { SetPropertyValue<string>(nameof(FORNITORE_EE_2017), ref fFORNITORE_EE_2017, value); }
        }
        string fNOTE;
        [Size(SizeAttribute.Unlimited)]
        public string NOTE
        {
            get { return fNOTE; }
            set { SetPropertyValue<string>(nameof(NOTE), ref fNOTE, value); }
        }
        string fVS_CLIENTE;
        [Size(SizeAttribute.Unlimited)]
        public string VS_CLIENTE
        {
            get { return fVS_CLIENTE; }
            set { SetPropertyValue<string>(nameof(VS_CLIENTE), ref fVS_CLIENTE, value); }
        }
        int fEquipment;
        public int Equipment
        {
            get { return fEquipment; }
            set { SetPropertyValue<int>(nameof(Equipment), ref fEquipment, value); }
        }
        string fTipoMercato;
        [Size(50)]
        public string TipoMercato
        {
            get { return fTipoMercato; }
            set { SetPropertyValue<string>(nameof(TipoMercato), ref fTipoMercato, value); }
        }
        string fNOTE_ATTIVAZ_2017;
        [Size(255)]
        public string NOTE_ATTIVAZ_2017
        {
            get { return fNOTE_ATTIVAZ_2017; }
            set { SetPropertyValue<string>(nameof(NOTE_ATTIVAZ_2017), ref fNOTE_ATTIVAZ_2017, value); }
        }
        string fEnergia_Verde;
        [Size(2)]
        public string Energia_Verde
        {
            get { return fEnergia_Verde; }
            set { SetPropertyValue<string>(nameof(Energia_Verde), ref fEnergia_Verde, value); }
        }
        DateTime fData_Presa_in_Carico_Fornitore;
        public DateTime Data_Presa_in_Carico_Fornitore
        {
            get { return fData_Presa_in_Carico_Fornitore; }
            set { SetPropertyValue<DateTime>(nameof(Data_Presa_in_Carico_Fornitore), ref fData_Presa_in_Carico_Fornitore, value); }
        }
        DateTime fData_Voltura;
        public DateTime Data_Voltura
        {
            get { return fData_Voltura; }
            set { SetPropertyValue<DateTime>(nameof(Data_Voltura), ref fData_Voltura, value); }
        }
        bool fMandato_Senza_Rappresentanza;
        public bool Mandato_Senza_Rappresentanza
        {
            get { return fMandato_Senza_Rappresentanza; }
            set { SetPropertyValue<bool>(nameof(Mandato_Senza_Rappresentanza), ref fMandato_Senza_Rappresentanza, value); }
        }
        long fEquipment_F0;
        public long Equipment_F0
        {
            get { return fEquipment_F0; }
            set { SetPropertyValue<long>(nameof(Equipment_F0), ref fEquipment_F0, value); }
        }
        long fEquipment_F1;
        public long Equipment_F1
        {
            get { return fEquipment_F1; }
            set { SetPropertyValue<long>(nameof(Equipment_F1), ref fEquipment_F1, value); }
        }
        long fEquipment_F2;
        public long Equipment_F2
        {
            get { return fEquipment_F2; }
            set { SetPropertyValue<long>(nameof(Equipment_F2), ref fEquipment_F2, value); }
        }
        long fEquipment_F3;
        public long Equipment_F3
        {
            get { return fEquipment_F3; }
            set { SetPropertyValue<long>(nameof(Equipment_F3), ref fEquipment_F3, value); }
        }
        bool fDISALIMENTABILITA;
        public bool DISALIMENTABILITA
        {
            get { return fDISALIMENTABILITA; }
            set { SetPropertyValue<bool>(nameof(DISALIMENTABILITA), ref fDISALIMENTABILITA, value); }
        }
        string fPROFIT_CENTER;
        [Size(50)]
        public string PROFIT_CENTER
        {
            get { return fPROFIT_CENTER; }
            set { SetPropertyValue<string>(nameof(PROFIT_CENTER), ref fPROFIT_CENTER, value); }
        }
        DateTime fDATA_CREAZIONE_RECORD;
        public DateTime DATA_CREAZIONE_RECORD
        {
            get { return fDATA_CREAZIONE_RECORD; }
            set { SetPropertyValue<DateTime>(nameof(DATA_CREAZIONE_RECORD), ref fDATA_CREAZIONE_RECORD, value); }
        }

    }
}
