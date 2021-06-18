using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Comunicazioni : BusinnessBase
    {
        public List<Communications> GetUserCommunication(int? userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Communications>().
                Where(c=>!userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }

        public Simba.DataLayer.simba_condomini.Communications getComunicazioneById(int idCom)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.Communications>().
                Where(c => c.Oid == idCom).First();
                return data;
            }
        }
    }
}
