using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using System.ComponentModel;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("COMMESSATRIAGECC")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Triage")]
    //[Indices("AbilitazioneEreditata;Abilitato")]
    //[Appearance("Apparato.inCreazione.noVisibile", TargetItems = "Name;Children;Parent;Apparatis;ApparatoPadre;AppSchedaMpes;Documentis;ControlliNormativis;RegMisureDettaglios;MasterDettaglios", Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    //[RuleCombinationOfPropertiesIsUnique("Unique.Apparato.Descrizione", DefaultContexts.Save, "Impianto,CodDescrizione, Descrizione")]
    [ImageName("LoadPageSetup")]
    [NavigationItem(true)]
    public abstract class CCTriage: XPObject, ITreeNode
    {
        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        public CCTriage(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                
                //OidApparatoSostituito = 0;
            }
        }

              private string name;
        protected abstract ITreeNode Parent { get; }
        protected abstract IBindingList Children { get; }
    
        #region Struttura  Intefaccia ad Albero
      private string fName;
          [VisibleInDetailView(false)]
        public string Name
        {
            get
            {
                return fName;
            }
            set
            {
                SetPropertyValue("Name", ref fName, value);
            }
        }
    
        #endregion
          #region ITreeNode
    
          
          IBindingList ITreeNode.Children
          {
              get
              { return Children; }
          }
          string ITreeNode.Name
          {
              get
              { return Name; }
          }
          ITreeNode ITreeNode.Parent
          {
              get
              { return Parent; }
          }
          #endregion
    


    }
}
