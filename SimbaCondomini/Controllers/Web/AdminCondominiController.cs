using Simba.Businness.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class AdminCondominiController : Controller
    {
        // GET: AdminCondomini
        public ActionResult Index()
        {
            return View();
        }

        private ActionResult Edit()
        {
            var model = new Condominio(0, 0, string.Empty, string.Empty, string.Empty, "0", "0", string.Empty);
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return Edit();
            }
            return Edit();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Condominio model)
        {
            Simba.Businness.Condomini c = new Simba.Businness.Condomini();
            if(model.Oid>0){
                c.UpdateCondominio(model);
            } else {
                c.SalvaCondominio(model);
            }
            return View(model);
        }
    }
}