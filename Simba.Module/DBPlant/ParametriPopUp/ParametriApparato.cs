using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBPlant
{
      [DevExpress.Persistent.Base.NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class ParametriApparato : XPObject
    {
        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";

        public ParametriApparato()
            : base()
        {
        }
        public ParametriApparato(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [NonPersistent,
        DisplayName("Descrizione")]
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

        private uint fNumeroCopie;
        [NonPersistent,
        DisplayName("NumeroCopie")]
        public uint NumeroCopie
        {
            get
            {
                return fNumeroCopie;
            }
            set
            {
                SetPropertyValue<uint>("NumeroCopie", ref fNumeroCopie, value);
            }
        }

        private Asset fVecchioApparato;
        [NonPersistent,
        DisplayName("Vecchio Apparato")]
        [Appearance("ParametriApparato.VecchioApparato.Visibility", Visibility = ViewItemVisibility.Hide)]
        public Asset VecchioApparato
        {
            get
            {
                return fVecchioApparato;
            }
            set
            {
                SetPropertyValue<Asset>("VecchioApparato", ref fVecchioApparato, value);
            }
        }
    }
}
