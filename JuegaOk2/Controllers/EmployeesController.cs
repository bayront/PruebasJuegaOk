using Logica.Logica;
using System;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace JuegaOk2.Controllers
{
    public class EmployeesController : Controller
    {

        // GET: Employees
        public ActionResult Index()
        {
            ViewBag.mssg = TempData["mssg"] as string;
            var employeesLista = new EmployeesMetodos().cargarProcedimientoAlmacenado();
            return View(employeesLista);
        }


        //Get: Enviar datos en Json para generar reporte
        public ActionResult GenerarReporte()
        {
            var employeesLista = new EmployeesMetodos().cargarDatosReporte();
            return Json(employeesLista, JsonRequestBehavior.AllowGet);
        }


        //Get: Detalles
        public ActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = new EmployeesMetodos().getDetalle(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Imagen = convertirImage(employee.Photo);
            return View(employee);
        }

        //Get: Crear
        public ActionResult Crear()
        {
            var employeesLista = new EmployeesMetodos().llenarCombox();
            ViewBag.ReportsTo = new SelectList(employeesLista, "EmployeeID", "informa");
            return View();
        }

        //Post: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(String LastName, String FirstName, String Title, String TitleOfCourtesy, DateTime? BirthDate, DateTime? HireDate, String Address, String City, String Region, String PostalCode, String Country, String HomePhone, String Extension, byte[] Photo, String Notes, int? ReportsTo, String PhotoPath)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase != null && FileBase.ContentLength>0)
            {
            WebImage image = new WebImage(FileBase.InputStream);
            Photo = image.GetBytes();
            }     
            
            if (BirthDate != null)
            {
                int fechasValidateBirthDate = DateTime.Compare(BirthDate.Value, DateTime.Today);
                if (fechasValidateBirthDate == 0)
                {
                    ModelState.AddModelError("BirthDate", "La fecha de nacimiento no puede ser igual a la fecha actual");
                }
                else if (fechasValidateBirthDate > 0)
                {
                    ModelState.AddModelError("BirthDate", "La fecha de nacimiento no puede ser posterior a la fecha actual");
                }
            }
            if (HireDate != null)
            {
                int fechasValidateHireDate = DateTime.Compare(HireDate.Value, DateTime.Today);
                if (fechasValidateHireDate > 0)
                {
                    ModelState.AddModelError("HireDate", "La fecha de contratación no puede ser posterior a la fecha actual");
                }
            }
            if (BirthDate!=null && HireDate != null)
            {
                int resultFechas = DateTime.Compare(BirthDate.Value, HireDate.Value);               
                
                if (resultFechas > 0)
                {
                    ModelState.AddModelError("HireDate", "La fecha de contratación no puede ser anterior a la fecha de nacimiento");
                }
                else if(resultFechas == 0)
                {
                    ModelState.AddModelError("HireDate", "La fecha de Contratación no puede ser igual a la fecha de nacimiento");
                }

            }

            if (ModelState.IsValid)
            {

                var employee = new EmployeesMetodos().postCrear(LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath, false);
                if (employee == true)
                {

                    TempData["mssg"] = "El empleado " + FirstName + " " + LastName + " ha sido guardado";
                    return RedirectToAction("Index");
                }

            }
            var modalemployee = new EmployeesMetodos().cargarVista(LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath, false, null);
            var employeesLista = new EmployeesMetodos().llenarCombox();
            ViewBag.ReportsTo = new SelectList(employeesLista, "EmployeeID", "informa", ReportsTo);
            return View(modalemployee);

        }

        // GET: Editar
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = new EmployeesMetodos().getDetalle(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var employeesLista = new EmployeesMetodos().llenarListEditar(id);
            ViewBag.ReportsTo = new SelectList(employeesLista, "EmployeeID", "informa", employee.ReportsTo);
            ViewBag.Imagen = convertirImage(employee.Photo);
            return View(employee);
        }

        // Post : Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int? id,String LastName, String FirstName, String Title, String TitleOfCourtesy, DateTime? BirthDate, DateTime? HireDate, String Address, String City, String Region, String PostalCode, String Country, String HomePhone, String Extension, byte[] Photo, String Notes, int? ReportsTo, String PhotoPath, bool Eliminado, byte[] RowVersion)
        {
            var employeesLista = new EmployeesMetodos().llenarListEditar(id);
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase != null && FileBase.ContentLength > 0)
            {
                WebImage image = new WebImage(FileBase.InputStream);
                Photo = image.GetBytes();
            }
            if (BirthDate != null)
            {
                int fechasValidateBirthDate = DateTime.Compare(BirthDate.Value, DateTime.Today);
                if (fechasValidateBirthDate == 0)
                {
                    ModelState.AddModelError("BirthDate", "La fecha de nacimiento no puede ser igual a la fecha actual");
                }
                else if (fechasValidateBirthDate > 0)
                {
                    ModelState.AddModelError("BirthDate", "La fecha de nacimiento no puede ser posterior a la fecha actual");
                }
            }
            if (HireDate != null)
            {
                int fechasValidateHireDate = DateTime.Compare(HireDate.Value, DateTime.Today);
                if (fechasValidateHireDate > 0)
                {
                    ModelState.AddModelError("HireDate", "La fecha de contratación no puede ser posterior a la fecha actual");
                }
            }
            if (BirthDate != null && HireDate != null)
            {
                int resultFechas = DateTime.Compare(BirthDate.Value, HireDate.Value);

                if (resultFechas > 0)
                {
                    ModelState.AddModelError("HireDate", "La fecha de contratación no puede ser anterior a la fecha de nacimiento");
                }
                else if (resultFechas == 0)
                {
                    ModelState.AddModelError("HireDate", "La fecha de Contratación no puede ser igual a la fecha de nacimiento");
                }

            }
            if (ModelState.IsValid)
            {
                var employee = new EmployeesMetodos().postEditar(id, LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath,Eliminado,RowVersion);
                if (employee == true)
                {
                    TempData["mssg"] = "El empleado " + FirstName+ " "+LastName + " ha sido editado";
                    return RedirectToAction("Index");
                }
                if (employee == false)
                {
                    ModelState.AddModelError(string.Empty,
                        "No se pueden guardar los cambios. El Empleado ha sido desactivado por otro usuario hace unos momentos."
                      + " Por favor haga clic en Cancelar para volver a la página anterior dado que no será posible continuar con la modificación");
                    var modalemployeeEliminado = new EmployeesMetodos().cargarVista(LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath, Eliminado, RowVersion);                   
                    ViewBag.ReportsTo = new SelectList(employeesLista, "EmployeeID", "FirstName", ReportsTo);
                    ViewBag.Imagen = convertirImage(modalemployeeEliminado.Photo);
                    return View(modalemployeeEliminado);
                }
            }
            var modalemployee = new EmployeesMetodos().cargarVista(LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath, Eliminado, RowVersion);
            ViewBag.ReportsTo = new SelectList(employeesLista, "EmployeeID", "informa", ReportsTo);
            ViewBag.Imagen = convertirImage(modalemployee.Photo);
            return View(modalemployee);

        }

        // Get : Eliminar

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = new EmployeesMetodos().getDetalle(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Imagen = convertirImage(employee.Photo);
            return View(employee);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {            
            var employee = new EmployeesMetodos().postEliminado(id);
            TempData["mssg"] = "El empleado ha sido eliminado";
            return RedirectToAction("Index");
        }


        //Convertir imagen para mostrarla en la vista
        public string convertirImage(byte[] image)
        {
            if(image == null)
            {
                string path = Server.MapPath("~/Content/imagenes/default_image.png");
                byte[] imageDefault = System.IO.File.ReadAllBytes(path);
                string base64Imagedefault = Convert.ToBase64String(imageDefault);
                return base64Imagedefault;
            }

            string base64Image = Convert.ToBase64String(image);
            return base64Image;
        }

    }
}