using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class OperatorRepository : ICarGoRepository<Operator>
    {
        private readonly CarGoDBContext _context;

        public OperatorRepository(CarGoDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Operator> GetAll()
        {
            return _context.Operators.ToList();
        }

        public void Add(Operator entity)
        {
            _context.Operators.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Operator entity)
        {
            _context.Operators.Update(entity);
            _context.SaveChanges();
        }
        public Operator GetByID(int id)
        {
            return _context.Operators.Find(id);
        }

        public void Delete(Operator entity)
        {
            _context.Operators.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
