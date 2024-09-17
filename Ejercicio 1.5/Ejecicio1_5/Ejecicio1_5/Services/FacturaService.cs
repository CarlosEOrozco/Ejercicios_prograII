using Ejecicio1_5.Data;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public void CrearFactura(Factura factura)
        {
            _facturaRepository.GuardarFactura(factura);
        }

        public Factura ObtenerFactura(int nroFactura)
        {
            return _facturaRepository.ObtenerFactura(nroFactura);
        }

        public List<Factura> ListarFacturas()
        {
            return _facturaRepository.ListarFacturas();
        }

        public List<Factura> ObtenerTodasLasFacturas()
        {
            return _facturaRepository.ListarFacturas(); // Llamada correcta al método
        }
    }
}
