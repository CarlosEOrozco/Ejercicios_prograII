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
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DataHelper _dataHelper;

        public ArticuloRepository()
        {
            _dataHelper = DataHelper.GetInstance();
        }

        public void GuardarArticulo(Articulo articulo)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@nombre", articulo.Nombre),
            new ParameterSQL("@precioUnitario", articulo.PrecioUnitario)
            };

            _dataHelper.ExecuteSPDML("sp_GuardarArticulo", parametros);
        }

        public List<Articulo> ListarArticulos()
        {
            var dataTable = _dataHelper.ExecuteSPQuery("sp_ListarArticulos", null);
            List<Articulo> articulos = new List<Articulo>();

            foreach (DataRow row in dataTable.Rows)
            {
                Articulo articulo = new Articulo
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nombre = row["nombre"].ToString(),
                    PrecioUnitario = Convert.ToDecimal(row["precioUnitario"])
                };

                articulos.Add(articulo);
            }

            return articulos;
        }

        public Articulo ObtenerArticulo(int id)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@id", id)
            };

            var dataTable = _dataHelper.ExecuteSPQuery("sp_ObtenerArticulo", parametros);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            DataRow row = dataTable.Rows[0];

            return new Articulo
            {
                Id = Convert.ToInt32(row["id"]),
                Nombre = row["nombre"].ToString(),
                PrecioUnitario = Convert.ToDecimal(row["precioUnitario"])
            };
        }
    }
}
