using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Domain
{
    // La clase Cliente representa a un cliente del banco.
    public class Cliente
    {
        // Propiedades del cliente: ID, Nombre, Apellido, y DNI.
        public int ClienteID { get; set; } // Identificador único del cliente (Primary Key)
        public string Nombre { get; set; } // Nombre del cliente
        public string Apellido { get; set; } // Apellido del cliente
        public string DNI { get; set; } // Documento Nacional de Identidad del cliente
    }
}
