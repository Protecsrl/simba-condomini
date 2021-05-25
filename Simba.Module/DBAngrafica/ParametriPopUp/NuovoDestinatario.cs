using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace CAMS.Module.DBAngrafica.ParametriPopUp
{
    [NavigationItem(false),
    System.ComponentModel.DisplayName("Parametri Destinatario da creare")]
    [DefaultClassOptions,    NonPersistent]

    public class NuovoDestinatario : XPObject
    {
        public NuovoDestinatario()
            : base()
        {
        }
        public NuovoDestinatario(Session session)
            : base(session)
        {
        }

        private string fNome;
        [NonPersistent,
        DisplayName("Nome")]
        public string Nome
        {
            get
            {
                return fNome;
            }
            set
            {
                SetPropertyValue<string>("Nome", ref fNome, value);
            }
        }

        private string fCognome;
        [NonPersistent,
        DisplayName("Cognome")]
        public string Cognome
        {
            get
            {
                return fCognome;
            }
            set
            {
                SetPropertyValue<string>("Cognome", ref fCognome, value);
            }
        }

        private string fEmail;
        [NonPersistent,
        DisplayName("Email")]
        public string Email
        {
            get
            {
                return fEmail;
            }
            set
            {
                SetPropertyValue<string>("Email", ref fEmail, value);
            }
        }

        private int fNumeroCopie;
        [NonPersistent,   DisplayName("NumeroCopie")]
        [MemberDesignTimeVisibility(false)]
        public int NumeroCopie
        {
            get
            {
                return fNumeroCopie;
            }
            set
            {
                SetPropertyValue<int>("NumeroCopie", ref fNumeroCopie, value);
            }
        }

        private Destinatari fVecchioDestinatario;
        [NonPersistent, DisplayName("Destinatario")]
        [Appearance("NuovoDestinatario.VecchioDestinatario.Visibility", Visibility = ViewItemVisibility.Hide)]
        public Destinatari VecchioDestinatario
        {
            get
            {
                return fVecchioDestinatario;
            }
            set
            {
                SetPropertyValue<Destinatari>("VecchioDestinatario", ref fVecchioDestinatario, value);
            }
        }
    }
}
