using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class Condomini : BusinnessBase
    {

        public List<Condominium> GetAll()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Condominium>().ToList();
                return data;
            }
        }


        public List<Condominium> GetAllByUser()
        {
            var type = User.GetUserType();

            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(User.GetUserId());
                switch (type)
                {
                    case Roles.Cond:
                        var data = uw.Query<Condominium>().Where(c => user.Building.Condominium.Oid==c.Oid).ToList();
                        return data;
                    case Roles.CondAdmin:
                        var adminConds = user.UserCondominiums.Select(c => c.IdCondominium.Oid);
                        var dataA = uw.Query<Condominium>().Where(c => adminConds.Contains(c.Oid)).ToList();
                        return dataA;
                    case Roles.Supplier:
                        return GetAll();
                    case Roles.SysAdmin:
                        return GetAll();
                    default:
                        return new List<Condominium>();
                }
            }
        }
    }
}
