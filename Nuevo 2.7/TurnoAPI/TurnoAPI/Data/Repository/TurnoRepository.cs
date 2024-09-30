using TurnoAPI.Models;

namespace TurnoAPI.Data.Repository
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly TurnosDbContext _context;
        public TurnoRepository(TurnosDbContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var entity = _context.TTurnos.Find(id);
            if (entity == null)
            {
                return false;
            }
            else
            {
                _context.TTurnos.Remove(entity);
                
                return _context.SaveChanges() > 0;
            }
        }

        public List<TTurno> GetAll()
        {
            return _context.TTurnos.ToList();
        }

        public bool Save(TTurno turno)
        {
            // Convertir el string a DateTime
            if (!DateTime.TryParse(turno.Fecha, out DateTime fechaReserva))
            {
                throw new ArgumentException("La fecha de reserva no es válida.");
            }

            // Verificar y ajustar la fecha de reserva
            if (fechaReserva < DateTime.Now.AddDays(1) || fechaReserva > DateTime.Now.AddDays(45))
            {
                throw new ArgumentException("La fecha de reserva debe ser al menos un día después de la fecha actual y no más de 45 días en el futuro.");
            }

            // Verificar si ya existe un turno para la misma fecha y hora
            bool existeTurno = _context.TTurnos.Any(t => t.Fecha == turno.Fecha && t.Hora == turno.Hora);
            if (existeTurno)
            {
                throw new InvalidOperationException("Ya existe un turno para la fecha y hora seleccionadas.");
            }

            _context.TTurnos.Add(turno);

            return _context.SaveChanges() > 0;
        }

        public bool Update(TTurno turno, int id)
        {
            var cambio = _context.TTurnos.Find(id);
            if (cambio == null)
            {
                return false;
            }
            else
            {
                turno.Fecha = cambio.Fecha;
                turno.Hora = cambio.Hora;
                turno.Cliente = cambio.Cliente;

                _context.TTurnos.Add(turno);
                return _context.SaveChanges() > 0;
            }

        }
    }
}
