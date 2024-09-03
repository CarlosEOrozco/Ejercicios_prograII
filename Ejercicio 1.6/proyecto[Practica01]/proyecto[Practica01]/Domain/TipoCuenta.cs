using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Domain
{
    public class TipoCuenta
    {
        // Propiedades del tipo de cuenta: ID y Nombre.
        public int TipoCuentaID { get; set; } // Identificador único del tipo de cuenta (Primary Key)
        public string Nombre { get; set; } // Nombre descriptivo del tipo de cuenta
    }
}
