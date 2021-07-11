using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Simba.Businness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimbaCondomini.Controllers
{
    public class RegioniDatiController : ApiController
    {

        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            Regioni cc = new Regioni();
            var ccc = cc.GetAll();
            List<Simba.Businness.LookupItem> list = new List<Simba.Businness.LookupItem>();
            foreach (var c in ccc)
            {
                list.Add(new Simba.Businness.LookupItem(c.Oid, c.Descrizione));
            }
            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }


    }
}