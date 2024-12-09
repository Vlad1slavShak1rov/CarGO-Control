using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class UsersRepository : ICarGoRepository<Users>
    {
        private readonly CarGoDBContext _context;
        public UsersRepository(CarGoDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Users> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Add(Users entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Users(Users entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
        public Users GetByID(int id)
        {
            return _context.Users.Find(id);
        }

        public void Delete(Users entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
