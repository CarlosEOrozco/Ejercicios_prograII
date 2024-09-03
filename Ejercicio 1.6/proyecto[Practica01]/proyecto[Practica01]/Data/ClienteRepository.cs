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
        public Cliente GetById(int id)
        {
            Cliente cliente = null; // Se inicializa el cliente como null
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@ClienteID", id) // Se crea un parámetro con el ID del cliente
            };

            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CLIENTE_POR_ID", parametros); // Ejecuta el SP y obtiene la tabla

            if (t != null && t.Rows.Count == 1) // Si hay exactamente un resultado
            {
                DataRow row = t.Rows[0]; // Obtiene la fila de resultados
                cliente = new Cliente
                {
                    ClienteID = (int)row["ClienteID"], // Mapea los campos de la tabla a las propiedades del cliente
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    DNI = row["DNI"].ToString()
                };
            }
            return cliente; // Retorna el cliente encontrado o null si no existe
        }

        public List<Cliente> GetAll()
        {
            // Implementación pendiente
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(Cliente cliente)
        {
            bool result = true;
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@ClienteID", cliente.ClienteID),
                new ParameterSQL("@Nombre", cliente.Nombre),
                new ParameterSQL("@Apellido", cliente.Apellido),
                new ParameterSQL("@DNI", cliente.DNI)
            };

            try
            {
                int rowsAffected = DataHelper.GetInstance().ExecuteSPDML("SP_GUARDAR_CLIENTE", parametros); // Ejecuta el SP de guardado
                result = rowsAffected > 0; // Verifica si al menos una fila fue afectada
            }
            catch (SqlException)
            {
                result = false; // Si ocurre un error, el resultado es false
            }

            return result; // Retorna el resultado de la operación
        }
    }
}
