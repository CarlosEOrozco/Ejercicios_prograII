using proyecto_Practica01_.Data;
using proyecto_Practica01_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Services
{
    // Esta clase maneja la lógica de negocio relacionada con las cuentas bancarias.
    public class CuentaService
    {
        private readonly ICuentaRepository _cuentaRepository; // Repositorio inyectado que maneja la interacción con la base de datos.

        // Constructor que recibe una implementación de ICuentaRepository e inyecta la dependencia.
        public CuentaService(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        // Método para guardar una nueva cuenta o actualizar una existente.
        // Se llama al método Save del repositorio, que realiza la operación en la base de datos.
        public bool GuardarCuenta(Cuenta cuenta)
        {
            return _cuentaRepository.Save(cuenta); // Retorna true si la cuenta fue guardada/actualizada correctamente.
        }

        // Método para obtener una cuenta por su ID.
        // Utiliza el repositorio para recuperar los datos de la base de datos.
        public Cuenta ObtenerCuentaPorId(int id)
        {
            return _cuentaRepository.GetById(id); // Retorna la cuenta con el ID especificado, o null si no se encuentra.
        }

        // Método para obtener todas las cuentas asociadas a un cliente específico.
        // Utiliza el repositorio para realizar la consulta en la base de datos.
        public List<Cuenta> ObtenerCuentasPorClienteId(int clienteId)
        {
            return _cuentaRepository.GetByClienteId(clienteId); // Retorna una lista de cuentas asociadas al cliente.
        }
    }
}
