//todo da cancellare?

using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;

namespace CAMS.Module.DBPlant
{
    [NavigationItem(false), System.ComponentModel.DisplayName("Parametri Immobile da creare")]
    [DefaultClassOptions, NonPersistent]
    [VisibleInDashboards(false)]
    [RuleCriteria("NuovoEdificio.Valida.Descrizione", DefaultContexts.Save, @"VerificaUnivocita",
    CustomMessageTemplate = "Descrizione o Cod Descrizione non univoca!, Modificare",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

 

    public class NuovoEdificio : XPObject
    {
        public NuovoEdificio()
            : base()
        {
        }
        public NuovoEdificio(Session session)
            : base(session)
        {
        }
        private string fDescrizione;
        [NonPersistent, XafDisplayName("Descrizione")]
        [RuleRequiredField("RReqField.NuovoEdificio.Descrizione", DefaultContexts.Save, @"La Descrizione è un campo obbligatorio")]
        [ImmediatePostData(true)]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
                //Evaluate("VerificaUnivocita");
                OnChanged("VerificaUnivocita");
            }
        }

        private string fCodDescrizione;
        [NonPersistent, Size(50), XafDisplayName("Cod Descrizione")]
        [RuleRequiredField("RReqField.NuovoEdificio.CodDescrizione", DefaultContexts.Save, @"Codice Descrizione è un campo obbligatorio")]
        [ImmediatePostData(true)]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
                // Evaluate("VerificaUnivocita");
                OnChanged("VerificaUnivocita");
            }
        }

        private Indirizzo fIndirizzo;
        [NonPersistent, XafDisplayName("Indirizzo")]
        [RuleRequiredField("RReqField.NuovoEdificio.Indirizzo", DefaultContexts.Save, @"Indirizzo è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [ExplicitLoading]
        public Indirizzo Indirizzo
        {
            get
            {
                return fIndirizzo;
            }
            set
            {
                SetPropertyValue<Indirizzo>("Indirizzo", ref fIndirizzo, value);
            }
        }

        private Immobile fVecchioEdificio;
        [NonPersistent, XafDisplayName("Immobile da Clonare")]
        [Appearance("NuovoEdificio.VecchioEdificio.Visibility", Visibility = ViewItemVisibility.Hide)]
        [ImmediatePostData(true)]
        public Immobile VecchioEdificio
        {
            get
            {
                return fVecchioEdificio;
            }
            set
            {
                SetPropertyValue<Immobile>("VecchioEdificio", ref fVecchioEdificio, value);

            }
        }


        private Immobile fNewEdificio;
        [NonPersistent, XafDisplayName("Immobile Nuovo")]
        [Appearance("NuovoEdificio.NewEdificio.Visibility", Visibility = ViewItemVisibility.Hide)]
        [ImmediatePostData(true)]
        public Immobile NewEdificio
        {
            get
            {
                return fNewEdificio;
            }
            set
            {
                SetPropertyValue<Immobile>("NewEdificio", ref fNewEdificio, value);

            }
        }

        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool UserIsAdminRuolo
        {
            get
            {
                return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }

        [NonPersistent]
        public bool VerificaUnivocita
        {
            get
            {
                int v = 0;
                if (this.NewEdificio != null)
                {
                    v = Session.Query<Immobile>().Where(w => w.CodDescrizione == this.CodDescrizione && w.Oid != this.NewEdificio.Oid).Count();
                    v = v + Session.Query<Immobile>().Where(w => w.Descrizione == this.Descrizione && w.Oid != this.NewEdificio.Oid).Count();
                }
                else
                {
                    v = Session.Query<Immobile>().Where(w => w.CodDescrizione == this.CodDescrizione).Count();
                    v = v + Session.Query<Immobile>().Where(w => w.Descrizione == this.Descrizione).Count();
                }
                if (v > 0)
                    return true;

                return false;
            }

        }


    }
}
