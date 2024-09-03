using proyecto_Practica01_.Data;
using proyecto_Practica01_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Cliente ObtenerClientePorId(int id)
        {
            return _clienteRepository.GetById(id);
        }

        public bool GuardarOActualizarCliente(Cliente cliente)
        {
            return _clienteRepository.SaveOrUpdate(cliente);
        }
    }
}
