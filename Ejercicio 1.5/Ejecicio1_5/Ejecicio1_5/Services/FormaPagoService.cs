using Ejecicio1_5.Data;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Services
{
    public class FormaPagoService
    {
        private readonly IFormaPagoRepository _formaPagoRepository;

        public FormaPagoService(IFormaPagoRepository formaPagoRepository)
        {
            _formaPagoRepository = formaPagoRepository;
        }

        public void CrearFormaPago(FormaPago formaPago)
        {
            _formaPagoRepository.GuardarFormaPago(formaPago);
        }

        public FormaPago ObtenerFormaPago(int id)
        {
            return _formaPagoRepository.ObtenerFormaPago(id);
        }

        public List<FormaPago> ListarFormasPago()
        {
            return _formaPagoRepository.ListarFormasPago();
        }
    }
}
