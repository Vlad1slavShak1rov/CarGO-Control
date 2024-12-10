using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class RolesRepository
    {
        private readonly CarGoDBContext _context;

        public RolesRepository(CarGoDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Roles> GetAll()
        {
            return _context.Roles;
        }

        public void Add(Roles entity)
        {
            _context.Roles.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Roles entity)
        {
            _context.Roles.Update(entity);
            _context.SaveChanges();
        }
        public Roles GetByID(int id)
        {
            return _context.Roles.Find(id);
        }

        public void Delete(Roles entity)
        {
            _context.Roles.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
