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
        public IQueryable<Operator> GetAll()
        {
            return _context.Operators;
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

        public Operator GetByLogin(string login)
        {
            return _context.Operators.FirstOrDefault(us => us.Name == login);
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
