namespace Ej_2_7.Data.Repositories
{
    public interface ITTurnoRepository
    {
        void Create(Models.TTurno turno);
        void Update(Models.TTurno turno);
        List<Models.TTurno> GetAll();
        Models.TTurno? GetById(int id);
        void Delete(int id);
    }
}
