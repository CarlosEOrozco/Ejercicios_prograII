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
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private readonly DataHelper _dataHelper;

        public DetalleFacturaRepository()
        {
            _dataHelper = DataHelper.GetInstance();
        }

        public void GuardarDetalleFactura(DetalleFactura detalle)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@nroFactura", detalle.Factura.NroFactura),
            new ParameterSQL("@articuloId", detalle.Articulo.Id),
            new ParameterSQL("@cantidad", detalle.Cantidad)
            };

            _dataHelper.ExecuteSPDML("sp_GuardarDetalleFactura", parametros);
        }

        public List<DetalleFactura> ObtenerDetallesPorFactura(int nroFactura)
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
