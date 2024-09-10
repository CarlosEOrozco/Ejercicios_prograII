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
    public class ClienteRepository : IClienteRepository
    {
        // Método para obtener un Cliente por su ID
        public Cliente GetById(int id)
        {
            Cliente cliente = null;  // Inicializa el objeto Cliente como nulo
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@ClienteID", id)  // Añade el parámetro para buscar por ID
            };

            // Ejecuta el procedimiento almacenado y obtiene los resultados en una tabla
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CLIENTE_POR_ID", parametros);

            // Si encuentra una fila, mapea los datos a la entidad Cliente
            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                cliente = new Cliente
                {
                    ClienteID = (int)row["ClienteID"],
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    DNI = row["DNI"].ToString()
                };
            }
            return cliente;  // Retorna el cliente o null si no se encontró
        }

        // Método para obtener todos los Clientes (no implementado aquí)
        public List<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        // Método para guardar un nuevo Cliente
        public bool Save(Cliente cliente)
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

                // Crea el comando SQL para insertar o actualizar un cliente y lo asocia a la transacción
                var cmd = new SqlCommand("SP_GUARDAR_CLIENTE", cnn, t)
                {
                    CommandType = CommandType.StoredProcedure  // Define que el comando es un procedimiento almacenado
                };

                // Añade los parámetros al comando SQL
                cmd.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);

                // Ejecuta el comando y verifica que se haya afectado al menos una fila
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo guardar el cliente.");  // Lanza una excepción si no se afectó ninguna fila
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

        public bool SaveOrUpdate(Cliente cliente)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();  // Comienza la transacción

                SqlCommand cmd;

                // Si el ClienteID es 0, es una nueva inserción
                if (cliente.ClienteID == 0)
                {
                    // Crear un nuevo comando para insertar un cliente
                    cmd = new SqlCommand("SP_GUARDAR_CLIENTE", cnn, t)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@ClienteID", 0);  // ClienteID es 0 porque estamos insertando
                }
                else
                {
                    // Crear un nuevo comando para actualizar un cliente existente
                    cmd = new SqlCommand("SP_GUARDAR_CLIENTE", cnn, t)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);  // Actualización de un cliente existente
                }

                // Añadir los parámetros comunes tanto para insertar como para actualizar
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);

                // Ejecutar el comando
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo guardar o actualizar el cliente.");
                }

                t.Commit();  // Confirmar la transacción
            }
            catch (SqlException ex)
            {
                if (t != null)
                {
                    t.Rollback();  // Revertir la transacción en caso de error
                }
                Console.WriteLine($"Error de SQL: {ex.Message}");
                result = false;
            }
            catch (Exception ex)
            {
                if (t != null)
                {
                    t.Rollback();  // Revertir la transacción en caso de error
                }
                Console.WriteLine($"Error: {ex.Message}");
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();  // Cerrar la conexión
                }
            }

            return result;  // Retornar true si la operación fue exitosa
        }
    }
}
