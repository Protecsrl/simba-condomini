using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web.Http;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Simba.Businness;
using System;

namespace SimbaCondomini.Controllers
{
    public class ComunicazioniDatiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            var userType = Simba.Businness.User.GetUserType();

            bool isPerson = userType == Roles.Cond;
            bool isAdminCond = userType == Roles.CondAdmin;
            bool isSupplier = userType == Roles.Supplier;

            int idCond = 0;
            try
            {
                if (loadOptions.Filter == null || loadOptions.Filter.Count > 0)
                {
                    idCond = Convert.ToInt32(loadOptions.Filter[0]);
                }

            }
            catch
            {

            }


            Comunicazioni c = new Comunicazioni();
            List<Simba.DataLayer.Database.Communications> data = null;
            if (isPerson) data = c.GetUserCommunication(Simba.Businness.User.GetUserId());
            if (isAdminCond) data = c.getComunicazioniCondominio(idCond);
            if (isSupplier) data = c.getComunicazioniSupplier();
            var datac = new List<Simba.Businness.Models.ComunicazioniTicket>();
            foreach (var cc in data)
            {
                datac.Add(new Simba.Businness.Models.ComunicazioniTicket(Simba.Businness.Models.ItemType.Comunicazione, cc.Oid, cc.Titolo, cc.ParentCommunication, cc.User.Nome, cc.Condominium.NomeCondominio, cc.DateInsert));
            }



            Ticket t = new Ticket();
            List<Simba.DataLayer.Database.Ticket> dataT = null;
            if (isPerson) dataT = t.GetUserTicket(Simba.Businness.User.GetUserId());
            if (isAdminCond) dataT = t.GetTicketCondominio(idCond);
            if (isSupplier) dataT = t.GetTicketSupplier(idCond);
            foreach (var cc in dataT)
            {
                var cond = cc.Condominium != null ? cc.Condominium.NomeCondominio : string.Empty;
                datac.Add(new Simba.Businness.Models.ComunicazioniTicket(Simba.Businness.Models.ItemType.Ticket, cc.Oid, cc.Titolo, 0, cc.User.Nome, cond, cc.DateCreation));
            }

            datac.Sort();
            loadOptions.Filter = null;
            //return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));
            return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));
        }
    }
}