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

        public bool Save(DetalleFactura detalle, int nroFactura)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Guardar DetalleFactura
                var cmd = new SqlCommand("sp_GuardarDetalleFactura", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                cmd.Parameters.AddWithValue("@articuloId", detalle.Articulo.Id);
                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo guardar el detalle de factura.");
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

        public bool Delete(int nroFactura, int articuloId)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Eliminar DetalleFactura
                var cmd = new SqlCommand("sp_EliminarDetalleFactura", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                cmd.Parameters.AddWithValue("@articuloId", articuloId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo eliminar el detalle de factura.");
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
