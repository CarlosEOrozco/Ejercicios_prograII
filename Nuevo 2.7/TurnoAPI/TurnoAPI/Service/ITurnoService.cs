using TurnoAPI.Models;

namespace TurnoAPI.Service
{
    public interface ITurnoService
    {
        List<TTurno> ObtenerTodos();
        bool Guardar(TTurno turno);
        bool Borrar(int id);
        bool Actualizar(TTurno turno, int id);
    }
}
