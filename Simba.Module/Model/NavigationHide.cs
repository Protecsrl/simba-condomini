using CAMS.Module.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMS.Module.Model
{
   public class NavigationHide
    {
        public string NavigationItemID { get; set; }
        public TipoNavigationItem TipoNavigationItem { get; set; }
        public Boolean Attivo { get; set; }
    }
}
