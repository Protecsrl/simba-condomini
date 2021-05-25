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
    [DefaultClassOptions,    Persistent("KGUASTO"),   System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KGuasto")]
    [NavigationItem("Procedure Attivita")]
    [ImageName("Kutenza")]
    [VisibleInDashboards(false)]
    public class KGuasto : XPObject
    {
        public KGuasto()
            : base()
        {
        }

        public KGuasto(Session session)
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
        [Persistent("DESCRIZIONE"),     Size(50)]
        [DbType("varchar(50)")]
        [RuleRequiredField("RuleUniq.KGuasto.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [RuleUniqueValue("RuleCombIsUnique_KGuasto.Desc", DefaultContexts.Save, "Descrizione",
CustomMessageTemplate = @"Attenzione è stata già inserita una Descrizione({Descrizione}) già presente. Reinserirla Nuovamente.",
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
        private KDefault fKDefault = KDefault.No;
        [Persistent("KDEFAULT")]
        [ImmediatePostData(true)]
        [Appearance("kGuasto.Default.Enable", Enabled = false, Criteria = "VerDefaut", Context = "DetailView")]
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
        [RuleRange("RuleRange.kGuasto", "Save", 1, 2)]
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
                int clkD1 = Session.QueryInTransaction<KGuasto>().Where(d => d.Default == KDefault.Si).ToList().Count();
                int clkD = Session.Query<KGuasto>().Where(d => d.Default == KDefault.Si).ToList().Count();
                if (clkD > 0 || clkD1 > 0)
                    return true;

                return false;
            }
        }

    }
}
