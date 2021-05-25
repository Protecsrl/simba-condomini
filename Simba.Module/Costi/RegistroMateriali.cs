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

 
using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;


//namespace CAMS.Module.Costi
//{
//    class RegistroMateriali
//    {
//    }
//}
namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGMATERIALI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Materiali")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_Opportunity")]
    public class RegistroMateriali : XPObject
    {
        public RegistroMateriali()
            : base()
        {
        }

        public RegistroMateriali(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegistroLavoriPreventivi fRegistroLavoriPreventivi;
        [Association(@"RegistroLavoriPreventivi.RegistroMateriali"),
        Persistent("REGLAVORIPREVENTIVI"),
        DisplayName("RegistroLavoriPreventivi")]
        [ExplicitLoading()]
        public RegistroLavoriPreventivi RegistroLavoriPreventivi
        {
            get
            {
                return fRegistroLavoriPreventivi;
            }
            set
            {
                SetPropertyValue<RegistroLavoriPreventivi>("RegistroLavoriPreventivi", ref fRegistroLavoriPreventivi, value);
            }
        }

        private MagazzinoArticoli fMagazzinoArticoli; //Association(@"MagazzinoArticoli.RegistroMateriali"),
        [        Persistent("MAGAZZINO"),        DisplayName("Articolo")]
        [ExplicitLoading()]
        public MagazzinoArticoli MagazzinoArticoli
        {
            get
            {
                return fMagazzinoArticoli;
            }
            set
            {
                SetPropertyValue<MagazzinoArticoli>("MagazzinoArticoli", ref fMagazzinoArticoli, value);
            }
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.RegistroMateriali.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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


        private double fQuantita;
        [Persistent("QUANTITA"), DisplayName("Quantità")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double Quantita
        {
            get
            {
                return fQuantita;
            }
            set
            {
                SetPropertyValue<double>("Quantita", ref fQuantita, value);
            }
        }
        private double fPrezzo;
        [Persistent("PREZZO"), DisplayName("Prezzo")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double Prezzo
        {
            get
            {
                return fPrezzo;
            }
            set
            {
                SetPropertyValue<double>("Prezzo", ref fPrezzo, value);
            }
        }

        [PersistentAlias("Quantita * Prezzo"), DisplayName("Imp. Totale")]
        [Appearance("RegistroLavoriPreventivi.ImportoTotale.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "ImportoTotale = 0")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double ImportoTotale
        {
            get
            {
                var tempObject = EvaluateAlias("ImportoTotale");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

     

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }

    }
}

