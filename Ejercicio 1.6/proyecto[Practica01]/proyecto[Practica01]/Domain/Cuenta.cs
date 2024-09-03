using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Domain
{
    // La clase Cuenta representa una cuenta bancaria asociada a un cliente.
    public class Cuenta
    {
        // Propiedades de la cuenta: ID, CBU, Saldo, Tipo de Cuenta, Último Movimiento, y ClienteID.
        public int CuentaID { get; set; } // Identificador único de la cuenta (Primary Key)
        public string CBU { get; set; } // Clave Bancaria Uniforme, identifica la cuenta
        public decimal Saldo { get; set; } // Saldo actual en la cuenta
        public int TipoCuentaID { get; set; } // ID del tipo de cuenta (Foreign Key)
        public DateTime UltimoMovimiento { get; set; } // Fecha y hora del último movimiento en la cuenta
        public int ClienteID { get; set; } // ID del cliente asociado (Foreign Key)
    }
}
