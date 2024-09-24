using Ej_2_7.Models;

namespace Ej_2_7.Data.Repositories
{
    public class TTurnoRepository : ITTurnoRepository
    {
        // Agregar
        private TurnosDbContext _context;

        public TTurnoRepository(TurnosDbContext context)
        {
            _context = context;
        }

        //interfaz ya implementada

        public void Create(TTurno turno)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TTurno> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TTurno turno)
        {
            throw new NotImplementedException();
        }

        TTurno? ITTurnoRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
