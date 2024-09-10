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
    public class TipoCuentaRepository : ITipoCuentaRepository
    {
        // Método para obtener un TipoCuenta por su ID
        public TipoCuenta GetById(int id)
        {
            TipoCuenta tipoCuenta = null;  // Inicializa el objeto TipoCuenta como nulo
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@TipoCuentaID", id)  // Añade el parámetro para buscar por ID
            };

            // Ejecuta el procedimiento almacenado y obtiene los resultados en una tabla
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_TIPO_CUENTA_POR_ID", parametros);

            // Si encuentra una fila, mapea los datos a la entidad TipoCuenta
            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                tipoCuenta = new TipoCuenta
                {
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    Nombre = row["Nombre"].ToString()
                };
            }
            return tipoCuenta;  // Retorna el tipo de cuenta o null si no se encontró
        }

        // Método para obtener todos los Tipos de Cuenta
        public List<TipoCuenta> GetAll()
        {
            List<TipoCuenta> tiposCuenta = new List<TipoCuenta>();  // Inicializa la lista de tipos de cuenta

            // Ejecuta el procedimiento almacenado para obtener todos los tipos de cuenta
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_TIPOS_CUENTA", null);

            // Mapea cada fila a un objeto TipoCuenta y lo añade a la lista
            foreach (DataRow row in t.Rows)
            {
                TipoCuenta tipoCuenta = new TipoCuenta
                {
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    Nombre = row["Nombre"].ToString()
                };
                tiposCuenta.Add(tipoCuenta);
            }

            return tiposCuenta;  // Retorna la lista de tipos de cuenta
        }

        // Método para guardar un nuevo TipoCuenta
        public bool Save(TipoCuenta tipoCuenta)
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

                // Crea el comando SQL para insertar un tipo de cuenta y lo asocia a la transacción
                var cmd = new SqlCommand("SP_INSERTAR_TIPO_CUENTA", cnn, t)
                {
                    CommandType = CommandType.StoredProcedure  // Define que el comando es un procedimiento almacenado
                };

                // Añade los parámetros al comando SQL
                cmd.Parameters.AddWithValue("@Nombre", tipoCuenta.Nombre);

                // Ejecuta el comando y verifica que se haya afectado al menos una fila
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo insertar el tipo de cuenta.");  // Lanza una excepción si no se insertó ninguna fila
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
