using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.Classi;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions, Persistent("KUBICAZIONE"), System.ComponentModel.DefaultProperty("CodDescrizione"), DevExpress.ExpressApp.Model.ModelDefault("Caption", "KUbicazione")]
    [ImageName("Kubicazione")]
    [NavigationItem("Procedure Attivita")]
    [VisibleInDashboards(false)]
    public class KUbicazione : XPObject
    {
        public KUbicazione()
            : base()
        {
        }
        public KUbicazione(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.Valore = 1;
                Default = KDefault.No;
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(250)]
        [DbType("varchar(250)")]
        [RuleRequiredField("RuReqField.kUbicazione.Descrizione", DefaultContexts.Save, @"La Descrizione è un campo obbligatorio")]
        [RuleUniqueValue("RuleCombIsUnique.KUbicazione.Descrizione", DefaultContexts.Save, "Descrizione",
CustomMessageTemplate = @"Attenzione è stato già inserito una Descrizione({Descrizione}) già presente. \r\nInserire Nuovamente.",
SkipNullOrEmptyValues = false)]
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
        [Persistent("COD_DESCRIZIONE"), Size(25)]
        [DbType("varchar(25)")]
        [RuleRequiredField("RuReqField.kUbicazione.codDescriz", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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



        private IndiceCriticita fIndiceCriticita;
        [Persistent("INDCRITICITA")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public IndiceCriticita IndiceCriticita
        {
            get
            {
                return fIndiceCriticita;
            }
            set
            {
                SetPropertyValue<IndiceCriticita>("IndiceCriticita", ref fIndiceCriticita, value);
            }
        }

        private KDefault fKDefault = KDefault.No;
        [Persistent("KDEFAULT")]
        [ImmediatePostData(true)]
        [Appearance("kUbicazione.Default.Enable", Enabled = false, Criteria = "VerDefaut", Context = "DetailView")]
        public KDefault Default
        {
            get
            {
                return fKDefault;
            }
            set
            {
                SetPropertyValue<KDefault>("KDefault", ref fKDefault, value);
            }
        }

        private double fValore;
        [Persistent("VALORE")]
        [RuleRange("RuleRange.kUbicazione", "Save", 1, 2)]
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

        private bool fVerDefaut;
        [NonPersistent, Browsable(false)]   //   [DbType("varchar(150)")]
        public bool VerDefaut
        {
            get
            {
                int clkD1 = Session.QueryInTransaction<KUbicazione>().Where(d => d.Default == KDefault.Si).ToList().Count();
                int clkD = Session.Query<KUbicazione>().Where(d => d.Default == KDefault.Si).ToList().Count();
                if (clkD > 0 || clkD1 > 0)
                    return true;

                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("Cod.({0})", CodDescrizione);
        }
    }
}
