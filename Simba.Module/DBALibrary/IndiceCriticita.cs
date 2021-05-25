using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,  Persistent("INDCRITICITA"),  System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", @"Indice Criticità")]
    [ImageName("IndentIncrease")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class IndiceCriticita : XPObject
    {
        public IndiceCriticita()
            : base()
        {
        }

        public IndiceCriticita(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(250)]
        [DbType("varchar(250)")]
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

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(25)]
        [DbType("varchar(25)")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private double fValore;
        [Persistent("VALORE")]
        public double Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<double>("Valore", ref fValore, value);
            }
        }
    }
}
