using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("REGPIANIFICAZIONEREV")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Pianificazione MP Revisione")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class RegPianificazioneMPRevisioni : XPObject
    {
        public RegPianificazioneMPRevisioni() : base() { }

        public RegPianificazioneMPRevisioni(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        RegPianificazioneMP fRegPianificazioneMP;
        [Association(@"RegPianificazioneMPRevisioni_RegPianificazioneMP"), Persistent("MPREGPIANIFICAZIONE"), DisplayName("Reg. Pianificazione MP")]
        public RegPianificazioneMP RegPianificazioneMP
        {
            get
            {
                return fRegPianificazioneMP;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DisplayName("Descrizione")]
        [DbType("varchar(100)")]
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

        [Association(@"RegPianificazioneMPRevisioni_RegPianifMPRevisioniDett", typeof(RegPianifMPRevisioniDett)), DisplayName("Registro Pianificazione MP Revisioni Dett")] //Aggregated
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegPianifMPRevisioniDett> RegPianifMPRevisioniDetts { get { return GetCollection<RegPianifMPRevisioniDett>("RegPianifMPRevisioniDetts"); } }

    }
}
