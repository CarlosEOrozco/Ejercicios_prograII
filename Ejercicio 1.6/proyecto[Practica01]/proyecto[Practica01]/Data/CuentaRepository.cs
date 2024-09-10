using proyecto_Practica01_.Domain;
using RepositoryExample.Data.Utils;
using RepositoryExample.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace proyecto_Practica01_.Data
{
    public class CuentaRepository : ICuentaRepository
    {
        // Método para obtener una cuenta por su ID
        public Cuenta GetById(int id)
        {
            Cuenta cuenta = null;  // Inicializa el objeto Cuenta como nulo
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@CuentaID", id)  // Añade el parámetro para buscar por ID
            };

            // Ejecuta el procedimiento almacenado y obtiene los resultados en una tabla
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CUENTA_POR_ID", parametros);

            // Si encuentra una fila, mapea los datos a la entidad Cuenta
            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                cuenta = new Cuenta
                {
                    CuentaID = (int)row["CuentaID"],
                    CBU = row["CBU"].ToString(),
                    Saldo = (decimal)row["Saldo"],
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    UltimoMovimiento = (DateTime)row["UltimoMovimiento"],
                    ClienteID = (int)row["ClienteID"]
                };
            }
            return cuenta;  // Retorna la cuenta o null si no se encontró
        }

        // Método para obtener todas las cuentas asociadas a un cliente
        public List<Cuenta> GetByClienteId(int clienteId)
        {
            List<Cuenta> cuentas = new List<Cuenta>();  // Inicializa la lista de cuentas

            // Crea los parámetros para el procedimiento almacenado
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@ClienteID", clienteId)  // Añade el parámetro ClienteID
            };

            // Ejecuta el procedimiento almacenado para obtener las cuentas por ClienteID
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CUENTAS_POR_CLIENTE", parametros);

            // Mapea cada fila a un objeto Cuenta y lo añade a la lista
            foreach (DataRow row in t.Rows)
            {
                Cuenta cuenta = new Cuenta
                {
                    CuentaID = (int)row["CuentaID"],
                    CBU = row["CBU"].ToString(),
                    Saldo = (decimal)row["Saldo"],
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    UltimoMovimiento = (DateTime)row["UltimoMovimiento"],
                    ClienteID = (int)row["ClienteID"]
                };
                cuentas.Add(cuenta);
            }

            return cuentas;  // Retorna la lista de cuentas
        }

        // Método para guardar una nueva cuenta
        public bool Save(Cuenta cuenta)
        {
            bool result = true;  // Inicializa el resultado como exitoso
            SqlConnection cnn = null;
            SqlTransaction t = null;  // Inicializa la transacción como nula

            try
            {
                // Obtiene la conexión desde el DataHelper y la abre
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();  // Inicia la transacción

                // Crea el comando SQL para insertar una cuenta y lo asocia a la transacción
                var cmd = new SqlCommand("SP_INSERTAR_CUENTA", cnn, t)
                {
                    CommandType = CommandType.StoredProcedure  // Define que el comando es un procedimiento almacenado
                };

                // Añade los parámetros al comando SQL
                cmd.Parameters.AddWithValue("@CBU", cuenta.CBU);
                cmd.Parameters.AddWithValue("@Saldo", cuenta.Saldo);
                cmd.Parameters.AddWithValue("@TipoCuentaID", cuenta.TipoCuentaID);
                cmd.Parameters.AddWithValue("@UltimoMovimiento", cuenta.UltimoMovimiento);
                cmd.Parameters.AddWithValue("@ClienteID", cuenta.ClienteID);

                // Ejecuta el comando y verifica que se haya afectado al menos una fila
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo insertar la cuenta.");  // Lanza una excepción si no se insertó ninguna fila
                }

                t.Commit();  // Confirma la transacción
            }
            catch (SqlException ex)
            {
                // Captura errores de SQL y realiza el rollback
                if (t != null)
                {
                    t.Rollback();  // Revertir la transacción en caso de error
                }
                Console.WriteLine($"Error de SQL: {ex.Message}");  // Muestra el error
                result = false;  // Indica que la operación falló
            }
            catch (Exception ex)
            {
                // Captura cualquier otro error y realiza el rollback
                if (t != null)
                {
                    t.Rollback();  // Revertir la transacción en caso de error
                }
                Console.WriteLine($"Error: {ex.Message}");  // Muestra el error
                result = false;  // Indica que la operación falló
            }
            finally
            {
                // Asegura que la conexión se cierra al final
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();  // Cierra la conexión
                }
            }

            return result;  // Retorna el resultado de la operación (true si fue exitosa, false si falló)
        }
    }
}
