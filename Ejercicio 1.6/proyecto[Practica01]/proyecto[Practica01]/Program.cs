using proyecto_Practica01_.Data;
using proyecto_Practica01_.Domain;
using proyecto_Practica01_.Services;

namespace proyecto_Practica01_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instanciar los repositorios de datos
            // Estos repositorios se encargan de interactuar directamente con la base de datos
            IClienteRepository clienteRepo = new ClienteRepository();
            ITipoCuentaRepository tipoCuentaRepo = new TipoCuentaRepository();
            ICuentaRepository cuentaRepo = new CuentaRepository();

            // Instanciar los servicios, que utilizan los repositorios para realizar operaciones de negocio
            ClienteService clienteService = new ClienteService(clienteRepo);
            TipoCuentaService tipoCuentaService = new TipoCuentaService(tipoCuentaRepo);
            CuentaService cuentaService = new CuentaService(cuentaRepo);

            // Inserta un nuevo cliente en la base de datos
            Cliente nuevoCliente = new Cliente
            {
                Nombre = "Juanfer",  // Nombre del cliente
                Apellido = "Pérez",  // Apellido del cliente
                DNI = "987456"  // DNI del cliente
            };

            // Guarda o actualiza el cliente utilizando el servicio
            bool clienteGuardado = clienteService.GuardarOActualizarCliente(nuevoCliente);
            if (clienteGuardado)
            {
                Console.WriteLine("Cliente guardado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al guardar el cliente.");
            }

            // Inserta un nuevo tipo de cuenta en la base de datos
            TipoCuenta nuevoTipoCuenta = new TipoCuenta
            {
                Nombre = "Caja de Ahorro"  // Nombre del tipo de cuenta
            };

            // Guarda el tipo de cuenta utilizando el servicio
            bool tipoCuentaGuardado = tipoCuentaService.GuardarTipoCuenta(nuevoTipoCuenta);
            if (tipoCuentaGuardado)
            {
                Console.WriteLine("Tipo de cuenta guardado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al guardar el tipo de cuenta.");
            }

            // Inserta una nueva cuenta asociada al cliente en la base de datos
            Cuenta nuevaCuenta = new Cuenta
            {
                CBU = "1234567890123456789012",  // CBU de la cuenta
                Saldo = 10000.00m,  // Saldo inicial de la cuenta
                TipoCuentaID = nuevoTipoCuenta.TipoCuentaID,  // ID del tipo de cuenta recién creado
                UltimoMovimiento = DateTime.Now,  // Fecha y hora del último movimiento (ahora)
                ClienteID = nuevoCliente.ClienteID  // ID del cliente recién creado
            };

            // Guarda la cuenta utilizando el servicio
            bool cuentaGuardada = cuentaService.GuardarCuenta(nuevaCuenta);
            if (cuentaGuardada)
            {
                Console.WriteLine("Cuenta guardada con éxito.");
            }
            else
            {
                Console.WriteLine("Error al guardar la cuenta.");
            }

            // Espera a que el usuario presione una tecla para cerrar la consola
            Console.ReadLine();
        }
    }
}
