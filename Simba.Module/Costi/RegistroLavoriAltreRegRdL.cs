using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace CAMS.Module.Costi
//{
//    class RegistroLavoriAltreRegRdL
//    {
//    }
//}
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.Costi
{
    [DefaultClassOptions,
    Persistent("REGLAVORIALTRERDL")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_WorkflowDefinition")]
    public class RegistroLavoriAltreRegRdL : XPObject
    {
        public RegistroLavoriAltreRegRdL()
            : base()
        {
        }

        public RegistroLavoriAltreRegRdL(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegistroLavori fRegistroLavori;
        [Association(@"RegistroLavoriAltreRegRdL.RegistroLavori"),
        Persistent("REGISTROLAVORI"),
        DisplayName("Registro Lavori")]
        [ExplicitLoading()]
        public RegistroLavori RegistroLavori
        {
            get
            {
                return fRegistroLavori;
            }
            set
            {
                SetPropertyValue<RegistroLavori>("RegistroLavori", ref fRegistroLavori, value);
            }
        }

        private RegistroRdL fRegRdl;
        [Association(@"RegistroLavoriAltreRegRdL_RegistroRdL"), Persistent("REGRDL"), DisplayName("Altri Registri RdL")]
       // [DataSourceCriteria("Apparato.Impianto.Immobile = '@This.RegistroCosti.Immobile'")]//  [DataSourceCriteria("SistemaTecnologico = '@This.SistemaTecnologico'")]
        //[DataSourceCriteria("[<RdL>][^.Oid == RegistroRdL.Oid And Impianto.Oid == '@This.RegistroCosti.Impianto.Oid' And UltimoStatoSmistamento.Oid In(4,5,8,9)] And RegistroCostis.Count = 0 And RegistroCostiAltreRegRdLs.Count = 0")]
        [DataSourceCriteria("[<RdL>][^.Oid == RegistroRdL.Oid And Impianto.Oid == '@This.RegistroLavori.Impianto.Oid' And UltimoStatoSmistamento.Oid In(11,2,3,4,5,8,9)] And RegistroLavoris.Count = 0 And RegistroLavoriAltreRegRdLs.Count = 0")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public RegistroRdL RegistroRdL
        {
            get
            {
                
                return fRegRdl;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegRdl, value);
            }
        }


    }
}
