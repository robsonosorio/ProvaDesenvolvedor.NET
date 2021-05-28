using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaVetta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JQuery()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult BuscaContrato()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult EfetuaBusca(string numero, long diretoria, string usuario, string senha)
        {
            try
            {
                ServiceReferenceCorreios.buscaContrato busca;
                busca = new ServiceReferenceCorreios.buscaContrato(numero, diretoria, usuario, senha);

                return Json(busca, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}