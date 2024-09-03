using proyecto_Practica01_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Data
{
    // Interfaz para definir las operaciones CRUD que se pueden realizar sobre una cuenta.
    public interface ICuentaRepository
    {
        // Obtener una cuenta por su ID.
        Cuenta GetById(int id);

        // Obtener todas las cuentas asociadas a un cliente.
        List<Cuenta> GetByClienteId(int clienteId);

        // Guardar una nueva cuenta.
        bool Save(Cuenta cuenta);
    }
}
