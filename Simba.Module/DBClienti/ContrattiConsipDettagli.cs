using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
//using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FileSystemData.BusinessObjects;
using System;
//using System.ComponentModel;
 
namespace CAMS.Module.DBClienti
{
    [DefaultClassOptions, Persistent("CONTRATTICONSIPDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettagli Registro Consip")]
    [ImageName("PackageProduct")]
    [FileAttachment("FileSystem")]
    //[NavigationItem(false)]
    public class ContrattiConsipDettagli : XPObject
    {
        public ContrattiConsipDettagli() : base() { }
        public ContrattiConsipDettagli(Session session) : base(session) { }
        public override void AfterConstruction() {
            base.AfterConstruction(); 
        if(this.FileSystem == null) 
            {
                //this.FileSystem.SubPath = "SL3";
            }
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                if (this.Oid == -1)
                {
                    #region in fase di creazione
                    string Sw_propertyName = propertyName;
                    switch (Sw_propertyName)
                    {
                        case "FileSystem":
                            if (newValue != oldValue && newValue != null)
                            {
                                FileSystemStoreObject _FileSystemStoreObject = (( FileSystemStoreObject)(newValue));
                                _FileSystemStoreObject.SubPath = CAMS.Module.Classi.SetVarSessione.PhysicalSubPathFileData;
                            }
                            break; 
                    }
                    #endregion
                }        
            }
        }





        private const string DateAndTimeOfDayEditMask = CAMSEditorCostantFormat.Data_e_nomeGG_EditMask;
        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"),   DisplayName("Descrizione")]
        [DbType("varchar(100)")]
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

        private ContrattiConsipRegistro fContrattiConsipRegistro;
        [Persistent("CONTRATTICONSIPREG"), DisplayName("Dettagli Contratto Consip")]
        [Association(@"RegistroContrattiConsip_Dettagli")]
        [ExplicitLoading]
        public ContrattiConsipRegistro ContrattiConsipRegistro
        {
            get
            {
                return fContrattiConsipRegistro;
            }
            set
            {
                SetPropertyValue<ContrattiConsipRegistro>("ContrattiConsipRegistro", ref fContrattiConsipRegistro, value);
            }
        }


        private ContrattiConsipStato fContrattiConsipStato;
        [Persistent("CONTRATTICONSIPSTATO"), DisplayName("Stato")]
        [ExplicitLoading]
        public ContrattiConsipStato ContrattiConsipStato
        {
            get
            {
                return fContrattiConsipStato;
            }
            set
            {
                SetPropertyValue<ContrattiConsipStato>("ContrattiConsipStato", ref fContrattiConsipStato, value);
            }
        }

        //private string fPath;
        //[Size(4000), Persistent("PATH"), DisplayName("Path")]
        //[DbType("varchar(4000)")]
        //[Appearance("consipdet.path", Criteria = "Utente != 'Admin'", Visibility = ViewItemVisibility.Hide)]
        //public string Path
        //{
        //    get
        //    {
        //        return fPath;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Path", ref fPath, value);
        //    }
        //}

        private string fPathNuovo;
        [Size(4000), Persistent("PATHNUOVO"), DisplayName("Pathnuova")]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(4000)")]
        public string PathNuovo
        {
            get
            {
                return fPathNuovo;
            }
            set
            {
                SetPropertyValue<string>("PathNuovo", ref fPathNuovo, value);
            }
        }

        //private string fPathPrima;
        //[Size(4000), Persistent("PATHPRIMA"), DisplayName("Pathrpima")]
        //[System.ComponentModel.Browsable(false)]
        //[DbType("varchar(4000)")]
        //public string PathPrima
        //{
        //    get
        //    {
        //        return fPathPrima;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("PathPrima", ref fPathPrima, value);
        //    }
        //}


        private string fNomeFileNuovo;
        [Size(4000), Persistent("NOMEFILENUOVO"), DisplayName("NomeFileNuovo")]
        [DbType("varchar(4000)")]
        [System.ComponentModel.Browsable(false)]
        public string NomeFileNuovo
        {
            get
            {
                return fNomeFileNuovo;
            }
            set
            {
                SetPropertyValue<string>("NomeFileNuovo", ref fNomeFileNuovo, value);
            }
        }

        //private FileData fFileTMP;
        //[NonPersistent]
        //[DisplayName("Filetmp")]
        //[Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[FileTypeFilter("DocumentFiles", 1, "*.*", "*.*")]        //[FileTypeFilter("AllFiles", 2, "*.*")]
        //[ImmediatePostData(true)]
        //public FileData FileTMP
        //{
        //    get { return fFileTMP; }
        //    set { SetPropertyValue("FileTMP", ref fFileTMP, value); }
        //}

        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
         [DisplayName("File")]
        public FileSystemStoreObject FileSystem
        {
            get { return GetPropertyValue<FileSystemStoreObject>("FileSystem"); }
            set { SetPropertyValue<FileSystemStoreObject>("FileSystem", value); }
        }


        private DateTime fData;
        [Persistent("DATA"),DisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("SchedeMPOwner.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }


        private  TipoRiservato fRiservato;
        [Persistent("RISERVATO"),        DisplayName("Riservato")]
        public TipoRiservato Riservato
        {
            get
            {
                return fRiservato;
            }
            set
            {
                SetPropertyValue<TipoRiservato>("Riservato", ref fRiservato, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(1000),
        DisplayName("Utente")]
        [DbType("varchar(1000)")]
        //  [Appearance("SchedeMPOwner.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("SchedeMPOwner.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }


        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }

    }
}

