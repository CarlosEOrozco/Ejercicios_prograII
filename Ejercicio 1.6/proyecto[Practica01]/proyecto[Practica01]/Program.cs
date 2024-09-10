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
            IClienteRepository clienteRepo = new ClienteRepository();
            ITipoCuentaRepository tipoCuentaRepo = new TipoCuentaRepository();
            ICuentaRepository cuentaRepo = new CuentaRepository();

            // Instanciar los servicios
            ClienteService clienteService = new ClienteService(clienteRepo);
            TipoCuentaService tipoCuentaService = new TipoCuentaService(tipoCuentaRepo);
            CuentaService cuentaService = new CuentaService(cuentaRepo);

            // Insertar un nuevo cliente en la base de datos
            Cliente nuevoCliente = new Cliente
            {
                Nombre = "Juanfer",
                Apellido = "Pérez",
                DNI = "987456"
            };

            // Guardar o actualizar el cliente
            bool clienteGuardado = clienteService.GuardarOActualizarCliente(nuevoCliente);
            if (clienteGuardado)
            {
                Console.WriteLine("Cliente guardado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al guardar el cliente.");
            }

            // Insertar un nuevo tipo de cuenta en la base de datos
            TipoCuenta nuevoTipoCuenta = new TipoCuenta
            {
                Nombre = "Caja de Ahorro"
            };

            // Guardar el tipo de cuenta
            bool tipoCuentaGuardado = tipoCuentaService.GuardarTipoCuenta(nuevoTipoCuenta);
            if (tipoCuentaGuardado)
            {
                Console.WriteLine("Tipo de cuenta guardado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al guardar el tipo de cuenta.");
            }

            // Insertar una nueva cuenta asociada al cliente
            Cuenta nuevaCuenta = new Cuenta
            {
                CBU = "1234567890123456789012",
                Saldo = 10000.00m,
                TipoCuentaID = nuevoTipoCuenta.TipoCuentaID,
                UltimoMovimiento = DateTime.Now,
                ClienteID = nuevoCliente.ClienteID
            };

            // Guardar la cuenta
            bool cuentaGuardada = cuentaService.GuardarCuenta(nuevaCuenta);
            if (cuentaGuardada)
            {
                Console.WriteLine("Cuenta guardada con éxito.");
            }
            else
            {
                Console.WriteLine("Error al guardar la cuenta.");
            }
        }
    }
}
