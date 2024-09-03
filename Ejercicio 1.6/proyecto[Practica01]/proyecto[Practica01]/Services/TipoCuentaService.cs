using proyecto_Practica01_.Data;
using proyecto_Practica01_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Services
{
    // Esta clase maneja la lógica de negocio relacionada con los tipos de cuenta.
    public class TipoCuentaService
    {
        private readonly ITipoCuentaRepository _tipoCuentaRepository; // Repositorio inyectado que maneja la interacción con la base de datos.

        // Constructor que recibe una implementación de ITipoCuentaRepository e inyecta la dependencia.
        public TipoCuentaService(ITipoCuentaRepository tipoCuentaRepository)
        {
            _tipoCuentaRepository = tipoCuentaRepository;
        }

        // Método para guardar un nuevo tipo de cuenta.
        // Se llama al método Save del repositorio, que realiza la operación en la base de datos.
        public bool GuardarTipoCuenta(TipoCuenta tipoCuenta)
        {
            return _tipoCuentaRepository.Save(tipoCuenta); // Retorna true si el tipo de cuenta fue guardado correctamente.
        }

        // Método para obtener un tipo de cuenta por su ID.
        // Utiliza el repositorio para recuperar los datos de la base de datos.
        public TipoCuenta ObtenerTipoCuentaPorId(int id)
        {
            return _tipoCuentaRepository.GetById(id); // Retorna el tipo de cuenta con el ID especificado, o null si no se encuentra.
        }

        // Método para obtener todos los tipos de cuenta.
        // Utiliza el repositorio para realizar la consulta en la base de datos.
        public List<TipoCuenta> ObtenerTodosLosTiposDeCuenta()
        {
            return _tipoCuentaRepository.GetAll(); // Retorna una lista con todos los tipos de cuenta disponibles.
        }
    }
}
