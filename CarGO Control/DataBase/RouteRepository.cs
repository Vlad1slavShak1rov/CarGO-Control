using System;
using System.Collections.Generic;
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
        public IEnumerable<Route> GetAll()
        {
            return _context.Routes.ToList();
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
