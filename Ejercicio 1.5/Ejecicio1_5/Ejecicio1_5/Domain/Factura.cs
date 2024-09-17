using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Domain
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public FormaPago FormaPago { get; set; }  // Relación con FormaPago
        public List<DetalleFactura> Detalles { get; set; }  // Lista de Detalles de Factura

        public Factura()
        {
            Detalles = new List<DetalleFactura>();  // Inicializa la lista de detalles
        }

        // Método para agregar un artículo a la factura
        public void AgregarArticulo(Articulo articulo, int cantidad)
        {
            // Verificar si el artículo ya está en los detalles
            var detalleExistente = Detalles.Find(d => d.Articulo.Id == articulo.Id);
            if (detalleExistente != null)
            {
                // Si ya existe, incrementa la cantidad
                detalleExistente.Cantidad += cantidad;
            }
            else
            {
                // Si no existe, agregar un nuevo detalle
                Detalles.Add(new DetalleFactura { Articulo = articulo, Cantidad = cantidad });
            }
        }
    }
}
