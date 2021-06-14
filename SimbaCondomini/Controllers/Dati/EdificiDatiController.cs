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
    public class EdificiDatiController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            return Get(0, loadOptions);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id, DataSourceLoadOptions loadOptions)
        {
            loadOptions.Take = 10;
            var idCondominio = id;
            if(loadOptions.Filter!=null && loadOptions.Filter.Count>=3){
                idCondominio = Convert.ToInt32(loadOptions.Filter[2]);
            }
            Edifici cc = new Edifici();
            var ccc = cc.GetByCondominium(idCondominio);
            List<Simba.Businness.Models.Edificio> list = new List<Simba.Businness.Models.Edificio>();
            foreach (var c in ccc)
            {
                list.Add(new Simba.Businness.Models.Edificio(c.Oid, c.Nome, c.Condominium.Oid));
            }
            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}