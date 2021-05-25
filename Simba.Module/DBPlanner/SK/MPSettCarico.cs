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
//    class MPSettCarico
//    {
//    }
//}
namespace CAMS.Module.DBPlanner.SK
{
    [DefaultClassOptions, Persistent("MP_SETTCARICO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "MPSettCarico")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    public class MPSettCarico : XPObject
    {

        public MPSettCarico()
           : base()
        {
        }

        public MPSettCarico(Session session)
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
     
     
       
        private int fCarico;
        [Persistent("ASSET"), DisplayName("Carico Asset")]
        public int Carico
        {
            get { return fCarico; }
            set { SetPropertyValue<int>("Carico", ref fCarico, value); }
        }
        
        private int fSettimana;
        [Persistent("SETTIMANA"), DisplayName("Settimana")]
        public int Settimana
        {
            get { return fSettimana; }
            set { SetPropertyValue<int>("Settimana", ref fSettimana, value); }
        }
        
        
        //public int TEMPOINSITO { get; set; }
        private int fTempoInSito;
        [Persistent("TEMPOINSITO"), DisplayName("fTempoInSito")]
        public int TempoInSito
        {
            get { return fTempoInSito; }
            set { SetPropertyValue<int>("TempoInSito", ref fTempoInSito, value); }
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
