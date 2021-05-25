using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.Metadata;
using System.Drawing;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("STATOSMISTAMENTO")]
    [System.ComponentModel.DefaultProperty("SSmistamento")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Smistamento")]
    [ImageName("Demo_ListEditors_Scheduler_Filter")]
    [NavigationItem("Ticket")]
    public class StatoSmistamento : XPObject
    {
        public StatoSmistamento()
            : base()
        {
        }
        public StatoSmistamento(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fStatoSmistamento;
        [Size(30), Persistent("STATOSMISTAMENTO"), DisplayName("Stato Smistamento")]
        [DbType("varchar(30)")]
        public string SSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<string>("SSmistamento", ref fStatoSmistamento, value);
            }
        }

        //[ToolTip("{SSmistamento}")] 
        //private Image fIcona;
        [Persistent("ICONA"), DisplayName("Icona")]
        //[ValueConverter(typeof(ImageValueConverter))]
        //[Size(-1)]       
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        [VisibleInListViewAttribute(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 20)]
        [ToolTip("{SSmistamento}")]  //  [ToolTip("{SSmistamento}")]
        [Delayed(true)]
        public byte[] Icona//Image Icona
        {
            //get { return GetDelayedPropertyValue<Image>("Icona"); }
            //set { SetDelayedPropertyValue<Image>("Icona", value); }
            get { return GetDelayedPropertyValue<byte[]>("Icona"); }
            set { SetDelayedPropertyValue<byte[]>("Icona", value); }
        }

        private string fFase;
        [Persistent("FASE"),
        DisplayName("Stato Fase")]
        public string Fase
        {
            get
            {
                return fFase;
            }
            set
            {
                SetPropertyValue<string>("Fase", ref fFase, value);
            }
        }

        //[Association(@"REGISTROSMISTAMENTODETTRefSTATOSMISTAMENTO", typeof(RegistroSmistamentoDett)),
        //DisplayName("Dettaglio")]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RegistroSmistamentoDett> REGISTROSMISTAMENTODETTs
        //{
        //    get
        //    {
        //        return GetCollection<RegistroSmistamentoDett>("REGISTROSMISTAMENTODETTs");
        //    }
        //}

        private int fOrdine;
        [Persistent("ORDINE"), DisplayName("Ordine"), MemberDesignTimeVisibility(false)]
        public int Ordine
        {
            get
            {
                return fOrdine;
            }
            set
            {
                SetPropertyValue<int>("Ordine", ref fOrdine, value);
            }
        }

        #region associazione 
        [Association(@"StatoSmistamento_rel_SOperativo", typeof(StatoSmistamento_SOperativo)), DisplayName("SOperativo")]
        //  [Appearance("RegRdL.AutorizzazioniRegistroRdLs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or AutorizzazioniRegistroRdLs.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<StatoSmistamento_SOperativo> StatoSmistamento_SOperativos
        {
            get
            {
                return GetCollection<StatoSmistamento_SOperativo>("StatoSmistamento_SOperativos");
            }
        }

        [Association(@"StatoSmistamento_Combo", typeof(StatoSmistamentoCombo)), Aggregated, DisplayName("SSmistamento in Combo")]
        //  [Appearance("RegRdL.AutorizzazioniRegistroRdLs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or AutorizzazioniRegistroRdLs.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<StatoSmistamentoCombo> StatoSmistamentoCombos
        {
            get
            {
                return GetCollection<StatoSmistamentoCombo>("StatoSmistamentoCombos");
            }
        }

        [Association(@"StatoSmistamento_rel_SOperativoSO", typeof(StatoSmistamento_SOperativoSO)), DisplayName("SOperativo in SO")]
        //  [Appearance("RegRdL.AutorizzazioniRegistroRdLs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or AutorizzazioniRegistroRdLs.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<StatoSmistamento_SOperativoSO> StatoSmistamento_SOperativoSOs
        {
            get
            {
                return GetCollection<StatoSmistamento_SOperativoSO>("StatoSmistamento_SOperativoSOs");
            }
        }
        #endregion



        //public void Dispose()
        //{
        //    if (fIcona != null)
        //    {
        //        fIcona.Dispose();
        //        fIcona = null;
        //    }
        //}
    }
}
