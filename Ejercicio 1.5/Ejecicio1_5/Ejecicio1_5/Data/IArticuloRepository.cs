using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Data
{
    public interface IArticuloRepository
    {
        void GuardarArticulo(Articulo articulo);
        Articulo ObtenerArticulo(int id);
        List<Articulo> ListarArticulos();
    }
}
