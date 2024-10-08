﻿using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Data
{
    public interface IFormaPagoRepository
    {
        void GuardarFormaPago(FormaPago formaPago);
        FormaPago ObtenerFormaPago(int id);
        List<FormaPago> ListarFormasPago();
    }
}
