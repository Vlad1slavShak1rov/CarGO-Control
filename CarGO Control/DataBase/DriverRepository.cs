using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class DriverRepository :ICarGoRepository<Driver>
    {
        private readonly CarGoDBContext _context;
        public DriverRepository(CarGoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Driver> GetAll()
        {
            return _context.Drivers;
        }

        public void Add(Driver entity)
        {
            _context.Drivers.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Driver entity)
        {
            _context.Drivers.Update(entity);
            _context.SaveChanges();
        }
        public Driver GetByID(int id)
        {
            return _context.Drivers.Find(id);
        }

        public Driver GetByLogin(string name)
        {
            return _context.Drivers.FirstOrDefault(us => us.Name == name);
        }
        public void Delete(Driver entity)
        {
            _context.Drivers.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
