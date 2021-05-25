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
    [DefaultClassOptions, Persistent("KDIMENSIONE"), System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KDimensione")]
    [RuleCombinationOfPropertiesIsUnique("RuleCombIsUnique_KDimensione.AppDescDefault", DefaultContexts.Save, "StandardApparato,Descrizione",
    CustomMessageTemplate = @"Attenzione è stato già inserito una Descrizione({Descrizione}) già presente questo Tipo di Apparato ({StandardApparato}). \r\nInserire Nuovamente.",
    SkipNullOrEmptyValues = false)]
    [ImageName("Kdimensione")]
    [NavigationItem("Procedure Attivita")]
    [VisibleInDashboards(false)]
    public class KDimensione : XPObject
    {
        public KDimensione()
            : base()
        {
        }

        public KDimensione(Session session)
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
                if (CAMS.Module.Classi.SetVarSessione.OidStandardAppararto > 0)
                { 
                    StdAsset nStandardApparato = Session.GetObjectByKey<StdAsset>(SetVarSessione.OidStandardAppararto );
                    this.StandardApparato = nStandardApparato;
                }
            }
        }



        public void InsertkDimensioneToTipoApparato(ref StdAsset NuovoTApparato)
        {
            if (NuovoTApparato != null)
            {
                NuovoTApparato.KDimensiones.Add(new KDimensione(Session)
                {
                    Descrizione = "Valore di Default",
                    Default = KDefault.Si,
                    StandardApparato = NuovoTApparato,
                    Valore = 1
                });
            }
        }



        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(150)]
        [RuleRequiredField("RuleReq.KDimensione.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [DbType("varchar(150)")]
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

        private StdAsset fStandardApparato;
        [Association(@"StdApparato_KDimensione"), Persistent("EQSTD"), DevExpress.Xpo.DisplayName("Tipo Apparato")]
        [RuleRequiredField("RuleReq.KDimensione.StandardApparato", DefaultContexts.Save, "Lo Standard dell'Apparato è un campo obbligatorio")]
        [ExplicitLoading()]
        public StdAsset StandardApparato
        {
            get
            {
                return fStandardApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StandardApparato", ref fStandardApparato, value);
            }
        }



        private KDefault fKDefault = KDefault.No;
        [Persistent("KDEFAULT"), ImmediatePostData(true)]
        [Appearance("Abilita.Modifica.Default", Enabled = false, Criteria = "VerDefaut And StandardApparato!= null", Context = "DetailView")]
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
                if (StandardApparato != null)
                {    // Retrieve all Accessory objects expre
                    //XPCollection<KDimensione> kD = new XPCollection<KDimensione>(Session);// Filter the retrieved collection according to current conditions  //  RefreshEdificiInseribili();     
                    int clkD1 = Session.QueryInTransaction<KDimensione>()
                             .Where(d => d.StandardApparato == StandardApparato && d.Default == KDefault.Si).ToList().Count();
                    int clkD = Session.Query<KDimensione>()
                                .Where(d => d.StandardApparato == StandardApparato && d.Default == KDefault.Si).ToList().Count();
                    if (clkD > 0 || clkD1 > 0)
                        return true;
                }
                return false;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (Session.IsNewObject(this))
            {
                //if (StandardApparato != null)
                //{
                //    this.SetPropertyValue("KDefault" , KDefault.Si);
                //}
            }
        }

    }
}
