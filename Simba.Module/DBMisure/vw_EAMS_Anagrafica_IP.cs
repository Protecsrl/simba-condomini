using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions,
    Persistent("MIS_IST_ANAGRAFICA")]
    [System.ComponentModel.DefaultProperty("Istrice Anagrafica")]
    [NavigationItem(false)]
    public class vw_EAMS_Anagrafica_IP : XPObject
    {
        public vw_EAMS_Anagrafica_IP()
            : base()
        {
        }

        public vw_EAMS_Anagrafica_IP(Session session)
            : base(session)
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





        int fID;
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
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
        string fPOLO;
        [Size(50)]
        public string POLO
        {
            get { return fPOLO; }
            set { SetPropertyValue<string>(nameof(POLO), ref fPOLO, value); }
        }
        string fPOD;
        [Size(25)]
        public string POD
        {
            get { return fPOD; }
            set { SetPropertyValue<string>(nameof(POD), ref fPOD, value); }
        }
        string fCOMUNE;
        [Size(50)]
        public string COMUNE
        {
            get { return fCOMUNE; }
            set { SetPropertyValue<string>(nameof(COMUNE), ref fCOMUNE, value); }
        }
        string fTIPO_UTILIZZO;
        [Size(25)]
        public string TIPO_UTILIZZO
        {
            get { return fTIPO_UTILIZZO; }
            set { SetPropertyValue<string>(nameof(TIPO_UTILIZZO), ref fTIPO_UTILIZZO, value); }
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
        double fCONSUMO_ANNUO;
        public double CONSUMO_ANNUO
        {
            get { return fCONSUMO_ANNUO; }
            set { SetPropertyValue<double>(nameof(CONSUMO_ANNUO), ref fCONSUMO_ANNUO, value); }
        }
        string fCLIENTE;
        [Size(125)]
        public string CLIENTE
        {
            get { return fCLIENTE; }
            set { SetPropertyValue<string>(nameof(CLIENTE), ref fCLIENTE, value); }
        }
        string fWBS;
        [Size(255)]
        public string WBS
        {
            get { return fWBS; }
            set { SetPropertyValue<string>(nameof(WBS), ref fWBS, value); }
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
        string fFORNITORE_CORRENTE;
        [Size(50)]
        public string FORNITORE_CORRENTE
        {
            get { return fFORNITORE_CORRENTE; }
            set { SetPropertyValue<string>(nameof(FORNITORE_CORRENTE), ref fFORNITORE_CORRENTE, value); }
        }
        string fSTATO;
        [Size(7)]
        public string STATO
        {
            get { return fSTATO; }
            set { SetPropertyValue<string>(nameof(STATO), ref fSTATO, value); }
        }

    }
}
