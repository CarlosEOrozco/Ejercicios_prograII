using proyecto_Practica01_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Data
{
    // Interfaz para definir las operaciones CRUD que se pueden realizar sobre un tipo de cuenta.
    public interface ITipoCuentaRepository
    {
        // Obtener un tipo de cuenta por su ID.
        TipoCuenta GetById(int id);

        // Obtener todos los tipos de cuenta.
        List<TipoCuenta> GetAll();

        // Guardar un nuevo tipo de cuenta.
        bool Save(TipoCuenta tipoCuenta);
    }
}
