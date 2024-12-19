using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class RouteRepository :ICarGoRepository<Route>
    {
        private readonly CarGoDBContext _context;
        public RouteRepository(CarGoDBContext context)
        {
            _context = context;
        }
        public IQueryable<Route> GetAll()
        {
            return _context.Routes;
        }

        public void Add(Route entity)
        {
            _context.Routes.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Route entity)
        {
            _context.Routes.Update(entity);
            _context.SaveChanges();
        }
        public Route GetByID(int id)
        {
            return _context.Routes.Find(id);
        }

        public Route GetByIDrivers(int id)
        {
            return _context.Routes.SingleOrDefault(r => r.DriverID == id);
        }

        public Route GetByTrackNum(string tracknum)
        {
            return _context.Routes.FirstOrDefault(r => r.TrackNumer == tracknum);
        }
        public void Delete(Route entity)
        {
            _context.Routes.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
