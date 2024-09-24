using EFWebApi.Models;

namespace EFWebApi.Data.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        //debo darle el dbcontext
        private LibrosDbContext _context;

        //ctor
        public LibroRepository(LibrosDbContext context)//se agrega el parentesis
        {
            _context = context;
        }

        //agrego las configuraciones del create
        public void Create(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var libroDeleted = GetById(id);
            if (libroDeleted != null)
            {
                _context.Libros.Remove(libroDeleted);

                _context.SaveChanges();
            }
        }

        //hago el get all
        public List<Libro> GetAll()
        {
            return _context.Libros.ToList();
        }

        public Libro? GetById(int id)
        {
            return _context.Libros.Find(id);
        }

        public void Update(Libro libro)
        {
           
            if (libro != null)
            {
                _context.Libros.Update(libro);
                _context.SaveChanges();
            }
        }
    }
}
