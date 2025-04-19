using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        //signature For property for each and every repository interface
        public IEmployeeRepository  EmployeeRepository { get; set; }

        public IDepartmentRepository   DepartmentRepository  { get; set; }
        Task <int> Complete();
        //void Dispose();
    }
}
