using Ejecicio1_5.Data.Utils;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public bool Save(Articulo articulo)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Guardar Articulo
                var cmd = new SqlCommand("sp_GuardarArticulo", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
                cmd.Parameters.AddWithValue("@precioUnitario", articulo.PrecioUnitario);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo guardar el artículo.");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Console.WriteLine($"Error: {ex.Message}");
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return result;
        }

        public bool Delete(int articuloId)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Eliminar Articulo
                var cmd = new SqlCommand("sp_EliminarArticulo", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@articuloId", articuloId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo eliminar el artículo.");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Console.WriteLine($"Error: {ex.Message}");
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return result;
        }
    }
}
