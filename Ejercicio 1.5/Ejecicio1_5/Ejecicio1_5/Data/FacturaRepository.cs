using Ejecicio1_5.Data.Utils;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Data
{
    internal class FacturaRepository : IFacturaRepository
    {
        private readonly DataHelper _dataHelper;

        public FacturaRepository()
        {
            _dataHelper = DataHelper.GetInstance(); // Obtiene la única instancia de DataHelper (patrón Singleton)
        }
        public void GuardarFactura(Factura factura)
        {
            // Crear los parámetros para insertar la factura
            List<ParameterSQL> parametros = new List<ParameterSQL>
        {
            new ParameterSQL("@NroFactura", factura.NroFactura),
            new ParameterSQL("@Fecha", factura.Fecha),
            new ParameterSQL("@Cliente", factura.Cliente),
            new ParameterSQL("@FormaPagoId", factura.FormaPago.Id)
        };

            // Llamar al procedimiento almacenado para insertar la factura
            int rowsAffected = _dataHelper.ExecuteSPDML("InsertarFactura", parametros);

            // Aquí también debes insertar los detalles de la factura usando un procedimiento almacenado adicional
            foreach (var detalle in factura.Detalles)
            {
                List<ParameterSQL> detalleParametros = new List<ParameterSQL>
            {
                new ParameterSQL("@NroFactura", factura.NroFactura),
                new ParameterSQL("@ArticuloId", detalle.Articulo.Id),
                new ParameterSQL("@Cantidad", detalle.Cantidad)
            };

                _dataHelper.ExecuteSPDML("InsertarDetalleFactura", detalleParametros);
            }
        }

        public Factura ObtenerFactura(int nroFactura)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@NroFactura", nroFactura)
            };

            // Ejecutar el procedimiento almacenado para obtener la factura
            DataTable facturaTable = _dataHelper.ExecuteSPQuery("ObtenerFactura", parametros);

            if (facturaTable != null && facturaTable.Rows.Count > 0)
            {
                // Crear un objeto Factura a partir de la tabla devuelta
                DataRow row = facturaTable.Rows[0];
                Factura factura = new Factura
                {
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Cliente = row["Cliente"].ToString(),
                    FormaPago = new FormaPago { Id = Convert.ToInt32(row["FormaPagoId"]) }
                };

                // Obtener los detalles de la factura
                DataTable detallesTable = _dataHelper.ExecuteSPQuery("ObtenerDetallesFactura", parametros);
                foreach (DataRow detalleRow in detallesTable.Rows)
                {
                    Articulo articulo = new Articulo
                    {
                        Id = Convert.ToInt32(detalleRow["ArticuloId"]),
                        Nombre = detalleRow["Nombre"].ToString(),
                        PrecioUnitario = Convert.ToDecimal(detalleRow["PrecioUnitario"])
                    };

                    DetalleFactura detalle = new DetalleFactura
                    {
                        Articulo = articulo,
                        Cantidad = Convert.ToInt32(detalleRow["Cantidad"])
                    };

                    factura.Detalles.Add(detalle);
                }

                return factura;
            }

            return null;
        }

        public List<Factura> ListarFacturas()
        {
            var dataTable = _dataHelper.ExecuteSPQuery("sp_ListarFacturas", null);
            List<Factura> facturas = new List<Factura>();

            foreach (DataRow row in dataTable.Rows)
            {
                Factura factura = new Factura
                {
                    NroFactura = Convert.ToInt32(row["nroFactura"]),
                    Fecha = Convert.ToDateTime(row["fecha"]),
                    Cliente = row["cliente"].ToString(),
                    FormaPago = new FormaPago { Id = Convert.ToInt32(row["formaPagoId"]), Nombre = row["formaPagoNombre"].ToString() }
                };

                facturas.Add(factura);
            }

            return facturas;
        }

        private List<DetalleFactura> ObtenerDetallesPorFactura(int nroFactura)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@nroFactura", nroFactura)
            };

            var dataTable = _dataHelper.ExecuteSPQuery("sp_ObtenerDetallesFactura", parametros);
            List<DetalleFactura> detalles = new List<DetalleFactura>();

            foreach (DataRow row in dataTable.Rows)
            {
                DetalleFactura detalle = new DetalleFactura
                {
                    Articulo = new Articulo
                    {
                        Id = Convert.ToInt32(row["articuloId"]),
                        Nombre = row["articuloNombre"].ToString(),
                        PrecioUnitario = Convert.ToDecimal(row["precioUnitario"])
                    },
                    Cantidad = Convert.ToInt32(row["cantidad"])
                };

                detalles.Add(detalle);
            }

            return detalles;
        }
    }
}
