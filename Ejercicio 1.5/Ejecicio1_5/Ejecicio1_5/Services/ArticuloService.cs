using Ejecicio1_5.Data;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Services
{
    public class ArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;

        public ArticuloService(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        public void CrearArticulo(Articulo articulo)
        {
            _articuloRepository.GuardarArticulo(articulo);
        }

        public Articulo ObtenerArticulo(int id)
        {
            return _articuloRepository.ObtenerArticulo(id);
        }

        public List<Articulo> ListarArticulos()
        {
            return _articuloRepository.ListarArticulos();
        }
    }
}
