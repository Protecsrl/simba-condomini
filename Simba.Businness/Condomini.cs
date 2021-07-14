using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Simba.DataLayer.Database;
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

        public List<Condominium> GetForSupplier()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());
                var ts = uw.Query<Simba.DataLayer.Database.TicketSuplliers>().Where(t => t.IdSuplier.Oid == user.Oid).Select(s => s.IdTicket.Oid);
                var data = uw.Query<Simba.DataLayer.Database.Ticket>()
                .Where(t => ts.Contains(t.Oid) || t.isPublic)
                .Select(x => x.Building.Condominium)
                .ToList();
                return data;
            }
        }


        public List<Condominium> GetAllByUser()
        {
            var type = User.GetUserType();

            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());
                switch (type)
                {
                    case Roles.Cond:
                        var data = uw.Query<Condominium>().Where(c => user.Building.Condominium.Oid == c.Oid).ToList();
                        return data;
                    case Roles.CondAdmin:
                        var adminConds = user.UserCondominiums.Select(c => c.IdCondominium.Oid);
                        var dataA = uw.Query<Condominium>().Where(c => adminConds.Contains(c.Oid)).ToList();
                        return dataA;
                    case Roles.Supplier:
                        return GetForSupplier();
                    case Roles.SysAdmin:
                        return GetAll();
                    default:
                        return new List<Condominium>();
                }
            }
        }

        public void SalvaCondominio(Models.Admin.Condominio condominio)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                Simba.DataLayer.Database.Comuni comune = uw.GetObjectByKey<Simba.DataLayer.Database.Comuni>(condominio.Comune);
                Condominium condom = new Condominium(uw)
                {
                    Code = condominio.Code,
                    Comune = comune,
                    Indirizzo = condominio.Indirizzo,
                    Latitudine = condominio.Latitudine,
                    Longitudine = condominio.Longitudine,
                    PartitaIva = condominio.PartitaIva
                };
                condom.Save();
                uw.CommitChanges();
            }
        }

        public void UpdateCondominio(Models.Admin.Condominio condominio)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                Simba.DataLayer.Database.Comuni comune = uw.GetObjectByKey<Simba.DataLayer.Database.Comuni>(condominio.Comune);
                Simba.DataLayer.Database.Condominium condom = uw.GetObjectByKey<Simba.DataLayer.Database.Condominium>(condominio.Oid);

                condom.Code = condominio.Code;
                condom.Comune = comune;
                condom.Indirizzo = condominio.Indirizzo;
                condom.Latitudine = condominio.Latitudine;
                condom.Longitudine = condominio.Longitudine;
                condom.PartitaIva = condominio.PartitaIva;

                condom.Save();
                uw.CommitChanges();
            }
        }
    }
}
