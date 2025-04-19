using CRUD.BLL.Interfaces;
using CRUD.DAL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly CRUDDbContext _context;

        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get ; set; }
        public UnitOfWork(CRUDDbContext Context)
        {
            EmployeeRepository = new EmployeeRepository(Context);
            DepartmentRepository = new Departmentrepository(Context);
            _context = Context;
        }


        public void Dispose()
        {
          _context.Dispose();
        }

        public async Task< int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
