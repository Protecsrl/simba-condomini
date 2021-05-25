
using CAMS.Module.Classi;
using CAMS.Module.DBDocument;
using CAMS.Module.DBPlant;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Drawing;
using System.IO;

namespace CAMS.Module.DBSpazi
{
    [DefaultClassOptions, Persistent("PIANI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Piani")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Gestione Spazi")]

//    [RuleCombinationOfPropertiesIsUnique("Piani_Unico_CategoriaPiano-Immobile", DefaultContexts.Save, @"CategoriaPiano;Immobile",
//CustomMessageTemplate = "Attenzione:Piano già esistente per questo Immobile",
//SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    [Appearance("Apparato.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
                FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]
    public class Piani : XPObject
    {

        public Piani() : base() { }
        public Piani(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                Abilitato = FlgAbilitato.Si;

                this.AbilitazioneEreditata = FlgAbilitato.Si;

                if (this.Localies != null)
                {
                    if (this.Localies.Count == 0)
                    {
                        var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

                        var NuovoLocali = xpObjectSpace.CreateObject<Locali>();
                        NuovoLocali.CodDescrizione = String.Format("IP");
                        NuovoLocali.Descrizione = String.Format("Intero Piano");
                        NuovoLocali.Piano = this;
                    }
                }
            }
        }

        private CategoriaPiano fCategoriadiPiano;
        [Persistent("CATEGORIAPIANO"), DevExpress.Xpo.DisplayName("Categoria di Piano")]
        //[Appearance("Piani.CategoriaPiano", Criteria = "Immobile  is null", Enabled = false)]
        [ExplicitLoading()]
        public CategoriaPiano CategoriadiPiano
        {
            get { return fCategoriadiPiano; }
            set { SetPropertyValue<CategoriaPiano>("CategoriadiPiano", ref fCategoriadiPiano, value); }
        }

        //private LocaliCategoria fLocaliCategoria;
        //[Persistent("LOCALICATEGORIA"), DevExpress.Xpo.DisplayName("Locali Categoria")]
        ////  [Appearance("Locali.LocaliCategoria", Enabled = false, Criteria = "!IsNullOrEmpty(Piani)", Context = "DetailView")]
        //[ExplicitLoading()]
        //public LocaliCategoria LocaliCategoria
        //{
        //    get { return fLocaliCategoria; }
        //    set { SetPropertyValue<LocaliCategoria>("LocaliCategoria", ref fLocaliCategoria, value); }
        //}





        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.Piani.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }
        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(7), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        //[Appearance("Locali.CodDescrizione", Enabled = false)]
        [DbType("varchar(100)")]
        public string CodDescrizione
        {
            get { return fCodDescrizione; }
            set { SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value); }
        }
        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

        [Association(@"PIANI_LOCALI", typeof(Locali)), Aggregated, DevExpress.Xpo.DisplayName("Locali")]
        [ExplicitLoading()]
        public XPCollection<Locali> Localies
        {
            get
            {
                return GetCollection<Locali>("Localies");
            }
        }


        private Immobile fImmobile;
        [Association(@"EDIFICIO_PIANI"), Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        [Appearance("Locali.Immobile", Enabled = false, Criteria = "!IsNullOrEmpty(Immobile)", Context = "DetailView")]
        [ExplicitLoading()]
        [VisibleInListView(false)]
        public Immobile Immobile
        {
            get { return fImmobile; }
            set { SetPropertyValue<Immobile>("Immobile", ref fImmobile, value); }
        }
        #region MISURE NETTE E LORDE
        // [PersistentAlias("Localies.Sum(Iif(Oid > 0,1,0))")]
        [PersistentAlias("Localies.Count")]
        [DevExpress.Xpo.DisplayName("n. Locali")]
        //[ModelDefault("DisplayFormat", "{0:N}")]
        //[ModelDefault("EditMask", "N")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int nrLocali
        {
            get
            {
                object tempObject = EvaluateAlias("nrLocali");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else { return 0; }
            }
        }
        
        
        //  misure da Locali --------------------------------
        [PersistentAlias("Localies.Sum(Iif(Area is not null,Area,0))")]
        [DevExpress.Xpo.DisplayName("Area Netta")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Area
        {
            get
            {
                object tempObject = EvaluateAlias("Area");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("Localies.Sum(Iif(Area is not null,Area,0) * Iif(Altezza is not null,Altezza,0))")]
        [DevExpress.Xpo.DisplayName("Volume Netto Locali")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public double Volume
        {
            get
            {
                object tempObject = EvaluateAlias("Volume");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("Localies.Sum(Iif(Perimetro is not null,Perimetro,0) * Iif(Altezza is not null,Altezza,0))")]
        [DevExpress.Xpo.DisplayName("Superficie Pareti Locali")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public double SuperficieParete
        {
            get
            {
                object tempObject = EvaluateAlias("SuperficieParete");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        //-----------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private double fAreaLorda;
        [Persistent("AREALORDA"), DevExpress.Xpo.DisplayName("Area Lorda")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double AreaLorda
        {
            get { return fAreaLorda; }
            set { SetPropertyValue<double>("AreaLorda", ref fAreaLorda, value); }
        }

        private double fAltezzaLorda;
        [Persistent("ALTEZZALORDA"), DevExpress.Xpo.DisplayName("Altezza Lorda")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Altezza tra Interpiano", "Altezza di Piano", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public double AltezzaLorda
        {
            get { return fAltezzaLorda; }
            set { SetPropertyValue<double>("AltezzaLorda", ref fAltezzaLorda, value); }
        }


        private double fPerimetroEsterno;
        [Persistent("PERIMETROESTERNO"), DevExpress.Xpo.DisplayName("Perimetro Esterno")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public double PerimetroEsterno
        {
            get { return fPerimetroEsterno; }
            set { SetPropertyValue<double>("PerimetroEsterno", ref fPerimetroEsterno, value); }
        }



        //[PersistentAlias("Localies.Sum(Iif(Area is not null,Area,0) * Iif(Altezza is not null,Altezza,0))")]
        [PersistentAlias("Iif(AreaLorda is not null,AreaLorda,0) * Iif(AltezzaLorda is not null,AltezzaLorda,0)")]
        [DevExpress.Xpo.DisplayName("Volume Lordo")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public double VolumeLordo
        {
            get
            {
                object tempObject = EvaluateAlias("VolumeLordo");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("Iif(PerimetroEsterno is not null,PerimetroEsterno,0) * Iif(AltezzaLorda is not null,AltezzaLorda,0)")]
        [DevExpress.Xpo.DisplayName("Superficie Parete Esterna")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public double SuperficiePareteEsterna
        {
            get
            {
                object tempObject = EvaluateAlias("SuperficiePareteEsterna");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

   

        #endregion

        #region ABILITAZIONE

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"),
        DisplayName("Attivo")]
        [VisibleInListView(false)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }


        private FlgAbilitato fAbilitazioneEreditata;
        [Persistent("ABILITAZIONETRASMESSA"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo da Gerarchia")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [Appearance("appSkMP.AbilitazioneEreditata", FontColor = "Black", Enabled = false)]
        public FlgAbilitato AbilitazioneEreditata
        {
            get
            {
                return fAbilitazioneEreditata;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("AbilitazioneEreditata", ref fAbilitazioneEreditata, value);
            }
        }

        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [Appearance("piani.Abilita.DateUnService", Criteria = "Abilitato = 'Si'", Enabled = false)]
        [RuleRequiredField("piani.Abilita.Obbligata", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Abilitato] == 'No'")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public DateTime DateUnService
        {
            get
            {
                return fDateUnService;
            }
            set
            {
                SetPropertyValue<DateTime>("DateUnService", ref fDateUnService, value);
            }
        }

        #endregion

        [Association(@"Documenti_Piani", typeof(Documenti)), DevExpress.Xpo.DisplayName("Documenti")]
        [Appearance("Piani.Documentis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }

        [Association(@"Planimetrie_Piani", typeof(Planimetrie)), DevExpress.Xpo.DisplayName("Planimetrie")]
        //[Appearance("Piani.Planimetrie.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        public XPCollection<Planimetrie> Planimetries
        {
            get
            {
                return GetCollection<Planimetrie>("Planimetries");
            }
        }


        #region File        DWG    file autocad

        private string fDwgPathName;
        [Persistent("DWGNAMEPATH"), Size(50), DevExpress.Xpo.DisplayName("Dwg Path Name")]
        [DbType("varchar(50)")]
        [VisibleInListView(false)]
        public string DwgPathName
        {
            get { return fDwgPathName; }
            set { SetPropertyValue<string>("DwgPathName", ref fDwgPathName, value); }
        }

        [Persistent("DWGFILE"), DisplayName("Dwg File Name")]
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [Delayed(true)]
        public FileData DwgFile
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("DwgFile");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("DwgFile", value);
                //DwgPathName = value.FileName;
            }
        }
        #endregion
        #region Salvataggio FILE    DwgName

        private FileData GetFilePianoDWG()
        {
            string PercorsoFile = GetPercorsoFileDWG();
            // string PathFileName = string.Format(@"{0}\App_Data\{1}", PercorsoFileMaster, this.ImageName.FileName); // @"C:\115\SNAM\OUTPMP\PMP_xls_SN_RM_14_GENNAIO_14_(A).xls";
            string PathFileName = string.Format(@"{0}\{1}", PercorsoFile, this.DwgPathName); // percorso del file , path del database del file
            FileData DwgFile_tmp = new FileData(this.Session);
            if (System.IO.File.Exists(PathFileName))
            {
                System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                DwgFile_tmp.LoadFromStream(PathFileName, excelStream);
                DwgFile_tmp.FileName = System.IO.Path.GetFileName(PathFileName);
                excelStream.Close();
                excelStream.Dispose();
            }
            else
            {
                DwgFile_tmp = GetPropertyValue<FileData>("DwgFile");

            }
            return DwgFile_tmp;
        }


        private void SaveFilePianoDWG()
        {
            string PercorsoFile = GetPercorsoFileDWG();
            string PathFileName = string.Format(@"{0}\{1}", PercorsoFile, this.DwgPathName); // percorso del file , path del database del file
            if (!System.IO.File.Exists(PathFileName))
            {
                System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
                this.DwgFile.SaveToStream(excelStream);
                excelStream.Close();
                excelStream.Dispose();
            }
            else
            {
                try
                {
                    System.IO.File.Delete(PathFileName);  // se il file è occupato allora non si puo andare avanti PAM_xls_EDI_AA_SIRR2.xls
                    System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
                    this.DwgFile.SaveToStream(excelStream);
                    excelStream.Close();
                    excelStream.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        private string GetPercorsoFileDWG()
        {
            string Path = string.Format(@"C:\AssemblaPRT15\CAMS\CAMS.Web\CAD\Doc\");
            if (!Directory.Exists(Path))
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(Path));
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }

            if (this.Immobile.Contratti != null)
            {
                Path = string.Format(@"C:\AssemblaPRT15\CAMS\CAMS.Web\CAD\Doc\{0}", this.Immobile.Contratti.Oid.ToString());
                if (!Directory.Exists(Path))
                {
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Path);
                        Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(Path));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }
                }
            }
            return Path;
        }


        #endregion

        protected override void OnSaving()
        {
            base.OnSaving();

            if (Session.IsObjectToSave(this))
                if (this.DwgFile != null)
                { this.DwgPathName = this.DwgFile.FileName; }
            // se la stanza non c'e' creo stanza 
            //if (Session.IsObjectToSave(this))
            //{
            //    if (this.Localies != null)
            //    {
            //        if (this.Localies.Count == 0)
            //        {
            //            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

            //            var NuovoLocali = xpObjectSpace.CreateObject<Locali>();
            //            NuovoLocali.CodDescrizione = String.Format("IP");
            //            NuovoLocali.Descrizione = String.Format("Intero Piano");
            //            NuovoLocali.Piano = this;
            //        }
            //    }

            //}
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!Session.IsNewObject(this))
            {
                //if (this.DwgFile != null)
                    //SaveFilePianoDWG();
            }
       
        }


        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public uint CambiatoFileDWG { get; set; }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            // base.IsLoading
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                // SEMPRE NUOVO E CAMBIO
                if (IsInvalidated)
                    return;

                if (!this.IsLoading)
                {
                    if (newValue != null && propertyName == "Abilitato")
                    {
                        FlgAbilitato newV = (FlgAbilitato)(newValue);
                        if (newV == FlgAbilitato.Si)
                        {
                            this.DateUnService = DateTime.MinValue;
                        }

                        foreach (Locali lo in this.Localies)
                        {
                            lo.AbilitazioneEreditata = newV;
                        }

                    }
                    // sempre   
                    if (newValue != null && propertyName == "DwgFile") //this.DwgFile
                    {
                        FileData newV = (FileData)(newValue);
                        FileData oldV = null;
                        //if (oldValue != null)
                        //    oldV = (double)(oldValue);

                        if (newV != null)
                        {
                            this.DwgPathName = this.DwgFile.FileName;
                            CambiatoFileDWG = 1;
                        }
                    }
                }
                // SOLO SE NUOVO
                if (this.Oid == -1)
                {
                    //if (newValue != null && propertyName == "Categoria")
                    //{
                    //    int newOid = ((DevExpress.Xpo.XPObject)(newValue)).Oid;
                    //    //if (newOid != 4)
                    //    //{
                    //    //    this.TipoIntervento = Session.GetObjectByKey<TipoIntervento>(0);
                    //    //}
                    //}
                    //

                }
                // SOLO SE esistente gia'
                if (this.Oid > 1)
                {
                    //if (newValue != null && propertyName == "UltimoStatoSmistamento")
                    //{
                    //    if (newValue != oldValue && newValue != null)
                    //    {

                    //    }
                    //}
                }

            }
        }


    }
}

