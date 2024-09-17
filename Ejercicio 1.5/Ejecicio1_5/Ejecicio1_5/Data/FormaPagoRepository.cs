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
    public class FormaPagoRepository : IFormaPagoRepository
    {
        private readonly DataHelper _dataHelper;

        public FormaPagoRepository()
        {
            _dataHelper = DataHelper.GetInstance();
        }

        public void GuardarFormaPago(FormaPago formaPago)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@nombre", formaPago.Nombre)
            };

            _dataHelper.ExecuteSPDML("sp_GuardarFormaPago", parametros);
        }

        public List<FormaPago> ListarFormasPago()
        {
            var dataTable = _dataHelper.ExecuteSPQuery("sp_ListarFormasPago", null);
            List<FormaPago> formasPago = new List<FormaPago>();

            foreach (DataRow row in dataTable.Rows)
            {
                FormaPago formaPago = new FormaPago
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nombre = row["nombre"].ToString()
                };

                formasPago.Add(formaPago);
            }

            return formasPago;
        }

        public FormaPago ObtenerFormaPago(int id)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@id", id)
            };

            var dataTable = _dataHelper.ExecuteSPQuery("sp_ObtenerFormaPago", parametros);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            DataRow row = dataTable.Rows[0];

            return new FormaPago
            {
                Id = Convert.ToInt32(row["id"]),
                Nombre = row["nombre"].ToString()
            };
        }

        public bool Save(FormaPago formaPago)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Guardar FormaPago
                var cmd = new SqlCommand("sp_GuardarFormaPago", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@nombre", formaPago.Nombre);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo guardar la forma de pago.");
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

        public bool Delete(int formaPagoId)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                // Eliminar FormaPago
                var cmd = new SqlCommand("sp_EliminarFormaPago", cnn, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@formaPagoId", formaPagoId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo eliminar la forma de pago.");
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
