using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness.Models
{
    public class Bacheca
    {
        public Bacheca(int idCondominio)
        {
            this.IdCondominio = idCondominio;
        }

        public int IdCondominio
        {
            get; private set;
        }
    }
}
