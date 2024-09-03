using proyecto_Practica01_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Data
{
    // Interfaz para definir las operaciones CRUD que se pueden realizar sobre un cliente.
    public interface IClienteRepository
    {
        // Obtener un cliente por su ID.
        Cliente GetById(int id);

        // Obtener todos los clientes.
        List<Cliente> GetAll();

        // Guardar un nuevo cliente o actualizar uno existente.
        bool SaveOrUpdate(Cliente cliente);
    }
}
