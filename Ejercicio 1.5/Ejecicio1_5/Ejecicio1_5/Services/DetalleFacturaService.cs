using Ejecicio1_5.Data;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Services
{
    public class DetalleFacturaService
    {
        private readonly IDetalleFacturaRepository _detalleFacturaRepository;

        public DetalleFacturaService(IDetalleFacturaRepository detalleFacturaRepository)
        {
            _detalleFacturaRepository = detalleFacturaRepository;
        }

        public void CrearDetalleFactura(DetalleFactura detalle)
        {
            _detalleFacturaRepository.GuardarDetalleFactura(detalle);
        }

        public List<DetalleFactura> ObtenerDetallesPorFactura(int nroFactura)
        {
            return _detalleFacturaRepository.ObtenerDetallesPorFactura(nroFactura);
        }
    }
}
