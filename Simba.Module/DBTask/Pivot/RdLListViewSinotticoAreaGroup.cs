using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMS.Module.DBTask.Pivot
{
    [DefaultClassOptions, Persistent("RDL_LIST_SINOTTICO_AREA_G")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Sinottico Area group")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem(true)]
    //[System.ComponentModel.DefaultProperty("Descrizione")]"Interventi"
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Ultimi3mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2)))", "Ultimi 3 Mesi", true, Index = 0)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Ultimi6mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2))) Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3)))  Or ([Mese] = GetMonth(AddMonths(Now(), -4)) And [Anno] = GetYear(AddMonths(Now(), -4)))  Or ([Mese] = GetMonth(AddMonths(Now(), -5)) And [Anno] = GetYear(AddMonths(Now(), -5)))  Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3)))", "Ultimi 6 Mesi", Index = 1)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Ultimi9mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2))) Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3))) Or ([Mese] = GetMonth(AddMonths(Now(), -4)) And [Anno] = GetYear(AddMonths(Now(), -4))) Or ([Mese] = GetMonth(AddMonths(Now(), -5)) And [Anno] = GetYear(AddMonths(Now(), -5))) Or ([Mese] = GetMonth(AddMonths(Now(), -6)) And [Anno] = GetYear(AddMonths(Now(), -6))) Or ([Mese] = GetMonth(AddMonths(Now(), -7)) And [Anno] = GetYear(AddMonths(Now(), -7))) Or ([Mese] = GetMonth(AddMonths(Now(), -8)) And [Anno] = GetYear(AddMonths(Now(), -8)))", "Ultimi Nove Mesi", Index = 2)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.AnnoinCorso", "[Anno] = GetYear(Now())", "Anno in Corso", Index = 3)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.AnnoScorso", "[Anno] = GetYear(AddYears(Now(), -1))", "Anno Scorso", Index = 4)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Tutto", "", "Tutto", Index = 5)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.1TrimAnnoinCorso", "[Mese] In(1,2,3) And [Anno] = GetYear(Now())", @"1° Trimestre", Index = 6)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.2TrimAnnoinCorso", "[Mese] In(4,5,6) And [Anno] = GetYear(Now())", @"2° Trimestre", Index = 7)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.3TrimAnnoinCorso", "[Mese] In(7,8,9) And [Anno] = GetYear(Now())", @"3° Trimestre", Index = 8)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.4TrimAnnoinCorso", "[Mese] In(10,11,12) And [Anno] = GetYear(Now())", @"4° Trimestre", Index = 9)]

    [ListViewFilter("RdLListViewSinotticoAreaGroup.1TrimAnnoScorso", "[Mese] In(1,2,3) And [Anno] = GetYear(AddYears(Now(), -1))", @"1° Trimestre (Anno Scorso)", Index = 10)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.2TrimAnnoScorso", "[Mese] In(4,5,6) And [Anno] = GetYear(AddYears(Now(), -1))", @"2° Trimestre (Anno Scorso)", Index = 11)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.3TrimAnnoScorso", "[Mese] In(7,8,9) And [Anno] = GetYear(AddYears(Now(), -1))", @"3° Trimestre (Anno Scorso)", Index = 12)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.4TrimAnnoScorso", "[Mese] In(10,11,12) And [Anno] = GetYear(AddYears(Now(), -1))", @"4° Trimestre (Anno Scorso)", Index = 13)]

    [ListViewFilter("RdLListViewSinotticoAreaGroup.AreaCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 14)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Categoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 15)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Categoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 16)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Categoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 17)]
    [ListViewFilter("RdLListViewSinotticoAreaGroup.Categoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 18)]

    #endregion
   public class RdLListViewSinotticoAreaGroup
    {
    }
}
