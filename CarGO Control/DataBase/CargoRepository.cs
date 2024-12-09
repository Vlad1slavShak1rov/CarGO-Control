using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class CargoRepository : ICarGoRepository<Cargo>
    {
        private readonly CarGoDBContext _context;
        public CargoRepository(CarGoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Cargo> GetAll()
        {
            return _context.Cargos.ToList();
        }

        public void Add(Cargo entity)
        {
            _context.Cargos.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Cargo entity)
        {
            _context.Cargos.Update(entity);
            _context.SaveChanges();
        }
        public Cargo GetByID(int id)
        {
            return _context.Cargos.Find(id);
        }

        public void Delete(Cargo entity)
        {
           _context.Cargos.Remove(entity);
           _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
