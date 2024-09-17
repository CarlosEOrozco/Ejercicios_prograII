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
    public class FacturaRepository : IFacturaRepository
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
            List<Factura> facturas = new List<Factura>();

            // Ejemplo de cómo listar facturas desde la base de datos
            SqlConnection cnn = DataHelper.GetInstance().GetConnection();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("SP_LISTAR_FACTURAS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var factura = new Factura
                    {
                        NroFactura = Convert.ToInt32(reader["FacturaID"]),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        Cliente = reader["Cliente"].ToString(),
                        // Cargar otras propiedades
                    };
                    facturas.Add(factura);
                }
                reader.Close();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
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

        public bool Save(Factura factura)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Guardar Factura
                var cmd = new SqlCommand("sp_GuardarFactura", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);
                cmd.Parameters.AddWithValue("@formaPagoId", factura.FormaPago.Id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo guardar la factura.");
                }

                // Commit transaction
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

        public bool Delete(int nroFactura)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Eliminar Factura
                var cmd = new SqlCommand("sp_EliminarFactura", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo eliminar la factura.");
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
