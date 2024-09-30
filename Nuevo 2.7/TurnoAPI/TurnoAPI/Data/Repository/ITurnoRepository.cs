using TurnoAPI.Models;

namespace TurnoAPI.Data.Repository
{
    public interface ITurnoRepository
    {
        List<TTurno> GetAll();
        bool Save(TTurno turno);
        bool Delete(int id);
        bool Update(TTurno turno, int id);
    }
}
