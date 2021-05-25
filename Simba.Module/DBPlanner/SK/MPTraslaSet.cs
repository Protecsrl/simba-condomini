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
//anno NUMBER(38),
//  apparato NUMBER,
//  settimana          NUMBER,
//  ordinamento NUMBER,
//  frequenza          NUMBER(38),
//  descrizione VARCHAR2(150),
//  min_traslaschedula NUMBER,
//  tipo               VARCHAR2(18),
//  descrschift VARCHAR2(16),
//  gg_moltiplicatore NUMBER,
//  tempoinsito        NUMBER,
//  data DATE,
//  appschedamp        NUMBER,
//  regpiano NUMBER,
//  utente             VARCHAR2(200),
//  count_edifici NUMBER,
//  mansione           NUMBER,
//  numman NUMBER
using CAMS.Module.DBPlanner;
using DevExpress.Persistent.Base;
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
#pragma warning disable CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using System.Threading.Tasks;
#pragma warning restore CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi

//namespace EAMSConsole.MP.XPOObj
//{
//    class MPTraslaSet
//    {
//    }
//}
namespace CAMS.Module.DBPlanner.SK
{
    [DefaultClassOptions, Persistent("MP_TRASLASETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "MPTraslaSet")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    public class MPTraslaSet : XPObject
    {

        public MPTraslaSet()
           : base()
        {
        }

        public MPTraslaSet(Session session)
           : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private int fAnno;
        [Persistent("ANNO"), DisplayName("Anno")]
        public int Anno
        {
            get { return fAnno; }
            set { SetPropertyValue<int>("Anno", ref fAnno, value); }
        }
        private int fScenario;
        [Persistent("SCENARIO"), DisplayName("Anno")]
        public int Scenario
        {
            get { return fScenario; }
            set { SetPropertyValue<int>("Scenario", ref fScenario, value); }
        }
        private int fEdificio;
        [Persistent("IMMOBILE"), DisplayName("fEdificio")]
        public int Immobile
        {
            get { return fEdificio; }
            set { SetPropertyValue<int>("Immobile", ref fEdificio, value); }
        }
        private int fImpianto;
        [Persistent("IMPIANTO"), DisplayName("fImpianto")]
        public int Impianto
        {
            get { return fImpianto; }
            set { SetPropertyValue<int>("Impianto", ref fImpianto, value); }
        }
        private int fAsset;
        [Persistent("ASSET"), DisplayName("Asset")]
        public int Apparato
        {
            get { return fAsset; }
            set { SetPropertyValue<int>("Asset", ref fAsset, value); }
        }
        private int fTempoOpt;
        [Persistent("TEMPOOPT"), DisplayName("fTempoOpt")]
        public int TempoOpt
        {
            get { return fTempoOpt; }
            set { SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value); }
        }
        private int fSettimana;
        [Persistent("SETTIMANA"), DisplayName("Settimana")]
        public int Settimana
        {
            get { return fSettimana; }
            set { SetPropertyValue<int>("Settimana", ref fSettimana, value); }
        }
        private int fSettimanaTrasla;
        [Persistent("SETTIMANATRASLA"), DisplayName("SettimanaTrasla")]
        public int SettimanaTrasla
        {
            get { return fSettimanaTrasla; }
            set { SetPropertyValue<int>("SettimanaTrasla", ref fSettimanaTrasla, value); }
        }
        private int fnewSettimana;
        [Persistent("NEWSETTIMANA"), DisplayName("fnewSettimana")]
        public int newSettimana
        {
            get { return fnewSettimana; }
            set { SetPropertyValue<int>("newSettimana", ref fnewSettimana, value); }
        }
        //public int Settimana { get; set; }
        //public int ORDINAMENTO { get; set; }
        private int fOrdinamento;
        [Persistent("ORDINAMENTO"), DisplayName("Ordinamento")]
        public int Ordinamento
        {
            get { return fOrdinamento; }
            set { SetPropertyValue<int>("Ordinamento", ref fOrdinamento, value); }
        }
        //public int FREQUENZA { get; set; }
        private int fFrequenza;
        [Persistent("FREQUENZA"), DisplayName("Frequenza")]
        public int Frequenza
        {
            get { return fFrequenza; }
            set { SetPropertyValue<int>("Frequenza", ref fFrequenza, value); }
        }

        //public string FreqDescrizione { get; set; }
        //public int /*Min_traslaschedula*/ { get; set; }
        private int fMinTraslaSK;
        [Persistent("MIN_TRASLASK"), DisplayName("MinTraslaSK")]
        public int MinTraslaSK
        {
            get { return fMinTraslaSK; }
            set { SetPropertyValue<int>("MinTraslaSK", ref fMinTraslaSK, value); }
        }
        //public string tipo { get; set; }
        private string fTipo;
        [Persistent("TIPO"), DisplayName("fTipo")]
        public string Tipo
        {
            get { return fTipo; }
            set { SetPropertyValue<string>("Tipo", ref fTipo, value); }
        }
        //public string descrschift { get; set; }
        //public int gg_moltiplicatore { get; set; }
        private int fGGMoltiplicatore;
        [Persistent("GGMOLTIPLICATORE"), DisplayName("GGMoltiplicatore")]
        public int GGMoltiplicatore
        {
            get { return fGGMoltiplicatore; }
            set { SetPropertyValue<int>("GGMoltiplicatore", ref fGGMoltiplicatore, value); }
        }
        //public int TEMPOINSITO { get; set; }
        private int fTempoInSito;
        [Persistent("TEMPOINSITO"), DisplayName("fTempoInSito")]
        public int TempoInSito
        {
            get { return fTempoInSito; }
            set { SetPropertyValue<int>("TempoInSito", ref fTempoInSito, value); }
        }
        //public DateTime data { get; set; }
        private DateTime fData;
        [Persistent("DATA"), DisplayName("Data Cadenza Obbligata"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
        public DateTime Data
        {
            get { return fData; }
            set { SetPropertyValue<DateTime>("Data", ref fData, value); }
        }
        //public int appschedamp { get; set; }
        private int fAppSkMP;
        [Persistent("APPSKMP"), DisplayName("fAppSkMP")]
        public int AppSkMP
        {
            get { return fAppSkMP; }
            set { SetPropertyValue<int>("AppSkMP", ref fAppSkMP, value); }
        }
        //public int regpiano { get; set; }
        private int fRegPiano;
        [Persistent("REGPIANO"), DisplayName("fRegPiano")]
        public int RegPiano
        {
            get { return fRegPiano; }
            set { SetPropertyValue<int>("RegPiano", ref fRegPiano, value); }
        }
        //public string utente { get; set; }
        private string fUtente;
        [Persistent("utente"), DisplayName("Utente")]
        public string Utente
        {
            get { return fUtente; }
            set { SetPropertyValue<string>("Utente", ref fUtente, value); }
        }
        //public int COUNT_EDIFICI { get; set; }
        private int fCountEdifici;
        [Persistent("COUNTEDIFICI"), DisplayName("fCountEdifici")]
        public int CountEdifici
        {
            get { return fCountEdifici; }
            set { SetPropertyValue<int>("CountEdifici", ref fCountEdifici, value); }
        }
        //public int mansione { get; set; }
        private int fMansione;
        [Persistent("MANSIONE"), DisplayName("fMansione")]
        public int Mansione
        {
            get { return fMansione; }
            set { SetPropertyValue<int>("Mansione", ref fMansione, value); }
        }
        //public int numman { get; set; }
        private int fNumMan;
        [Persistent("NUMMAN"), DisplayName("fNumMan")]
        public int NumMan
        {
            get { return fNumMan; }
            set { SetPropertyValue<int>("NumMan", ref fNumMan, value); }
        }


        

    }
}
