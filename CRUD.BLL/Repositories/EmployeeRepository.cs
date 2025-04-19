using CRUD.BLL.Interfaces;
using CRUD.DAL.Context;
using CRUD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CRUDDbContext _context;

        public EmployeeRepository(CRUDDbContext Context) : base(Context)
        {
            _context = Context;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)

         => _context.employees.Where(e => e.Address == address);

        public IQueryable<Employee> GetEmployeesByName(string name)
      =>   _context.employees.Where(u=>u.Name.ToLower().Contains(name.ToLower()));
        
    }
}
