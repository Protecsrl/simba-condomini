using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant.Vista
{
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Carico Immobile per Mansione")]
    [DefaultClassOptions, Persistent("V_EDIFICIO_MANSIONE")]
    [NavigationItem(false)]

    public class EdificioMansioneCarico : XPLiteObject
    {
        public EdificioMansioneCarico() : base() { }

        public EdificioMansioneCarico(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }


        private string fcodice;
        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }


        private Immobile fImmobile;
        [MemberDesignTimeVisibility(false)]
        [Association(@"Edificio_MansioneCarico", typeof(Immobile))]
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue("Immobile", ref fImmobile, value);
            }
        }

        private Mansioni fMansioni;
        [Persistent("MANSIONI"),        DisplayName("Mansioni")]
        [Appearance("EdificioMansioneCarico.Impianto", Enabled = false)]
        [ExplicitLoading()]
        public Mansioni Mansioni
        {
            get
            {
                return fMansioni;
            }
            set
            {
                SetPropertyValue<Mansioni>("Mansioni", ref fMansioni, value);
            }
        }

        private int fCarico;
        [Persistent("CARICO"),
        DisplayName("Carico in Sito (min)")]
        [Appearance("EdificioMansioneCarico.Carico", Enabled = false)]
        [VisibleInDetailView(false)]
        public int Carico
        {
            get
            {
                return fCarico;
            }
            set
            {
                SetPropertyValue<int>("Carico", ref fCarico, value);
            }
        }
        private int fGGLavoroCarico;
        [Persistent("GGLAVOROCARICO"), DisplayName("Giorno Carico")]
        [System.ComponentModel.Browsable(false)]
        public int GGLavoroCarico
        {
            get
            {
                return fGGLavoroCarico;
            }
            set
            {
                SetPropertyValue<int>("GGLavoroCarico", ref fGGLavoroCarico, value);
            }
        }
        //------
        private string fGGHLavoroCarico;
        [Persistent("GGHLAVOROCARICO"), DisplayName("Carico in Sito")]
        public string GGHLavoroCarico
        {
            get
            {
                return fGGHLavoroCarico;
            }
            set
            {
                SetPropertyValue<string>("GGHLavoroCarico", ref fGGHLavoroCarico, value);
            }
        }
        //------
        private double fGGTrasferimento;
        [Persistent("GGTRASFERIMENTO"), DisplayName("Carico Trasferimento")]
        [System.ComponentModel.Browsable(false)]
        public double GGTrasferimento
        {
            get
            {
                return fGGTrasferimento;
            }
            set
            {
                SetPropertyValue<double>("GGTrasferimento", ref fGGTrasferimento, value);
            }
        }

        //------

        private string fGGHTrasferimentoCarico;
        [Persistent("GGHTRASFERIMENTOCARICO"), DisplayName("Carico Trasferimento")]
        public string GGHTrasferimentoCarico
        {
            get
            {
                return fGGHTrasferimentoCarico;
            }
            set
            {
                SetPropertyValue<string>("GGHTrasferimentoCarico", ref fGGHTrasferimentoCarico, value);
            }

        }
        //------
        private double fGGTotale;
        [Persistent("GGTOTALE"), DisplayName("Giorni Totali")]
        [System.ComponentModel.Browsable(false)]
        public double GGTotale
        {
            get
            {
                return fGGTotale;
            }
            set
            {
                SetPropertyValue<double>("GGTotale", ref fGGTotale, value);
            }

        }

        //------

        private string fGGHTotaleCarico;
        [Persistent("GGHTOTALECARICO"), DisplayName("Totale Carico")]
        public string GGHTotaleCarico
        {
            get
            {
                return fGGHTotaleCarico;
            }
            set
            {
                SetPropertyValue<string>("GGHTotaleCarico", ref fGGHTotaleCarico, value);
            }

        }
        //------

        private string fGGCaricoLavUomo;
        [Persistent("GGCARICOLAVUOMO"), DisplayName("Giorni Carico Lavoro Uomo")]
        [System.ComponentModel.Browsable(false)]
        public string GGCaricoLavUomo
        {
            get
            {
                return fGGCaricoLavUomo;
            }
            set
            {
                SetPropertyValue<string>("GGCaricoLavUomo", ref fGGCaricoLavUomo, value);
            }

        }
        //------

        private double fRestoCapacita;
        [Persistent("RESTOCAPACITA"), DisplayName("Resto Capacità (perc.)")]
        public double RestoCapacita
        {
            get
            {
                return fRestoCapacita;
            }
            set
            {
                SetPropertyValue<double>("RestoCapacita", ref fRestoCapacita, value);
            }

        }
        //------

        private int fCapacitaNecessaria;
        [Persistent("CAPACITANECESSARIA"), DisplayName("Capacità min. Necessaria")]
        public int CapacitaNecessaria
        {
            get
            {
                return fCapacitaNecessaria;
            }
            set
            {
                SetPropertyValue<int>("CapacitaNecessaria", ref fCapacitaNecessaria, value);
            }

        }
        //------
    }
}
