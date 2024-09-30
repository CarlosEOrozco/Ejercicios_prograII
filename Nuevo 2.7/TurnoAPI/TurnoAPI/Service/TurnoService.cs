using TurnoAPI.Data.Repository;
using TurnoAPI.Models;

namespace TurnoAPI.Service
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _repository;
        public TurnoService(ITurnoRepository repository)
        {
            _repository = repository;
        }
        public bool Actualizar(TTurno turno, int id)
        {
            return _repository.Update(turno, id);
        }

        public bool Borrar(int id)
        {
            return _repository.Delete(id);
        }

        public bool Guardar(TTurno turno)
        {
            return _repository.Save(turno);
        }

        public List<TTurno> ObtenerTodos()
        {
            return _repository.GetAll();
        }
    }
}
