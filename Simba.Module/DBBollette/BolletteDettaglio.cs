using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBBollette
{
    [DefaultClassOptions, Persistent("BOLLETTEDETTAGLIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "DettaglioBollette")]
    [ImageName("Action_Inline_Edit")]
    //[Appearance("RegMisure.Disabilita.DettaglioMisureMagZero", TargetItems = "Immobile;Impianto;Master;DataInserimento", Criteria = "RegMisureDettaglios.Count() > 0", Enabled = false)]
    [NavigationItem(true)]
    public class BolletteDettaglio : XPObject
    {
        public BolletteDettaglio() : base() { }

        public BolletteDettaglio(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private Bollette fBollette;
        [Association(@"Bollette_BolletteDettaglio"), Persistent("BOLLETTE")]
        [DisplayName("Bolletta")]
        //[System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        public Bollette Bollette
        {
            get
            {
                return fBollette;
            }
            set
            {
                SetPropertyValue<Bollette>("Bollette", ref fBollette, value);
            }
        }


        private string fDescrizioneTotale;
        [Size(100), Persistent("DESCRIZIONETOTALE"), DisplayName("Descrizione Totale")]
        [DbType("varchar(100)")]
        public string DescrizioneTotale
        {
            get
            {
                return fDescrizioneTotale;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneTotale", ref fDescrizioneTotale, value);
            }
        }

        //DescrizioneSubtotale	DESCRIZIONESUBTOTALE		Descrizione Subtotale

        private string fDescrizioneSubtotale;
        [Size(100), Persistent("DESCRIZIONESUBTOTALE"), DisplayName("Descrizione Subtotale")]
        [DbType("varchar(100)")]
        public string DescrizioneSubtotale
        {
            get
            {
                return fDescrizioneSubtotale;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneSubtotale", ref fDescrizioneSubtotale, value);
            }
        }

        //TipoRiga	TIPORIGA		Tipo riga
        private string fTipoRiga;
        [Size(10), Persistent("TIPORIGA"), DisplayName("Tipo Riga")]
        [DbType("varchar(10)")]
        public string TipoRiga
        {
            get
            {
                return fTipoRiga;
            }
            set
            {
                SetPropertyValue<string>("TipoRiga", ref fTipoRiga, value);
            }
        }

        //DescrizioneTiporiga	DESCRIZIONETIPORIGA		Descrizione tipo riga

        private string fDescrizioneTiporiga;
        [Size(100), Persistent("DESCRIZIONETIPORIGA"), DisplayName("Descrizione Tipo Riga")]
        [DbType("varchar(100)")]
        public string DescrizioneTiporiga
        {
            get
            {
                return fDescrizioneTiporiga;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneTiporiga", ref fDescrizioneTiporiga, value);
            }
        }

        //DataInizioPeriodo	DATAINIZIOPERIODO		Data Inizio Periodo
        private DateTime fDataInizioPeriodo;
        [Persistent("DATAINIZIOPERIODO"), DisplayName("Data Inizio Periodo")]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        public DateTime DataInizioPeriodo
        {
            get
            {
                return fDataInizioPeriodo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInizioPeriodo", ref fDataInizioPeriodo, value);
            }
        }

        //DataFinePeriodo	DATAFINEPERIODO		Data Fine Periodo
        private DateTime fDataFinePeriodo;
        [Persistent("DATAFINEPERIODO"), DisplayName("Data Fine Periodo")]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        public DateTime DataFinePeriodo
        {
            get
            {
                return fDataFinePeriodo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataFinePeriodo", ref fDataFinePeriodo, value);
            }
        }

        //Qta	QTAFATT.		Qta Fatt.
        private double fQta;
        [Persistent("QTAFATT"), DisplayName("Qta Fatt.")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double Qta
        {
            get
            {
                return fQta;
            }
            set
            {
                SetPropertyValue<double>("Qta", ref fQta, value);
                //if ()
                //{ OnChanged("ImportoTotale"); }
            }
        }


        //private UnitaMisura fUnMisuraConsumi;
        //[Persistent("UNITAMISURA"), DisplayName("Un. Misura")]
        //public UnitaMisura UnMisuraConsumi
        //{
        //    get
        //    {
        //        return fUnMisuraConsumi;
        //    }
        //    set
        //    {
        //        SetPropertyValue<UnitaMisura>("UnMisuraConsumi", ref fUnMisuraConsumi, value);
        //    }
        //}



        //PrezzoUnitario	PREZZOUNITARIO		Prezzo unitario

        private double fPrezzoUnitario;
        [Persistent("PREZZOUNITARIO"), DisplayName("Prezzo Unitario")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double PrezzoUnitario
        {
            get
            {
                return fPrezzoUnitario;
            }
            set
            {
                SetPropertyValue<double>("PrezzoUnitario", ref fPrezzoUnitario, value);
                //if( )
                //{ OnChanged("ImportoTotale"); }
            }
        }

        //PrezzoTotale	PREZZOTOTALE		Prezzo totale
        private double fPrezzoTotale;
        [Persistent("PREZZOTOTALE"), DisplayName("Prezzo Totale")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double PrezzoTotale
        {
            get
            {
                return fPrezzoTotale;
            }
            set
            {
                SetPropertyValue<double>("PrezzoTotale", ref fPrezzoTotale, value);
                //if ()
                //{ OnChanged("ImportoTotale"); }
            }
        }





        //        CodIVA	COD.IVAAPPL.		Cod.IVA


        private string fCodIVA;
        [Size(7), Persistent("CODIVA"), DisplayName("Cod.IVA")]
        [DbType("varchar(7)")]
        public string CodIVA
        {
            get
            {
                return fCodIVA;
            }
            set
            {
                SetPropertyValue<string>("CodIVA", ref fCodIVA, value);
            }
        }
        //GruppoFatturazione	GRUPPOFATTURAZIONE		Gruppo fatturazione


        private string fGruppoFatturazione;
        [Size(7), Persistent("GRUPPOFATTURAZIONE"), DisplayName("GruppoFatturazione")]
        [DbType("varchar(7)")]
        public string GruppoFatturazione
        {
            get
            {
                return fGruppoFatturazione;
            }
            set
            {
                SetPropertyValue<string>("GruppoFatturazione", ref fGruppoFatturazione, value);
            }
        }



        //TipoContratto	TIPOCONTRATTO		Tipo contratto

        private string fTipoContratto;
        [Size(7), Persistent("TIPOCONTRATTO"), DisplayName("Tipo Contratto")]
        [DbType("varchar(7)")]
        public string TipoContratto
        {
            get
            {
                return fTipoContratto;
            }
            set
            {
                SetPropertyValue<string>("TipoContratto", ref fTipoContratto, value);
            }
        }


        private string fUtente;
        [Size(100), Persistent("UTENTE"), DevExpress.Xpo.DisplayName("Utente")]
        [Appearance("RegBollette.Abilita.Utente", Enabled = false)]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get { return fUtente; }
            set { SetPropertyValue<string>("Utente", ref fUtente, value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Upadate")]
        [Appearance("RegBollette.Abilita.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }

        #region consumi energia
        private double fConsumoAttivaF3;
        [Persistent("CONSUMOATTIVAF3"), DisplayName("Consumo F3")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kWh")]
        [ImmediatePostData(true)]
        public double ConsumoAttivaF3
        {
            get { return fConsumoAttivaF3; }
            set { SetPropertyValue<double>("ConsumoAttivaF3", ref fConsumoAttivaF3, value); }
        }

        private double fConsumoAttivaF2;
        [Persistent("CONSUMOATTIVAF2"), DisplayName("Consumo F2")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kWh")]
        [ImmediatePostData(true)]
        public double ConsumoAttivaF2
        {
            get { return fConsumoAttivaF2; }
            set { SetPropertyValue<double>("ConsumoAttivaF2", ref fConsumoAttivaF2, value); }
        }
        private double fConsumoAttivaF1;
        [Persistent("CONSUMOATTIVAF1"), DisplayName("Consumo F1")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kWh")]
        [ImmediatePostData(true)]
        public double ConsumoAttivaF1
        {
            get { return fConsumoAttivaF1; }
            set { SetPropertyValue<double>("ConsumoAttivaF1", ref fConsumoAttivaF1, value); }
        }
        private double fConsumoAttivaF0;
        [Persistent("CONSUMOATTIVAF0"), DisplayName("Consumo F0")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kWh")]
        [ImmediatePostData(true)]
        public double ConsumoAttivaF0
        {
            get { return fConsumoAttivaF0; }
            set { SetPropertyValue<double>("ConsumoAttivaF0", ref fConsumoAttivaF0, value); }
        }
        private double fConsumoTotale;
        [Persistent("CONSUMOATTIVATOTALE"), DisplayName("Consumo Totale")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kWh")]
        [ImmediatePostData(true)]
        public double ConsumoTotale
        {
            get { return fConsumoTotale; }
            set { SetPropertyValue<double>("ConsumoTotale", ref fConsumoTotale, value); }
        }
        #endregion 

        #region consumi energia
        private double fConsumoREAttivaF3;
        [Persistent("CONSUMOREATTIVAF3"), DisplayName("Consumo Reattiva F3")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kVAh")]
        [ImmediatePostData(true)]
        public double ConsumoREAttivaF3
        {
            get { return fConsumoREAttivaF3; }
            set { SetPropertyValue<double>("ConsumoREAttivaF3", ref fConsumoREAttivaF3, value); }
        }

        private double fConsumoREAttivaF2;
        [Persistent("CONSUMOREATTIVAF2"), DisplayName("Consumo Reattiva F2")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kVAh")]
        [ImmediatePostData(true)]
        public double ConsumoREAttivaF2
        {
            get { return fConsumoREAttivaF2; }
            set { SetPropertyValue<double>("ConsumoREAttivaF2", ref fConsumoREAttivaF2, value); }
        }
        private double fConsumoREAttivaF1;
        [Persistent("CONSUMOREATTIVAF1"), DisplayName("Consumo Reattiva F1")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kVAh")]
        [ImmediatePostData(true)]
        public double ConsumoREAttivaF1
        {
            get { return fConsumoREAttivaF1; }
            set { SetPropertyValue<double>("ConsumoREAttivaF1", ref fConsumoREAttivaF1, value); }
        }
        private double fConsumoREAttivaF0;
        [Persistent("CONSUMOREATTIVAF0"), DisplayName("Consumo Reattiva F0")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kVAh")]
        [ImmediatePostData(true)]
        public double ConsumoREAttivaF0
        {
            get { return fConsumoREAttivaF0; }
            set { SetPropertyValue<double>("ConsumoREAttivaF0", ref fConsumoREAttivaF0, value); }
        }
        private double fConsumoREAttivaTotale;
        [Persistent("CONSUMOREATTIVATOTALE"), DisplayName("Consumo Reattiva Totale")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:N} kVAh")]
        [ImmediatePostData(true)]
        public double ConsumoREAttivaTotale
        {
            get { return fConsumoREAttivaTotale; }
            set { SetPropertyValue<double>("ConsumoREAttivaTotale", ref fConsumoREAttivaTotale, value); }
        }
        #endregion 



        #region importi energia
        private double fImportoAttivaF3;
        [Persistent("IMPORTOATTIVAF3"), DisplayName("Importo F3")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"C")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:C}")]
        [ImmediatePostData(true)]
        public double ImportoAttivaF3
        {
            get { return fImportoAttivaF3; }
            set { SetPropertyValue<double>("ImportoAttivaF3", ref fImportoAttivaF3, value); }
        }

        private double fImportoAttivaF2;
        [Persistent("IMPORTOATTIVAF2"), DisplayName("Importo F2")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"C")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:C}")]
        [ImmediatePostData(true)]
        public double ImportoAttivaF2
        {
            get { return fImportoAttivaF2; }
            set { SetPropertyValue<double>("ImportoAttivaF2", ref fImportoAttivaF2, value); }
        }
        private double fImportoAttivaF1;
        [Persistent("IMPORTOATTIVAF1"), DisplayName("Importo F1")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"C")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:C}")]
        [ImmediatePostData(true)]
        public double ImportoAttivaF1
        {
            get { return fImportoAttivaF1; }
            set { SetPropertyValue<double>("ImportoAttivaF1", ref fImportoAttivaF1, value); }
        }
        private double fImportoAttivaF0;
        [Persistent("IMPORTOATTIVAF0"), DisplayName("Importo F0")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"C")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:C}")]
        [ImmediatePostData(true)]
        public double ImportoAttivaF0
        {
            get { return fImportoAttivaF0; }
            set { SetPropertyValue<double>("ImportoAttivaF0", ref fImportoAttivaF0, value); }
        }
        private double fImportoTotale;
        [Persistent("IMPORTOATTIVATOTALE"), DisplayName("Importo Totale")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"C")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:C}")]
        [ImmediatePostData(true)]
        public double ImportoTotale
        {
            get { return fImportoTotale; }
            set { SetPropertyValue<double>("ImportoTotale", ref fImportoTotale, value); }
        }
        #endregion 


    }
}
