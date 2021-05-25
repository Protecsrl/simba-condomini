using CAMS.Module.DBTask;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("CONDUTTORI")]
    [System.ComponentModel.DefaultProperty("RisorseTeam")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Conduttori")]
    [ImageName("Action_EditModel")]
    //[Appearance("Impianto.inCreazione.noVisibile", TargetItems = "APPARATOes;ControlliNormativis;DestinatariControlliNormativis;Documentis;ImpiantoMansioneCaricos;RegistroCostis;RegMisures;VisibileListaAppInseribili;", Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("Impianto.dopoCreazione.noEdit", TargetItems = "Immobile", Criteria = @"Oid != -1", Enabled = false)]
    [RuleCombinationOfPropertiesIsUnique("Unique.Conduttori.Descrizione", DefaultContexts.Save, "Immobile,RisorseTeam")]
    //[DeferredDeletion(true)]
    [NavigationItem("Patrimonio")]
    public class Conduttori : XPObject
    {
        public Conduttori() : base() { }
        public Conduttori(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                //if (this.Immobile != null)
                //{
                //    this.Descrizione = "Nuovo Impiant";
                //    this.CodDescrizione = this.Immobile.CodDescrizione + "_";
                //}
                //this.Abilitato = FlgAbilitato.Si;
            }


        }

        private Immobile fImmobile;
        [Association(@"Edificio_TRisorseConduttori")]
        [Persistent("Immobile"), DevExpress.Xpo.DisplayName("Immobile")]
        [RuleRequiredField("Conduttori.Immobile", DefaultContexts.Save, "L'immobile è un campo obbligatorio")]
        //[ExplicitLoading()]
        [ImmediatePostData(true)]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue<Immobile>("Immobile", ref fImmobile, value);
            }
        }

        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        [Persistent("RISORSATEAM"), System.ComponentModel.DisplayName("Team")]
        //[DataSourceProperty("ListaFiltraComboRisorseTeam")]
        [Appearance("Conduttori.RisorseTeam.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RisorseTeam)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [RuleRequiredField("Conduttori.RisorseTeam", DefaultContexts.Save, "RisorseTeam è un campo obbligatorio")]
        //[ExplicitLoading()]
        public RisorseTeam RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
                //OnChanged("Mansione");
            }
        }

        //[PersistentAlias("Iif(AssRisorseTeam.Count = 0 , 'NA', " +    "Iif(AssRisorseTeam.Count = 1 , RisorsaCapo.Mansione.Descrizione, 'Misto'))")]
        [PersistentAlias("Iif(RisorseTeam is null,null, RisorseTeam.RisorsaCapo.Mansione.Descrizione)"), DisplayName("Mansione")]
        public string Mansione
        {
            get
            {
                var tempObject = EvaluateAlias("Mansione");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }


        [NonPersistent]
        private XPCollection<RisorseTeam> fListaFiltraComboRisorseTeam;
        [MemberDesignTimeVisibility(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RisorseTeam> ListaFiltraComboRisorseTeam
        {
            get
            {
                if (fListaFiltraComboRisorseTeam == null)
                    if (this.Oid > 0 || this.Oid == -1)
                    {
                        GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.And);

                        int oidareapolo = this.Immobile.Contratti.AreaDiPolo.Oid;
                        string ParCriteria = string.Format("RisorsaCapo.CentroOperativo.AreaDiPolo.Oid = {0}", oidareapolo.ToString());

                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(ParCriteria));

                        fListaFiltraComboRisorseTeam = new XPCollection<RisorseTeam>(Session);
                        if (!string.IsNullOrEmpty(criteriaOP2.ToString()))
                        {
                            fListaFiltraComboRisorseTeam.Criteria = criteriaOP2;
                        }
                        else
                        {
                            ParCriteria = "Oid=0";// non vede un cazzo
                            fListaFiltraComboRisorseTeam.Criteria = CriteriaOperator.Parse(ParCriteria);
                        }

                        List<Conduttori> giaInseriti = this.Immobile.Conduttoris.ToList();
                        for (int i = 0; i < giaInseriti.Count; i++)
                        {
                            try
                            {
                                string add = string.Format("Oid != {1}", giaInseriti[i], giaInseriti[i].RisorseTeam.Oid);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(add));
                            }
                            catch
                            {
                            }
                        }

                        if (!string.IsNullOrEmpty(criteriaOP2.ToString()))
                        {
                            fListaFiltraComboRisorseTeam.Criteria = criteriaOP2;
                        }
                        OnChanged("ListaFiltraComboRisorseTeam");

                    }
                return fListaFiltraComboRisorseTeam;
            }
        }




    }
}

