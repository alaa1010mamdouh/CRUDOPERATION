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
    public class Departmentrepository : GenericRepository<Department>,IDepartmentRepository
    {
        public Departmentrepository(CRUDDbContext Context):base(Context)
        {
            
        }
    }
}
