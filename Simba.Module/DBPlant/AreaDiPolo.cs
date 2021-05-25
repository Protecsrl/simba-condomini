using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("AREADIPOLO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Zona di Polo")]
    [RuleCombinationOfPropertiesIsUnique("Unique.AreadiPolo", DefaultContexts.Save, "CodDescrizione, Descrizione")]
    [ImageName("SnapToCells")]
    [NavigationItem("Polo")]
    public class AreaDiPolo : XPObject
    {
        public AreaDiPolo()
            : base()
        {
        }
        public AreaDiPolo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(100),
        DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.AreaDiPolo.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
        [Persistent("COD_DESCRIZIONE"), Size(5)]
        [RuleRequiredField("RReqField.AreadiPolo.CodDescrizione", DefaultContexts.Save, "Il Cod Descrizione è un campo obbligatorio")]
        [DbType("varchar(5)")]
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

        private int fUSLG;
        [ Persistent("USLG"),
        DisplayName("Unità Standard Lavoro Giornaliero")]

        public int USLG
        {
            get
            {
                return fUSLG;
            }
            set
            {
                SetPropertyValue<int>("USLG", ref fUSLG, value);
            }
        }

        private int fUSLS;
        [  Persistent("USLS"),
        DisplayName("Unità Standard Lavoro Settimanale")]
        public int USLS
        {
            get
            {
                return fUSLS;
            }
            set
            {
                SetPropertyValue<int>("USLS", ref fUSLS, value);
            }
        }


        private Polo fPolo;
        [RuleRequiredField("RReqField.AreaDiPolo.Polo", DefaultContexts.Save, "Polo è un campo obbligatorio")]
        [Association(@"Polo_AreaDiPolo"), DisplayName("Polo")]
        [Persistent("POLO")]
        [ImmediatePostData(true)]
        [ExplicitLoading]
        public Polo Polo
        {
            get
            {
                return fPolo;
            }
            set
            {
                SetPropertyValue<Polo>("Polo", ref fPolo, value);
            }
        }



        [Association(@"AreaDiPolo_CentroOperativo", typeof(CentroOperativo)),
        DisplayName("C.O. di Area")]
        public XPCollection<CentroOperativo> CentroOperativos
        {
            get
            {
                return GetCollection<CentroOperativo>("CentroOperativos");
            }
        }

        [Association(@"AreaDiPolo_Contratti", typeof(Contratti)),Aggregated,  DisplayName("Commesse")]
        public XPCollection<Contratti> Contratti
        {
            get
            {
                return GetCollection<Contratti>("Contratti");
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }
        //[PersistentAlias("AreaDiPolo.CentroOperativo"),
        //DisplayName("CentroOperativo")]
        //[MemberDesignTimeVisibility(false)]
        //public CentroOperativo CentroOperativo
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("CentroOperativo");
        //        if (tempObject != null)
        //        {
        //            return (CentroOperativo)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //[PersistentAlias("AreaDiPolo.Risorse"),
        //DisplayName("Risorse")]
        //[MemberDesignTimeVisibility(false)]
        //public Risorse Risorse
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("Risorse");
        //        if (tempObject != null)
        //        {
        //            return (Risorse)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}



    }
}
