using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Data
{
    public interface IFacturaRepository
    {
        void GuardarFactura(Factura factura);
        Factura ObtenerFactura(int nroFactura);
    }
}
