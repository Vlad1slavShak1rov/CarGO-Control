using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class TruckRepository : ICarGoRepository<Truck>
    {
        private readonly CarGoDBContext _context;
        public TruckRepository(CarGoDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Truck> GetAll()
        {
            return _context.Trucks;
        }

        public void Add(Truck entity)
        {
            _context.Trucks.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Truck entity)
        {
            _context.Trucks.Update(entity);
            _context.SaveChanges();
        }
        public Truck GetByID(int id)
        {
            return _context.Trucks.Find(id);
        }

        public Truck GetByIDDriver(int id)
        {
            return _context.Trucks
                .Include(r => r.Driver)
               .FirstOrDefault(r => r.ID == id);
        }
        public Truck GetBySignleLicensePlate(string licensePlat)
        {
            return _context.Trucks.SingleOrDefault(tr => tr.LicensePlate == licensePlat);
        }
        public void Delete(Truck entity)
        {
            _context.Trucks.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
