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
    public class OrdenesMetodos
    {

        public List<OrdenViewModel> cargarOrden()
        {
            List<OrdenViewModel> lista;
            using (dbPruebaEntities db = new dbPruebaEntities())
            {

                lista = (from ord in db.VistaOrden
                         select new OrdenViewModel
                         {
                             OrderID = ord.OrderID,
                             ContactName = ord.ContactName,
                             Empleado = ord.TitleOfCourtesy + ord.FirstName + " " + ord.LastName,
                             OrderDate = ord.OrderDate,
                             Total = ord.Subtotal
                         }).ToList();
            }
            return lista;
        }

        public OrdenViewModel getDetalle(int? id)
        {
            OrdenViewModel Orden = new OrdenViewModel();

            using (dbPruebaEntities db = new dbPruebaEntities())
            {
                foreach (var item in db.VistaOrden)
                {
                    if (item.OrderID == id)
                    {
                        Orden.OrderID = item.OrderID;
                        Orden.ContactName = item.ContactName;
                        Orden.Empleado = item.TitleOfCourtesy + item.FirstName + " " + item.LastName;
                        Orden.OrderDate = item.OrderDate;
                        Orden.RequiredDate = item.RequiredDate;
                        Orden.ShippedDate = item.ShippedDate;
                        Orden.CompanyName = item.CompanyName;
                        Orden.Freight = item.Freight;
                        Orden.ShipName = item.ShipName;
                        Orden.ShipAddress = item.ShipAddress;
                        Orden.ShipCity = item.ShipCity;
                        Orden.ShipRegion = item.ShipRegion;
                        Orden.ShipPostalCode = item.ShipPostalCode;
                        Orden.ShipCountry = item.ShipCountry;
                        Orden.Total = item.Subtotal;
                        Orden.DescuentoTotal = item.DiscountTotal;
                    }
                }
                return Orden;

            }

        }

        public List<OrderDetailsExtendViewModel> cargarOrdenDetalle(int? id)
        {
            List<OrderDetailsExtendViewModel> lista;
            using (dbPruebaEntities db = new dbPruebaEntities())
            {

                lista = (from ord in db.Order_Details_Extended
                         where ord.OrderID == id
                         select new OrderDetailsExtendViewModel
                         {
                            OrderID=ord.OrderID,
                            ProductName = ord.ProductName,
                            UnitPrice = ord.UnitPrice,
                            Quantity = ord.Quantity,
                            Discount = (Math.Round(ord.Discount,2))*100,
                            ExtendedPrice = ord.ExtendedPrice
                         }).ToList();
            }
            return lista;
        }
    }
}
