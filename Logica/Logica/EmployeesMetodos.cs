using Datos;
using Modelo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Logica
{
    public class EmployeesMetodos
    {
        //Para el index
        public List<EmployeeViewModel> cargarDatos()
        {
            List<EmployeeViewModel> lista;
            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                lista = (from emp in db.Employees
                         where emp.Eliminado == false
                         select new EmployeeViewModel
                         {
                             EmployeeID = emp.EmployeeID,
                             LastName = emp.LastName,
                             FirstName = emp.FirstName,
                             Title = emp.Title,
                             TitleOfCourtesy = emp.TitleOfCourtesy,
                             BirthDate = emp.BirthDate,
                             HireDate = emp.HireDate,
                             Address = emp.Address,
                             City = emp.City,
                             Region = emp.Region,
                             PostalCode = emp.PostalCode,
                             Country = emp.Country,
                             HomePhone = emp.HomePhone,
                             Extension = emp.Extension,
                             Photo = emp.Photo,
                             Notes = emp.Notes,
                             ReportsTo = emp.ReportsTo,
                             PhotoPath = emp.PhotoPath,
                             Eliminado = emp.Eliminado,
                             RowVersion = emp.RowVersion,
                             informa = emp.TitleOfCourtesy + emp.FirstName + " "+emp.LastName
                         }).ToList();
            }
            return lista;
        }

        public List<EmployeeViewModel> cargarProcedimientoAlmacenado()
        {
            List<EmployeeViewModel> lista;
            dbPruebaEntities db = new dbPruebaEntities();
            var listaGet = db.GetEmployees().ToList();
            lista = (from emp in listaGet
                     where emp.Eliminado == false
                     select new EmployeeViewModel
                     {
                         EmployeeID = emp.EmployeeID,
                         LastName = emp.LastName,
                         FirstName = emp.FirstName,
                         Title = emp.Title,
                         TitleOfCourtesy = emp.TitleOfCourtesy,
                         BirthDate = emp.BirthDate,
                         HireDate = emp.HireDate,
                         Address = emp.Address,
                         City = emp.City,
                         Region = emp.Region,
                         PostalCode = emp.PostalCode,
                         Country = emp.Country,
                         HomePhone = emp.HomePhone,
                         Extension = emp.Extension,
                         Photo = emp.Photo,
                         Notes = emp.Notes,
                         ReportsTo = emp.ReportsTo,
                         PhotoPath = emp.PhotoPath,
                         Eliminado = emp.Eliminado,
                         RowVersion = emp.RowVersion,
                         informa = emp.TitleOfCourtesy + emp.FirstName + " " + emp.LastName
                     }).ToList();

            return lista;
        }

        public List<EmployeeViewModel> cargarDatosReporte()
        {
            List<EmployeeViewModel> lista;
            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                lista = (from emp in db.Employees
                         select new EmployeeViewModel
                         {
                             EmployeeID = emp.EmployeeID,
                             LastName = emp.LastName,
                             FirstName = emp.FirstName,
                             Title = emp.Title,
                             TitleOfCourtesy = emp.TitleOfCourtesy,
                             BirthDate = emp.BirthDate,
                             HireDate = emp.HireDate,
                             Address = emp.Address,
                             City = emp.City,
                             Region = emp.Region,
                             PostalCode = emp.PostalCode,
                             Country = emp.Country,
                             HomePhone = emp.HomePhone,
                             Extension = emp.Extension,
                             Photo = emp.Photo,
                             Notes = emp.Notes,
                             Eliminado = emp.Eliminado,
                             informa = emp.TitleOfCourtesy + emp.FirstName + " " + emp.LastName
                         }).ToList();
            }
            return lista;
        }

        //Datos para cargar los selectList al crear
        public List<EmployeeViewModel> llenarCombox()
        {
            List<EmployeeViewModel> lista;
            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                lista = (from emp in db.Employees
                         where emp.Eliminado == false
                         select new EmployeeViewModel
                         {
                             EmployeeID = emp.EmployeeID,                             
                             informa = emp.TitleOfCourtesy + emp.FirstName + " " + emp.LastName
                         }).ToList();
            }
            return lista;
        }

        //Datos para cargar los selectList al editar -- para no mostrar al usuario seleccionado
        public List<EmployeeViewModel> llenarListEditar(int? id)
        {
            List<EmployeeViewModel> lista;
            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                lista = (from emp in db.Employees
                         where emp.Eliminado == false && emp.EmployeeID != id
                         select new EmployeeViewModel
                         {
                             EmployeeID = emp.EmployeeID,
                             informa = emp.TitleOfCourtesy + emp.FirstName + " " + emp.LastName
                         }).ToList();
            }
            return lista;
        }

        //Trae el empleado por medio del id para ser mostrado en la vista detalle
        public EmployeeViewModel getDetalle(int? id)
        {
            EmployeeViewModel oEmployees = new EmployeeViewModel();
            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                var employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return null;
                }     
                if(employee.Eliminado == true)
                {
                    return null;
                }
                oEmployees.EmployeeID = employee.EmployeeID;
                oEmployees.LastName = employee.LastName;
                oEmployees.FirstName = employee.FirstName;
                oEmployees.Title = employee.Title;
                oEmployees.TitleOfCourtesy = employee.TitleOfCourtesy;
                oEmployees.BirthDate = employee.BirthDate;
                oEmployees.HireDate = employee.HireDate;
                oEmployees.Address = employee.Address;
                oEmployees.City = employee.City;
                oEmployees.Region = employee.Region;
                oEmployees.PostalCode = employee.PostalCode;
                oEmployees.Country = employee.Country;
                oEmployees.HomePhone = employee.HomePhone;
                oEmployees.Extension = employee.Extension;
                oEmployees.Photo = employee.Photo;
                oEmployees.Notes = employee.Notes;
                oEmployees.ReportsTo = employee.ReportsTo;
                oEmployees.PhotoPath = employee.PhotoPath;
                oEmployees.Eliminado = employee.Eliminado;
                oEmployees.RowVersion = employee.RowVersion;
                oEmployees.informa = employee.TitleOfCourtesy + " " + employee.FirstName + " " + employee.LastName;

            }
            return oEmployees;

        }

        //Devuelve true al controlador create:post si los datos se guardaron con éxito
        public bool postCrear(string LastName, string FirstName, string Title, string TitleOfCourtesy, DateTime? BirthDate, DateTime? HireDate, string Address, string City, string Region, string PostalCode, string Country, string HomePhone, string Extension, byte[] Photo, string Notes, int? ReportsTo, string PhotoPath, bool Eliminado)
        {
            var dbEmployees = new Employees();

            using (dbPruebaEntities db = new dbPruebaEntities())
            {

                try
                {
                    dbEmployees.LastName = LastName;
                    dbEmployees.FirstName = FirstName;
                    dbEmployees.Title = Title;
                    dbEmployees.TitleOfCourtesy = TitleOfCourtesy;
                    dbEmployees.BirthDate = BirthDate;
                    dbEmployees.HireDate = HireDate;
                    dbEmployees.Address = Address;
                    dbEmployees.City = City;
                    dbEmployees.Region = Region;
                    dbEmployees.PostalCode = PostalCode;
                    dbEmployees.Country = Country;
                    dbEmployees.HomePhone = HomePhone;
                    dbEmployees.Extension = Extension;
                    dbEmployees.Photo = Photo;
                    dbEmployees.Notes = Notes;
                    dbEmployees.ReportsTo = ReportsTo;
                    dbEmployees.PhotoPath = PhotoPath;
                    dbEmployees.Eliminado = Eliminado;
                    db.Employees.Add(dbEmployees);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }


        //Devuelve true al controlador creae:post si los datos se guardaron con éxito
        public bool postEditar(int? id,string LastName, string FirstName, string Title, string TitleOfCourtesy, DateTime? BirthDate, DateTime? HireDate, string Address, string City, string Region, string PostalCode, string Country, string HomePhone, string Extension, byte[] Photo, string Notes, int? ReportsTo, string PhotoPath, bool Eliminado, byte[] RowVersion)
        {
            var dbEmployees = new Employees();

            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                dbEmployees = db.Employees.Find(id);
                if(dbEmployees.Eliminado == true)
                {
                    return false;
                }

                try
                {
                    dbEmployees.LastName = LastName;
                    dbEmployees.FirstName = FirstName;
                    dbEmployees.Title = Title;
                    dbEmployees.TitleOfCourtesy = TitleOfCourtesy;
                    dbEmployees.BirthDate = BirthDate;
                    dbEmployees.HireDate = HireDate;
                    dbEmployees.Address = Address;
                    dbEmployees.City = City;
                    dbEmployees.Region = Region;
                    dbEmployees.PostalCode = PostalCode;
                    dbEmployees.Country = Country;
                    dbEmployees.HomePhone = HomePhone;
                    dbEmployees.Extension = Extension;
                    dbEmployees.Photo = Photo;
                    dbEmployees.Notes = Notes;
                    dbEmployees.ReportsTo = ReportsTo;
                    dbEmployees.PhotoPath = PhotoPath;
                    dbEmployees.Eliminado = Eliminado;
                    dbEmployees.RowVersion = RowVersion;
                    db.Entry(dbEmployees).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }

        //Método para eliminado lógico
        public bool postEliminado(int id)
        {

            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                Employees dbEmployees = db.Employees.Find(id);
                dbEmployees.Eliminado = true;
                db.Entry(dbEmployees).State = EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }

        public EmployeeViewModel cargarVista(String LastName, String FirstName, String Title, String TitleOfCourtesy, DateTime? BirthDate, DateTime? HireDate, String Address, String City, String Region, String PostalCode, String Country, String HomePhone, String Extension, byte[] Photo, String Notes, int? ReportsTo, String PhotoPath, bool Eliminado, byte[] RowVersion)
        {
            var oEmployees = new EmployeeViewModel();
            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                oEmployees.LastName = LastName;
                oEmployees.FirstName = FirstName;
                oEmployees.Title = Title;
                oEmployees.TitleOfCourtesy = TitleOfCourtesy;
                oEmployees.BirthDate = BirthDate;
                oEmployees.HireDate = HireDate;
                oEmployees.Address = Address;
                oEmployees.City = City;
                oEmployees.Region = Region;
                oEmployees.PostalCode = PostalCode;
                oEmployees.Country = Country;
                oEmployees.HomePhone = HomePhone;
                oEmployees.Extension = Extension;
                oEmployees.Photo = Photo;
                oEmployees.Notes = Notes;
                oEmployees.ReportsTo = ReportsTo;
                oEmployees.PhotoPath = PhotoPath;
                oEmployees.Eliminado = Eliminado;
                oEmployees.RowVersion = RowVersion;
            }
            return oEmployees;
        }

    }
}
