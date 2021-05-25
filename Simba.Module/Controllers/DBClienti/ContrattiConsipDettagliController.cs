using CAMS.Module.Classi;
using CAMS.Module.DBClienti;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FileSystemData.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace CAMS.Module.Controllers.DBClienti
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ContrattiConsipDettagliController : ViewController
    {
        public ContrattiConsipDettagliController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        //private IObjectSpace xpObjectSpace;

        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            if (xpObjectSpace != null)
                scaFiltroStatoDocumento.Items.Clear();

            string a = "";
            string id0 = "0";

            if (View is ListView)
            {
                ListView Lv = (ListView)View;
                if (Lv.Id == "ContrattiConsipRegistro_ContrattiConsipDettaglis_ListView")
                {
                    //var ab =   ((ListView)View).ObjectSpace.GetObjectsQuery<ContrattiConsipDettagli>();
                    if (Lv.CollectionSource.GetCount() > 0)
                    {
                        List<ContrattiConsipStato> List = xpObjectSpace.GetObjects<ContrattiConsipStato>().ToList();
                        foreach (ContrattiConsipStato obj in List)
                        {
                            scaFiltroStatoDocumento.Items.Add((new ChoiceActionItem()
                            {
                                Id = obj.Oid.ToString(),
                                Caption = obj.Descrizione,
                                Data = obj.Oid.ToString()
                            }));
                        }
                        scaFiltroStatoDocumento.Items.Add((new ChoiceActionItem()
                        {

                            //Id = (List.Count() + 1).ToString(), 
                            Id = id0.ToString(),
                            Caption = "Tutto",
                            Data = id0.ToString()
                        }));
                    }
                }
                //--------------------
                if (!SetVarSessione.IsAdminRuolo)
                    saCaricadaDirectory.Active.SetItemValue("Active", false);
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void scaFiltroStatoDocumento_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (View is ListView)
            {
                if (((ListView)View).Id.Equals("ContrattiConsipRegistro_ContrattiConsipDettaglis_ListView"))
                {
                    ListView lv = (ListView)View;
                    if (e.SelectedChoiceActionItem.Data.ToString() != "0")
                    {
                        lv.CollectionSource.SetCriteria("criteriaRicercaTestuale", "ContrattiConsipStato.Oid = " + e.SelectedChoiceActionItem.Data.ToString());
                    }
                    else
                    {
                        lv.CollectionSource.SetCriteria("criteriaRicercaTestuale", "ContrattiConsipStato.Oid > 0");

                    }

                }
            }
        }

        private void saCaricadaDirectory_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            string[] dirs = System.IO.Directory.GetFiles(@"X:\DATICONSIP\", "*.*",
                                         System.IO.SearchOption.AllDirectories);
            foreach (string path in dirs)
            {
                if (System.IO.File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path);
                }
                else if (System.IO.Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
            }
        }


        private void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = System.IO.Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = System.IO.Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        private void ProcessFile(string path)
        {
            System.Diagnostics.Debug.WriteLine("Processed file '{0}'.", path);

            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            ContrattiConsipDettagli f = null;
            try
            {
                f = xpObjectSpace.FindObject<ContrattiConsipDettagli>(DevExpress.Data.Filtering.CriteriaOperator.Parse("PathPrima is not null And PathPrima = ?", path));
            }
            catch
            {

            }
            if (f != null)
            {
                f = null;

            }
            else
            {
                if (f == null)
                {
                    ContrattiConsipDettagli NuovoContrConsipDett = xpObjectSpace.CreateObject<ContrattiConsipDettagli>();
                    f = NuovoContrConsipDett;
                    //f.PathPrima = path;
                    f.PathNuovo = System.IO.Path.GetDirectoryName(path);
                    //f.NomeFileNuovo = System.IO.Path.GetFileName(path);
                    f.Save();
                }
            }

            if (f != null)
                if (f.PathNuovo != null && f.NomeFileNuovo != null)
                {
                    string NewFilePath = Path.Combine(f.PathNuovo, f.NomeFileNuovo);

                    if (!Directory.Exists(Path.GetDirectoryName(NewFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(NewFilePath));
                    }

                    if (File.Exists(path))
                    {
                        try
                        {
                            //FileSystemData.BusinessObjects.FileSystemStoreObject fss = xpObjectSpace.CreateObject<FileSystemData.BusinessObjects.FileSystemStoreObject>();
                            Session Sess = ((XPObjectSpace)xpObjectSpace).Session;
                            FileData fd = new FileData(Sess);
                            //Stream sourceStream = new MemoryStream();
                            if (System.IO.File.Exists(NewFilePath)) //
                            {
                                System.IO.Stream excelStreamTMP = new System.IO.FileStream(NewFilePath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                                fd.LoadFromStream(NewFilePath, excelStreamTMP);
                                fd.FileName = System.IO.Path.GetFileName(NewFilePath);
                                f.SetMemberValue("FileTMP", fd);
                                //Stream sourceStream = new MemoryStream();
                                //excelStreamTMP.CopyTo(sourceStream);
                                excelStreamTMP.Close();
                                excelStreamTMP.Dispose();
                            }

                            //FileData fd = ObjectSpace.FindObject<FileData>(null); // Use any other IObjectSpace APIs to query required data.
                            FileSystemStoreObject fss = xpObjectSpace.CreateObject<FileSystemStoreObject>();
                            Stream sourceStream = new MemoryStream();
                            ((IFileData)fd).SaveToStream(sourceStream);
                            sourceStream.Position = 0;
                            ((IFileData)fss).LoadFromStream(fd.FileName, sourceStream);

                            //System.IO.Stream excelStream = new System.IO.FileStream(NewFilePath, System.IO.FileMode.Open);
                            //sourceStream.Position = 0;
                            //((DevExpress.Persistent.Base.IFileData)f.FileSystem).LoadFromStream(System.IO.Path.GetFileName(NewFilePath), sourceStream);
                            f.SetMemberValue("FileSystem", fss);       //f.FileSystem = fss;                 
                            f.Utente = "Admin";
                            xpObjectSpace.CommitChanges();

                            //excelStream.Close();
                            //excelStream.Dispose();

                            //                   //System.IO.File.Copy(f.Path, NewFilePath, true);      
                            //ContrattiConsipDettagli NuovoContrConsipDett = xpObjectSpace.CreateObject<ContrattiConsipDettagli>();
                            //((DevExpress.Persistent.Base.IFileData)fd).SaveToStream(sourceStream);                        //((DevExpress.Persistent.Base.IFileData)fss).LoadFromStream(fd.FileName, sourceStream);

                            //NuovoContrConsipDett.Descrizione = System.IO.Path.GetFileName(path);
                            //NuovoContrConsipDett.Path = path;
                            //NuovoContrConsipDett.Save();
                            //xpObjectSpace.CommitChanges();
                        }
                        catch
                        {
                            f.Descrizione = "non caricato";
                        }
                    }
                }
            xpObjectSpace.Dispose();
            //ContrattiConsipDettagli NuovoContrConsipDett = xpObjectSpace.CreateObject<ContrattiConsipDettagli>();
            ////NuovoContrConsipDett.Descrizione = System.IO.Path.GetFileName(path);
            //NuovoContrConsipDett.Path = path;
            //NuovoContrConsipDett.Save();
            //xpObjectSpace.CommitChanges();
        }


    }
}

//int oidImpianto = lv.CollectionSource.List.Cast<ApparatoMap>()        .Select(s => s.OidImpianto).First();
//CollectionSource LstTeamRisorseAssociabiliCEdifici = 
//List<ContrattiConsipStato> List = Lv.Items.Cast<ContrattiConsipDettagli>().Select(s => s.ContrattiConsipStato).ToList();
//    /// questa è velocissimo!!!!
//List<ContrattiConsipStato> List = ((ListView)View).CollectionSource.List.Cast<ContrattiConsipDettagli>()
//                                        .Select(s => s.ContrattiConsipStato).Distinct().ToList();
//   .Select(s => s.Addetto).Distinct().ToList();
//List<Addetti> lstAddetti = session.Query<AddettiSpecializzazioni>().Where(a => a.TR.Id == idTR) .Select(s => s.Addetto).Distinct().ToList();
//var lstPMS_BLSpecTCad = ((ListView)View).CollectionSource.List.Cast<PMS>()
//              .Where(w => w.Frequenza.TipoCadenza != "su condizione") .GroupBy(s => new { s.Immobile, s.Specializzazione, s.IdPmp.TipoCadenza }).ToList();
//List<object> searchedObjects = new List<object>();
//var cl = ((ListView)View).CollectionSource.List;               
//var dvParent = (DetailView)(View.ObjectSpace).Owner;
//ContrattiConsipRegistro ConConsip = (ContrattiConsipRegistro)dvParent.CurrentObject; 
//if (dvParent.Id == "Destinatari_DetailView")
//{
//    if (dvParent.ViewEditMode == ViewEditMode.Edit)
//    {
//        if (dvParent.FindItem("DestinatariControlliNormativis") != null)
//        {
//        }
//    }
//}