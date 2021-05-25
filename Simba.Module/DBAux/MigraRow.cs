using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
//using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FileSystemData.BusinessObjects;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
//namespace CAMS.Module.DBAux
//{
//    class MigraRow
//    {
//    }
//}
//using System.ComponentModel; 
namespace CAMS.Module.DBAux
{
    [DefaultClassOptions, Persistent("MIGRAROW")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Migra Row")]
    [ImageName("PackageProduct")]
    //[NavigationItem(false)]
    public class MigraRow : XPObject
    {
        public MigraRow() : base() { }
        public MigraRow(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        private const string DateAndTimeOfDayEditMask = CAMSEditorCostantFormat.Data_e_nomeGG_EditMask;

        private int _RifOid;
        [Persistent("RifOid"), DisplayName("RifOid")]
        public int RifOid
        {
            get { return _RifOid; }
            set { SetPropertyValue<int>("RifOid", ref _RifOid, value); }
        }

        private string _oraValore;
        [Persistent("ora_Valore"), DisplayName("ora_Valore")]
        [Size(SizeAttribute.Unlimited)]
        //[DbType("CLOB")]      //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string oraValore
        {
            get { return _oraValore; }
            set { SetPropertyValue<string>("oraValore", ref _oraValore, value); }
        }

        private string _ms_Valore;
        [Persistent("ms_Valore"), DisplayName("ms_Valore")]
        [Size(SizeAttribute.Unlimited)]
        //[DbType("CLOB")]      //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ms_Valore
        {
            get { return _ms_Valore; }
            set { SetPropertyValue<string>("ms_Valore", ref _ms_Valore, value); }
        }

        private string _tipo;
        [Size(100), Persistent("tipo"), DisplayName("tipo")]
        [DbType("varchar(100)")]
        public string tipo
        {
            get { return _tipo; }
            set { SetPropertyValue<string>("tipo", ref _tipo, value); }
        }

        private string _Colonna;
        [Size(100), Persistent("Colonna"), DisplayName("Colonna")]
        [DbType("varchar(100)")]
        public string Colonna
        {
            get { return _Colonna; }
            set { SetPropertyValue<string>("Colonna", ref _Colonna, value); }
        }

        private string _Tab;
        [Size(100), Persistent("Tab"), DisplayName("Tab")]
        [DbType("varchar(100)")]
        public string Tab
        {
            get { return _Tab; }
            set { SetPropertyValue<string>("Tab", ref _Tab, value); }
        }

        private DateTime fData;
        [Persistent("Data"), DisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [System.ComponentModel.Browsable(false)]
        public DateTime Data
        {
            get { return fData; }
            set { SetPropertyValue<DateTime>("Data", ref fData, value); }
        }

        private string _Note;
        [Size(100), Persistent("Note"), DisplayName("Note")]
        [DbType("varchar(4000)")]
        public string Note
        {
            get { return _Note; }
            set { SetPropertyValue<string>("Note", ref _Note, value); }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.ms_Valore);
        }

    }
}

