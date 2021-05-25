
//namespace CAMS.Module.DBGestOrari
//{
//    class GestioneOrariCircuiti
//    {
//    }
//}
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBGestOrari
{
    [DefaultClassOptions, Persistent("GESTIONEORARICIRCUITI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gestione Orari Circuiti")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]
    public class GestioneOrariCircuiti : XPObject
    {
        public GestioneOrariCircuiti() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public GestioneOrariCircuiti(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        [Persistent("GESTIONEORARI"), System.ComponentModel.DisplayName("Gestione Orari")]
        [Association("GestioneOrari_GestioneOrariCircuiti")]
        //  ******************************[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Immobile.Commesse'),Oid > 0,Immobile.Commesse.CommessePrioritas.Single(Priorita))")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid == '@This.Immobile.Commesse.Oid']")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Immobile.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        //[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        //[Appearance("RdL.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.Abilita.Priorita", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Delayed(true)]
        public GestioneOrari GestioneOrari
        {
            get { return GetDelayedPropertyValue<GestioneOrari>("GestioneOrari"); }
            set { SetDelayedPropertyValue<GestioneOrari>("GestioneOrari", value); }

        }

        [Persistent("GESTIONENUOVIORARI"), System.ComponentModel.DisplayName("Nuova Programmazione Orari")]
        [Association("GestioneNuoviOrari_GestioneOrariCircuiti")]
        //  ******************************[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Immobile.Commesse'),Oid > 0,Immobile.Commesse.CommessePrioritas.Single(Priorita))")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid == '@This.Immobile.Commesse.Oid']")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Immobile.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        //[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        //[Appearance("RdL.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.Abilita.Priorita", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Delayed(true)]
        public GestioneNuoviOrari GestioneNuoviOrari
        {
            get { return GetDelayedPropertyValue<GestioneNuoviOrari>("GestioneNuoviOrari"); }
            set { SetDelayedPropertyValue<GestioneNuoviOrari>("GestioneNuoviOrari", value); }

        }
        private tbcircuiti ftbcircuiti;
        [Persistent("CIRCUITO"), System.ComponentModel.DisplayName("Circuiti")]
        //  **** where c.stagione= '2020 / 2021' -- and c.stato= 'ATT' ***[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Immobile.Commesse'),Oid > 0,Immobile.Commesse.CommessePrioritas.Single(Priorita))")]
        [DataSourceCriteria("stagione ==  '2020 / 2021' And stato == 'ATT'")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Immobile.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        //[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        //[Appearance("RdL.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.Abilita.Priorita", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Delayed(true)]
        public tbcircuiti Circuiti
        {
            get { return GetDelayedPropertyValue<tbcircuiti>("Circuiti"); }
            set { SetDelayedPropertyValue<tbcircuiti>("Circuiti", value); }

        }

    }
}
