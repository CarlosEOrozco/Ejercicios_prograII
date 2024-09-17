using Ejecicio1_5.Data;
using Ejecicio1_5.Domain;
using Ejecicio1_5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Instancia del repositorio y servicio de Articulo
            ArticuloRepository articuloRepository = new ArticuloRepository();
            ArticuloService articuloService = new ArticuloService(articuloRepository);

            // Menú simple para interactuar con el usuario
            int opcion;
            do
            {
                Console.WriteLine("---- Menú Artículos ----");
                Console.WriteLine("1. Crear Artículo e Insertar en BD");
                Console.WriteLine("2. Listar y Eliminar Artículos de la BD");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        CrearArticulo(articuloService);
                        break;
                    case 2:
                        //ListarYEliminarArticulos(articuloService);
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intente nuevamente.");
                        break;
                }

                Console.WriteLine();
            } while (opcion != 0);
        }

        static void CrearArticulo(ArticuloService articuloService)
        {
            // Pedir datos al usuario
            Console.Write("Ingrese el nombre del artículo: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese el precio unitario del artículo: ");
            decimal precioUnitario = Convert.ToDecimal(Console.ReadLine());

            // Crear nuevo artículo
            Articulo nuevoArticulo = new Articulo
            {
                Nombre = nombre,
                PrecioUnitario = precioUnitario
            };

            // Guardar artículo en la BD
            articuloService.CrearArticulo(nuevoArticulo);
            Console.WriteLine("Artículo insertado correctamente en la base de datos.");
        }

        //static void ListarYEliminarArticulos(ArticuloService articuloService)
        //{
        //    // Listar los artículos
        //    List<Articulo> articulos = articuloService.ListarArticulos();

        //    Console.WriteLine("---- Lista de Artículos en la Base de Datos ----");
        //    if (articulos.Count > 0)
        //    {
        //        foreach (var articulo in articulos)
        //        {
        //            Console.WriteLine($"ID: {articulo.Id}, Nombre: {articulo.Nombre}, Precio: {articulo.PrecioUnitario}");

        //            // Eliminar cada artículo
        //            articuloService.EliminarArticulo(articulo.Id);
        //            Console.WriteLine($"Artículo con ID {articulo.Id} eliminado de la base de datos.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No se encontraron artículos.");
        //    }



        //}
    }
}
