using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.IO;

namespace CAMS.Module.DBDocument
{
    [DefaultClassOptions, Persistent("DOCPLANIMETRIE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [Appearance("Planimetrie.VidiFile", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "Planimetrie_DetailView_VediFile", Criteria = "not(IsVisibleFileEdit)",
                                                                                            Priority = 1, FontColor = "Black", Enabled = false)]
    [Appearance("Planimetrie.VidiFile.hide", AppearanceItemType = "ViewItem", TargetItems = "FileDWFFullName", Context = "Planimetrie_DetailView_VediFile", Criteria = "not(IsVisibleFileEdit)",
                                                                                        Priority = 2, Visibility = ViewItemVisibility.Hide)]

    [ImageName("Demo_String_InSpecialFormat_Properties")]
    [NavigationItem("Navigazione Documenti")]
    public class Planimetrie : XPObject
    {
        public Planimetrie()
            : base()
        {
        }
        public Planimetrie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("Planimetrie.Descrizione", DefaultContexts.Save, "la Descrizione è un campo obbligatorio")]
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


        [Association(@"Planimetrie_Servizio"), Persistent("SERVIZIO"), DisplayName("Servizio")]
        //[Appearance("Planimetrie.Impianto", Visibility = ViewItemVisibility.Hide)]
        [DataSourceCriteria(@"Immobile == '@This.Immobile'")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Servizio");
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
            }
        }

        private Immobile fImmobile;
        [Association(@"Planimetrie_Immobile"), Persistent("IMMOBILE"), DisplayName("Immobile")]
        [ExplicitLoading()]
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

        [Association(@"Planimetrie_Piani"), Persistent("PIANO"), DisplayName("Piani")]
        [DataSourceCriteria(@"Immobile == '@This.Immobile'")]
        //[Appearance("Documenti.Piani", Visibility = ViewItemVisibility.Hide)]
        [Delayed(true)]
        [ExplicitLoading()]
        public Piani Piano
        {
            get
            {
                return GetDelayedPropertyValue<Piani>("Piano");
            }
            set
            {
                SetDelayedPropertyValue<Piani>("Piano", value);
            }
        }


        private TipoPlanimetria FTipoPlanimetria;
        [Persistent("TIPOPLANIMETRIA")]
        // [Appearance("RegMisure.Abilita.Impianto", Criteria = "(not (Immobile is null)) or (not (Impianto is null)) or (not (RdL is null)) or (not (Apparato is null))", Enabled = false)]
        public TipoPlanimetria TipoPlanimetria
        {
            get
            {
                return FTipoPlanimetria;
            }
            set
            {
                SetPropertyValue<TipoPlanimetria>("TipoPlanimetria", ref FTipoPlanimetria, value);
            }
        }


        #region DWF FIle
        [NonPersistent]
        [System.ComponentModel.Browsable(true)]
        bool IsVisibleFileEdit { get; set; }

        private FileData fFileDWF;
        [NonPersistent]
        [DisplayName("File")]
        [Appearance("planimetrie.FIleDWF", Criteria = "not(IsVisibleFileEdit)", Visibility = ViewItemVisibility.Hide)]
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [FileTypeFilter("DocumentFiles", 1, "*.dwf", "*.DWF")]
        //[FileTypeFilter("AllFiles", 2, "*.*")]
        [ImmediatePostData(true)]
        public FileData FileDWF
        {
            get { return fFileDWF; }
            set { SetPropertyValue("FileDWF", ref fFileDWF, value); }
        }

        [Persistent("DATASHEET"), DevExpress.Xpo.DisplayName("File upload")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData DataSheet
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("DataSheet");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("DataSheet", value);
            }
        }


        private string fFileDWFFullName;
        [Persistent("FILEDWFNAMEPATH"), Size(150), DevExpress.Xpo.DisplayName("Nome File")]
        [DbType("varchar(150)")]
        [VisibleInListView(false)]
        [System.ComponentModel.Browsable(false)]
        public string FileDWFFullName
        {
            get { return fFileDWFFullName; }
            set { SetPropertyValue<string>("FileDWFFullName", ref fFileDWFFullName, value); }
        }

        [PersistentAlias("FileDWFFullName")] //"Iif(FileDWFFullName is not null,FileDWFFullName,null)"
        [DevExpress.Xpo.DisplayName("Nome file")]
        public string DWFFNameFile
        {
            get
            {
                object tempObject = EvaluateAlias("DWFFNameFile");
                if (tempObject != null)
                {
                    string fullpath = (string)tempObject.ToString();                    //string fileName = @"C:\mydir\myfile.ext";                    //string path = @"C:\mydir\";                    //string result;
                    return Path.GetFileName(fullpath);                              // ci metto solo il nome file
                }
                else { return null; }
            }
        }

        private string fOBJDWF;
        [Persistent("OBJDWFNAMEPATH"), Size(150), DevExpress.Xpo.DisplayName("Documento")]
        [DbType("varchar(150)")]
        [VisibleInListView(false)]
        public string OBJDWF
        {
            get { return fOBJDWF; }
            set { SetPropertyValue<string>("OBJDWF", ref fOBJDWF, value); }
        }

        //private string _UrlDWF = string.Empty;
        //[NonPersistent, DevExpress.Xpo.DisplayName(@"DWF->")]
        ////   [Appearance("Planimetria.Url.visualizza", Criteria = "InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
        //[EditorAlias("HperLinkDWF")]
        //public string UrlDWF
        //{
        //    get
        //    {
        //        object tempFile = EvaluateAlias("DWFFNameFile");
        //        if (tempFile != null)
        //        {
        //            return String.Format(@"/CAD/WebFormCAD.aspx?{0}={1}&{2}={3}",
        //                "File", tempFile.ToString(),
        //                 "File", tempFile.ToString());
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        SetPropertyValue("UrlDWF", ref _UrlDWF, value);
        //    }
        //}

        #endregion


        #region Salvataggio FILE    DwgName

        public FileData GetFilePiano(string PathFileName)
        {
            //FileData f = this.GetPropertyValue<FileData>(Tipofile);
            //if (f == null)
            //    return null;

            //string FullName = this.GetPropertyValue<string>(Tipofile );// ""DwfFileFName
            //if (FullName == null)
            //    return null;

            //string PercorsoFile = GetPercorsoFile();            // string PathFileName = string.Format(@"{0}\App_Data\{1}", PercorsoFileMaster, this.ImageName.FileName); // @"C:\115\SNAM\OUTPMP\PMP_xls_SN_RM_14_GENNAIO_14_(A).xls";
            //string PathFileName = string.Format(@"{0}\{1}", PercorsoFile, FullName); // percorso del file , path del database del file
            FileData DwgFile_tmp = new FileData(this.Session);
            if (System.IO.File.Exists(PathFileName))
            {
                System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                DwgFile_tmp.LoadFromStream(PathFileName, excelStream);
                DwgFile_tmp.FileName = System.IO.Path.GetFileName(PathFileName);
                excelStream.Close();
                excelStream.Dispose();
            }
            //else
            //{
            //    DwgFile_tmp = this.GetPropertyValue<FileData>(Tipofile);
            //}
            return DwgFile_tmp;
        }


        public string GetFilebyFileData(FileData f)
        {
            if (f == null)
                return "";

            string FullName = f.FileName;
            if (FullName == null)
                return "";

            try
            {
                string PercorsoFile = GetPercorsoFile(); // Request.PhysicalApplicationPath - C:\inetpub\wwwroot\EAMS_SL3_03112017_6\
                string PathFileName = string.Format(@"{0}\{1}", PercorsoFile, FullName);
                if (System.IO.File.Exists(PathFileName))
                {
                    System.IO.File.Delete(PathFileName);  // se il file è occupato allora non si puo andare 
                }
                System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
                f.SaveToStream(excelStream);
                excelStream.Close();
                excelStream.Dispose();

                return PathFileName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            //if (!System.IO.File.Exists(PathFileName))
            //{
            //    System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
            //    f.SaveToStream(excelStream);
            //    excelStream.Close();
            //    excelStream.Dispose();
            //}
            //else
            //{
            //    try
            //    {
            //        System.IO.File.Delete(PathFileName);  // se il file è occupato allora non si puo andare 
            //        System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
            //        f.SaveToStream(excelStream);
            //        excelStream.Close();
            //        excelStream.Dispose();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception(e.Message);
            //    }
            //}
        }


        private void SaveFilePiano(FileData f)
        {
            //FileData f = this.GetPropertyValue<FileData>(Tipofile);
            if (f == null)
                return;

            string FullName = f.FileName;
            if (FullName == null)
                return;

            string PercorsoFile = GetPercorsoFile(); // Request.PhysicalApplicationPath - C:\inetpub\wwwroot\EAMS_SL3_03112017_6\
            string PathFileName = string.Format(@"{0}\{1}", PercorsoFile, FullName); // percorso del file , path del database del file
            if (!System.IO.File.Exists(PathFileName))
            {
                System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
                f.SaveToStream(excelStream);
                excelStream.Close();
                excelStream.Dispose();
            }
            else
            {
                try
                {
                    System.IO.File.Delete(PathFileName);  // se il file è occupato allora non si puo andare 
                    System.IO.Stream excelStream = new System.IO.FileStream(PathFileName, System.IO.FileMode.Create);
                    f.SaveToStream(excelStream);
                    excelStream.Close();
                    excelStream.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        private string GetPercorsoFile()
        {

            string WebCADDocPath = CAMS.Module.Classi.SetVarSessione.WebCADPath;//CAMS.Module.Classi.SetVarSessione.WebCADPath;(<-- FINO AL 15.07.2020) System.Web.Hosting.HostingEnvironment.MapPath("/CAD/Doc/");	//"C:\\AssemblaPRT15\\CAMS\\CAMS.Web\\CAD\\Doc\\"	string
             //string WebCADDocPath = this.OBJDWF;
            string path1 = WebCADDocPath;
            if (!Directory.Exists(path1))
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(path1);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path1));
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
            if (this.Immobile != null)
            {
                if (this.Immobile.Contratti != null)
                {
                    path1 = string.Format(@"{0}{1}", WebCADDocPath, this.Immobile.Contratti.Oid.ToString());
                    if (!Directory.Exists(path1))
                    {
                        try
                        {
                            DirectoryInfo di = Directory.CreateDirectory(path1);
                            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path1));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
                    }
                }
            }
            return path1;
        }


        #endregion

        protected override void OnLoading()
        {
            base.OnLoading();
            if (!Session.IsNewObject(this))
            {
                //if (this.DwgFile != null)
                //    SaveFilePianoDWG();
            }
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //if (!Session.IsNewObject(this))
            //{
            //    if (this.DwfFileFName != null)
            //    {
            //        this.Document1 = GetFilePiano(this.DwfFileFName);
            //    }
            //}
        }


        protected override void OnSaving()
        {
            base.OnSaving();

            if (Session.IsObjectToSave(this))
            {
                if (this.FileDWF != null)
                {
                    if (this.DWFFNameFile != this.FileDWF.FileName)
                    {
                        //string path = GetPercorsoFile();
                        //this.FileDWFFullName = Path.Combine(path, this.FileDWF.FileName);
                        //SaveFilePiano(this.FileDWF);
                    }
                }
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!Session.IsNewObject(this))
            {
                //if (this.DwgFile != null)
                //    SaveFilePianoDWG();
            }
        }


        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public uint CambiatoFileDWG { get; set; }



        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "FileDWF")
                {
                    FileData newFile = (FileData)(newValue);
                    if (newValue != oldValue || newValue != null)
                    {
                        //string path = GetPercorsoFile();
                        //this.FileDWFFullName = Path.Combine(path, newFile.FileName);

                        //SaveFilePiano(this.FileDWF);
                    }
                }
            }
        }


    }
}
