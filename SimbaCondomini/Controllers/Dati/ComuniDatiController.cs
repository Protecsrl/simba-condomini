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
    public class ComuniDatiController : ApiController
    {
        public HttpResponseMessage Get(int id, DataSourceLoadOptions loadOptions)
        {
            loadOptions.Take = 10;
            var IdProvincia
            = id;
            if (loadOptions.Filter != null && loadOptions.Filter.Count >= 2)
            {
                if (loadOptions.Filter[1].ToString() == "=")
                    IdProvincia = Convert.ToInt32(loadOptions.Filter[2]);
                else
                    IdProvincia = Convert.ToInt32(loadOptions.Filter[1]);
            }
            Comuni cc = new Comuni();
            var ccc = cc.GetAll(IdProvincia);
            List<Simba.Businness.LookupItem> list = new List<Simba.Businness.LookupItem>();
            foreach (var c in ccc)
            {
                list.Add(new Simba.Businness.LookupItem(c.Oid, c.DenominazioneItaliano));
            }

            // return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
            return Request.CreateResponse(list);
        }


    }
}