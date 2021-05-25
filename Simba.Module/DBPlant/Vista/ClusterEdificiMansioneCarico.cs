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
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Carico ClusterEdifici per Mansione")]
    [DefaultClassOptions, Persistent("V_CLUSTEREDIFICI_MANSIONE")]
    [NavigationItem(false)]

    public class ClusterEdificiMansioneCarico : XPLiteObject
    {

        public ClusterEdificiMansioneCarico() : base() { }

        public ClusterEdificiMansioneCarico(Session session) : base(session) { }

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


        [MemberDesignTimeVisibility(false)]
        [Association(@"ClusterEdifici_MansioneCarico", typeof(ClusterEdifici))]
        [Persistent("CLUSTEREDIFICI"), DisplayName("Cluster Edifici")]
        [ExplicitLoading]
        [Delayed(true)]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
            }
            set
            {
                SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
            }
        }

        private Mansioni fMansioni;
        [Persistent("MANSIONI"),
        DisplayName("Mansioni")]
        [Appearance("ClusterEdificiMansioneCarico.Impianto", Enabled = false)]
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
        DisplayName("Carico (min)")]
        [Appearance("ClusterEdificiMansioneCarico.Carico", Enabled = false)]
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



//GGLAVOROCARICO	82,13
//GGHLAVOROCARICO	82 gg 3 h
//GGTRASFERIMENTO	6,84
//GGHTRASFERIMENTOCARICO	6 gg 20 h
//GGTOTALE	88,97
//GGHTOTALECARICO	88 gg 23 h 
//GGCARICOLAVUOMO	235 gg
//RESTOCAPACITA	62,15
//CAPACITANECESSARIA	1

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
        [Persistent("GGHLAVOROCARICO"), DisplayName("Ore Giorno Carico")]
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
        [Persistent("GGTRASFERIMENTO"), DisplayName("Giorno Trasferimento")]
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
        [Persistent("GGHTRASFERIMENTOCARICO"), DisplayName("Giorno Orario Trasferimento")]
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
        [Persistent("GGHTOTALECARICO"), DisplayName("Giorni Ore Totale Carico")]
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
        [Persistent("RESTOCAPACITA"), DisplayName("Resto Capacità")]
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
        [Persistent("CAPACITANECESSARIA"), DisplayName("Capacità Necessaria")]
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
