using Logica.Logica;
using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace JuegaOk2.Controllers
{
    public class OrdenesController : Controller
    {
        // GET: Ordenes
        public ActionResult Index()
        {
            var OrdenesLista = new OrdenesMetodos().cargarOrden();
            return View(OrdenesLista);
        }

        //GetDetalleOrden
        public ActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Orden = new OrdenesMetodos().getDetalle(id);
            if (Orden == null)
            {
                return HttpNotFound();
            }
            return Json(Orden, JsonRequestBehavior.AllowGet);
        }


        //GetDetalleExtend
        public ActionResult GetOrderDetalleExtend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var OrdenDetalleExt = new OrdenesMetodos().cargarOrdenDetalle(id);
            if (OrdenDetalleExt == null)
            {
                return HttpNotFound();
            }
            return Json(OrdenDetalleExt, JsonRequestBehavior.AllowGet);
        }

    }
}