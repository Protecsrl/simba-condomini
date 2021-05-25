using System;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.Costi
{
    [DefaultClassOptions,
    Persistent("REGCOSTIALTRERDL")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_WorkflowDefinition")]
    public class RegistroCostiAltreRegRdL : XPObject
    {
        public RegistroCostiAltreRegRdL()
            : base()
        {
        }

        public RegistroCostiAltreRegRdL(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //private RegistroCosti fRegistroCosti;
        //[Association(@"RegistroCosti_RegistroCostiAltreRegRdL"),
        //Persistent("REGISTROCOSTI"),
        //DisplayName("Registro Costi")]
        //[ExplicitLoading()]
        //public RegistroCosti RegistroCosti
        //{
        //    get
        //    {
        //        return fRegistroCosti;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegistroCosti>("RegistroCosti", ref fRegistroCosti, value);
        //    }
        //}

       // private RegistroRdL fRegRdl;
       // [Association(@"RegistroCostiAltreRegRdL_RegistroRdL"),  Persistent("REGRDL"),    DisplayName("Altri Registri RdL")]
       //// [DataSourceCriteria("Apparato.Impianto.Immobile = '@This.RegistroCosti.Immobile'")]//  [DataSourceCriteria("SistemaTecnologico = '@This.SistemaTecnologico'")]
       // [DataSourceCriteria("[<RdL>][^.Oid == RegistroRdL.Oid And Impianto.Oid == '@This.RegistroCosti.Impianto.Oid' And UltimoStatoSmistamento.Oid In(4,5,8,9)] And RegistroCostis.Count = 0 And RegistroCostiAltreRegRdLs.Count = 0")]

       // [ImmediatePostData(true)]
       // [ExplicitLoading()]
       // public RegistroRdL RegistroRdL
       // {
       //     get
       //     {
                
       //         return fRegRdl;
       //     }
       //     set
       //     {
       //         SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegRdl, value);
       //     }
       // }


    }
}
